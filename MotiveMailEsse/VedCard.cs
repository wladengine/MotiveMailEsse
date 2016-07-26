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
        private bool isMain;
        private Guid _VedId;
        private bool IsPortfolioAnonymPartMotivLetter;
        private bool IsPortfolioAnonymPartEssay;
        private bool IsPortfolioCommonPart;
        private bool IsPhilosophyEssay;

        public VedCard(Guid id, bool _isMain)
        {
            InitializeComponent();
            _VedId = id;
            isMain = _isMain;
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
                                , case when (EXISTS (select * from ed.ExamsVedSelectedMarkType ST
                                where ST.ExamsVedId = extExamsVed.Id and MarkTypeId = 6)) then 1 else 0 end as IsPhilosophyEssay  
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
            IsPhilosophyEssay = r.Field<int>("IsPhilosophyEssay") == 1;

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
            else if (IsPhilosophyEssay)
            {
                DataGridViewButtonColumn dgvColMail = new DataGridViewButtonColumn();
                dgvColMail.HeaderText = "Оценка";
                dgvColMail.Name = "PhilosophyEssay";
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
        private void FillGridIfIsMain()
        {
            if (IsPortfolioAnonymPartMotivLetter || IsPortfolioAnonymPartEssay)
            {
                string query = @"
SELECT ExamsVedId
, PersonId
, " + (isLoad ? "FIO as 'Фамилия'" : "[PersonVedNumber] as 'Рег.номер'") + @"
" + (IsPortfolioAnonymPartMotivLetter ? @" , Marks.MarkValue as 'Оценка за м.письмо' " : "")
 + (IsPortfolioAnonymPartEssay ? @", Marks.MarkValue as 'Оценка за эссе' " : "") + @"
FROM ed.ExamsVedHistory History
join ed.extPerson on extPerson.Id = History.PersonId "

+ (IsPortfolioAnonymPartMotivLetter ? (@"
left join ed.ExamsVedHistoryMark Marks on History.Id = Marks.ExamsVedHistoryId and Marks.ExamsVedMarkTypeId=3  ") : "")

+ (IsPortfolioAnonymPartEssay ? (@"
left join ed.ExamsVedHistoryMark Marks2 on History.Id = Marks2.ExamsVedHistoryId and Marks2.ExamsVedMarkTypeId=4 ") : "")

+ @"WHERE ExamsVedId = @Id";

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.AddVal("@Id", _VedId);

                while (dgvVedPersonList.Columns.Count > 0)
                {
                    dgvVedPersonList.Columns.Remove(dgvVedPersonList.Columns[0]);
                }

                DataTable tbl = Util.BDC.GetDataTable(query, dic);
                dgvVedPersonList.DataSource = tbl;

                if (dgvVedPersonList.Columns.Contains("Фамилия"))
                    dgvVedPersonList.Columns["Фамилия"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                if (dgvVedPersonList.Columns.Contains("Рег.номер"))
                    dgvVedPersonList.Columns["Рег.номер"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dgvVedPersonList.Columns["ExamsVedId"].Visible = false;
                dgvVedPersonList.Columns["PersonId"].Visible = false;
            }
            else if (IsPortfolioCommonPart)
            {
                string query = @"
SELECT ExamsVedId, PersonId, FIO as 'Фамилия', 
Marks.MarkValue as 'Оценка за портфолио' 
FROM ed.ExamsVedHistory History 
join ed.extPerson on extPerson.Id = History.PersonId
left join ed.ExamsVedHistoryMark Marks on History.Id = Marks.ExamsVedHistoryId and Marks.ExamsVedMarkTypeId=5 "
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
                    dgvVedPersonList.Columns["ФИО"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dgvVedPersonList.Columns["ExamsVedId"].Visible = false;
                dgvVedPersonList.Columns["PersonId"].Visible = false;
            }
            else if (IsPhilosophyEssay)
            {
                string query = @"
SELECT ExamsVedId, PersonId, FIO as 'Фамилия', 
Marks.MarkValue as 'Оценка за эссе по философии' 
FROM ed.ExamsVedHistory History 
join ed.extPerson on extPerson.Id = History.PersonId
left join ed.ExamsVedHistoryMark Marks on History.Id = Marks.ExamsVedHistoryId and Marks.ExamsVedMarkTypeId=6 "
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
        private void FillGrid()
        {
            if (isMain)
            {
                FillGridIfIsMain();
                return;
            }
            if (IsPortfolioAnonymPartMotivLetter || IsPortfolioAnonymPartEssay)
            { 
                string query = @"
SELECT ExamsVedId
, PersonId
, " + (isLoad ? "FIO as 'Фамилия'":"[PersonVedNumber] as 'Рег.номер'")+ @"
"+ (IsPortfolioAnonymPartMotivLetter ? @" , case when (Marks.MarkIsChecked=1) then Marks.MarkValue else Details.MarkValue end as 'Оценка за м.письмо' " : "" )
 + (IsPortfolioAnonymPartEssay ? @"case when (Marks2.MarkIsChecked=1) then Marks2.MarkValue else Details2.MarkValue end as 'Оценка за эссе' " : "") + @"
FROM ed.ExamsVedHistory History
join ed.extPerson on extPerson.Id = History.PersonId "

+ (IsPortfolioAnonymPartMotivLetter ? (@"
left join ed.ExamsVedHistoryMark Marks on History.Id = Marks.ExamsVedHistoryId and Marks.ExamsVedMarkTypeId=3
left join ed.ExamsVedMarkDetails Details on Details.ExamsVedHistoryMarkId = Marks.Id and Details.ExaminerName like '%"+Util.GetUserNameRectorat()+@"%' " ) : "")

+ (IsPortfolioAnonymPartEssay ? (@"
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
                    dgvVedPersonList.Columns["Фамилия"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                
                if (dgvVedPersonList.Columns.Contains("Рег.номер"))
                    dgvVedPersonList.Columns["Рег.номер"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                
                dgvVedPersonList.Columns["ExamsVedId"].Visible = false;
                dgvVedPersonList.Columns["PersonId"].Visible = false;
            }
            else if (IsPortfolioCommonPart)
            {
                string query = @"
SELECT ExamsVedId, PersonId, FIO as 'Фамилия', 
case when (Marks.MarkIsChecked=1) then Marks.MarkValue else Details.MarkValue end  as 'Оценка за портфолио' 
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
                    dgvVedPersonList.Columns["ФИО"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                
                dgvVedPersonList.Columns["ExamsVedId"].Visible = false;
                dgvVedPersonList.Columns["PersonId"].Visible = false;
            } 
            else if (IsPhilosophyEssay)
            {
                string query = @"
SELECT ExamsVedId, PersonId, FIO as 'Фамилия', 
case when (Marks.MarkIsChecked=1) then Marks.MarkValue else Details.MarkValue end as 'Оценка за эссе по философии' 
FROM ed.ExamsVedHistory History 
join ed.extPerson on extPerson.Id = History.PersonId
left join ed.ExamsVedHistoryMark Marks on History.Id = Marks.ExamsVedHistoryId and Marks.ExamsVedMarkTypeId=6
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
                    Util.OpenExamMarkCard(this, id, _VedId, ctype, row, isMain, new UpdateHandler(FillGrid), new OpenHandler(OpenNextCard), new OpenHandler(OpenPrevCard));
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
                    Util.OpenExamMarkCard(this, id, _VedId, type, row, isMain, new UpdateHandler(FillGrid), new OpenHandler(OpenNextCard), new OpenHandler(OpenPrevCard));
                }
            }
            else if (IsPhilosophyEssay)
            {
                if (e.RowIndex < 0 || (e.ColumnIndex != dgvVedPersonList.Columns["PhilosophyEssay"].Index))
                    return;
                else
                {
                    int row = this.dgvVedPersonList.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvVedPersonList.Rows[row].Cells["PersonId"].Value;
                    CardType type = CardType.Uknown;
                    if (e.ColumnIndex == dgvVedPersonList.Columns["PhilosophyEssay"].Index)
                        type = CardType.PhilosophyEssay;
                    Util.OpenExamMarkCard(this, id, _VedId, type, row, isMain, new UpdateHandler(FillGrid), new OpenHandler(OpenNextCard), new OpenHandler(OpenPrevCard));
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
