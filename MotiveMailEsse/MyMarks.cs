using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotiveMailEssay
{
    public partial class MyMarks : Form
    {
        public MyMarks()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm; 
        }
        public void FillDataGrid()
        {
            FillDataGrid(dgvMotiv);
            FillDataGrid(dgvEssay);
            FillDataGrid(dgvPortfolio);
            FillDataGrid(dgvPhil);
        }
        public void FillDataGrid(DataGridView dgv)
        {
            int Type = 0;
            if (dgv == dgvMotiv) Type = 3;
            else if (dgv == dgvEssay) Type = 4;
            else if (dgv == dgvPortfolio) Type = 5;
            else if (dgv == dgvPhil) Type = 6;

            string query = @" select  
extExamsVed.Id as ExamsVedId
, extExamsVed.Number as 'Номер ведомости'
, extExamsVed.Date as 'Дата экзамена'
, extExamsVed.ExamName as 'Экзамен'
, History.PersonId as PersonId
, case when (extExamsVed.isLoad = 1) then convert(nvarchar(100),extPerson.FIO) else PersonVedNumber end as 'Абитуриент'
, Details.MarkValue as 'Оценка'
, convert(bit, (case when (select convert(bit, case when (IsMain=1) then 1 else 0 end) from ed.ExaminerInExamsVed where extExamsVed.Id = ExaminerInExamsVed.ExamsVedId and ExaminerInExamsVed.ExaminerAccount like '%" + Util.GetUserNameRectorat() + @"%') 
is null then 0 else 1 end))
as IsMain
from ed.extExamsVed
join ed.ExamsVedHistory History on History.ExamsVedId = extExamsVed.Id
join ed.ExamsVedHistoryMark Mark on Mark.ExamsVedHistoryId = History.Id
join ed.ExamsVedMarkDetails Details on Details.ExamsVedHistoryMarkId = Mark.Id
join ed.extPerson on ExtPerson.Id = History.PersonId
where 
(Mark.MarkIsChecked = 0 or Mark.MarkIsChecked is null) and 
Details.MarkValue is null and Details.ExaminerName like '%" + Util.GetUserNameRectorat() + @"%' and Mark.ExamsVedMarkTypeId = " + Type.ToString() + @" 
";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            while (dgv.Columns.Count > 0)
            {
                dgv.Columns.Remove(dgv.Columns[0]);
            }
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;

            dgv.DataSource = tbl;

            if (dgv.Columns.Contains("Экзамен"))
                dgv.Columns["Экзамен"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dgv.Columns.Contains("Абитуриент"))
                dgv.Columns["Абитуриент"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv.Columns["ExamsVedId"].Visible = false;
            dgv.Columns["PersonId"].Visible = false;
            dgv.Columns["IsMain"].Visible = false;


            int newcolindex = 6;
            if (Type == 3)
            {
                DataGridViewButtonColumn dgvColMail = new DataGridViewButtonColumn();
                dgvColMail.HeaderText = "Мотивационное письмо";
                dgvColMail.Name = "MotiveMail";
                dgvColMail.Text = "Просмотр";
                dgvColMail.UseColumnTextForButtonValue = true;
                dgv.Columns.Insert(newcolindex, dgvColMail);
            }
            else if (Type == 5)
            {
                DataGridViewButtonColumn dgvColMail = new DataGridViewButtonColumn();
                dgvColMail.HeaderText = "Оценка"; 
                dgvColMail.Name = "Portfolio";
                dgvColMail.Text = "Просмотр";
                dgvColMail.UseColumnTextForButtonValue = true;
                dgv.Columns.Insert(newcolindex, dgvColMail);
            }
            else if (Type == 6)
            {
                DataGridViewButtonColumn dgvColMail = new DataGridViewButtonColumn();
                dgvColMail.HeaderText = "Оценка";
                dgvColMail.Name = "PhilosophyEssay";
                dgvColMail.Text = "Просмотр";
                dgvColMail.UseColumnTextForButtonValue = true;
                dgv.Columns.Insert(newcolindex, dgvColMail);
            }
            else if (Type == 4)
            {
                DataGridViewButtonColumn dgvColEssay = new DataGridViewButtonColumn();
                dgvColEssay.Name = "Essay";
                dgvColEssay.HeaderText = "Эссе";
                dgvColEssay.Text = "Просмотр";
                dgvColEssay.UseColumnTextForButtonValue = true;
                dgv.Columns.Insert(newcolindex, dgvColEssay);
            } 
        }

        private void MyMarks_Load(object sender, EventArgs e)
        {
            FillDataGrid();

            if (dgvMotiv.Rows.Count > 0)
                tabControl1.SelectedTab = tabPageMotiv;
            else if (dgvEssay.Rows.Count > 0)
                tabControl1.SelectedTab = tabPageEssay;
            else if (dgvPortfolio.Rows.Count > 0)
                tabControl1.SelectedTab = tabPagePortfolio;
            else if (dgvPhil.Rows.Count > 0)
                tabControl1.SelectedTab = tabPagePhil;
        }

        private void dgvVedPersonList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender == dgvMotiv)
            {
                if (e.RowIndex < 0 || (e.ColumnIndex != dgvMotiv.Columns["MotiveMail"].Index ))
                    return;
                else
                {
                    int row = this.dgvMotiv.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvMotiv.Rows[row].Cells["PersonId"].Value;
                    Guid _VedId = (Guid)dgvMotiv.Rows[row].Cells["ExamsVedId"].Value;
                    bool isMain = (bool)dgvMotiv.Rows[row].Cells["IsMain"].Value;
                    CardType ctype = CardType.Uknown;
                    if (e.ColumnIndex == dgvMotiv.Columns["MotiveMail"].Index)
                        ctype = CardType.Motivation;
                    Util.OpenExamMarkCard(this, id, _VedId, ctype, row, isMain, new UpdateHandler(FillDataGrid), null, null);
                }
            }
            else if (sender == dgvEssay)
            {
                if (e.RowIndex < 0 || (e.ColumnIndex != dgvEssay.Columns["Essay"].Index))
                    return;
                else
                {
                    int row = this.dgvEssay.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvEssay.Rows[row].Cells["PersonId"].Value;
                    Guid _VedId = (Guid)dgvEssay.Rows[row].Cells["ExamsVedId"].Value;
                    bool isMain = (bool)dgvEssay.Rows[row].Cells["IsMain"].Value;
                    CardType ctype = CardType.Uknown;
                    if (e.ColumnIndex == dgvEssay.Columns["Essay"].Index)
                        ctype = CardType.Essay;
                    Util.OpenExamMarkCard(this, id, _VedId, ctype, row, isMain, new UpdateHandler(FillDataGrid), null,null);
                }
            }

            else if (sender == dgvPortfolio)
            {
                if (e.RowIndex < 0 || (e.ColumnIndex != dgvPortfolio.Columns["Portfolio"].Index))
                    return;
                else
                {
                    int row = this.dgvPortfolio.CurrentCell.RowIndex;
                    Guid _VedId = (Guid)dgvPortfolio.Rows[row].Cells["ExamsVedId"].Value;
                    Guid id = (Guid)dgvPortfolio.Rows[row].Cells["PersonId"].Value;
                    CardType type = CardType.Uknown;
                    if (e.ColumnIndex == dgvPortfolio.Columns["Portfolio"].Index)
                        type = CardType.Portfolio;
                    bool isMain = (bool)dgvPortfolio.Rows[row].Cells["IsMain"].Value;
                    Util.OpenExamMarkCard(this, id, _VedId, type, row, isMain, new UpdateHandler(FillDataGrid), null, null );
                }
            }
            else if (sender == dgvPhil)
            {
                if (e.RowIndex < 0 || (e.ColumnIndex != dgvPhil.Columns["PhilosophyEssay"].Index))
                    return;
                else
                {
                    int row = this.dgvPhil.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvPhil.Rows[row].Cells["PersonId"].Value;
                    CardType type = CardType.Uknown;
                    if (e.ColumnIndex == dgvPhil.Columns["PhilosophyEssay"].Index)
                        type = CardType.PhilosophyEssay;
                    Guid _VedId = (Guid)dgvPhil.Rows[row].Cells["ExamsVedId"].Value; 
                    bool isMain = (bool)dgvPhil.Rows[row].Cells["IsMain"].Value;
                    Util.OpenExamMarkCard(this, id, _VedId, type, row, isMain, new UpdateHandler(FillDataGrid), null, null);
                }
            }
        }
    }
}
