using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace goVisualNovel
{
    public partial class WelcomeForm : Form
    {
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\goVisualNovel";
        private Color OddBackColor = Color.White;
        private Color EvenBackColor = Color.LavenderBlush;
        private Color RawForeColor = Color.Black;
        private Color SelectBackColor = Color.PaleVioletRed;
        private Color SelectForeColor = Color.White;

        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            VNTable.DefaultCellStyle.SelectionBackColor = SelectBackColor;
            VNTable.DefaultCellStyle.SelectionForeColor = SelectForeColor;

            XmlDocument doc = new XmlDocument();
            try { doc.Load(path + "\\VNSettings.xml"); }
            catch(DirectoryNotFoundException)
            {
                Directory.CreateDirectory(path);
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));
                doc.AppendChild(doc.CreateElement("vns"));
            }
            catch(FileNotFoundException)
            {
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));
                doc.AppendChild(doc.CreateElement("vns"));
            }
            foreach(XmlNode node in doc.DocumentElement.GetElementsByTagName("vn"))
            {
                VNTable.Rows.Add(1);
                VNTable[0, VNTable.Rows.Count - 1].Value = ((XmlElement)node).GetElementsByTagName("VNName")[0].InnerText;
                VNTable[1, VNTable.Rows.Count - 1].Value = ((XmlElement)node).GetElementsByTagName("SpecialCode")[0].InnerText;
                VNTable[2, VNTable.Rows.Count - 1].Value = ((XmlElement)node).GetElementsByTagName("WordsFilter")[0].InnerText;
            }
        }

        #region operator buttons
        private void New_Click(object sender, EventArgs e)
        {
            if (VNTable.SelectedRows.Count == 0)
            {
                VNTable.Rows.Add(1);
                VNTable.Rows[VNTable.Rows.Count - 1].Selected = true;
            }
            else
            {
                VNTable.Rows.Insert(VNTable.SelectedRows[0].Index + 1);
                VNTable.Rows[VNTable.SelectedRows[0].Index + 1].Selected = true;
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int i = VNTable.SelectedRows[0].Index;
            VNTable.Rows.Remove(VNTable.SelectedRows[0]);
            if (i > 0) VNTable.Rows[i - 1].Selected = true;
        }

        private void Up_Click(object sender, EventArgs e)
        {
            DataGridViewRow temp = VNTable.SelectedRows[0];
            int i = temp.Index;
            VNTable.Rows.Remove(temp);
            VNTable.Rows.Insert(i - 1, temp);
            VNTable.Rows[i - 1].Selected = true;
        }

        private void Down_Click(object sender, EventArgs e)
        {
            DataGridViewRow temp = VNTable.SelectedRows[0];
            int i = temp.Index;
            VNTable.Rows.Remove(temp);
            VNTable.Rows.Insert(i + 1, temp);
            VNTable.Rows[i + 1].Selected = true;
        }
        #endregion

        #region table itself
        private void VNTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            VNTable.BeginEdit(false);
        }

        private void VNTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            VNTable.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = VNTable.Rows[e.RowIndex].DefaultCellStyle.BackColor;
            VNTable.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = VNTable.Rows[e.RowIndex].DefaultCellStyle.ForeColor;
        }

        private void VNTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            VNTable.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = VNTable.DefaultCellStyle.SelectionBackColor;
            VNTable.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = VNTable.DefaultCellStyle.SelectionForeColor;
        }

        private void VNTable_Click(object sender, EventArgs e)
        {
            if (VNTable.SelectedRows.Count == 0) return;
            if (VNTable.GetRowDisplayRectangle(VNTable.SelectedRows[0].Index, true).Contains(VNTable.PointToClient(MousePosition))) return;
            VNTable.ClearSelection();
        }

        private void VNTable_SelectionChanged(object sender, EventArgs e)
        {
            if(VNTable.IsCurrentCellInEditMode) VNTable.EndEdit();
            if (VNTable.SelectedRows.Count == 0)
                Up.Enabled = Down.Enabled = Accept.Enabled = Delete.Enabled = false;
            else
            {
                Accept.Enabled = Delete.Enabled = true;
                if (VNTable.SelectedRows[0].Index > 0) Up.Enabled = true;
                else Up.Enabled = false;
                if (VNTable.SelectedRows[0].Index < VNTable.Rows.Count - 1) Down.Enabled = true;
                else Down.Enabled = false;
            }
        }

        private void VNTable_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < VNTable.Rows.Count; i++)
            {
                if (i % 2 == 1) VNTable.Rows[i].DefaultCellStyle.BackColor = OddBackColor;
                else VNTable.Rows[i].DefaultCellStyle.BackColor = EvenBackColor;
                VNTable.Rows[i].DefaultCellStyle.ForeColor = RawForeColor;
            }
        }
        #endregion

        private void Accept_Click(object sender, EventArgs e)
        {
            SaveDoc();
            int rowi = VNTable.SelectedRows[0].Index;
            string VNName = VNTable[0, rowi].Value != null ? VNTable[0, rowi].Value.ToString() : "";
            string SpecialCode = VNTable[1, rowi].Value != null ? VNTable[1, rowi].Value.ToString() : "";
            string WordsFilter = VNTable[2, rowi].Value != null ? VNTable[2, rowi].Value.ToString() : "";
            try
            {
                Program.StartExtText(VNName, "ja", SpecialCode, 2, "shift-jis", WordsFilter);
            }
            catch (Exception)
            {
                MessageBox.Show("解析错误！");
                return;
            }
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            SaveDoc();
            Close();
            Program.myExit();
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            Program.ShowSettingsForm();
        }

        private void SaveDoc()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlElement root = doc.CreateElement("vns");
            for (int i = 0; i < VNTable.Rows.Count; i++)
            {
                XmlElement vn = doc.CreateElement("vn");
                //if the name is empty, then skip it
                if (VNTable[0, i].Value == null || VNTable[0, i].Value.ToString().Trim() == string.Empty) continue;
                //loop to store all the cell values in this row
                for (int j = 0; j < VNTable.Columns.Count; j++)
                {
                    XmlElement element = doc.CreateElement(VNTable.Columns[j].Name);
                    XmlNode node = doc.CreateTextNode(VNTable[j, i].Value != null ? VNTable[j, i].Value.ToString() : "");
                    element.AppendChild(node);
                    vn.AppendChild(element);
                }
                root.AppendChild(vn);
            }
            doc.AppendChild(root);
            doc.Save(path + "\\VNSettings.xml");
        }
    }
}
