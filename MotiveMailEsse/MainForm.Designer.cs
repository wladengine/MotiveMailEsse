﻿namespace MotiveMailEssay
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.listsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDics = new System.Windows.Forms.ToolStripMenuItem();
            this.entryListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.работыОжидающиеОценкиМоиОценкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listsToolStripMenuItem,
            this.smiDics,
            this.работыОжидающиеОценкиМоиОценкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(667, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // listsToolStripMenuItem
            // 
            this.listsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainListToolStripMenuItem});
            this.listsToolStripMenuItem.Name = "listsToolStripMenuItem";
            this.listsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.listsToolStripMenuItem.Text = "Списки";
            // 
            // mainListToolStripMenuItem
            // 
            this.mainListToolStripMenuItem.Name = "mainListToolStripMenuItem";
            this.mainListToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.mainListToolStripMenuItem.Text = "Список ведомостей";
            this.mainListToolStripMenuItem.Click += new System.EventHandler(this.mainListToolStripMenuItem_Click);
            // 
            // smiDics
            // 
            this.smiDics.Name = "smiDics";
            this.smiDics.Size = new System.Drawing.Size(12, 20);
            // 
            // entryListToolStripMenuItem
            // 
            this.entryListToolStripMenuItem.Name = "entryListToolStripMenuItem";
            this.entryListToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // работыОжидающиеОценкиМоиОценкиToolStripMenuItem
            // 
            this.работыОжидающиеОценкиМоиОценкиToolStripMenuItem.Name = "работыОжидающиеОценкиМоиОценкиToolStripMenuItem";
            this.работыОжидающиеОценкиМоиОценкиToolStripMenuItem.Size = new System.Drawing.Size(257, 20);
            this.работыОжидающиеОценкиМоиОценкиToolStripMenuItem.Text = "Работы, ожидающие оценки (Мои оценки)";
            this.работыОжидающиеОценкиМоиОценкиToolStripMenuItem.Click += new System.EventHandler(this.работыОжидающиеОценкиМоиОценкиToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(667, 475);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проверка мотивационных писем и эссе";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem listsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiDics;
        private System.Windows.Forms.ToolStripMenuItem entryListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem работыОжидающиеОценкиМоиОценкиToolStripMenuItem;
    }
}