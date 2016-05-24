using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EducServLib;

namespace MotiveMailEssay
{
    public partial class VedList : Form
    {
        public VedList()
        {
            InitializeComponent();
            ExtraInit();
        }

        void ExtraInit()
        {
            LoadFaculties();
            LoadStudyBasis();
            LoadStudyLevel();
            LoadExams();
            FillGrid();
            this.Text = "Список ведомостей";
            cbFaculty.SelectedValueChanged += new EventHandler(cbFaculty_SelectedValueChanged);
            cbStudyLevel.SelectedIndexChanged += new EventHandler(cbStudyLevel_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbExam.SelectedIndexChanged += new EventHandler(cbExam_SelectedIndexChanged);
        }

        public int? StudyLevelId
        {
            get { return ComboServ.GetComboIdInt(cbStudyLevel); }
            set { ComboServ.SetComboId(cbStudyLevel, value); }
        }
        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public int? StudyBasisId
        {
            get { return ComboServ.GetComboIdInt(cbStudyBasis); }
            set { ComboServ.SetComboId(cbStudyBasis, value); }
        }
        public int? ExamId
        {
            get { return ComboServ.GetComboIdInt(cbExam); }
            set { ComboServ.SetComboId(cbExam, value); }
        }

        private void LoadFaculties()
        {
            string query = "SELECT DISTINCT ExamsVed.FacultyId AS Id, SP_Faculty.Name AS Name FROM ed.ExamsVed Inner join ed.SP_Faculty on SP_Faculty.Id = ExamsVed.FacultyId ";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbFaculty, bind, false, true);
        }

        private void LoadStudyBasis()
        {
            string query = @"SELECT DISTINCT ExamsVed.StudyBasisId AS Id, StudyBasis.Name AS Name FROM ed.ExamsVed Inner join ed.StudyBasis on StudyBasis.Id = ExamsVed.StudyBasisId ";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbStudyBasis, bind, false, true);
        }

        private void LoadStudyLevel()
        {
            string query = @"SELECT DISTINCT ExamsVed.StudyLevelGroupId AS Id, StudyLevelGroup.Name AS Name FROM ed.ExamsVed Inner join ed.StudyLevelGroup on StudyLevelGroup.Id = ExamsVed.StudyLevelGroupId ";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbStudyLevel, bind, false, true);
        }

        private void LoadExams()
        {
            string query = @"SELECT DISTINCT ExamsVed.ExamId AS Id, ExamName.Name AS Name FROM ed.ExamsVed INNER JOIN ed.Exam on Exam.Id = ExamsVed.ExamId INNER JOIN ed.ExamName ON ExamName.Id = Exam.ExamNameId";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (cbFaculty.SelectedIndex != 0)
            {
                query += " WHERE ExamsVed.FacultyId = @FacultyId";
                dic.AddVal("@FacultyId", FacultyId);
            }
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbExam, bind, false, true);
        }

        private void FillGrid()
        {
            string query = @"SELECT distinct ExamsVed.[Id] as ExamsVedId, ExamsVed.Number as 'Номер ведомости'
                                , StudyLevelGroup.Name AS 'Уровень'
                                , SP_Faculty.Name AS 'Факультет'
                                , StudyBasis.Name AS 'Основа' 
                                ,[Date] as 'Дата' 
                                , ExamName.Name AS 'Экзамен'
                                ,[IsLocked] ,[IsLoad] ,[IsAdd] ,[AddCount]
                            FROM ed.[ExamsVed]
                            Inner join ed.StudyLevelGroup on StudyLevelGroup.Id = ExamsVed.StudyLevelGroupId  
                            left join ed.StudyBasis on StudyBasis.Id = ExamsVed.StudyBasisId 
                            Inner join ed.SP_Faculty on SP_Faculty.Id = ExamsVed.FacultyId 
                            INNER JOIN ed.Exam on Exam.Id = ExamsVed.ExamId 
                            INNER JOIN ed.ExamName ON ExamName.Id = Exam.ExamNameId
                            INNER JOIN ed.ExaminerInExamsVed on ExaminerInExamsVed.ExamsVedId = ExamsVed.Id";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //string filter = " WHERE ed.ExaminerInExamsVed.ExaminerAccount = '" + System.Environment.UserName + "' ";//Util.GetUserName() + "' "; 
            string filter = " WHERE 1=1 ";// = '" + System.Environment.UserName + "' ";//Util.GetUserName() + "' "; 
            if (cbFaculty.SelectedIndex != 0)
            {
                filter += " and ExamsVed.FacultyId = @FacultyId";
                dic.AddVal("@FacultyId", FacultyId);
            }
            if (cbStudyLevel.SelectedIndex != 0)
            {

                filter += " and ExamsVed.StudyLevelGroupId = @StudyLevelGroupId";
                dic.AddVal("@StudyLevelGroupId", StudyLevelId);
            }
            if (cbStudyBasis.SelectedIndex != 0)
            {

                filter += " and ExamsVed.StudyBasisId = @StudyBasisId";
                dic.AddVal("@StudyBasisId", StudyBasisId);
            }
            if (cbExam.SelectedIndex != 0)
            {

                filter += " and ExamsVed.ExamId = @ExamId";
                dic.AddVal("@ExamId", ExamId);
            }
            string orderby = " ORDER BY ExamsVed.Number";
            DataTable tbl = Util.BDC.GetDataTable(query + filter + orderby, dic);
            if (tbl.Rows.Count >= 0)
            {
                dgvVedList.DataSource = tbl;
                dgvVedList.Columns["ExamsVedId"].Visible = false;
                dgvVedList.Columns["IsLocked"].Visible = false;
                dgvVedList.Columns["IsLoad"].Visible = false;
                dgvVedList.Columns["IsAdd"].Visible = false;
                dgvVedList.Columns["AddCount"].Visible = false;
                dgvVedList.Columns["Экзамен"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvVedList.ReadOnly = true;
            }
            if (tbl.Rows.Count > 0)
            {
                btnVedOpen.Enabled = true;
            }
            else
                btnVedOpen.Enabled = false;
        }

        private void dgvVedList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //Guid id = (Guid)dgvVedList.Rows[e.RowIndex].Cells["ExamsVedId"].Value;
            //Util.OpenVedCard(this, id, new UpdateHandler(FillGrid));
            try
            {
                if ((bool)dgvVedList.Rows[e.RowIndex].Cells["IsLocked"].Value == true)
                {
                    Guid id = (Guid)dgvVedList.Rows[e.RowIndex].Cells["ExamsVedId"].Value;
                    Util.OpenVedCard(this, id, new UpdateHandler(FillGrid));
                }
                else
                {
                    MessageBox.Show("Вы выбрали незакрытую ведомость. Просмотр отменен.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void dgvVedList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex >= dgvVedList.Rows.Count)
                return;
        }

        void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadExams();
            FillGrid();
        }
        void cbStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        void cbExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnVedOpen_Click(object sender, EventArgs e)
        {
            int row = this.dgvVedList.CurrentCell.RowIndex;
            if (row < 0)
                return;
            try
            {
                if ((bool) dgvVedList.Rows[row].Cells["IsLocked"].Value == true)
                {
                    Guid id = (Guid)dgvVedList.Rows[row].Cells["ExamsVedId"].Value;
                    Util.OpenVedCard(this, id, new UpdateHandler(FillGrid));
                }
                else
                {
                    MessageBox.Show("Вы выбрали незакрытую ведомость. Просмотр отменен.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            { 

            }
        }
    }
}
