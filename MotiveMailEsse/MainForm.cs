using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotiveMailEssay
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Util.MainForm = this;
            Start();
        }

        void Start()
        {
            Util.OpenVedList();
        }

        protected override void OnClosed(EventArgs e)
        {
            Util.ClearTempFolder();
            base.OnClosed(e);
        }

        private void mainListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.OpenVedList();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string query = @"Select * from 
ed.ExamsVedHistoryMark Mark
join ed.ExamsVedMarkDetails Details on Mark.Id = Details.ExamsVedHistoryMarkId
where Details.ExaminerName like '%" + Util.GetUserNameRectorat() + "%' and Details.MarkValue is NULL and (Mark.MarkIsChecked = 0 or Mark.MarkIsChecked is null) ";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>());
            if (tbl.Rows.Count >0)
            {
                DialogResult dlg = MessageBox.Show("Найдено несколько непроверенных работ, где ожидается внесение оценок. Открыть список работ?", "Найдены непроверенные работы", MessageBoxButtons.YesNoCancel);
                if (dlg == DialogResult.Yes)
                {
                    new MyMarks().Show();
                    e.Cancel = true;
                }
                else if (dlg == System.Windows.Forms.DialogResult.No)
                {

                }
                else if (dlg == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                } 
            }  
        }

        private void работыОжидающиеОценкиМоиОценкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = @"Select * from 
ed.ExamsVedHistoryMark Mark
join ed.ExamsVedMarkDetails Details on Mark.Id = Details.ExamsVedHistoryMarkId
where Details.ExaminerName like '%" + Util.GetUserNameRectorat() + "%' and Details.MarkValue is NULL and (Mark.MarkIsChecked = 0 or Mark.MarkIsChecked is null) ";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>());
            if (tbl.Rows.Count > 0)
            {
                    new MyMarks().Show();
            }  
            else
            {
                MessageBox.Show("Работ, ожидающих введение оценки нет");
            }
        }
    }
}

