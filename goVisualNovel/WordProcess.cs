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
            const int MAX_SEG_BUFFER_SIZE = Program.EXT_BYTES_MAX_SIZE * 20; //associate with MyMeCab
            IntPtr pSegBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * MAX_SEG_BUFFER_SIZE);
            MeCabSegment(text, pSegBuffer);
            string SegStr = MyConverter.pBufferToString(pSegBuffer, MAX_SEG_BUFFER_SIZE, "utf-8");
            Marshal.FreeHGlobal(pSegBuffer);
            string[] pair = SegStr.Split('\n');
            string[,] table = new string[pair.Length, 3];
            for (int i = 0; i < pair.Length; i++)
            {
                string[] attrs = pair[i].Split('\t', ',');
                table[i, 0] = attrs[0];
                table[i, 1] = attrs[1];
                table[i, 2] = attrs.Length >= 9 ? katakanaToHiragana(attrs[8]) : "";
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
            IntPtr res
        );
    }
}
