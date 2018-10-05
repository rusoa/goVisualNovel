using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace goVisualNovel
{
    static class WordProcess
    {
        public static string[,] Process(string text, string language)
        {
            switch(language)
            {
                case "ja": return JaProcess(text);
                default: return NoProcess(text);
            }
        }

        private static string[,] JaProcess(string text)
        {
            int SegBufferSize = Program.EXT_BYTES_MAX_SIZE * 20;
            IntPtr pSegBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * SegBufferSize);
            MeCabSegment(text, pSegBuffer, SegBufferSize);
            string SegStr = MyConverter.pBufferToString(pSegBuffer, SegBufferSize, "utf-8");
            Marshal.FreeHGlobal(pSegBuffer);

            SegStr = SegStr.Trim(Program.WhiteSpaceChars);
            string[,] table;
            if (SegStr == string.Empty)
            {
                table = new string[1, 3];
                table[0, 0] = text;
                table[0, 1] = table[0, 2] = "";
            }
            else
            {
                string[] Words = SegStr.Split('\n');
                table = new string[Words.Length, 3];
                for (int i = 0; i < Words.Length; i++)
                {
                    string[] WordAttrs = Words[i].Split('\t', ',');
                    table[i, 0] = WordAttrs.Length >= 1 ? WordAttrs[0] : "";
                    table[i, 1] = WordAttrs.Length >= 2 ? WordAttrs[1] : "";
                    table[i, 2] = WordAttrs.Length >= 9 ? katakanaToHiragana(WordAttrs[8]) : "";
                }
            }
            return table;
        }

        private static string katakanaToHiragana(string src)
        {
            char[] buffer = new char[src.Length];
            for(int i = 0; i < src.Length; i++)
            {
                if (src[i] > 0x30a1 && src[i] < 0x30f6) buffer[i] = (char)(src[i] - 0x60);
                else buffer[i] = src[i];
            }
            return new string(buffer);
        }

        private static string[,] NoProcess(string text)
        {
            string[,] table = new string[1, 1];
            table[0, 0] = text;
            return table;
        }

        [DllImport("MyMeCab.dll")]
        public static extern void MeCabSegment(
            [MarshalAs(UnmanagedType.LPWStr)] string text,
            IntPtr pSegBuffer,
            int SegBufferSize
        );
    }
}
