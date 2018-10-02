using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace goVisualNovel
{
    class VisualNovel
    {
        private string _VNName;
        public string VNName
        {
            get { return _VNName; }
            private set
            {
                if (value.Trim(Program.WhiteSpaceChars) == string.Empty)
                    throw new ArgumentNullException();
                _VNName = value;
            }
        }

        public string ProcName { get; }

        public string Language { get; }

        public IntPtr HookAddr { get; } //true addr = module base addr(automaticly get) + bias addr(this)

        private int _RegIndex;
        public int HookIndex
        {
            get { return _RegIndex; }
            private set
            {
                if (value < 0 || value > 5)
                    throw new ArgumentOutOfRangeException();
                _RegIndex = value;
            }
        }

        public bool HookValueAsAddr { get; }

        public int HookValueAsAddrBias { get; }

        private int _BytesPerRead;
        public int BytesPerRead //read 1 or 2 bytes per break. Nothing to do with encoding.
        {
            get { return _BytesPerRead; }
            private set
            {
                if (value < 1 || value > 2)
                    throw new ArgumentOutOfRangeException();
                _BytesPerRead = value;
            }
        }

        public string ProcEncoding { get; }

        private string _WordsFilter;
        public string WordsFilter
        {
            get { return _WordsFilter; }
            private set
            {
                if (value != null)
                {
                    value = value.Trim(Program.WhiteSpaceChars);
                    if (value != string.Empty)
                    {
                        _WordsFilter = value;
                        return;
                    }
                }
                _WordsFilter = null;
            }
        }

        public VisualNovel(string VNName, string Language, string SpecialCode, string WordsFilter)
        {
            SpecialDecoder sc = new SpecialDecoder(SpecialCode);
            this.VNName = VNName;
            this.ProcName = sc.ProcName;
            this.Language = Language;
            this.HookAddr = sc.HookAddr;
            this.HookIndex = sc.HookIndex;
            this.HookValueAsAddr = sc.HookValueAsAddr;
            this.HookValueAsAddrBias = sc.HookValueAsAddrBias;
            this.BytesPerRead = sc.BytesPerRead;
            this.ProcEncoding = sc.ProcEncoding;
            this.WordsFilter = WordsFilter;
        }
    }
}
