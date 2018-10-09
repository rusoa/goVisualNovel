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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.Select_Btn = new System.Windows.Forms.Button();
            this.Exit_Btn = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.VNTable = new System.Windows.Forms.DataGridView();
            this.Up = new System.Windows.Forms.Button();
            this.Down = new System.Windows.Forms.Button();
            this.Setting_Btn = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Clear_MenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.Setting_MenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit_MenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.VNName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNSettings = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.VNTable)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Select_Btn
            // 
            this.Select_Btn.Enabled = false;
            this.Select_Btn.Location = new System.Drawing.Point(266, 376);
            this.Select_Btn.Name = "Select_Btn";
            this.Select_Btn.Size = new System.Drawing.Size(75, 23);
            this.Select_Btn.TabIndex = 0;
            this.Select_Btn.Text = "选择";
            this.Select_Btn.UseVisualStyleBackColor = true;
            this.Select_Btn.Click += new System.EventHandler(this.Select_Click);
            // 
            // Exit_Btn
            // 
            this.Exit_Btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Exit_Btn.Location = new System.Drawing.Point(347, 376);
            this.Exit_Btn.Name = "Exit_Btn";
            this.Exit_Btn.Size = new System.Drawing.Size(75, 23);
            this.Exit_Btn.TabIndex = 1;
            this.Exit_Btn.Text = "退出";
            this.Exit_Btn.UseVisualStyleBackColor = true;
            this.Exit_Btn.Click += new System.EventHandler(this.Exit_Click);
            // 
            // New
            // 
            this.New.Location = new System.Drawing.Point(12, 12);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(54, 23);
            this.New.TabIndex = 4;
            this.New.Text = "新建";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Location = new System.Drawing.Point(72, 12);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(54, 23);
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
            this.VNTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.VNTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
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
            this.VNTable.ColumnHeadersVisible = false;
            this.VNTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VNName,
            this.VNSettings});
            this.VNTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.VNTable.GridColor = System.Drawing.Color.Gainsboro;
            this.VNTable.Location = new System.Drawing.Point(13, 42);
            this.VNTable.MultiSelect = false;
            this.VNTable.Name = "VNTable";
            this.VNTable.RowHeadersVisible = false;
            this.VNTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.VNTable.RowTemplate.Height = 48;
            this.VNTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VNTable.Size = new System.Drawing.Size(409, 328);
            this.VNTable.TabIndex = 8;
            this.VNTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.VNTable_CellBeginEdit);
            this.VNTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VNTable_CellContentClick);
            this.VNTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VNTable_CellDoubleClick);
            this.VNTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.VNTable_CellEndEdit);
            this.VNTable.SelectionChanged += new System.EventHandler(this.VNTable_SelectionChanged);
            this.VNTable.Click += new System.EventHandler(this.VNTable_Click);
            this.VNTable.Paint += new System.Windows.Forms.PaintEventHandler(this.VNTable_Paint);
            // 
            // Up
            // 
            this.Up.Enabled = false;
            this.Up.Location = new System.Drawing.Point(132, 12);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(54, 23);
            this.Up.TabIndex = 9;
            this.Up.Text = "上移";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // Down
            // 
            this.Down.Enabled = false;
            this.Down.Location = new System.Drawing.Point(192, 12);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(54, 23);
            this.Down.TabIndex = 10;
            this.Down.Text = "下移";
            this.Down.UseVisualStyleBackColor = true;
            this.Down.Click += new System.EventHandler(this.Down_Click);
            // 
            // Setting_Btn
            // 
            this.Setting_Btn.Location = new System.Drawing.Point(347, 12);
            this.Setting_Btn.Name = "Setting_Btn";
            this.Setting_Btn.Size = new System.Drawing.Size(75, 23);
            this.Setting_Btn.TabIndex = 11;
            this.Setting_Btn.Text = "主设置";
            this.Setting_Btn.UseVisualStyleBackColor = true;
            this.Setting_Btn.Click += new System.EventHandler(this.Setting_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "goVisualNovel";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Clear_MenuStrip,
            this.Setting_MenuStrip,
            this.toolStripSeparator1,
            this.Exit_MenuStrip});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 76);
            // 
            // Clear_MenuStrip
            // 
            this.Clear_MenuStrip.Name = "Clear_MenuStrip";
            this.Clear_MenuStrip.Size = new System.Drawing.Size(100, 22);
            this.Clear_MenuStrip.Text = "清屏";
            this.Clear_MenuStrip.Click += new System.EventHandler(this.Clear_MenuStrip_Click);
            // 
            // Setting_MenuStrip
            // 
            this.Setting_MenuStrip.Name = "Setting_MenuStrip";
            this.Setting_MenuStrip.Size = new System.Drawing.Size(100, 22);
            this.Setting_MenuStrip.Text = "设置";
            this.Setting_MenuStrip.Click += new System.EventHandler(this.Setting_MenuStrip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // Exit_MenuStrip
            // 
            this.Exit_MenuStrip.Name = "Exit_MenuStrip";
            this.Exit_MenuStrip.Size = new System.Drawing.Size(100, 22);
            this.Exit_MenuStrip.Text = "退出";
            this.Exit_MenuStrip.Click += new System.EventHandler(this.Exit_MenuStrip_Click);
            // 
            // VNName
            // 
            this.VNName.DataPropertyName = "VNName";
            this.VNName.HeaderText = "名称";
            this.VNName.Name = "VNName";
            this.VNName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.VNName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VNName.Width = 359;
            // 
            // VNSettings
            // 
            this.VNSettings.HeaderText = "配置";
            this.VNSettings.Image = ((System.Drawing.Image)(resources.GetObject("VNSettings.Image")));
            this.VNSettings.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.VNSettings.Name = "VNSettings";
            this.VNSettings.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.VNSettings.Width = 48;
            // 
            // WelcomeForm
            // 
            this.AcceptButton = this.Select_Btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Exit_Btn;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.Setting_Btn);
            this.Controls.Add(this.Down);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.VNTable);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.New);
            this.Controls.Add(this.Exit_Btn);
            this.Controls.Add(this.Select_Btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "goVisualNovel";
            this.Load += new System.EventHandler(this.WelcomeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VNTable)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Select_Btn;
        private System.Windows.Forms.Button Exit_Btn;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.DataGridView VNTable;
        private System.Windows.Forms.Button Up;
        private System.Windows.Forms.Button Down;
        private System.Windows.Forms.Button Setting_Btn;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem Clear_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem Setting_MenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Exit_MenuStrip;
        private System.Windows.Forms.DataGridViewTextBoxColumn VNName;
        private System.Windows.Forms.DataGridViewImageColumn VNSettings;
    }
}