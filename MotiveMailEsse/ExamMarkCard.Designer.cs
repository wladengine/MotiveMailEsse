namespace MotiveMailEssay
{
    partial class ExamMarkCard
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbExamMark = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvAbitFiles = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lbPersonNumber = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.cbFaculty = new System.Windows.Forms.CheckBox();
            this.epErrorInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.cbEntry = new System.Windows.Forms.CheckBox();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbitFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrorInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(692, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Оценка";
            // 
            // tbExamMark
            // 
            this.tbExamMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExamMark.Location = new System.Drawing.Point(752, 12);
            this.tbExamMark.Name = "tbExamMark";
            this.tbExamMark.Size = new System.Drawing.Size(100, 20);
            this.tbExamMark.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(13, 499);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvAbitFiles
            // 
            this.dgvAbitFiles.AllowUserToAddRows = false;
            this.dgvAbitFiles.AllowUserToDeleteRows = false;
            this.dgvAbitFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAbitFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAbitFiles.Location = new System.Drawing.Point(12, 84);
            this.dgvAbitFiles.Name = "dgvAbitFiles";
            this.dgvAbitFiles.Size = new System.Drawing.Size(840, 323);
            this.dgvAbitFiles.TabIndex = 3;
            this.dgvAbitFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAbitFiles_CellClick);
            this.dgvAbitFiles.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAbitFiles_CellFormatting);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Рег. номер:";
            // 
            // lbPersonNumber
            // 
            this.lbPersonNumber.AutoSize = true;
            this.lbPersonNumber.Location = new System.Drawing.Point(85, 15);
            this.lbPersonNumber.Name = "lbPersonNumber";
            this.lbPersonNumber.Size = new System.Drawing.Size(13, 13);
            this.lbPersonNumber.TabIndex = 5;
            this.lbPersonNumber.Text = "0";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(738, 499);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(114, 23);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Следующая работа";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Location = new System.Drawing.Point(618, 499);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(114, 23);
            this.btnPrev.TabIndex = 7;
            this.btnPrev.Text = "Предыдущая работа";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // cbFaculty
            // 
            this.cbFaculty.AutoSize = true;
            this.cbFaculty.Location = new System.Drawing.Point(12, 38);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(213, 17);
            this.cbFaculty.TabIndex = 8;
            this.cbFaculty.Text = "Добавить сортировку по факультету";
            this.cbFaculty.UseVisualStyleBackColor = true;
            this.cbFaculty.CheckedChanged += new System.EventHandler(this.cbFaculty_CheckedChanged);
            // 
            // epErrorInput
            // 
            this.epErrorInput.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(483, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(369, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Для изменения статуса файла щёлкните на ячейку в столбце \"Статус\"";
            // 
            // cbEntry
            // 
            this.cbEntry.AutoSize = true;
            this.cbEntry.Location = new System.Drawing.Point(12, 61);
            this.cbEntry.Name = "cbEntry";
            this.cbEntry.Size = new System.Drawing.Size(211, 17);
            this.cbEntry.TabIndex = 10;
            this.cbEntry.Text = "Добавить сортировку по заявлению";
            this.cbEntry.UseVisualStyleBackColor = true;
            this.cbEntry.CheckedChanged += new System.EventHandler(this.cbEntry_CheckedChanged);
            // 
            // tbComment
            // 
            this.tbComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbComment.Location = new System.Drawing.Point(12, 435);
            this.tbComment.Multiline = true;
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(839, 58);
            this.tbComment.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 419);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Комментарий к работе:";
            // 
            // ExamMarkCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 534);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.cbEntry);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lbPersonNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAbitFiles);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbExamMark);
            this.Controls.Add(this.label1);
            this.Name = "ExamMarkCard";
            this.Text = "ExamMarkCard";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbitFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epErrorInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbExamMark;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvAbitFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbPersonNumber;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.CheckBox cbFaculty;
        private System.Windows.Forms.ErrorProvider epErrorInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbEntry;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label4;
    }
}