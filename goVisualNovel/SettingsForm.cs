using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goVisualNovel
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            SettingsTree.SelectedNode = SettingsTree.Nodes.Find("Appearance", false)[0];
        }

        private void SettingsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch(e.Node.Name)
            {
                case "Appearance":
                    AppearanceSetting();
                    break;
                case "Translation":
                    TranslationSetting();
                    break;
                default:
                    break;
            }
        }

        private void AppearanceSetting()
        {
            SettingsPanel.Controls.Clear();
            
            Label l = new Label();
            l.Text = "施工中...";
            SettingsPanel.Controls.Add(l);
        }

        private void TranslationSetting()
        {
            SettingsPanel.Controls.Clear();

            CheckBox cb = new CheckBox();
            cb.Name = "YoudaoCheckBox";
            cb.Text = "有道翻译";
            cb.Location = new Point(5, 5);
            cb.Size = new Size(76, 24);
            cb.CheckStateChanged += YoudaoCheckBox_CheckStateChanged;

            Label l1 = new Label();
            l1.Text = "AppKey:";
            l1.Location = new Point(98, 7);
            l1.Size = new Size(47, 16);
            l1.TextAlign = ContentAlignment.MiddleLeft;

            TextBox t1 = new TextBox();
            t1.Name = "Youdao_AppKey";
            t1.Location = new Point(146, 5);
            t1.Size = new Size(110, 21);
            t1.Text = Properties.Settings.Default.Youdao_AppKey;

            Label l2 = new Label();
            l2.Text = "AppSecret:";
            l2.Location = new Point(270, 7);
            l2.Size = new Size(65, 16);
            l2.TextAlign = ContentAlignment.MiddleLeft;

            TextBox t2 = new TextBox();
            t2.Name = "Youdao_AppSecret";
            t2.Location = new Point(336, 5);
            t2.Size = new Size(210, 21);
            t2.Text = Properties.Settings.Default.Youdao_AppSecret;

            t1.Enabled = t2.Enabled = false;

            SettingsPanel.Controls.AddRange(new Control[] { cb, l1, t1, l2, t2 });

            cb.CheckState = Properties.Settings.Default.Youdao ? CheckState.Checked : CheckState.Unchecked;
        }

        private void YoudaoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            Control t1 = SettingsPanel.Controls.Find("Youdao_AppKey", false)[0];
            Control t2 = SettingsPanel.Controls.Find("Youdao_AppSecret", false)[0];
            if (((CheckBox)sender).CheckState == CheckState.Checked)
            {
                Properties.Settings.Default.Youdao = true;
                t1.Enabled = t2.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.Youdao = false;
                t1.Enabled = t2.Enabled = false;
            }
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Youdao = ((CheckBox)SettingsPanel.Controls.Find("YoudaoCheckBox", false)[0]).CheckState == CheckState.Checked ? true : false;
            Properties.Settings.Default.Youdao_AppKey = SettingsPanel.Controls.Find("Youdao_AppKey", false)[0].Text;
            Properties.Settings.Default.Youdao_AppSecret = SettingsPanel.Controls.Find("Youdao_AppSecret", false)[0].Text;
            Properties.Settings.Default.Save();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            Apply_Click(sender, e);
            Close();
        }
    }
}
