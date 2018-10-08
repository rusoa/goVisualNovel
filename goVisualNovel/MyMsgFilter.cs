using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goVisualNovel
{
    class MyMsgFilter : IMessageFilter
    {
        public const int WM_USER = 0x400;
        public const int WM_EXT_TEXT = WM_USER + 100;
        public const int WM_EXT_TEXT_GET_PID_SUCCESS = WM_EXT_TEXT + 10;
        public const int WM_EXT_TEXT_GET_HPROC_FAILED = WM_EXT_TEXT + 11;
        public const int WM_EXT_TEXT_GET_HTHREAD_FAILED = WM_EXT_TEXT + 12;
        public const int WM_EXT_TEXT_GET_BASE_FAILED = WM_EXT_TEXT + 13;
        public const int WM_EXT_TEXT_GET_CTX_FAILED = WM_EXT_TEXT + 20;
        public const int WM_EXT_TEXT_SET_CTX_FAILED = WM_EXT_TEXT + 21;
        public const int WM_EXT_TEXT_ENTER_DEBUG_FAILED = WM_EXT_TEXT + 30;
        public const int WM_EXT_TEXT_GET_CODE_FAILED = WM_EXT_TEXT + 40;
        public const int WM_EXT_TEXT_SET_CODE_FAILED = WM_EXT_TEXT + 41;
        public const int WM_EXT_TEXT_GET_MEM_FAILED = WM_EXT_TEXT + 42;
        public const int WM_EXT_TEXT_EXT_SUCCESS = WM_EXT_TEXT + 50;
        public const int WM_EXT_TEXT_EXIT_PASSIVE = WM_EXT_TEXT + 60;
        public const int WM_EXT_TEXT_EXIT_ACTIVE = WM_EXT_TEXT + 61;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg < WM_EXT_TEXT || m.Msg > WM_EXT_TEXT_EXIT_ACTIVE)
                return false;

            switch(m.Msg)
            {
                case WM_EXT_TEXT_GET_PID_SUCCESS:
                    Program.form1.StatusLabel.Text = "正在等待提取文字...";
                    break;
                case WM_EXT_TEXT_EXT_SUCCESS:
                    Program.onExtText();
                    break;
                case WM_EXT_TEXT_EXIT_PASSIVE:
                    Program.onExtTextExitPassive();
                    break;
                case WM_EXT_TEXT_EXIT_ACTIVE:
                    break;
                default:
                    Program.onExtTextExitError(m.Msg);
                    break;
            }

            return true;
        }
    }
}
