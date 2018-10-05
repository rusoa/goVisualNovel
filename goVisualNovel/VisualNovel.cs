using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace goVisualNovel
{
    class VisualNovel
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Hooker
        {
            public IntPtr Addr;
            public int EspBias;
            public bool ValueAsAddr;
            public int ValueAsAddrBias;
            public int BytesPerRead;
        };

        public string VNName;
        public string ModuleName;
        public string Language;
        public string ProcEncoding;
        public Hooker[] Hookers;
        public string[] WordsFilter;

        public VisualNovel(string VNName)
        {
            this.VNName = VNName;
        }

        public void SetAttrsFromHCode(string HCode)
        {
            HCode = HCode.Trim(Program.WhiteSpaceChars);
            if (HCode == string.Empty) throw new ArgumentException();

            HCode = Regex.Replace(HCode, "^/H.N?", ""); //skip "A|B|W|S|Q|H" and "N"
            HCode = Regex.Replace(HCode, "#.*?(?=@)", ""); //skip "level"

            //skip "name or ordinal"
            ModuleName = Regex.Match(HCode, "(?<=@.*?:).*$").Value;
            if (ModuleName == string.Empty) throw new ArgumentException();

            int Addr = Convert.ToInt32(Regex.Match(HCode, "(?<=@).*?(?=:)").Value, 16);

            //get hookers
            string[] HookerStrs = Regex.Match(HCode, "^.*?(?=@)").Value.Split(':');
            if (HookerStrs.Length < 1 || HookerStrs.Length > 2) throw new ArgumentException();

            Hookers = new Hooker[HookerStrs.Length];
            for (int i = 0; i < Hookers.Length; i++)
            {
                //HookerStrs[i] is like this: EspBias[*ValueAsAddrBias]
                Hookers[i].EspBias = Cvt2EspBias(Regex.Match(HookerStrs[i], "^.*?(?=(\\*|$))").Value);
                Hookers[i].ValueAsAddr = HookerStrs[i].IndexOf('*') != -1;
                if (Hookers[i].ValueAsAddr)
                    Hookers[i].ValueAsAddrBias = Cvt2ValueAsAddrBias(Regex.Match(HookerStrs[i], "(?<=\\*).*$").Value);
                Hookers[i].Addr = (IntPtr)Addr;
                Hookers[i].BytesPerRead = Hookers.Length == 1 ? 2 : 1;
            }
        }

        public void SetWordsFilterFromStr(string str)
        {
            str = str.Trim(Program.WhiteSpaceChars);
            if(str == null || str == string.Empty) WordsFilter = new string[0];
            else WordsFilter = str.Split(',');
        }

        //HookEspBias, the value addr bias to &ctx.Esp
        //when the value is positive, the result addr = ctx.Esp + value
        //when the value is zero, then hook the return addr
        //when the value is nagative, the result addr = (char *)&ctx.Ebp + value
        //but to esp, ebp, esi and edi we may do little more convert
        //actually it imitate the stack frame when execute PUSHAD, but we can also use it to calculate addr
        private static int Cvt2EspBias(string str)
        {
            if (str == string.Empty) throw new ArgumentException();

            int a;
            if (str.StartsWith("-"))
            {
                a = -0x10 - Convert.ToInt32(str.Substring(1), 16);
                if (a == -0x24) a = 0;
                else if (a == -0x28) a = -0x4;
                else if (a == -0x2c) a = -0x24;
                else if (a == -0x30) a = -0x28;
            }
            else
            {
                a = Convert.ToInt32(str, 16);
            }
            return a;
        }

        private static int Cvt2ValueAsAddrBias(string str)
        {
            if (str == string.Empty) return 0;
            else if (str.StartsWith("-")) return -Convert.ToInt32(str.Substring(1), 16);
            else return Convert.ToInt32(str, 16);
        }
    }
}
