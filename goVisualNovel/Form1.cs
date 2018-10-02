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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this
            Width = Screen.GetBounds(this).Width;
            Location = new Point(0, 0);
            TransparencyKey = BackColor;
            Paint += Form1_AutoHeight;

            //WaitingLabel
            WaitingLabel.BackColor = BackColorDefault;
            WaitingLabel.Location = new Point((Width - WaitingLabel.Width) / 2, WordsTopMargin);
            WaitingLabel.Padding = new Padding(0, 5, 0, 5);

            //TextPanel
            TextPanel.Width = 1500;
            TextPanel.Height = 0;
            TextPanel.Location = new Point((Width - TextPanel.Width) / 2, WordsTopMargin);

            //TranslationPanel
            TranslationPanel.Width = 1500;
            TranslationPanel.Height = 0;
            TranslationPanel.Location = new Point((Width - TranslationPanel.Width) / 2, TranslationTopMargin);

            //DicPanel
            DicPanel.MouseLeave += HideDic;
        }

        private void Form1_AutoHeight(object sender, PaintEventArgs e)
        {
            int Bottom = 0;
            foreach (Control c in Controls)
                Bottom = Math.Max(Bottom, c.Bottom);
            Height = Bottom;
        }

        #region text and translation
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

        public void RefreshText()
        {
            WaitingLabel.Hide();
            ClearLastResult();

            int wordsCount = Program.TextTable.GetLength(0);
            if (wordsCount == 0) return;

            Label[] words = new Label[wordsCount];
            int[,] rawMap = new int[wordsCount + 1, 2]; //rawMap[i, 0]: the index of first word in raw i, rawMap[i, 1]: the width of this raw
            Graphics g = TextPanel.CreateGraphics(); //for counting the text size
            int maxWordHeight = 0;
            int raw = 0; //count of raws
            for (int i = 0; i < wordsCount; i++)
            {
                //generate new word label
                words[i] = new Label();
                words[i].AutoSize = false;
                words[i].Text = Program.TextTable[i, 0];
                words[i].Font = WordsFont;
                SizeF sizeF = g.MeasureString(words[i].Text, WordsFont);
                words[i].Size = new Size((int)Math.Ceiling(sizeF.Width) + 4, words[i].Height); //keep out the margin, else the text would be displayed incompletely. The height will be dealed after.
                maxWordHeight = Math.Max(maxWordHeight, (int)Math.Ceiling(sizeF.Height));
                words[i].TextAlign = ContentAlignment.MiddleCenter;
                words[i].Margin = new Padding(0);
                words[i].ForeColor = Color.White;
                //divide the words to default and emphasize by property
                if(!PropertyFilter.Contains(Program.TextTable[i, 1]))
                {
                    words[i].MouseEnter += (object sender, EventArgs e) => { ((Label)sender).BackColor = BackColorDefault; };
                    words[i].MouseLeave += (object sender, EventArgs e) => { ((Label)sender).BackColor = BackColorEmpha; };
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

            //adjust the height to a equal value (max height)
            for (int i = 0; i < wordsCount; i++)
                words[i].Size = new Size(words[i].Width, maxWordHeight);

            //allocate to different raws and set position
            rawMap[raw + 1, 0] = wordsCount;
            for (int r = 0; r <= raw; r++)
            {
                words[rawMap[r, 0]].Location = new Point(Math.Max((TextPanel.Width - rawMap[r, 1]) / 2, 0), (maxWordHeight + WordsRawMargin) * r);
                for (int i = rawMap[r, 0] + 1; i < rawMap[r + 1, 0]; i++)
                    words[i].Location = new Point(words[i - 1].Location.X + words[i - 1].Width + WordsMargin, words[i - 1].Location.Y);
            }
            
            //confirm the index
            TextPanel.Controls.AddRange(words);
            for (int i = 0; i < wordsCount; i++)
                TextPanel.Controls.SetChildIndex(words[i], i);

            TextPanel.Height = maxWordHeight * (raw + 1) + WordsRawMargin * raw;

            TranslationPanel.Location = new Point(TranslationPanel.Location.X, TextPanel.Bottom + TranslationTopMargin);
        }

        private void ClearLastResult()
        {
            TextPanel.Controls.Clear();
            TranslationPanel.Controls.Clear();
            ExplanationBuffer.Clear();
        }

        //it's a method that crosses between the threads, so we write like this
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
                t.Margin = new Padding(0);
                t.ForeColor = Color.White;
                t.BackColor = BackColorDefault;
                t.Location = new Point((TranslationPanel.Width - t.Width) / 2, (t.Height + TranslationRawMargin) * i);

                TranslationPanel.Controls.Add(t);
                int MaxBottom = 0;
                foreach (Control c in TranslationPanel.Controls)
                    MaxBottom = Math.Max(MaxBottom, c.Bottom);
                TranslationPanel.Height = MaxBottom;
            }
        }
        #endregion

        #region DicPanel
        private string WaitingQueryText = "正在查询...";
        private string NotFoundText = "未找到";
        private Label WordLabelNow = null;
        private Dictionary<int, string> ExplanationBuffer = new Dictionary<int, string>();

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
            if(WordLabelNow != null)
            {
                Rectangle rl = WordLabelNow.RectangleToScreen(WordLabelNow.ClientRectangle);
                Rectangle rp = DicPanel.RectangleToScreen(DicPanel.ClientRectangle);
                Rectangle r = new Rectangle(rl.Left, rl.Bottom, rp.Right - rl.Left, rp.Top - rl.Bottom);
                if (r.Contains(MousePosition) || rl.Contains(MousePosition) || rp.Contains(MousePosition)) return;
            }
            DicPanel.Hide();
        }

        private void DicPanel_Paint(object sender, PaintEventArgs e)
        {
            int i = TextPanel.Controls.GetChildIndex(WordLabelNow);
            word_dic.Text = WordLabelNow.Text;
            property_dic.Text = Program.TextTable[i, 1];
            pronunciation_dic.Text = "「" + Program.TextTable[i, 2] + "」";
            Form1_AutoHeight(null, null);
        }

        private delegate void OnWordExplanationDelegate(string[] strs);
        private void OnWordExplanation(string[] strs)
        {
            if (DicPanel.InvokeRequired)
            {
                OnWordExplanationDelegate d = OnWordExplanation;
                //the invoke method push all the object in object[] to d as params
                DicPanel.Invoke(d, new object[] { strs });
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
        #endregion

        #region copy button on DicPanel
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

        #region contentMenuStrip
        private void Clear_MenuStrip_Click(object sender, EventArgs e)
        {
            ClearLastResult();
        }

        private void Setting_MenuStrip_Click(object sender, EventArgs e)
        {
            Program.ShowSettingsForm();
        }

        private void Exit_MenuStrip_Click(object sender, EventArgs e)
        {
            Program.myExit();
        }
        #endregion
    }
}
