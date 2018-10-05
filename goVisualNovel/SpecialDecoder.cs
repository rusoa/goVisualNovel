//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace goVisualNovel
//{
//    class SpecialDecoder
//    {
//        public string ModuleName { get; }
//        public VisualNovel.Hooker[] Hookers { get; }

//        public SpecialDecoder(string SpecialCode)
//        {
//            SpecialCode = SpecialCode.Trim(Program.WhiteSpaceChars);
//            if (SpecialCode == string.Empty)
//                throw new ArgumentNullException();

//            Regex.Replace(SpecialCode, "^/H.+N*", ""); //skip "A|B|W|S|Q|H" and "N"
//            Regex.Replace(SpecialCode, "#.*?(?=@)", ""); //skip "level"

//            //skip "name or ordinal"
//            ModuleName = Regex.Match(SpecialCode, "(?<=:).*$").Value;
//            if (ModuleName == string.Empty)
//                throw new ArgumentNullException();

//            int addr = Convert.ToInt32(Regex.Match(SpecialCode, "(?<=@).*?(?=:)").Value, 16);

//            //get hookers
//            string[] HookerStrs = Regex.Match(SpecialCode, "^.*?(?=@)").Value.Split(':');
//            if(HookerStrs.Length < 1 || HookerStrs.Length > 2)
//                throw new ArgumentNullException();

//            Hookers = new Hooker[HookerStrs.Length];
//            for(int i = 0; i < Hookers.Length; i++)
//            {
//                //HookerStrs[i] is like this: EspBias[*ValueAsAddrBias]
//                Hookers[i].EspBias = Cvt2EspBias(Regex.Match(HookerStrs[i], "^.*?(?=[*$])").Value);
//                Hookers[i].ValueAsAddr = HookerStrs[i].IndexOf('*') != -1;
//                if (Hookers[i].ValueAsAddr)
//                    Hookers[i].ValueAsAddrBias = Cvt2ValueAsAddrBias(Regex.Match(HookerStrs[i], "(?<=\\*).*$").Value);
//                Hookers[i].Addr = (IntPtr)addr;
//                Hookers[i].BytesPerRead = Hookers.Length == 1 ? 2 : 1;
//            }
//        }

//        //HookEspBias, the value addr bias to &ctx.Esp
//        //when the value is positive, the result addr = ctx.Esp + value
//        //when the value is zero, then hook the return addr
//        //when the value is nagative, the result addr = (char *)&ctx.Ebp + value
//        //but to esp, ebp, esi and edi we may do little more convert
//        //actually it imitate the stack frame when execute PUSHAD, but we can also use it to calculate addr
//        private static int Cvt2EspBias(string str)
//        {
//            if (str == string.Empty) throw new ArgumentException();

//            int a;
//            if (str.StartsWith("-"))
//            {
//                a = -0x10 - Convert.ToInt32(str.Substring(1), 16);
//                if (a == -0x24) a = 0;
//                else if (a == -0x28) a = -0x4;
//                else if (a == -0x2c) a = -0x24;
//                else if (a == -0x30) a = -0x28;
//            }
//            else
//            {
//                a = Convert.ToInt32(str, 16);
//                if (a == 0) a = 0x100;
//            }
//            return a;
//        }

//        private static int Cvt2ValueAsAddrBias(string str)
//        {
//            if (str == string.Empty) return 0;
//            else if (str.StartsWith("-")) return -Convert.ToInt32(str.Substring(1), 16);
//            else return Convert.ToInt32(str, 16);
//        }
//    }
//}
