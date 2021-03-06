﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace goVisualNovel
{
    public class VisualNovel
    {
        //the array of this struct will be send to text extractor, so keep as simiple as possible
        [StructLayout(LayoutKind.Sequential)]
        public struct Hooker
        {
            public IntPtr Addr;
            public int EspBias;
            public bool ValueAsAddr;
            public int ValueAsAddrBias;
            public int BytesPerRead;
        };

        public string VNName { get; set; }
        public string Language { get; set; }
        public string ProcEncoding { get; set; }
        public string ModuleName { get; set; }
        public List<Hooker> Hookers { get; set; }
        public List<string> WordsFilter { get; set; }

        public VisualNovel(bool CreateNewHook = false)
        {
            ModuleName = VNName = "";
            Language = "ja";
            ProcEncoding = "shift-jis";
            Hookers = new List<Hooker>();
            WordsFilter = new List<string>();
        }

        /**
         * deep copy for the normal copy constructor doesn't co-work with json serializer
         */
        public void CopyTo(ref VisualNovel vn)
        {
            vn.VNName = VNName;
            vn.Language = Language;
            vn.ProcEncoding = ProcEncoding;
            vn.ModuleName = ModuleName;
            vn.Hookers = new List<Hooker>(Hookers.ToArray());
            vn.WordsFilter = new List<string>(WordsFilter.ToArray());
        }

        public void SetAttrsFromHCode(string HCode)
        {
            HCode = HCode.Trim(Program.WhiteSpaceChars);
            if (HCode == string.Empty) throw new ArgumentException();

            HCode = Regex.Replace(HCode, "^/[Hh].[Nn]?", ""); //skip "A|B|W|S|Q|H" and "N"
            HCode = Regex.Replace(HCode, "#.*?(?=@)", ""); //skip "level"

            //skip "name or ordinal"
            string ModuleName_tmp = Regex.Match(HCode, "(?<=@.*?:).*$").Value;
            if (ModuleName_tmp == string.Empty) ModuleName_tmp = VNName + ".exe";

            //hook addr
            IntPtr Addr_tmp = (IntPtr)Convert.ToUInt32(Regex.Match(HCode, "(?<=@).*?(?=(:|$))").Value, 16);

            //get hookers, HookerStrs[i] is like this: EspBias[*ValueAsAddrBias]
            string[] HookerStrs = Regex.Match(HCode, "^.*?(?=@)").Value.Split(':');
            if (HookerStrs.Length < 1 || HookerStrs.Length > 2) throw new ArgumentException();
            List<Hooker> Hookers_tmp = new List<Hooker>();
            for (int i = 0; i < HookerStrs.Length; i++)
            {
                Hooker h = new Hooker();
                h.EspBias = Cvt2EspBias(Regex.Match(HookerStrs[i], "^.*?(?=(\\*|$))").Value);
                h.ValueAsAddr = HookerStrs[i].IndexOf('*') != -1;
                if (h.ValueAsAddr)
                    h.ValueAsAddrBias = Cvt2ValueAsAddrBias(Regex.Match(HookerStrs[i], "(?<=\\*).*$").Value);
                h.Addr = Addr_tmp;
                h.BytesPerRead = HookerStrs.Length == 1 ? 2 : 1;
                Hookers_tmp.Add(h);
            }

            ModuleName = ModuleName_tmp;
            Hookers = Hookers_tmp;
        }

        /**
         * HookEspBias, the value addr bias to &ctx.Esp
         * when the value is positive, the result addr = ctx.Esp + value
         * when the value is zero, then hook the return addr
         * when the value is nagative, the result addr = (char *)&ctx.Ebp + value
         * but to esp, ebp, esi and edi we may do little more convert
         * actually it imitate the stack frame when execute PUSHAD, but we can also use it to calculate addr
         */
        private static int Cvt2EspBias(string str)
        {
            if (str == string.Empty) throw new ArgumentException();

            int a;
            if (str.StartsWith("-"))
            {
                a = -0x10 - Convert.ToInt32(str.Substring(1), 16);
                if (a == -0x24) a = 0x00;
                else if (a == -0x28) a = -0x04;
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
