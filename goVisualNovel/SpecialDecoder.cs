using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace goVisualNovel
{
    class SpecialDecoder
    {
        public string ProcEncoding { get; }
        public int HookEspBias { get; }
        public bool HookValueAsAddr { get; }
        public int HookValueAsAddrBias { get; }
        public IntPtr HookAddr { get; }
        public string ModuleName { get; }

        public SpecialDecoder(string SpecialCode)
        {
            if (SpecialCode.Trim(Program.WhiteSpaceChars) == string.Empty)
                throw new ArgumentNullException();

            string temp;

            //the code looks like this (from auth): /H(A|B|W|S|Q|H)[N](data_offset[*drdo])[:sub_offset[*drso]][#level]@addr[:module[:(name|#ordinal)]]

            //but after thinking for a while I found the (A|B|W|S|Q|H) and [N] make no sense at all so we just skip them

            //HookEspBias, the value addr bias to &ctx.Esp
            //when the value is positive, the result addr = ctx.Esp + value
            //when the value is zero, then hook the return addr
            //when the value is nagative, the result addr = (char *)&ctx.Ebp + value
            //but to esp, ebp, esi and edi we may do little more convert
            //actually it imitate the stack frame when execute PUSHAD, but we can also use it to calculate addr
            temp = Regex.Match(SpecialCode, "(?<=^/H.+)[^N].*?(?=[\\*:#@])").Value;
            if (temp == string.Empty) throw new ArgumentException();
            else if (temp.StartsWith("-"))
            {
                HookEspBias = -0x10 - Convert.ToInt32(temp.Substring(1), 16);
                if (HookEspBias == -0x24) HookEspBias = 0;
                else if (HookEspBias == -0x28) HookEspBias = -0x4;
                else if (HookEspBias == -0x2c) HookEspBias = -0x24;
                else if (HookEspBias == -0x30) HookEspBias = -0x28;
            }
            else
            {
                HookEspBias = Convert.ToInt32(temp, 16);
                if (HookEspBias == 0) HookEspBias = 0x100;
            }

            //HookEspBias value as addr?
            if (Regex.IsMatch(SpecialCode, "^.*\\*.*?[:@]"))
            {
                HookValueAsAddr = true;
                temp = Regex.Match(SpecialCode, "(?<=^.*\\*).*?(?=[:@])").Value;
                if (temp == string.Empty) HookValueAsAddrBias = 0;
                else if (temp.StartsWith("-")) HookValueAsAddrBias = -Convert.ToInt32(temp.Substring(1), 16);
                else HookValueAsAddrBias = Convert.ToInt32(temp, 16);
            }
            else
            {
                HookValueAsAddr = false;
                HookValueAsAddrBias = 0;
            }

            //[...]unsupport for second hook yet

            HookAddr = (IntPtr)Convert.ToInt32(Regex.Match(SpecialCode, "(?<=@).*?(?=:)").Value, 16);

            //skip analyzing the meaningless things after :

            ModuleName = Regex.Match(SpecialCode, "(?<=:).*$").Value;
            if (ModuleName == string.Empty) throw new ArgumentException();
        }
    }
}
