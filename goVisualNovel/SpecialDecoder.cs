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
        public string ProcName { get; }
        public IntPtr HookAddr { get; }
        public int HookIndex { get; }
        public bool HookValueAsAddr { get; }
        public int HookValueAsAddrBias { get; }
        public int BytesPerRead { get; }
        public string ProcEncoding { get; }

        public SpecialDecoder(string SpecialCode)
        {
            if (SpecialCode.Trim(Program.WhiteSpaceChars) == string.Empty)
                throw new ArgumentNullException();

            string temp;

            temp = Regex.Match(SpecialCode, "(?<=^/H).").Value;
            if(temp == string.Empty) throw new ArgumentException();
            ProcEncoding = temp == "W" ? "utf-16" : "shift-jis";

            //[...]unsupport for N yet

            temp = Regex.Match(SpecialCode, "(?<=^/H.+)[^N].*?(?=[\\*:@])").Value;
            if (RegMap.ContainsKey(temp)) HookIndex = RegMap[temp];
            else throw new ArgumentException();
            
            if(Regex.IsMatch(SpecialCode, "^.*\\*.*@")) //is contain '*' before '@'?
            {
                HookValueAsAddr = true;
                temp = Regex.Match(SpecialCode, "(?<=^.*\\*).*?(?=[:@])").Value;
                if (temp == string.Empty) HookValueAsAddrBias = 0;
                else if(temp.StartsWith("-")) HookValueAsAddrBias = -Convert.ToInt32(temp.Substring(1), 16);
                else HookValueAsAddrBias = Convert.ToInt32(temp, 16);
            }
            else
            {
                HookValueAsAddr = false;
                HookValueAsAddrBias = 0;
            }

            //[...]unsupport for : yet

            HookAddr = (IntPtr)Convert.ToInt32(Regex.Match(SpecialCode, "(?<=@).*?(?=:)").Value, 16);

            //[...]need to confirm this in some ways
            BytesPerRead = 2;

            ProcName = Regex.Match(SpecialCode, "(?<=:).*$").Value;
            if (ProcName == string.Empty) throw new ArgumentException();
        }

        //[...]the kinds is poor now
        private static Dictionary<string, int> RegMap = new Dictionary<string, int>()
        {
            { "-4", 0 },  //eax
            { "-8", 1 },  //ecx
            { "-C", 2 },  //edx
            { "-10", 3 }, //ebx
            { "-1C", 4 }, //esi
            { "-20", 5 }, //edi
            { "-14", 6 }, //esp
            { "-18", 7 }  //ebp
        };
    }
}
