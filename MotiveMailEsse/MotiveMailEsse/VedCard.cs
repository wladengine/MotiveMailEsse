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
    public delegate void UpdateHandler();
    public delegate string OpenHandler (ref int row);
    public partial class VedCard : Form
    {
        public UpdateHandler _handler;
        
        private Guid _VedId;

        public VedCard(Guid id)
        {
            InitializeComponent();
            _VedId = id;
            FillCombos();
            FillGrid();
            DgvAddColumns();
        }

        private void FillCombos()
        {
            this.Text = "Ведомость";
            string query = @"SELECT ExamsVed.[Id], [Number], [StudyLevelGroupId]
                                  ,[FacultyId], [StudyBasisId], [Date]
                                  ,[ExamId], ExamName.Name as ExamName, [IsLocked], [IsLoad]
                                  ,[IsAdd],[AddCount]
                              FROM ed.ExamsVed INNER JOIN ed.Exam on Exam.Id = ExamsVed.ExamId INNER JOIN ed.ExamName ON ExamName.Id = Exam.ExamNameId WHERE ExamsVed.Id = @Id";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@Id", _VedId);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;

            DataRow r = tbl.Rows[0];
            tbVedNum.Text = r.Field<int>("Number").ToString();
            tbExamDate.Text = r.Field<DateTime>("Date").ToString();
            tbExamName.Text = r.Field<String>("ExamName");
        }
        private void DgvAddColumns()
        {
            DataGridViewButtonColumn dgvColMail = new DataGridViewButtonColumn();
            dgvColMail.HeaderText = "Мотивационное письмо";
            dgvColMail.Name = "MotiveMail";
            dgvColMail.Text = "Просмотр";
            dgvColMail.UseColumnTextForButtonValue = true;
            dgvVedPersonList.Columns.Insert(3, dgvColMail);
            DataGridViewButtonColumn dgvColEssay = new DataGridViewButtonColumn();
            dgvColEssay.Name = "Essay";
            dgvColEssay.HeaderText = "Эссе";
            dgvColEssay.Text = "Просмотр";
            dgvColEssay.UseColumnTextForButtonValue = true;
            dgvVedPersonList.Columns.Insert(5, dgvColEssay);  
        }

        private void FillGrid()
        {
            string query = @"SELECT [ExamsVedId]
                                    ,[PersonId] , [PersonVedNumber] as 'Рег.номер'
                                    , Mark as 'Оценка за м.письмо' , OralMark as 'Оценка за эссе'
                               FROM [ed].[ExamsVedHistory]
                               WHERE ExamsVedId = @Id";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@Id", _VedId);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            dgvVedPersonList.DataSource = tbl;
            dgvVedPersonList.Columns["ExamsVedId"].Visible = false;
            dgvVedPersonList.Columns["PersonId"].Visible = false;
            
        }
 
        private void dgvVedPersonList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != dgvVedPersonList.Columns["MotiveMail"].Index && e.ColumnIndex != dgvVedPersonList.Columns["Essay"].Index))
                return;
            else
            {
                int row = this.dgvVedPersonList.CurrentCell.RowIndex;
                Guid id = (Guid)dgvVedPersonList.Rows[row].Cells["PersonId"].Value;
                int type = 0;
                if (e.ColumnIndex == dgvVedPersonList.Columns["MotiveMail"].Index)
                    type = 2;
                if (e.ColumnIndex == dgvVedPersonList.Columns["Essay"].Index)
                    type = 3;
                Util.OpenExamMarkCard(this, id, _VedId, type, row, new UpdateHandler(FillGrid), new OpenHandler(OpenNextCard), new OpenHandler(OpenPrevCard));
            }
        }

        private string OpenNextCard(ref int rowIndex)
        {
            try
            {
                if (rowIndex > -1 && rowIndex < dgvVedPersonList.RowCount - 1)
                    rowIndex++;
                else
                    rowIndex = 0;
                Guid id = (Guid)dgvVedPersonList.Rows[rowIndex].Cells["PersonId"].Value;
                return id.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private string OpenPrevCard(ref int rowIndex)
        {
            try
            {
                if (rowIndex > 0 && rowIndex < dgvVedPersonList.RowCount)
                    rowIndex--;
                else
                    rowIndex = dgvVedPersonList.RowCount - 1;
                Guid id = (Guid)dgvVedPersonList.Rows[rowIndex].Cells["PersonId"].Value;
                return id.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
