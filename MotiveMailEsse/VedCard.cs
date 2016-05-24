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

        private bool isLoad;
        private Guid _VedId;
        private bool IsPortfolioAnonymPartMotivLetter;
        private bool IsPortfolioAnonymPartEssay;
        private bool IsPortfolioCommonPart;

        public VedCard(Guid id)
        {
            InitializeComponent();
            _VedId = id;
            FillCombos();
            FillGrid();
        }

        private void FillCombos()
        {
            this.Text = "Ведомость";
            string query = @"SELECT extExamsVed.Id
                                , extExamsVed.Number
                                , extExamsVed.StudyLevelGroupId
                                , extExamsVed.FacultyId
                                , extExamsVed.StudyBasisId
                                , extExamsVed.Date
                                , extExamsVed.ExamId
                                , extExamsVed.ExamName as ExamName
                                , extExamsVed.IsLocked 
                                , extExamsVed.IsLoad 
                                , case when (EXISTS (select * from ed.ExamsVedSelectedMarkType ST
                                where ST.ExamsVedId = extExamsVed.Id and MarkTypeId = 3)) then 1 else 0 end as IsPortfolioAnonymPartMotivLetter
                                , case when (EXISTS (select * from ed.ExamsVedSelectedMarkType ST
                                where ST.ExamsVedId = extExamsVed.Id and MarkTypeId = 4)) then 1 else 0 end as IsPortfolioAnonymPartEssay
                                , case when (EXISTS (select * from ed.ExamsVedSelectedMarkType ST
                                where ST.ExamsVedId = extExamsVed.Id and MarkTypeId = 5)) then 1 else 0 end as IsPortfolioCommonPart  
                                  FROM ed.extExamsVed  
                                  join ed.Exam on extExamsVed.ExamId = Exam.Id
                                  WHERE extExamsVed.Id = @Id";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@Id", _VedId);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;

            DataRow r = tbl.Rows[0];
            isLoad = r.Field<bool>("IsLoad");
            tbVedNum.Text = r.Field<int>("Number").ToString();
            tbExamDate.Text = r.Field<DateTime>("Date").ToString();
            tbExamName.Text = r.Field<String>("ExamName");
            IsPortfolioCommonPart = r.Field<int>("IsPortfolioCommonPart")==1;

            IsPortfolioAnonymPartMotivLetter = r.Field<int>("IsPortfolioAnonymPartMotivLetter")==1;
            IsPortfolioAnonymPartEssay = r.Field<int>("IsPortfolioAnonymPartEssay")==1;


        }
        private void DgvAddColumns()
        {
            if (IsPortfolioCommonPart)
            {
                DataGridViewButtonColumn dgvColMail = new DataGridViewButtonColumn();
                dgvColMail.HeaderText = "Оценка";
                dgvColMail.Name = "Portfolio";
                dgvColMail.Text = "Просмотр";
                dgvColMail.UseColumnTextForButtonValue = true;
                dgvVedPersonList.Columns.Insert(3, dgvColMail);
            }
            else
            {
                if (IsPortfolioAnonymPartMotivLetter)
                {
                    DataGridViewButtonColumn dgvColMail = new DataGridViewButtonColumn();
                    dgvColMail.HeaderText = "Мотивационное письмо";
                    dgvColMail.Name = "MotiveMail";
                    dgvColMail.Text = "Просмотр";
                    dgvColMail.UseColumnTextForButtonValue = true;
                    dgvVedPersonList.Columns.Insert(3, dgvColMail);
                }
                if (IsPortfolioAnonymPartEssay)
                {
                    DataGridViewButtonColumn dgvColEssay = new DataGridViewButtonColumn();
                    dgvColEssay.Name = "Essay";
                    dgvColEssay.HeaderText = "Эссе";
                    dgvColEssay.Text = "Просмотр";
                    dgvColEssay.UseColumnTextForButtonValue = true;
                    dgvVedPersonList.Columns.Insert(5, dgvColEssay);
                }
            }
        }           

        private void FillGrid()
        {
            if (IsPortfolioAnonymPartMotivLetter || IsPortfolioAnonymPartEssay)
            {
                string query = @"SELECT [ExamsVedId]
                                    , [PersonId] 
                                    , "+(isLoad ? "FIO as 'Фамилия'":"[PersonVedNumber] as 'Рег.номер'")
                               +@", Mark as 'Оценка за м.письмо' , OralMark as 'Оценка за эссе'
                               FROM [ed].[ExamsVedHistory]
                               join ed.extPerson on extPerson.Id = PersonId
                               WHERE ExamsVedId = @Id";
               query = @"
SELECT ExamsVedId
, PersonId
 , " + (isLoad ? "FIO as 'Фамилия'":"[PersonVedNumber] as 'Рег.номер'")+ @"
"+ (IsPortfolioAnonymPartMotivLetter ? @" , Details.MarkValue as 'Оценка за м.письмо' " : "" )
 + (IsPortfolioAnonymPartEssay ? @", Details2.MarkValue as 'Оценка за эссе' ":"") +@"
FROM ed.ExamsVedHistory History
join ed.extPerson on extPerson.Id = History.PersonId "

+ (IsPortfolioAnonymPartMotivLetter ? (@"
left join ed.ExamsVedHistoryMark Marks on History.Id = Marks.ExamsVedHistoryId and Marks.ExamsVedMarkTypeId=3
left join ed.ExamsVedMarkDetails Details on Details.ExamsVedHistoryMarkId = Marks.Id and Details.ExaminerName like '%"+Util.GetUserNameRectorat()+@"%' " ) : "") 

+ (IsPortfolioAnonymPartMotivLetter ? (@"
left join ed.ExamsVedHistoryMark Marks2 on History.Id = Marks2.ExamsVedHistoryId and Marks2.ExamsVedMarkTypeId=4
left join ed.ExamsVedMarkDetails Details2 on Details2.ExamsVedHistoryMarkId = Marks2.Id and Details2.ExaminerName like '%" + Util.GetUserNameRectorat() + @"%'" ) : "")

+@"WHERE ExamsVedId = @Id" ;
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.AddVal("@Id", _VedId);
                while (dgvVedPersonList.Columns.Count > 0)
                {
                    dgvVedPersonList.Columns.Remove(dgvVedPersonList.Columns[0]);
                }
                DataTable tbl = Util.BDC.GetDataTable(query, dic);
                dgvVedPersonList.DataSource = tbl;
                if (dgvVedPersonList.Columns.Contains("Фамилия"))
                {
                    dgvVedPersonList.Columns["Фамилия"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvVedPersonList.Columns.Contains("Рег.номер"))
                {
                    dgvVedPersonList.Columns["Рег.номер"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvVedPersonList.Columns["ExamsVedId"].Visible = false;
                dgvVedPersonList.Columns["PersonId"].Visible = false;
            }
            else if (IsPortfolioCommonPart)
            {
//                string query = @"SELECT [ExamsVedId]
//                               , Person.Id as PersonId
//                               , Person.Surname + ' ' + Person.Name + ' '+Person.SecondName as 'ФИО'
//                               --, [PersonVedNumber] as 'Рег.номер'
//                               , Mark as 'Оценка за портфолио'  
//                               FROM [ed].[ExamsVedHistory]
//                               join ed.Person on Person.Id = ExamsVedHistory.PersonId
//                               WHERE ExamsVedId = @Id";
                string query = @"
SELECT ExamsVedId, PersonId, FIO as 'Фамилия', 
Details.MarkValue as 'Оценка за портфолио' 
FROM ed.ExamsVedHistory History 
join ed.extPerson on extPerson.Id = History.PersonId
left join ed.ExamsVedHistoryMark Marks on History.Id = Marks.ExamsVedHistoryId and Marks.ExamsVedMarkTypeId=5
left join ed.ExamsVedMarkDetails Details on Details.ExamsVedHistoryMarkId = Marks.Id and Details.ExaminerName like '%" + Util.GetUserNameRectorat() + @"%' "
+ @"WHERE ExamsVedId = @Id";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.AddVal("@Id", _VedId);
                DataTable tbl = Util.BDC.GetDataTable(query, dic);
                while (dgvVedPersonList.Columns.Count > 0)
                {
                    dgvVedPersonList.Columns.Remove(dgvVedPersonList.Columns[0]);
                }
                dgvVedPersonList.DataSource = tbl;
                if (dgvVedPersonList.Columns.Contains("ФИО"))
                {
                    dgvVedPersonList.Columns["ФИО"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvVedPersonList.Columns["ExamsVedId"].Visible = false;
                dgvVedPersonList.Columns["PersonId"].Visible = false;
            } 
            DgvAddColumns();
        }

        private void dgvVedPersonList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsPortfolioAnonymPartMotivLetter || IsPortfolioAnonymPartEssay)
            {
                if (e.RowIndex < 0 || (e.ColumnIndex != dgvVedPersonList.Columns["MotiveMail"].Index && e.ColumnIndex != dgvVedPersonList.Columns["Essay"].Index))
                    return;
                else
                {
                    int row = this.dgvVedPersonList.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvVedPersonList.Rows[row].Cells["PersonId"].Value;
                    
                    CardType ctype = CardType.Uknown;
                    if (e.ColumnIndex == dgvVedPersonList.Columns["MotiveMail"].Index)
                        ctype = CardType.Motivation; 
                    if (e.ColumnIndex == dgvVedPersonList.Columns["Essay"].Index)
                        ctype = CardType.Essay; 
                    Util.OpenExamMarkCard(this, id, _VedId, ctype, row, new UpdateHandler(FillGrid), new OpenHandler(OpenNextCard), new OpenHandler(OpenPrevCard));
                }
            }
            else if (IsPortfolioCommonPart)
            {
                if (e.RowIndex < 0 || (e.ColumnIndex != dgvVedPersonList.Columns["Portfolio"].Index))
                    return;
                else
                {
                    int row = this.dgvVedPersonList.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvVedPersonList.Rows[row].Cells["PersonId"].Value;
                    CardType type = CardType.Uknown;
                    if (e.ColumnIndex == dgvVedPersonList.Columns["Portfolio"].Index)
                        type = CardType.Portfolio; 
                    Util.OpenExamMarkCard(this, id, _VedId, type, row, new UpdateHandler(FillGrid), new OpenHandler(OpenNextCard), new OpenHandler(OpenPrevCard));
                }
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
