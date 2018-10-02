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
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == Program.WM_EXT_TEXT_EXT)
            {
                Program.onExtText();
                return true;
            }
            else if (m.Msg == Program.WM_EXT_TEXT_EXIT_PASSIVE)
            {
                Program.onExtTextExitPassive();
                return true;
            }
            else if (m.Msg == Program.WM_EXT_TEXT_EXIT_ERROR)
            {
                Program.onExtTextExitError();
                return true;
            }
            return false;
        }
    }
}
