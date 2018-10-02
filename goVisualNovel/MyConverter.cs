using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace goVisualNovel
{
    static class MyConverter
    {
        public static string pBufferToString(IntPtr p, int size, string encoding)
        {
            byte[] buffer = new byte[size];
            //copy bytes from pointer to byte[]
            for (int i = 0; i < size; i++)
                buffer[i] = Marshal.ReadByte(p, i);
            //decode with bytes
            string str = Encoding.GetEncoding(encoding).GetString(buffer);
            //cut the end \0s
            for (int i = str.Length - 1; i >= 0; i--)
                if (str[i] != '\0') { str = str.Substring(0, i + 1); break; }
            return str;
        }
    }
}
