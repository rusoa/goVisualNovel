﻿using System.Drawing;

namespace goVisualNovel
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TextPanel = new System.Windows.Forms.Panel();
            this.TranslationPanel = new System.Windows.Forms.Panel();
            this.DicPanel = new System.Windows.Forms.Panel();
            this.copy_dic = new System.Windows.Forms.Label();
            this.spliter_dic = new System.Windows.Forms.Label();
            this.word_dic = new System.Windows.Forms.Label();
            this.explanation_dic = new System.Windows.Forms.Label();
            this.property_dic = new System.Windows.Forms.Label();
            this.pronunciation_dic = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.DicPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextPanel
            // 
            this.TextPanel.BackColor = System.Drawing.Color.Transparent;
            this.TextPanel.Location = new System.Drawing.Point(0, 0);
            this.TextPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(0, 0);
            this.TextPanel.TabIndex = 2;
            // 
            // TranslationPanel
            // 
            this.TranslationPanel.BackColor = System.Drawing.Color.Transparent;
            this.TranslationPanel.Location = new System.Drawing.Point(0, 0);
            this.TranslationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TranslationPanel.Name = "TranslationPanel";
            this.TranslationPanel.Size = new System.Drawing.Size(0, 0);
            this.TranslationPanel.TabIndex = 3;
            // 
            // DicPanel
            // 
            this.DicPanel.AutoSize = true;
            this.DicPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DicPanel.BackColor = System.Drawing.Color.Black;
            this.DicPanel.Controls.Add(this.copy_dic);
            this.DicPanel.Controls.Add(this.spliter_dic);
            this.DicPanel.Controls.Add(this.word_dic);
            this.DicPanel.Controls.Add(this.explanation_dic);
            this.DicPanel.Controls.Add(this.property_dic);
            this.DicPanel.Controls.Add(this.pronunciation_dic);
            this.DicPanel.Location = new System.Drawing.Point(0, 0);
            this.DicPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DicPanel.MinimumSize = new System.Drawing.Size(250, 353);
            this.DicPanel.Name = "DicPanel";
            this.DicPanel.Size = new System.Drawing.Size(250, 353);
            this.DicPanel.TabIndex = 5;
            this.DicPanel.Visible = false;
            this.DicPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DicPanel_Paint);
            // 
            // copy_dic
            // 
            this.copy_dic.BackColor = System.Drawing.Color.Transparent;
            this.copy_dic.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Underline);
            this.copy_dic.ForeColor = System.Drawing.Color.DimGray;
            this.copy_dic.Location = new System.Drawing.Point(200, 32);
            this.copy_dic.Margin = new System.Windows.Forms.Padding(0);
            this.copy_dic.Name = "copy_dic";
            this.copy_dic.Size = new System.Drawing.Size(40, 16);
            this.copy_dic.TabIndex = 6;
            this.copy_dic.Text = "复制";
            this.copy_dic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.copy_dic.Click += new System.EventHandler(this.copy_dic_Click);
            this.copy_dic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.copy_dic_MouseDown);
            this.copy_dic.MouseEnter += new System.EventHandler(this.copy_dic_MouseEnter);
            this.copy_dic.MouseLeave += new System.EventHandler(this.copy_dic_MouseLeave);
            this.copy_dic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.copy_dic_MouseUp);
            // 
            // spliter_dic
            // 
            this.spliter_dic.BackColor = System.Drawing.Color.Transparent;
            this.spliter_dic.Font = new System.Drawing.Font("宋体", 10F);
            this.spliter_dic.ForeColor = System.Drawing.Color.DimGray;
            this.spliter_dic.Location = new System.Drawing.Point(10, 88);
            this.spliter_dic.Margin = new System.Windows.Forms.Padding(0);
            this.spliter_dic.Name = "spliter_dic";
            this.spliter_dic.Size = new System.Drawing.Size(230, 14);
            this.spliter_dic.TabIndex = 5;
            this.spliter_dic.Text = "_________________________________";
            this.spliter_dic.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // word_dic
            // 
            this.word_dic.BackColor = System.Drawing.Color.Transparent;
            this.word_dic.Font = new System.Drawing.Font("宋体", 22F);
            this.word_dic.ForeColor = System.Drawing.Color.White;
            this.word_dic.Location = new System.Drawing.Point(10, 25);
            this.word_dic.Margin = new System.Windows.Forms.Padding(0);
            this.word_dic.Name = "word_dic";
            this.word_dic.Size = new System.Drawing.Size(190, 30);
            this.word_dic.TabIndex = 4;
            this.word_dic.Text = "目覚める";
            this.word_dic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // explanation_dic
            // 
            this.explanation_dic.AutoSize = true;
            this.explanation_dic.BackColor = System.Drawing.Color.Transparent;
            this.explanation_dic.Font = new System.Drawing.Font("宋体", 10F);
            this.explanation_dic.ForeColor = System.Drawing.Color.White;
            this.explanation_dic.Location = new System.Drawing.Point(20, 124);
            this.explanation_dic.Margin = new System.Windows.Forms.Padding(0);
            this.explanation_dic.MaximumSize = new System.Drawing.Size(210, 0);
            this.explanation_dic.Name = "explanation_dic";
            this.explanation_dic.Size = new System.Drawing.Size(84, 14);
            this.explanation_dic.TabIndex = 3;
            this.explanation_dic.Text = "正在查询...";
            // 
            // property_dic
            // 
            this.property_dic.BackColor = System.Drawing.Color.Transparent;
            this.property_dic.Font = new System.Drawing.Font("宋体", 12F);
            this.property_dic.ForeColor = System.Drawing.Color.Silver;
            this.property_dic.Location = new System.Drawing.Point(180, 64);
            this.property_dic.Margin = new System.Windows.Forms.Padding(0);
            this.property_dic.Name = "property_dic";
            this.property_dic.Size = new System.Drawing.Size(60, 16);
            this.property_dic.TabIndex = 2;
            this.property_dic.Text = "動詞";
            this.property_dic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pronunciation_dic
            // 
            this.pronunciation_dic.BackColor = System.Drawing.Color.Transparent;
            this.pronunciation_dic.Font = new System.Drawing.Font("宋体", 12F);
            this.pronunciation_dic.ForeColor = System.Drawing.Color.Chartreuse;
            this.pronunciation_dic.Location = new System.Drawing.Point(10, 64);
            this.pronunciation_dic.Margin = new System.Windows.Forms.Padding(0);
            this.pronunciation_dic.Name = "pronunciation_dic";
            this.pronunciation_dic.Size = new System.Drawing.Size(170, 16);
            this.pronunciation_dic.TabIndex = 1;
            this.pronunciation_dic.Text = "「めざめる」";
            this.pronunciation_dic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.Black;
            this.StatusLabel.Font = new System.Drawing.Font("宋体", 24F);
            this.StatusLabel.ForeColor = System.Drawing.Color.White;
            this.StatusLabel.Location = new System.Drawing.Point(450, 6);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(111, 43);
            this.StatusLabel.TabIndex = 6;
            this.StatusLabel.Text = "状态栏";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.DicPanel);
            this.Controls.Add(this.TranslationPanel);
            this.Controls.Add(this.TextPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DicPanel.ResumeLayout(false);
            this.DicPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel TextPanel;
        private System.Windows.Forms.Panel TranslationPanel;
        private System.Windows.Forms.Panel DicPanel;
        private System.Windows.Forms.Label pronunciation_dic;
        private System.Windows.Forms.Label explanation_dic;
        private System.Windows.Forms.Label property_dic;
        private System.Windows.Forms.Label word_dic;
        private System.Windows.Forms.Label spliter_dic;
        private System.Windows.Forms.Label copy_dic;
        public System.Windows.Forms.Label StatusLabel;
    }
}
