namespace MotiveMailEssay
{
    partial class VedCard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVedNum = new System.Windows.Forms.TextBox();
            this.tbExamDate = new System.Windows.Forms.TextBox();
            this.tbExamName = new System.Windows.Forms.TextBox();
            this.dgvVedPersonList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVedPersonList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ведомость №";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Экзамен";
            // 
            // tbVedNum
            // 
            this.tbVedNum.Location = new System.Drawing.Point(117, 6);
            this.tbVedNum.Name = "tbVedNum";
            this.tbVedNum.ReadOnly = true;
            this.tbVedNum.Size = new System.Drawing.Size(99, 20);
            this.tbVedNum.TabIndex = 3;
            // 
            // tbExamDate
            // 
            this.tbExamDate.Location = new System.Drawing.Point(117, 36);
            this.tbExamDate.Name = "tbExamDate";
            this.tbExamDate.ReadOnly = true;
            this.tbExamDate.Size = new System.Drawing.Size(99, 20);
            this.tbExamDate.TabIndex = 4;
            // 
            // tbExamName
            // 
            this.tbExamName.Location = new System.Drawing.Point(117, 65);
            this.tbExamName.Name = "tbExamName";
            this.tbExamName.ReadOnly = true;
            this.tbExamName.Size = new System.Drawing.Size(322, 20);
            this.tbExamName.TabIndex = 5;
            // 
            // dgvVedPersonList
            // 
            this.dgvVedPersonList.AllowUserToAddRows = false;
            this.dgvVedPersonList.AllowUserToDeleteRows = false;
            this.dgvVedPersonList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVedPersonList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVedPersonList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVedPersonList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVedPersonList.Location = new System.Drawing.Point(12, 101);
            this.dgvVedPersonList.Name = "dgvVedPersonList";
            this.dgvVedPersonList.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.dgvVedPersonList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVedPersonList.Size = new System.Drawing.Size(637, 336);
            this.dgvVedPersonList.TabIndex = 6;
            this.dgvVedPersonList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVedPersonList_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(574, 443);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // VedCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 473);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvVedPersonList);
            this.Controls.Add(this.tbExamName);
            this.Controls.Add(this.tbExamDate);
            this.Controls.Add(this.tbVedNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "VedCard";
            this.Text = "VedCard";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVedPersonList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVedNum;
        private System.Windows.Forms.TextBox tbExamDate;
        private System.Windows.Forms.TextBox tbExamName;
        private System.Windows.Forms.DataGridView dgvVedPersonList;
        private System.Windows.Forms.Button btnClose;
    }
}