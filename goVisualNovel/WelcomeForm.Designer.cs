namespace goVisualNovel
{
    partial class WelcomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.Accept = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.VNTable = new System.Windows.Forms.DataGridView();
            this.VNName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecialCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WordsFilter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Up = new System.Windows.Forms.Button();
            this.Down = new System.Windows.Forms.Button();
            this.Setting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VNTable)).BeginInit();
            this.SuspendLayout();
            // 
            // Accept
            // 
            this.Accept.Enabled = false;
            this.Accept.Location = new System.Drawing.Point(617, 376);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 0;
            this.Accept.Text = "选择";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(698, 376);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "退出";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(12, 12);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(75, 23);
            this.New.TabIndex = 4;
            this.New.Text = "新建";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Location = new System.Drawing.Point(93, 12);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 5;
            this.Delete.Text = "删除";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // VNTable
            // 
            this.VNTable.AllowUserToAddRows = false;
            this.VNTable.AllowUserToDeleteRows = false;
            this.VNTable.AllowUserToResizeColumns = false;
            this.VNTable.AllowUserToResizeRows = false;
            this.VNTable.BackgroundColor = System.Drawing.Color.White;
            this.VNTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VNTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.VNTable.ColumnHeadersHeight = 40;
            this.VNTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.VNTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VNName,
            this.SpecialCode,
            this.WordsFilter});
            this.VNTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.VNTable.GridColor = System.Drawing.Color.Gainsboro;
            this.VNTable.Location = new System.Drawing.Point(13, 42);
            this.VNTable.MultiSelect = false;
            this.VNTable.Name = "VNTable";
            this.VNTable.RowHeadersVisible = false;
            this.VNTable.RowTemplate.Height = 30;
            this.VNTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VNTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VNTable.Size = new System.Drawing.Size(759, 328);
            this.VNTable.TabIndex = 8;
            this.VNTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.VNTable_CellBeginEdit);
            this.VNTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VNTable_CellDoubleClick);
            this.VNTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.VNTable_CellEndEdit);
            this.VNTable.SelectionChanged += new System.EventHandler(this.VNTable_SelectionChanged);
            this.VNTable.Click += new System.EventHandler(this.VNTable_Click);
            this.VNTable.Paint += new System.Windows.Forms.PaintEventHandler(this.VNTable_Paint);
            // 
            // VNName
            // 
            this.VNName.HeaderText = "名称";
            this.VNName.Name = "VNName";
            this.VNName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VNName.Width = 232;
            // 
            // SpecialCode
            // 
            this.SpecialCode.HeaderText = "特殊码";
            this.SpecialCode.Name = "SpecialCode";
            this.SpecialCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SpecialCode.Width = 292;
            // 
            // WordsFilter
            // 
            this.WordsFilter.HeaderText = "词语过滤器(英文逗号分隔)";
            this.WordsFilter.Name = "WordsFilter";
            this.WordsFilter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WordsFilter.Width = 232;
            // 
            // Up
            // 
            this.Up.Enabled = false;
            this.Up.Location = new System.Drawing.Point(174, 12);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(75, 23);
            this.Up.TabIndex = 9;
            this.Up.Text = "上移";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // Down
            // 
            this.Down.Enabled = false;
            this.Down.Location = new System.Drawing.Point(255, 12);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(75, 23);
            this.Down.TabIndex = 10;
            this.Down.Text = "下移";
            this.Down.UseVisualStyleBackColor = true;
            this.Down.Click += new System.EventHandler(this.Down_Click);
            // 
            // Setting
            // 
            this.Setting.Location = new System.Drawing.Point(697, 12);
            this.Setting.Name = "Setting";
            this.Setting.Size = new System.Drawing.Size(75, 23);
            this.Setting.TabIndex = 11;
            this.Setting.Text = "设置";
            this.Setting.UseVisualStyleBackColor = true;
            this.Setting.Click += new System.EventHandler(this.Setting_Click);
            // 
            // WelcomeForm
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.Setting);
            this.Controls.Add(this.Down);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.VNTable);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.New);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "goVisualNovel";
            this.Load += new System.EventHandler(this.WelcomeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VNTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.DataGridView VNTable;
        private System.Windows.Forms.Button Up;
        private System.Windows.Forms.Button Down;
        private System.Windows.Forms.Button Setting;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecialCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WordsFilter;
    }
}