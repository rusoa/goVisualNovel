using goVisualNovel.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goVisualNovel
{
    public partial class Form1 : Form
    {
        #region vars
        private int TextMaxWidth = 1500;
        private int WordsTopMargin = 6;
        private int WordsMargin = 1;
        private int WordsRawMargin = 1;
        private Font WordsFont = new Font("宋体", 24);
        private int TranslationTopMargin = 6;
        private int TranslationRawMargin = 1;
        private Font TranslationFont = new Font("宋体", 24);
        private Color BackColorDefault = Color.FromArgb(30, 30, 30);
        private Color BackColorEmpha = Color.Black;
        private string[] PropertyFilter = new string[] { "記号" };
        private string WaitingQueryText = "正在查询...";
        private string NotFoundText = "未找到";
        private Label WordLabelNow = null;
        private Dictionary<int, string> ExplanationBuffer = new Dictionary<int, string>();

        private Action<object, PaintEventArgs> AutoHeight = (object o, PaintEventArgs pea) =>
        {
            int a = 0;
            foreach (Control c in ((Control)o).Controls)
                a = Math.Max(a, c.Bottom);
            ((Control)o).Height = a;
        };
        #endregion

        #region this
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Width = Screen.GetBounds(this).Width;
            Location = new Point(0, 0);
            TransparencyKey = BackColor;
            Paint += new PaintEventHandler(AutoHeight);

            StatusLabel.BackColor = BackColorDefault;
            StatusLabel.Paint += (object o, PaintEventArgs pea) =>
            {
                Graphics g = CreateGraphics();
                SizeF sizeF = g.MeasureString(StatusLabel.Text, TranslationFont);
                g.Dispose();
                StatusLabel.Size = new Size((int)Math.Ceiling(sizeF.Width) + 4, (int)Math.Ceiling(sizeF.Height));
                StatusLabel.Location = new Point((Width - StatusLabel.Width) / 2, WordsTopMargin);
            };

            TextPanel.Width = TextMaxWidth;
            TextPanel.Height = 0;
            TextPanel.Location = new Point((Width - TextPanel.Width) / 2, WordsTopMargin);
            TextPanel.Paint += (object o, PaintEventArgs pea) =>
                { TranslationPanel.Location = new Point(TranslationPanel.Location.X, TextPanel.Bottom + TranslationTopMargin); };

            TranslationPanel.Width = TextMaxWidth;
            TranslationPanel.Height = 0;
            TranslationPanel.Location = new Point((Width - TranslationPanel.Width) / 2, TranslationTopMargin);

            DicPanel.MouseLeave += HideDic;
        }
        #endregion

        #region text and translation
        public void RefreshText()
        {
            StatusLabel.Hide();

            int wordsCount = Program.TextTable.GetLength(0);
            if (wordsCount == 0) return;

            Label[] words = new Label[wordsCount];
            int[,] rawMap = new int[wordsCount + 1, 2]; //rawMap[i, 0]: the index of first word in raw i, rawMap[i, 1]: the width of this raw
            Graphics g = TextPanel.CreateGraphics(); //for counting the text size
            int maxWordHeight = 0;
            int raw = 0; //count of raws

            for (int i = 0; i < wordsCount; i++)
            {
                //generate new word labels
                words[i] = new Label();
                words[i].AutoSize = false;
                words[i].Text = Program.TextTable[i, 0];
                words[i].Font = WordsFont;
                words[i].TextAlign = ContentAlignment.MiddleCenter;
                words[i].ForeColor = Color.White;
                SizeF sizeF = g.MeasureString(words[i].Text, WordsFont);
                words[i].Size = new Size((int)Math.Ceiling(sizeF.Width) + 4, words[i].Height); //make ample width
                maxWordHeight = Math.Max(maxWordHeight, (int)Math.Ceiling(sizeF.Height)); //will be set after
                
                //group the words by property
                if(!PropertyFilter.Contains(Program.TextTable[i, 1]))
                {
                    words[i].MouseEnter += (object o, EventArgs ea) => { ((Label)o).BackColor = BackColorDefault; };
                    words[i].MouseLeave += (object o, EventArgs ea) => { ((Label)o).BackColor = BackColorEmpha; };
                    words[i].MouseHover += ShowDic;
                    words[i].MouseLeave += HideDic;
                    words[i].BackColor = BackColorEmpha;
                }
                else
                {
                    words[i].BackColor = BackColorDefault;
                }

                //divide raws
                if (rawMap[raw, 1] + words[i].Width > TextPanel.Width)
                {
                    if(rawMap[raw, 0] > 1)
                        rawMap[raw, 1] -= WordsMargin;
                    rawMap[++raw, 0] = i;
                }
                rawMap[raw, 1] += words[i].Width + WordsMargin;
            }
            g.Dispose();

            //set all the height to the max
            for (int i = 0; i < wordsCount; i++)
                words[i].Size = new Size(words[i].Width, maxWordHeight);

            //allocate to different raws and set position
            rawMap[raw + 1, 0] = wordsCount; //boundary condition
            for (int r = 0; r <= raw; r++)
            {
                words[rawMap[r, 0]].Location = new Point(Math.Max((TextPanel.Width - rawMap[r, 1]) / 2, 0), (maxWordHeight + WordsRawMargin) * r);
                for (int i = rawMap[r, 0] + 1; i < rawMap[r + 1, 0]; i++)
                    words[i].Location = new Point(words[i - 1].Location.X + words[i - 1].Width + WordsMargin, words[i - 1].Location.Y);
            }

            //set the index for easily finding
            TextPanel.Controls.AddRange(words);
            for (int i = 0; i < wordsCount; i++)
                TextPanel.Controls.SetChildIndex(words[i], i);

            TextPanel.Height = maxWordHeight * (raw + 1) + WordsRawMargin * raw;
        }

        //it's a cross-thread method
        private delegate void RefreshTranslationDelegate(int i, string str);
        public void RefreshTranslation(int i, string str)
        {
            if(TranslationPanel.InvokeRequired)
            {
                RefreshTranslationDelegate d = RefreshTranslation;
                TranslationPanel.Invoke(d, i, str);
            }
            else
            {
                Label t = new Label();
                t.AutoSize = false;
                t.Text = str != string.Empty ? str : " ";
                t.Font = TranslationFont;
                Graphics g = CreateGraphics();
                SizeF sizeF = g.MeasureString(t.Text, TranslationFont);
                g.Dispose();
                t.Size = new Size((int)Math.Ceiling(sizeF.Width) + 4, (int)Math.Ceiling(sizeF.Height));
                t.TextAlign = ContentAlignment.MiddleCenter;
                t.ForeColor = Color.White;
                t.BackColor = BackColorDefault;
                t.Location = new Point((TranslationPanel.Width - t.Width) / 2, (t.Height + TranslationRawMargin) * i);

                TranslationPanel.Controls.Add(t);

                AutoHeight(TranslationPanel, null);
            }
        }

        public void ClearLastResult()
        {
            TextPanel.Controls.Clear();
            TranslationPanel.Controls.Clear();
            ExplanationBuffer.Clear();
        }
        #endregion

        #region DicPanel
        private void ShowDic(object sender, EventArgs e)
        {
            if (DicPanel.Visible && WordLabelNow == (Label)sender) return;

            WordLabelNow = (Label)sender;
            DicPanel.Location = new Point(Math.Max(MousePosition.X - DicPanel.Width / 3 * 2, 0), WordLabelNow.RectangleToScreen(WordLabelNow.ClientRectangle).Bottom + 1);

            int i = TextPanel.Controls.GetChildIndex(WordLabelNow);
            if (!ExplanationBuffer.ContainsKey(i))
            {
                explanation_dic.Text = WaitingQueryText;
                Task<string[]> SearchDic = new Task<string[]>(() => HJJP.Search(WordLabelNow.Text));
                SearchDic.ContinueWith(t => { OnWordExplanation(t.Result); });
                SearchDic.Start();
            }
            else
            {
                explanation_dic.Text = ExplanationBuffer[i];
            }

            DicPanel.Refresh();
            DicPanel.Show();
        }

        private void HideDic(object sender, EventArgs e)
        {
            if (WordLabelNow == null) return;

            Rectangle rl = WordLabelNow.RectangleToScreen(WordLabelNow.ClientRectangle);
            Rectangle rp = DicPanel.RectangleToScreen(DicPanel.ClientRectangle);
            Rectangle r = new Rectangle(rl.Left, rl.Bottom, rp.Right - rl.Left, rp.Top - rl.Bottom);
            if (r.Contains(MousePosition) || rl.Contains(MousePosition) || rp.Contains(MousePosition)) return;

            DicPanel.Hide();
        }

        private void DicPanel_Paint(object sender, PaintEventArgs e)
        {
            int i = TextPanel.Controls.GetChildIndex(WordLabelNow);

            word_dic.Text = WordLabelNow.Text;
            property_dic.Text = Program.TextTable[i, 1];
            pronunciation_dic.Text = "「" + Program.TextTable[i, 2] + "」";

            OnPaint(null);
        }

        private delegate void OnWordExplanationDelegate(string[] strs);
        private void OnWordExplanation(string[] strs)
        {
            if (DicPanel.InvokeRequired)
            {
                OnWordExplanationDelegate d = OnWordExplanation;
                DicPanel.Invoke(d, new object[] { strs }); //the invoke method recognize what in object[] as params
            }
            else
            {
                if (strs.Length == 0)
                {
                    explanation_dic.Text = NotFoundText;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < strs.Length; i++)
                        sb.Append(string.Format("{0}. {1}\n\n", i + 1, strs[i]));
                    explanation_dic.Text = sb.ToString();
                }
                ExplanationBuffer.Add(TextPanel.Controls.GetChildIndex(WordLabelNow), explanation_dic.Text);
            }
        }

        private void copy_dic_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(word_dic.Text);
        }

        private void copy_dic_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            ((Label)sender).ForeColor = Color.Silver;
        }

        private void copy_dic_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            ((Label)sender).ForeColor = Color.DimGray;
        }

        private void copy_dic_MouseDown(object sender, MouseEventArgs e)
        {
            ((Label)sender).ForeColor = Color.DimGray;
        }

        private void copy_dic_MouseUp(object sender, MouseEventArgs e)
        {
            ((Label)sender).ForeColor = Color.Silver;
        }
        #endregion
    }
}
