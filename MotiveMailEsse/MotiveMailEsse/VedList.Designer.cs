namespace MotiveMailEssay
{
    partial class VedList
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
            this.dgvVedList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.cbStudyBasis = new System.Windows.Forms.ComboBox();
            this.cbExam = new System.Windows.Forms.ComboBox();
            this.btnVedOpen = new System.Windows.Forms.Button();
            this.cbStudyLevel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVedList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVedList
            // 
            this.dgvVedList.AllowUserToAddRows = false;
            this.dgvVedList.AllowUserToDeleteRows = false;
            this.dgvVedList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVedList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVedList.Location = new System.Drawing.Point(12, 123);
            this.dgvVedList.Name = "dgvVedList";
            this.dgvVedList.ReadOnly = true;
            this.dgvVedList.RowHeadersVisible = false;
            this.dgvVedList.Size = new System.Drawing.Size(780, 320);
            this.dgvVedList.TabIndex = 0;
            this.dgvVedList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVedList_CellDoubleClick);
            this.dgvVedList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvVedList_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Факультет";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Основа обучения";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Экзамен";
            // 
            // cbFaculty
            // 
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Location = new System.Drawing.Point(117, 9);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(304, 21);
            this.cbFaculty.TabIndex = 4;
            // 
            // cbStudyBasis
            // 
            this.cbStudyBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyBasis.FormattingEnabled = true;
            this.cbStudyBasis.Location = new System.Drawing.Point(117, 63);
            this.cbStudyBasis.Name = "cbStudyBasis";
            this.cbStudyBasis.Size = new System.Drawing.Size(304, 21);
            this.cbStudyBasis.TabIndex = 5;
            // 
            // cbExam
            // 
            this.cbExam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExam.FormattingEnabled = true;
            this.cbExam.Location = new System.Drawing.Point(117, 94);
            this.cbExam.Name = "cbExam";
            this.cbExam.Size = new System.Drawing.Size(304, 21);
            this.cbExam.TabIndex = 6;
            // 
            // btnVedOpen
            // 
            this.btnVedOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVedOpen.Location = new System.Drawing.Point(13, 457);
            this.btnVedOpen.Name = "btnVedOpen";
            this.btnVedOpen.Size = new System.Drawing.Size(98, 23);
            this.btnVedOpen.TabIndex = 7;
            this.btnVedOpen.Text = "Открыть";
            this.btnVedOpen.UseVisualStyleBackColor = true;
            this.btnVedOpen.Click += new System.EventHandler(this.btnVedOpen_Click);
            // 
            // cbStudyLevel
            // 
            this.cbStudyLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyLevel.FormattingEnabled = true;
            this.cbStudyLevel.Location = new System.Drawing.Point(117, 36);
            this.cbStudyLevel.Name = "cbStudyLevel";
            this.cbStudyLevel.Size = new System.Drawing.Size(304, 21);
            this.cbStudyLevel.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Уровень";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 492);
            this.Controls.Add(this.cbStudyLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnVedOpen);
            this.Controls.Add(this.cbExam);
            this.Controls.Add(this.cbStudyBasis);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvVedList);
            this.Name = "VedList";
            this.Text = "VedList";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVedList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVedList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.ComboBox cbStudyBasis;
        private System.Windows.Forms.ComboBox cbExam;
        private System.Windows.Forms.Button btnVedOpen;
        private System.Windows.Forms.ComboBox cbStudyLevel;
        private System.Windows.Forms.Label label4;
    }
}