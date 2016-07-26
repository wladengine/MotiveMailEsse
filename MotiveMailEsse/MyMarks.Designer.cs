namespace MotiveMailEssay
{
    partial class MyMarks
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMotiv = new System.Windows.Forms.TabPage();
            this.dgvMotiv = new System.Windows.Forms.DataGridView();
            this.tabPageEssay = new System.Windows.Forms.TabPage();
            this.dgvEssay = new System.Windows.Forms.DataGridView();
            this.tabPagePortfolio = new System.Windows.Forms.TabPage();
            this.dgvPortfolio = new System.Windows.Forms.DataGridView();
            this.tabPagePhil = new System.Windows.Forms.TabPage();
            this.dgvPhil = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPageMotiv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotiv)).BeginInit();
            this.tabPageEssay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEssay)).BeginInit();
            this.tabPagePortfolio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortfolio)).BeginInit();
            this.tabPagePhil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhil)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageMotiv);
            this.tabControl1.Controls.Add(this.tabPageEssay);
            this.tabControl1.Controls.Add(this.tabPagePortfolio);
            this.tabControl1.Controls.Add(this.tabPagePhil);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1061, 497);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageMotiv
            // 
            this.tabPageMotiv.Controls.Add(this.dgvMotiv);
            this.tabPageMotiv.Location = new System.Drawing.Point(4, 22);
            this.tabPageMotiv.Name = "tabPageMotiv";
            this.tabPageMotiv.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotiv.Size = new System.Drawing.Size(1053, 471);
            this.tabPageMotiv.TabIndex = 0;
            this.tabPageMotiv.Text = "Мотивационные письма";
            this.tabPageMotiv.UseVisualStyleBackColor = true;
            // 
            // dgvMotiv
            // 
            this.dgvMotiv.AllowUserToAddRows = false;
            this.dgvMotiv.AllowUserToDeleteRows = false;
            this.dgvMotiv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMotiv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMotiv.Location = new System.Drawing.Point(6, 36);
            this.dgvMotiv.Name = "dgvMotiv";
            this.dgvMotiv.ReadOnly = true;
            this.dgvMotiv.Size = new System.Drawing.Size(1041, 429);
            this.dgvMotiv.TabIndex = 0;
            this.dgvMotiv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVedPersonList_CellClick);
            // 
            // tabPageEssay
            // 
            this.tabPageEssay.Controls.Add(this.dgvEssay);
            this.tabPageEssay.Location = new System.Drawing.Point(4, 22);
            this.tabPageEssay.Name = "tabPageEssay";
            this.tabPageEssay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEssay.Size = new System.Drawing.Size(1053, 471);
            this.tabPageEssay.TabIndex = 3;
            this.tabPageEssay.Text = "Эссе";
            this.tabPageEssay.UseVisualStyleBackColor = true;
            // 
            // dgvEssay
            // 
            this.dgvEssay.AllowUserToAddRows = false;
            this.dgvEssay.AllowUserToDeleteRows = false;
            this.dgvEssay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEssay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEssay.Location = new System.Drawing.Point(6, 36);
            this.dgvEssay.Name = "dgvEssay";
            this.dgvEssay.ReadOnly = true;
            this.dgvEssay.Size = new System.Drawing.Size(1041, 429);
            this.dgvEssay.TabIndex = 1;
            this.dgvEssay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVedPersonList_CellClick);
            // 
            // tabPagePortfolio
            // 
            this.tabPagePortfolio.Controls.Add(this.dgvPortfolio);
            this.tabPagePortfolio.Location = new System.Drawing.Point(4, 22);
            this.tabPagePortfolio.Name = "tabPagePortfolio";
            this.tabPagePortfolio.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePortfolio.Size = new System.Drawing.Size(1053, 471);
            this.tabPagePortfolio.TabIndex = 1;
            this.tabPagePortfolio.Text = "Портфолио";
            this.tabPagePortfolio.UseVisualStyleBackColor = true;
            // 
            // dgvPortfolio
            // 
            this.dgvPortfolio.AllowUserToAddRows = false;
            this.dgvPortfolio.AllowUserToDeleteRows = false;
            this.dgvPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPortfolio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPortfolio.Location = new System.Drawing.Point(6, 36);
            this.dgvPortfolio.Name = "dgvPortfolio";
            this.dgvPortfolio.ReadOnly = true;
            this.dgvPortfolio.Size = new System.Drawing.Size(1041, 429);
            this.dgvPortfolio.TabIndex = 1;
            this.dgvPortfolio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVedPersonList_CellClick);
            // 
            // tabPagePhil
            // 
            this.tabPagePhil.Controls.Add(this.dgvPhil);
            this.tabPagePhil.Location = new System.Drawing.Point(4, 22);
            this.tabPagePhil.Name = "tabPagePhil";
            this.tabPagePhil.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePhil.Size = new System.Drawing.Size(1053, 471);
            this.tabPagePhil.TabIndex = 2;
            this.tabPagePhil.Text = "Философия";
            this.tabPagePhil.UseVisualStyleBackColor = true;
            // 
            // dgvPhil
            // 
            this.dgvPhil.AllowUserToAddRows = false;
            this.dgvPhil.AllowUserToDeleteRows = false;
            this.dgvPhil.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPhil.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhil.Location = new System.Drawing.Point(6, 36);
            this.dgvPhil.Name = "dgvPhil";
            this.dgvPhil.ReadOnly = true;
            this.dgvPhil.Size = new System.Drawing.Size(1041, 429);
            this.dgvPhil.TabIndex = 1;
            this.dgvPhil.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVedPersonList_CellClick);
            // 
            // MyMarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 521);
            this.Controls.Add(this.tabControl1);
            this.Name = "MyMarks";
            this.Text = "Непроверенные работы";
            this.Load += new System.EventHandler(this.MyMarks_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageMotiv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotiv)).EndInit();
            this.tabPageEssay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEssay)).EndInit();
            this.tabPagePortfolio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPortfolio)).EndInit();
            this.tabPagePhil.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMotiv;
        private System.Windows.Forms.DataGridView dgvMotiv;
        private System.Windows.Forms.TabPage tabPagePortfolio;
        private System.Windows.Forms.TabPage tabPagePhil;
        private System.Windows.Forms.TabPage tabPageEssay;
        private System.Windows.Forms.DataGridView dgvEssay;
        private System.Windows.Forms.DataGridView dgvPortfolio;
        private System.Windows.Forms.DataGridView dgvPhil;

    }
}