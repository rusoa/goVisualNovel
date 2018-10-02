using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

namespace goVisualNovel
{
    static class Program
    {
        #region import dll
        [DllImport("ExtText.dll")]
        public static extern void ExtText(
            [MarshalAs(UnmanagedType.LPWStr)] string ProcName,
            IntPtr HookAddr,
            int HookIndex,
            bool HookValueAsAddr,
            int HookValueAsAddrBias,
            int BytesPerRead,
            int MainThreadId,
            IntPtr ExtBuffer,
            IntPtr StopExtText
        );
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId(); //get true thread id, not managed
        #endregion

        #region global const
        public const int EXT_BYTES_MAX_SIZE = 233;
        public const int WM_USER = 0x400;
        public const int WM_EXT_TEXT_EXT = WM_USER + 233;
        public const int WM_EXT_TEXT_EXIT_PASSIVE = WM_USER + 234;
        public const int WM_EXT_TEXT_EXIT_ERROR = WM_USER + 235;
        #endregion

        #region global var
        public static Form1 form1;
        public static WelcomeForm welform;
        public static SettingsForm setform;
        public static string[,] TextTable; //word itself, word property, word pronunciation
        public static readonly char[] WhiteSpaceChars = new char[]
        {
            (char)0x00, (char)0x01, (char)0x02, (char)0x03, (char)0x04, (char)0x05, (char)0x06, (char)0x07, (char)0x08, (char)0x09, (char)0x0a,
            (char)0x0b, (char)0x0c, (char)0x0d, (char)0x0e, (char)0x0f, (char)0x10, (char)0x11, (char)0x12, (char)0x13, (char)0x14, (char)0x15,
            (char)0x16, (char)0x17, (char)0x18, (char)0x19, (char)0x20, (char)0x1a, (char)0x1b, (char)0x1c, (char)0x1d, (char)0x1e, (char)0x1f,
            (char)0x7f, (char)0x85, (char)0x27, (char)0x22, (char)0x5C, (char)0x00, (char)0x07, (char)0x08, (char)0x0C, (char)0x0A, (char)0x0D,
            (char)0x09, (char)0x0B, (char)0x2028, (char)0x2029
        };
        #endregion

        #region private var
        private static Thread ExtTextThread;
        private volatile static IntPtr pExtBuffer;
        private volatile static IntPtr pStopExtText;
        private static VisualNovel vn;
        #endregion

        [STAThread]
        static void Main()
        {
            //Properties.Settings.Default.Reset();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.AddMessageFilter(new MyMsgFilter());
            welform = new WelcomeForm();
            welform.Show();
            Application.Run();
        }

        #region Ext Text and Translation
        public static void StartExtText(string VNName, string Language, string SpecialCode, string WordsFilter)
        {
            if (form1 != null && !form1.IsDisposed) form1.Close();
            StopExtText();

            vn = new VisualNovel(VNName, Language, SpecialCode, WordsFilter);

            int Mtid = GetCurrentThreadId();
            pExtBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * EXT_BYTES_MAX_SIZE);
            pStopExtText = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(bool)));

            Marshal.WriteByte(pStopExtText, 0);
            ExtTextThread = new Thread(() =>
                ExtText(
                    vn.ProcName,
                    vn.HookAddr,
                    vn.HookIndex,
                    vn.HookValueAsAddr,
                    vn.HookValueAsAddrBias,
                    vn.BytesPerRead,
                    Mtid,
                    pExtBuffer,
                    pStopExtText
                )
            );
            ExtTextThread.IsBackground = true;
            ExtTextThread.Start();

            form1 = new Form1();
            form1.Show();
        }

        public static void StopExtText(int cd = 1500)
        {
            if (ExtTextThread == null || !ExtTextThread.IsAlive) return;
            Marshal.WriteByte(pStopExtText, 1);
            while (ExtTextThread.IsAlive && cd-- > 0) Thread.Sleep(1);
            if (cd <= 0) ExtTextThread.Abort();
            Marshal.FreeHGlobal(pExtBuffer);
            Marshal.FreeHGlobal(pStopExtText);
        }

        public static void onExtText()
        {
            string OriginText = MyConverter.pBufferToString(pExtBuffer, EXT_BYTES_MAX_SIZE, vn.ProcEncoding);
            if(vn.WordsFilter != null)
                foreach(string w in vn.WordsFilter.Split(',')) OriginText = OriginText.Replace(w, "");
            TextTable = WordProcess.Process(OriginText, vn.Language);
            form1.RefreshText();
            List<Task<string>> TranslateTasks = GenerateTranslateTasks(OriginText);
            foreach (Task<string> task in TranslateTasks)
            {
                task.ContinueWith(t => { form1.RefreshTranslation(TranslateTasks.IndexOf(t), t.Result.ToString()); });
                task.Start();
            }
        }

        private static List<Task<string>> GenerateTranslateTasks(string OriginText)
        {
            List<Task<string>> TranslateTasks = new List<Task<string>>();
            if (Properties.Settings.Default.Youdao)
                TranslateTasks.Add(new Task<string>(() =>
                    new Youdao(Properties.Settings.Default.Youdao_AppKey,
                               Properties.Settings.Default.Youdao_AppSecret).Translate(
                        OriginText,
                        vn.Language,
                        Properties.Settings.Default.LocalLang)
                ));
            return TranslateTasks;
        }

        public static void onExtTextExitPassive()
        {
            if(form1 != null && !form1.IsDisposed) form1.Close();
            if(welform != null && welform.IsDisposed)
            {
                welform = new WelcomeForm();
                welform.Show();
            }
        }

        public static void onExtTextExitError()
        {
            MessageBox.Show("提取文字失败！");
            onExtTextExitPassive();
        }
        #endregion

        public static void ShowSettingsForm()
        {
            if (setform != null && !setform.IsDisposed)
            {
                setform.Focus();
            }
            else
            {
                setform = new SettingsForm();
                setform.Show();
            }
        }

        //exit gently
        public static void myExit()
        {
            StopExtText();
            Application.Exit();
        }
    }
}
