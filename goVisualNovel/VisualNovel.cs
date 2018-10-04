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

        public string Language { get; }

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

        public VisualNovel(string VNName, string Language, string ProcEncoding, string WordsFilter)
        {
            this.VNName = VNName;
            this.Language = Language;
            this.ProcEncoding = ProcEncoding;
            this.WordsFilter = WordsFilter;
        }
    }
}
