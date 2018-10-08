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
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace goVisualNovel
{
    public partial class WelcomeForm : Form
    {
        #region vars
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\goVisualNovel\\VNSettings.json";
        private List<VisualNovel> VNList = null;
        private BindingList<VisualNovel> bVNList;

        private Color OddBackColor = Color.White;
        private Color EvenBackColor = Color.LavenderBlush;
        private Color RawForeColor = Color.Black;
        private Color SelectBackColor = Color.PaleVioletRed;
        private Color SelectForeColor = Color.White;
        #endregion

        #region form
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            VNTable.DefaultCellStyle.SelectionBackColor = SelectBackColor;
            VNTable.DefaultCellStyle.SelectionForeColor = SelectForeColor;

            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(path, FileMode.Open);
                sr = new StreamReader(fs, Encoding.GetEncoding("utf-8"));
                VNList = JsonConvert.DeserializeObject<List<VisualNovel>>(sr.ReadToEnd());
            }
            catch(DirectoryNotFoundException)
            {
                Directory.CreateDirectory(path);
                VNList = new List<VisualNovel>();
            }
            catch(FileNotFoundException)
            {
                VNList = new List<VisualNovel>();
            }
            catch(Exception)
            {
                MessageBox.Show("读取配置信息错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VNList = new List<VisualNovel>();
            }
            if (sr != null) sr.Close();
            if (fs != null) fs.Close();

            bVNList = new BindingList<VisualNovel>(VNList);
            VNTable.AutoGenerateColumns = false;
            VNTable.DataSource = bVNList;
        }
        #endregion

        #region items edit buttons
        private void New_Click(object sender, EventArgs e)
        {
            VisualNovel vn = new VisualNovel();
            if (VNTable.SelectedRows.Count == 0)
            {
                bVNList.Add(vn);
                VNTable.Rows[VNTable.Rows.Count - 1].Selected = true;
            }
            else
            {
                bVNList.Insert(VNTable.SelectedRows[0].Index + 1, vn);
                VNTable.Rows[VNTable.SelectedRows[0].Index + 1].Selected = true;
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int i = VNTable.SelectedRows[0].Index;
            bVNList.RemoveAt(i);
            if (i > 0) VNTable.Rows[i - 1].Selected = true;
        }

        private void Up_Click(object sender, EventArgs e)
        {
            int i = VNTable.SelectedRows[0].Index;
            VisualNovel temp = bVNList[i];
            bVNList.Remove(temp);
            bVNList.Insert(i - 1, temp);
            VNTable.Rows[i - 1].Selected = true;
        }

        private void Down_Click(object sender, EventArgs e)
        {
            int i = VNTable.SelectedRows[0].Index;
            VisualNovel temp = bVNList[i];
            bVNList.Remove(temp);
            bVNList.Insert(i + 1, temp);
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

        private void VNTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                VisualNovel vn = new VisualNovel();
                VNList[e.RowIndex].CopyTo(ref vn);

                VNSettingsForm VNSet = new VNSettingsForm(vn);

                DialogResult res = VNSet.ShowDialog();
                if(res == DialogResult.OK)
                {
                    VNList[e.RowIndex] = vn;
                    VNTable.Refresh();
                }
            }
        }

        private void VNTable_SelectionChanged(object sender, EventArgs e)
        {
            if(VNTable.IsCurrentCellInEditMode) VNTable.EndEdit();
            if (VNTable.SelectedRows.Count == 0)
                Up.Enabled = Down.Enabled = Select_Btn.Enabled = Delete.Enabled = false;
            else
            {
                Select_Btn.Enabled = Delete.Enabled = true;
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

        #region form buttons
        private void Select_Click(object sender, EventArgs e)
        {
            SaveVNList();
            try
            {
                Program.StartExtText(VNList[VNTable.SelectedRows[0].Index]);
            }
            catch(Exception)
            {
                MessageBox.Show("启动失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            SaveVNList();
            Close();
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            Program.ShowSettingsForm();
        }

        private void SaveVNList()
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("utf-8"));
            sw.Write(JsonConvert.SerializeObject(VNList));
            sw.Close();
            fs.Close();
        }
        #endregion
    }
}
