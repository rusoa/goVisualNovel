﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;

namespace goVisualNovel
{
    public partial class VNSettingsForm : Form
    {
        #region vars
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\goVisualNovel";
        private const int MAX_HOOKERS_NUM = 2;
        private VisualNovel vn;

        private Dictionary<string, string> LanguageDic = new Dictionary<string, string>()
        {
            { "日语", "ja" },
            { "英语", "en" }
        };

        private Dictionary<string, int> HookerDic = new Dictionary<string, int>()
        {
            { "arg8", 0x20 },
            { "arg7", 0x1c },
            { "arg6", 0x18 },
            { "arg5", 0x14 },
            { "arg4", 0x10 },
            { "arg3", 0x0c },
            { "arg2", 0x08 },
            { "arg1", 0x04 },
            { "eax", -0x14 },
            { "ecx", -0x18 },
            { "edx", -0x1c },
            { "ebx", -0x20 },
            { "esi", 0x24 },
            { "ebi", 0x28 },
            { "esp", 0x00 },
            { "ebp", 0x04 },
        };
        #endregion

        public VNSettingsForm(VisualNovel vn)
        {
            InitializeComponent();
            this.vn = vn;
        }

        private void VNSettingsForm_Load(object sender, EventArgs e)
        {
            #region controls logic and data sources
            BindingSource bLanguage = new BindingSource();
            bLanguage.DataSource = LanguageDic;
            Language_ComboBox.DataSource = bLanguage;
            Language_ComboBox.DisplayMember = "Key";
            Language_ComboBox.ValueMember = "Value";

            Hook1_CheckBox.Paint += (object o, PaintEventArgs pea) =>
            {
                Hook1Addr_TextBox.Enabled = Hook1EspBias_ComboBox.Enabled = Hook1ValueAsAddr_CheckBox.Enabled = Hook1BytesPerRead_ComboBox.Enabled =
                    Hook1_CheckBox.CheckState == CheckState.Checked;
            };

            Func<string, Control> GetControlByName = name =>
            {
                foreach (Control c in Controls)
                    if (c.Name == name) return c;
                return null;
            };
            for (int i = 0; i < MAX_HOOKERS_NUM; i++)
            {
                BindingSource bHookers = new BindingSource();
                bHookers.DataSource = HookerDic;
                ComboBox HookiEspBias_ComboBox = (ComboBox)GetControlByName(string.Format("Hook{0}EspBias_ComboBox", i));
                Hook0EspBias_ComboBox.DataSource = bHookers;
                Hook0EspBias_ComboBox.DisplayMember = "Key";
                Hook0EspBias_ComboBox.ValueMember = "Value";

                CheckBox HookiValueAsAddr_CheckBox = (CheckBox)GetControlByName(string.Format("Hook{0}ValueAsAddr_CheckBox", i));
                TextBox HookiValueAsAddrBias_TextBox = (TextBox)GetControlByName(string.Format("Hook{0}ValueAsAddrBias_TextBox", i));
                HookiValueAsAddr_CheckBox.Paint += (object o, PaintEventArgs ea) =>
                    { HookiValueAsAddrBias_TextBox.Enabled = HookiValueAsAddr_CheckBox.CheckState == CheckState.Checked; };
            }
            #endregion

            #region set attrs from object
            VNName_TextBox.Text = vn.VNName;
            foreach (KeyValuePair<string, string> pair in LanguageDic)
            {
                if (pair.Value != vn.Language) continue;
                Language_ComboBox.SelectedItem = pair;
                break;
            }
            ProcEncoding_ComboBox.SelectedItem = vn.ProcEncoding;
            ModuleName_TextBox.Text = vn.ModuleName;
            for (int i = 0; i < vn.Hookers.Count; i++)
            {
                foreach(Control c in Controls)
                {
                    if (c.Name == "Hook" + i + "Addr_TextBox")
                    {
                        ((TextBox)c).Text = "0x" + vn.Hookers[i].Addr.ToString("x");
                    }  
                    else if (c.Name == "Hook" + i + "EspBias_ComboBox")
                    {
                        foreach (KeyValuePair<string, int> pair in HookerDic)
                        {
                            if (pair.Value != vn.Hookers[i].EspBias) continue;
                            ((ComboBox)c).SelectedItem = pair;
                            break;
                        }
                    }
                    else if (c.Name == "Hook" + i + "ValueAsAddr_CheckBox")
                    {
                        ((CheckBox)c).CheckState = vn.Hookers[i].ValueAsAddr ? CheckState.Checked : CheckState.Unchecked;
                    }
                    else if(c.Name == "Hook" + i + "ValueAsAddrBias_TextBox")
                    {
                        ((TextBox)c).Text = "0x" + vn.Hookers[i].ValueAsAddrBias.ToString("x");
                    }
                    else if(c.Name == "Hook" + i + "BytesPerRead_ComboBox")
                    {
                        ((ComboBox)c).SelectedItem = vn.Hookers[i].BytesPerRead.ToString();
                    }
                }
            }
            if (vn.Hookers.Count == 1)
                Hook1_CheckBox.CheckState = CheckState.Unchecked;
            WordsFilter_TextBox.Text = string.Join(",", vn.WordsFilter);
            #endregion

            #region controls value change bindings
            VNName_TextBox.TextChanged += (object o, EventArgs ea) =>
                { vn.VNName = VNName_TextBox.Text; };

            Language_ComboBox.SelectedIndexChanged += (object o, EventArgs ea) =>
                { vn.Language = (string)Language_ComboBox.SelectedValue; };

            ProcEncoding_ComboBox.SelectedIndexChanged += (object o, EventArgs ea) =>
                { vn.ProcEncoding = (string)ProcEncoding_ComboBox.SelectedValue; };

            ModuleName_TextBox.TextChanged += (object o, EventArgs ea) =>
                { vn.ModuleName = ModuleName_TextBox.Text; };

            foreach (Control c in Controls)
            {
                if(Regex.IsMatch(c.Name, "Hook\\dAddr_TextBox"))
                {
                    ((TextBox)c).TextChanged += (object o, EventArgs ea) =>
                    {
                        string text = ((TextBox)o).Text;

                        if (!Regex.IsMatch(text, "^0x[0-9a-eA-E]*$"))
                        {
                            AccessHooker(o, h => { ((TextBox)o).Text = "0x" + h.Addr.ToString("x"); return h; });
                            ((TextBox)o).Select(text.Length, 0);
                            return;
                        }

                        if (text == "0x") text = "0x0";
                        AccessHooker(o, h => { h.Addr = (IntPtr)Convert.ToUInt32(text, 16); return h; }); //[...]溢出，考虑确认时一次性检验
                    };
                }
                else if (Regex.IsMatch(c.Name, "Hook\\dEspBias_ComboBox"))
                {
                    ((ComboBox)c).SelectedIndexChanged += (object o, EventArgs ea) =>
                    {
                        AccessHooker(o, h => { h.EspBias = (int)((ComboBox)o).SelectedValue; return h; });
                    };
                }
                else if (Regex.IsMatch(c.Name, "Hook\\dValueAsAddr_CheckBox"))
                {
                    ((CheckBox)c).CheckStateChanged += (object o, EventArgs ea) =>
                    {
                        AccessHooker(o, h => { h.ValueAsAddr = ((CheckBox)o).CheckState == CheckState.Checked; return h; });
                    };
                }
                else if (Regex.IsMatch(c.Name, "Hook\\dValueAsAddrBias_TextBox"))
                {
                    ((TextBox)c).TextChanged += (object o, EventArgs ea) =>
                    {
                        string text = ((TextBox)o).Text;

                        if (!Regex.IsMatch(text, "^0x[0-9a-eA-E]*$"))
                        {
                            AccessHooker(o, h => { ((TextBox)o).Text = "0x" + h.ValueAsAddrBias.ToString("x"); return h; });
                            ((TextBox)o).Select(text.Length, 0);
                            return;
                        }

                        if (text == "0x") text = "0x0";
                        AccessHooker(o, h => { h.Addr = (IntPtr)Convert.ToUInt32(text, 16); return h; }); //[...]同上
                    };
                }
                else if (Regex.IsMatch(c.Name, "Hook\\dBytesPerRead_ComboBox"))
                {
                    ((ComboBox)c).SelectedIndexChanged += (object o, EventArgs ea) =>
                    {
                        AccessHooker(o, h => { h.BytesPerRead = Convert.ToInt16(((ComboBox)o).SelectedItem); return h; });
                    };
                }
            }
            #endregion
        }

        /**
         * Hooker idx depends on the ((Control)o).Name "Hook[i]xxxx"
         */
        private void AccessHooker(object o, Func<VisualNovel.Hooker, VisualNovel.Hooker> f)
        {
            int i = Convert.ToInt16(((Control)o).Name[4].ToString());
            vn.Hookers[i] = f(vn.Hookers[i]);
        }

        private void ImportFromHCode_Click(object sender, EventArgs e)
        {
            string str = "";
        st: str = Interaction.InputBox("由于本程序对原提取器功能做了一些扩展，\n\n因此仅支持从特殊码中导入一部分设置。", "请输入特殊码", str);
            try
            {
                if(!string.IsNullOrEmpty(str))
                    vn.SetAttrsFromHCode(str);
            }
            catch(Exception)
            {
                MessageBox.Show("解析失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto st;
            }
        }
    }
}