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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.Select_Btn = new System.Windows.Forms.Button();
            this.Exit_Btn = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.VNTable = new System.Windows.Forms.DataGridView();
            this.VNName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VNSettings = new System.Windows.Forms.DataGridViewImageColumn();
            this.Up = new System.Windows.Forms.Button();
            this.Down = new System.Windows.Forms.Button();
            this.Setting_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VNTable)).BeginInit();
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
            this.New.Size = new System.Drawing.Size(23, 23);
            this.New.TabIndex = 4;
            this.New.Text = "+";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Delete
            // 
            this.Delete.Enabled = false;
            this.Delete.Location = new System.Drawing.Point(41, 12);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(23, 23);
            this.Delete.TabIndex = 5;
            this.Delete.Text = "-";
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
            this.VNTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VNTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.VNTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VNTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
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
            this.VNTable.RowTemplate.Height = 36;
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
            // VNName
            // 
            this.VNName.DataPropertyName = "VNName";
            this.VNName.HeaderText = "名称";
            this.VNName.Name = "VNName";
            this.VNName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.VNName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VNName.Width = 370;
            // 
            // VNSettings
            // 
            this.VNSettings.HeaderText = "配置";
            this.VNSettings.Image = ((System.Drawing.Image)(resources.GetObject("VNSettings.Image")));
            this.VNSettings.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.VNSettings.Name = "VNSettings";
            this.VNSettings.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.VNSettings.Width = 36;
            // 
            // Up
            // 
            this.Up.Enabled = false;
            this.Up.Location = new System.Drawing.Point(70, 12);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(23, 23);
            this.Up.TabIndex = 9;
            this.Up.Text = "↑";
            this.Up.UseVisualStyleBackColor = true;
            this.Up.Click += new System.EventHandler(this.Up_Click);
            // 
            // Down
            // 
            this.Down.Enabled = false;
            this.Down.Location = new System.Drawing.Point(99, 12);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(23, 23);
            this.Down.TabIndex = 10;
            this.Down.Text = "↓";
            this.Down.UseVisualStyleBackColor = true;
            this.Down.Click += new System.EventHandler(this.Down_Click);
            // 
            // Setting_Btn
            // 
            this.Setting_Btn.Location = new System.Drawing.Point(347, 13);
            this.Setting_Btn.Name = "Setting_Btn";
            this.Setting_Btn.Size = new System.Drawing.Size(75, 23);
            this.Setting_Btn.TabIndex = 11;
            this.Setting_Btn.Text = "主设置";
            this.Setting_Btn.UseVisualStyleBackColor = true;
            this.Setting_Btn.Click += new System.EventHandler(this.Setting_Click);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn VNName;
        private System.Windows.Forms.DataGridViewImageColumn VNSettings;
    }
}