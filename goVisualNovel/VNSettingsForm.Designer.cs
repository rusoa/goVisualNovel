namespace goVisualNovel
{
    partial class VNSettingsForm
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
            this.Accept_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.VNName_TextBox = new System.Windows.Forms.TextBox();
            this.ModuleName_TextBox = new System.Windows.Forms.TextBox();
            this.ProcEncoding_ComboBox = new System.Windows.Forms.ComboBox();
            this.Language_ComboBox = new System.Windows.Forms.ComboBox();
            this.WordsFilter_TextBox = new System.Windows.Forms.TextBox();
            this.Hook0Addr_TextBox = new System.Windows.Forms.TextBox();
            this.Hook0EspBias_ComboBox = new System.Windows.Forms.ComboBox();
            this.Hook0ValueAsAddr_CheckBox = new System.Windows.Forms.CheckBox();
            this.Hook0ValueAsAddrBias_TextBox = new System.Windows.Forms.TextBox();
            this.Hook1_CheckBox = new System.Windows.Forms.CheckBox();
            this.ImportFromHCode = new System.Windows.Forms.Button();
            this.Hook1ValueAsAddrBias_TextBox = new System.Windows.Forms.TextBox();
            this.Hook1ValueAsAddr_CheckBox = new System.Windows.Forms.CheckBox();
            this.Hook1EspBias_ComboBox = new System.Windows.Forms.ComboBox();
            this.Hook1Addr_TextBox = new System.Windows.Forms.TextBox();
            this.VNName_Label = new System.Windows.Forms.Label();
            this.ModuleName_Label = new System.Windows.Forms.Label();
            this.Hook0Addr_Label = new System.Windows.Forms.Label();
            this.Hook0EspBias_Label = new System.Windows.Forms.Label();
            this.Hook0BytesPerRead_Label = new System.Windows.Forms.Label();
            this.Hook0BytesPerRead_ComboBox = new System.Windows.Forms.ComboBox();
            this.Hook1Addr_Label = new System.Windows.Forms.Label();
            this.Hook1EspBias_Label = new System.Windows.Forms.Label();
            this.Hook1BytesPerRead_Label = new System.Windows.Forms.Label();
            this.Hook1BytesPerRead_ComboBox = new System.Windows.Forms.ComboBox();
            this.WordsFilter_Label = new System.Windows.Forms.Label();
            this.Language_Label = new System.Windows.Forms.Label();
            this.ProcEncoding_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Accept_Button
            // 
            this.Accept_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Accept_Button.Location = new System.Drawing.Point(146, 466);
            this.Accept_Button.Name = "Accept_Button";
            this.Accept_Button.Size = new System.Drawing.Size(75, 23);
            this.Accept_Button.TabIndex = 0;
            this.Accept_Button.Text = "确定";
            this.Accept_Button.UseVisualStyleBackColor = true;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(227, 466);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 1;
            this.Cancel_Button.Text = "取消";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            // 
            // VNName_TextBox
            // 
            this.VNName_TextBox.Location = new System.Drawing.Point(47, 12);
            this.VNName_TextBox.Name = "VNName_TextBox";
            this.VNName_TextBox.Size = new System.Drawing.Size(255, 21);
            this.VNName_TextBox.TabIndex = 3;
            // 
            // ModuleName_TextBox
            // 
            this.ModuleName_TextBox.Location = new System.Drawing.Point(47, 121);
            this.ModuleName_TextBox.Name = "ModuleName_TextBox";
            this.ModuleName_TextBox.Size = new System.Drawing.Size(255, 21);
            this.ModuleName_TextBox.TabIndex = 4;
            // 
            // ProcEncoding_ComboBox
            // 
            this.ProcEncoding_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProcEncoding_ComboBox.FormattingEnabled = true;
            this.ProcEncoding_ComboBox.Items.AddRange(new object[] {
            "shift-jis",
            "utf-8",
            "utf-16",
            "utf-16-le",
            "utf-16-be"});
            this.ProcEncoding_ComboBox.Location = new System.Drawing.Point(204, 39);
            this.ProcEncoding_ComboBox.Name = "ProcEncoding_ComboBox";
            this.ProcEncoding_ComboBox.Size = new System.Drawing.Size(98, 20);
            this.ProcEncoding_ComboBox.TabIndex = 5;
            // 
            // Language_ComboBox
            // 
            this.Language_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Language_ComboBox.FormattingEnabled = true;
            this.Language_ComboBox.Location = new System.Drawing.Point(83, 39);
            this.Language_ComboBox.Name = "Language_ComboBox";
            this.Language_ComboBox.Size = new System.Drawing.Size(80, 20);
            this.Language_ComboBox.TabIndex = 6;
            // 
            // WordsFilter_TextBox
            // 
            this.WordsFilter_TextBox.Location = new System.Drawing.Point(11, 439);
            this.WordsFilter_TextBox.Name = "WordsFilter_TextBox";
            this.WordsFilter_TextBox.Size = new System.Drawing.Size(291, 21);
            this.WordsFilter_TextBox.TabIndex = 7;
            // 
            // Hook0Addr_TextBox
            // 
            this.Hook0Addr_TextBox.Location = new System.Drawing.Point(47, 179);
            this.Hook0Addr_TextBox.Name = "Hook0Addr_TextBox";
            this.Hook0Addr_TextBox.Size = new System.Drawing.Size(255, 21);
            this.Hook0Addr_TextBox.TabIndex = 8;
            // 
            // Hook0EspBias_ComboBox
            // 
            this.Hook0EspBias_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Hook0EspBias_ComboBox.FormattingEnabled = true;
            this.Hook0EspBias_ComboBox.Location = new System.Drawing.Point(47, 206);
            this.Hook0EspBias_ComboBox.Name = "Hook0EspBias_ComboBox";
            this.Hook0EspBias_ComboBox.Size = new System.Drawing.Size(74, 20);
            this.Hook0EspBias_ComboBox.TabIndex = 9;
            // 
            // Hook0ValueAsAddr_CheckBox
            // 
            this.Hook0ValueAsAddr_CheckBox.AutoSize = true;
            this.Hook0ValueAsAddr_CheckBox.Location = new System.Drawing.Point(144, 208);
            this.Hook0ValueAsAddr_CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.Hook0ValueAsAddr_CheckBox.Name = "Hook0ValueAsAddr_CheckBox";
            this.Hook0ValueAsAddr_CheckBox.Size = new System.Drawing.Size(120, 16);
            this.Hook0ValueAsAddr_CheckBox.TabIndex = 10;
            this.Hook0ValueAsAddr_CheckBox.Text = "值作为指针,偏移:";
            this.Hook0ValueAsAddr_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Hook0ValueAsAddrBias_TextBox
            // 
            this.Hook0ValueAsAddrBias_TextBox.Location = new System.Drawing.Point(267, 206);
            this.Hook0ValueAsAddrBias_TextBox.Name = "Hook0ValueAsAddrBias_TextBox";
            this.Hook0ValueAsAddrBias_TextBox.Size = new System.Drawing.Size(35, 21);
            this.Hook0ValueAsAddrBias_TextBox.TabIndex = 11;
            // 
            // Hook1_CheckBox
            // 
            this.Hook1_CheckBox.AutoSize = true;
            this.Hook1_CheckBox.Location = new System.Drawing.Point(11, 273);
            this.Hook1_CheckBox.Name = "Hook1_CheckBox";
            this.Hook1_CheckBox.Size = new System.Drawing.Size(84, 16);
            this.Hook1_CheckBox.TabIndex = 12;
            this.Hook1_CheckBox.Text = "开启双Hook";
            this.Hook1_CheckBox.UseVisualStyleBackColor = true;
            // 
            // ImportFromHCode
            // 
            this.ImportFromHCode.Location = new System.Drawing.Point(204, 92);
            this.ImportFromHCode.Name = "ImportFromHCode";
            this.ImportFromHCode.Size = new System.Drawing.Size(98, 23);
            this.ImportFromHCode.TabIndex = 13;
            this.ImportFromHCode.Text = "从特殊码导入";
            this.ImportFromHCode.UseVisualStyleBackColor = true;
            this.ImportFromHCode.Click += new System.EventHandler(this.ImportFromHCode_Click);
            // 
            // Hook1ValueAsAddrBias_TextBox
            // 
            this.Hook1ValueAsAddrBias_TextBox.Location = new System.Drawing.Point(267, 322);
            this.Hook1ValueAsAddrBias_TextBox.Name = "Hook1ValueAsAddrBias_TextBox";
            this.Hook1ValueAsAddrBias_TextBox.Size = new System.Drawing.Size(35, 21);
            this.Hook1ValueAsAddrBias_TextBox.TabIndex = 17;
            // 
            // Hook1ValueAsAddr_CheckBox
            // 
            this.Hook1ValueAsAddr_CheckBox.AutoSize = true;
            this.Hook1ValueAsAddr_CheckBox.Location = new System.Drawing.Point(146, 324);
            this.Hook1ValueAsAddr_CheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.Hook1ValueAsAddr_CheckBox.Name = "Hook1ValueAsAddr_CheckBox";
            this.Hook1ValueAsAddr_CheckBox.Size = new System.Drawing.Size(120, 16);
            this.Hook1ValueAsAddr_CheckBox.TabIndex = 16;
            this.Hook1ValueAsAddr_CheckBox.Text = "值作为指针,偏移:";
            this.Hook1ValueAsAddr_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Hook1EspBias_ComboBox
            // 
            this.Hook1EspBias_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Hook1EspBias_ComboBox.FormattingEnabled = true;
            this.Hook1EspBias_ComboBox.Location = new System.Drawing.Point(47, 322);
            this.Hook1EspBias_ComboBox.Name = "Hook1EspBias_ComboBox";
            this.Hook1EspBias_ComboBox.Size = new System.Drawing.Size(74, 20);
            this.Hook1EspBias_ComboBox.TabIndex = 15;
            // 
            // Hook1Addr_TextBox
            // 
            this.Hook1Addr_TextBox.Location = new System.Drawing.Point(47, 295);
            this.Hook1Addr_TextBox.Name = "Hook1Addr_TextBox";
            this.Hook1Addr_TextBox.Size = new System.Drawing.Size(255, 21);
            this.Hook1Addr_TextBox.TabIndex = 14;
            // 
            // VNName_Label
            // 
            this.VNName_Label.AutoSize = true;
            this.VNName_Label.Location = new System.Drawing.Point(9, 15);
            this.VNName_Label.Margin = new System.Windows.Forms.Padding(0);
            this.VNName_Label.Name = "VNName_Label";
            this.VNName_Label.Size = new System.Drawing.Size(35, 12);
            this.VNName_Label.TabIndex = 18;
            this.VNName_Label.Text = "名称:";
            // 
            // ModuleName_Label
            // 
            this.ModuleName_Label.AutoSize = true;
            this.ModuleName_Label.Location = new System.Drawing.Point(9, 125);
            this.ModuleName_Label.Margin = new System.Windows.Forms.Padding(0);
            this.ModuleName_Label.Name = "ModuleName_Label";
            this.ModuleName_Label.Size = new System.Drawing.Size(35, 12);
            this.ModuleName_Label.TabIndex = 19;
            this.ModuleName_Label.Text = "模块:";
            // 
            // Hook0Addr_Label
            // 
            this.Hook0Addr_Label.AutoSize = true;
            this.Hook0Addr_Label.Location = new System.Drawing.Point(9, 182);
            this.Hook0Addr_Label.Margin = new System.Windows.Forms.Padding(0);
            this.Hook0Addr_Label.Name = "Hook0Addr_Label";
            this.Hook0Addr_Label.Size = new System.Drawing.Size(35, 12);
            this.Hook0Addr_Label.TabIndex = 20;
            this.Hook0Addr_Label.Text = "地址:";
            // 
            // Hook0EspBias_Label
            // 
            this.Hook0EspBias_Label.AutoSize = true;
            this.Hook0EspBias_Label.Location = new System.Drawing.Point(9, 209);
            this.Hook0EspBias_Label.Margin = new System.Windows.Forms.Padding(0);
            this.Hook0EspBias_Label.Name = "Hook0EspBias_Label";
            this.Hook0EspBias_Label.Size = new System.Drawing.Size(35, 12);
            this.Hook0EspBias_Label.TabIndex = 21;
            this.Hook0EspBias_Label.Text = "位于:";
            // 
            // Hook0BytesPerRead_Label
            // 
            this.Hook0BytesPerRead_Label.AutoSize = true;
            this.Hook0BytesPerRead_Label.Location = new System.Drawing.Point(9, 235);
            this.Hook0BytesPerRead_Label.Name = "Hook0BytesPerRead_Label";
            this.Hook0BytesPerRead_Label.Size = new System.Drawing.Size(83, 12);
            this.Hook0BytesPerRead_Label.TabIndex = 22;
            this.Hook0BytesPerRead_Label.Text = "单次读取字节:";
            // 
            // Hook0BytesPerRead_ComboBox
            // 
            this.Hook0BytesPerRead_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Hook0BytesPerRead_ComboBox.FormattingEnabled = true;
            this.Hook0BytesPerRead_ComboBox.Items.AddRange(new object[] {
            "1",
            "2"});
            this.Hook0BytesPerRead_ComboBox.Location = new System.Drawing.Point(98, 232);
            this.Hook0BytesPerRead_ComboBox.Name = "Hook0BytesPerRead_ComboBox";
            this.Hook0BytesPerRead_ComboBox.Size = new System.Drawing.Size(42, 20);
            this.Hook0BytesPerRead_ComboBox.TabIndex = 23;
            // 
            // Hook1Addr_Label
            // 
            this.Hook1Addr_Label.AutoSize = true;
            this.Hook1Addr_Label.Location = new System.Drawing.Point(9, 298);
            this.Hook1Addr_Label.Margin = new System.Windows.Forms.Padding(0);
            this.Hook1Addr_Label.Name = "Hook1Addr_Label";
            this.Hook1Addr_Label.Size = new System.Drawing.Size(35, 12);
            this.Hook1Addr_Label.TabIndex = 25;
            this.Hook1Addr_Label.Text = "地址:";
            // 
            // Hook1EspBias_Label
            // 
            this.Hook1EspBias_Label.AutoSize = true;
            this.Hook1EspBias_Label.Location = new System.Drawing.Point(9, 325);
            this.Hook1EspBias_Label.Margin = new System.Windows.Forms.Padding(0);
            this.Hook1EspBias_Label.Name = "Hook1EspBias_Label";
            this.Hook1EspBias_Label.Size = new System.Drawing.Size(35, 12);
            this.Hook1EspBias_Label.TabIndex = 26;
            this.Hook1EspBias_Label.Text = "位于:";
            // 
            // Hook1BytesPerRead_Label
            // 
            this.Hook1BytesPerRead_Label.AutoSize = true;
            this.Hook1BytesPerRead_Label.Location = new System.Drawing.Point(9, 351);
            this.Hook1BytesPerRead_Label.Margin = new System.Windows.Forms.Padding(0);
            this.Hook1BytesPerRead_Label.Name = "Hook1BytesPerRead_Label";
            this.Hook1BytesPerRead_Label.Size = new System.Drawing.Size(83, 12);
            this.Hook1BytesPerRead_Label.TabIndex = 28;
            this.Hook1BytesPerRead_Label.Text = "单次读取字节:";
            // 
            // Hook1BytesPerRead_ComboBox
            // 
            this.Hook1BytesPerRead_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Hook1BytesPerRead_ComboBox.FormattingEnabled = true;
            this.Hook1BytesPerRead_ComboBox.Items.AddRange(new object[] {
            "1",
            "2"});
            this.Hook1BytesPerRead_ComboBox.Location = new System.Drawing.Point(95, 348);
            this.Hook1BytesPerRead_ComboBox.Name = "Hook1BytesPerRead_ComboBox";
            this.Hook1BytesPerRead_ComboBox.Size = new System.Drawing.Size(45, 20);
            this.Hook1BytesPerRead_ComboBox.TabIndex = 29;
            // 
            // WordsFilter_Label
            // 
            this.WordsFilter_Label.AutoSize = true;
            this.WordsFilter_Label.Location = new System.Drawing.Point(9, 424);
            this.WordsFilter_Label.Margin = new System.Windows.Forms.Padding(0);
            this.WordsFilter_Label.Name = "WordsFilter_Label";
            this.WordsFilter_Label.Size = new System.Drawing.Size(167, 12);
            this.WordsFilter_Label.TabIndex = 30;
            this.WordsFilter_Label.Text = "词语过滤器(用英文逗号分隔):";
            // 
            // Language_Label
            // 
            this.Language_Label.AutoSize = true;
            this.Language_Label.Location = new System.Drawing.Point(45, 42);
            this.Language_Label.Margin = new System.Windows.Forms.Padding(0);
            this.Language_Label.Name = "Language_Label";
            this.Language_Label.Size = new System.Drawing.Size(35, 12);
            this.Language_Label.TabIndex = 31;
            this.Language_Label.Text = "语言:";
            // 
            // ProcEncoding_Label
            // 
            this.ProcEncoding_Label.AutoSize = true;
            this.ProcEncoding_Label.Location = new System.Drawing.Point(166, 42);
            this.ProcEncoding_Label.Margin = new System.Windows.Forms.Padding(0);
            this.ProcEncoding_Label.Name = "ProcEncoding_Label";
            this.ProcEncoding_Label.Size = new System.Drawing.Size(35, 12);
            this.ProcEncoding_Label.TabIndex = 32;
            this.ProcEncoding_Label.Text = "编码:";
            // 
            // VNSettingsForm
            // 
            this.AcceptButton = this.Accept_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(314, 501);
            this.Controls.Add(this.ProcEncoding_Label);
            this.Controls.Add(this.Language_Label);
            this.Controls.Add(this.WordsFilter_Label);
            this.Controls.Add(this.Hook1BytesPerRead_ComboBox);
            this.Controls.Add(this.Hook1BytesPerRead_Label);
            this.Controls.Add(this.Hook1EspBias_Label);
            this.Controls.Add(this.Hook1Addr_Label);
            this.Controls.Add(this.Hook0BytesPerRead_ComboBox);
            this.Controls.Add(this.Hook0BytesPerRead_Label);
            this.Controls.Add(this.Hook0EspBias_Label);
            this.Controls.Add(this.Hook0Addr_Label);
            this.Controls.Add(this.ModuleName_Label);
            this.Controls.Add(this.VNName_Label);
            this.Controls.Add(this.Hook1ValueAsAddrBias_TextBox);
            this.Controls.Add(this.Hook1ValueAsAddr_CheckBox);
            this.Controls.Add(this.Hook1EspBias_ComboBox);
            this.Controls.Add(this.Hook1Addr_TextBox);
            this.Controls.Add(this.ImportFromHCode);
            this.Controls.Add(this.Hook1_CheckBox);
            this.Controls.Add(this.Hook0ValueAsAddrBias_TextBox);
            this.Controls.Add(this.Hook0ValueAsAddr_CheckBox);
            this.Controls.Add(this.Hook0EspBias_ComboBox);
            this.Controls.Add(this.Hook0Addr_TextBox);
            this.Controls.Add(this.WordsFilter_TextBox);
            this.Controls.Add(this.Language_ComboBox);
            this.Controls.Add(this.ProcEncoding_ComboBox);
            this.Controls.Add(this.ModuleName_TextBox);
            this.Controls.Add(this.VNName_TextBox);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Accept_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "VNSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "提取器配置";
            this.Load += new System.EventHandler(this.VNSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Accept_Button;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.TextBox VNName_TextBox;
        private System.Windows.Forms.TextBox ModuleName_TextBox;
        private System.Windows.Forms.ComboBox ProcEncoding_ComboBox;
        private System.Windows.Forms.ComboBox Language_ComboBox;
        private System.Windows.Forms.TextBox WordsFilter_TextBox;
        private System.Windows.Forms.TextBox Hook0Addr_TextBox;
        private System.Windows.Forms.ComboBox Hook0EspBias_ComboBox;
        private System.Windows.Forms.CheckBox Hook0ValueAsAddr_CheckBox;
        private System.Windows.Forms.TextBox Hook0ValueAsAddrBias_TextBox;
        private System.Windows.Forms.CheckBox Hook1_CheckBox;
        private System.Windows.Forms.Button ImportFromHCode;
        private System.Windows.Forms.TextBox Hook1ValueAsAddrBias_TextBox;
        private System.Windows.Forms.CheckBox Hook1ValueAsAddr_CheckBox;
        private System.Windows.Forms.ComboBox Hook1EspBias_ComboBox;
        private System.Windows.Forms.TextBox Hook1Addr_TextBox;
        private System.Windows.Forms.Label VNName_Label;
        private System.Windows.Forms.Label ModuleName_Label;
        private System.Windows.Forms.Label Hook0Addr_Label;
        private System.Windows.Forms.Label Hook0EspBias_Label;
        private System.Windows.Forms.Label Hook0BytesPerRead_Label;
        private System.Windows.Forms.ComboBox Hook0BytesPerRead_ComboBox;
        private System.Windows.Forms.Label Hook1Addr_Label;
        private System.Windows.Forms.Label Hook1EspBias_Label;
        private System.Windows.Forms.Label Hook1BytesPerRead_Label;
        private System.Windows.Forms.ComboBox Hook1BytesPerRead_ComboBox;
        private System.Windows.Forms.Label WordsFilter_Label;
        private System.Windows.Forms.Label Language_Label;
        private System.Windows.Forms.Label ProcEncoding_Label;
    }
}