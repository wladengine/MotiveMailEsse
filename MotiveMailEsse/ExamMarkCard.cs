using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MotiveMailEssay
{
    public partial class ExamMarkCard : Form
    {
        public UpdateHandler _handlerUpdate;
        public OpenHandler _handlerOpenNext;
        public OpenHandler _handlerOpenPrev;

        private Guid gMarkDetailsId;
        private Guid gHistoryMarkId;

        public bool isClosed;
        private string _ExaminerName;
        private string _faculty;
        private Guid _PersonId;
        private int _Barcode;
        private Guid _VedomostId;
        private CardType _type;
        private int _itype;

        protected int ownerRowIndex = 0;
        private string IsApprovedTrue = "Проверено";
        private string IsApprovedNULL= "Не проверялось";
        private string IsApprovedFalse = "Отклонено";
        private bool IsVisible = true;
        private bool isMain;

        public ExamMarkCard(Guid id, Guid VedId, CardType type, int row, bool _isMain, UpdateHandler _hUp, OpenHandler _hOpNext, OpenHandler _hOpPrev)
        {
            InitializeComponent();
            isClosed = false;
            _PersonId = id;
            _VedomostId = VedId;
            ownerRowIndex = row;
            _type = type;
            isMain = _isMain;
            _ExaminerName = Util.GetUserName();
            this.MdiParent = Util.MainForm;
            _handlerUpdate = _hUp;
                _handlerOpenNext = _hOpNext;
                _handlerOpenPrev = _hOpPrev;
            switch (_type)
            {
                case CardType.Motivation:
                    {
                        this.Text = "Мотивационное письмо";
                        _itype = 3;
                        break;
                    }
                case CardType.Essay:
                    {
                        this.Text = "Эссе";
                        _itype = 4;
                        break;
                    }
                case CardType.Portfolio:
                    {
                        this.Text = "Портфолио";
                        _itype = 5;
                        break;
                    }
                case CardType.PhilosophyEssay:
                    {
                        this.Text = "Реферат по философии";
                        _itype = 6;
                        break;
                    }
            }
            FillCombos();
            GdvAddColumns();
        }
        private void GetId()
        {
            string squery = @"select
                                Mark.Id,
                                ISNULL (MarkIsChecked, 0) as MarkIsChecked
                                from ed.ExamsVedHistoryMark Mark  
                                join ed.ExamsVedHistory History on Mark.ExamsVedHistoryId = History.Id
                                where History.PersonId = @PersonId  and History.ExamsVedId = @VedId and Mark.ExamsVedMarkTypeId = @ExamsVedMarkTypeId";
            DataTable tbl_h = Util.BDC.GetDataTable(squery, new Dictionary<string, object> {{ "@PersonId", _PersonId }, {"@VedId", _VedomostId} , {"@ExamsVedMarkTypeId", _itype}});
            if (tbl_h.Rows.Count > 0)
            {
                gHistoryMarkId = tbl_h.Rows[0].Field<Guid>("Id");
                
            }
            else
            {
                MessageBox.Show("Ошибка! Отсутствует запись в таблице ExamsVedHistoryMark. ", "Ошибка!");
                this.isClosed = true;
                return;
            }

            squery = @"select
                                Details.Id
                                from ed.ExamsVedMarkDetails Details
                                join ed.ExamsVedHistoryMark Mark on Details.ExamsVedHistoryMarkId = Mark.Id
                                where Mark.Id = @HistoryMarkId and Details.ExaminerName like @ExaminerName";
            DataTable tbl_f = Util.BDC.GetDataTable(squery, new Dictionary<string, object> { { "@ExaminerName", "%"+Util.GetUserNameRectorat()+"%" }, { "@HistoryMarkId", gHistoryMarkId } });
            if (tbl_f.Rows.Count>0)
            {
                gMarkDetailsId = tbl_f.Rows[0].Field<Guid>("Id");
            }
            else
            {
                squery = @"select Details.Id
                           from ed.ExamsVedMarkDetails Details
                           join ed.ExamsVedHistoryMark Mark on Details.ExamsVedHistoryMarkId = Mark.Id
                           where Mark.Id = @HistoryMarkId ";
                DataTable tbl_cnt = Util.BDC.GetDataTable(squery, new Dictionary<string, object> {{ "@HistoryMarkId", gHistoryMarkId } });
                squery = @" select  ISNULL([ExaminerCount],0) from ed.ExamsVed where Id=@VedId ";
                int ExaminerCount = int.Parse(Util.BDC.GetValue(squery, new Dictionary<string, object>() { { "@VedId", _VedomostId } }).ToString());
                // Например три или более записей создано, то закрыть доступ
                if (tbl_cnt.Rows.Count >= ExaminerCount && !isMain)
                {
                    if (DialogResult.Yes == MessageBox.Show("Эту работу уже проверяет максимальное количество проверяющих, открыть следующую работу?","Ограничение количества проверяющих", MessageBoxButtons.YesNo))
                    {
                        ShowNext();
                    }
                    else
                    {
                        this.isClosed = true;
                        this.Close();
                    }
                }
                else  
                {
                    if (!isMain)
                    {
                        bool MarkIsChecked = tbl_h.Rows[0].Field<bool>("MarkIsChecked");
                        if (MarkIsChecked)
                        {
                            DialogResult dlg = MessageBox.Show("Данная работа уже была проверена главным проверяющим, оценка утверждена, работа не подлежит проверке. Открыть следующую работу?", "Ошибка!", MessageBoxButtons.YesNo);
                            if (dlg == System.Windows.Forms.DialogResult.Yes)
                                ShowNext();
                            else
                            {
                                this.isClosed = true;
                                return;
                            }
                        }
                        else
                        {
                            gMarkDetailsId = Guid.NewGuid();
                            Util.BDC.GetValue(@"insert into ed.ExamsVedMarkDetails (Id, ExamsVedHistoryMarkId, ExaminerName, Date) 
values (@Id, @HistoryMarkId, @ExaminerName, @Date)", new Dictionary<string, object>() { { "@Id", gMarkDetailsId }, { "@HistoryMarkId", gHistoryMarkId }, { "@ExaminerName", _ExaminerName }, { "@Date", DateTime.Now } });
                        }
                    }
                    else
                    {
                        gMarkDetailsId = Guid.NewGuid();
                        Util.BDC.GetValue(@"insert into ed.ExamsVedMarkDetails (Id, ExamsVedHistoryMarkId, ExaminerName, Date) 
values (@Id, @HistoryMarkId, @ExaminerName, @Date)", new Dictionary<string, object>() { { "@Id", gMarkDetailsId }, { "@HistoryMarkId", gHistoryMarkId }, { "@ExaminerName", _ExaminerName }, { "@Date", DateTime.Now } });
                    }
                }
            }
        }
        private void FillCombos()
        {
            GetId();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@Id", _VedomostId);
            dic.AddVal("@PersonId", _PersonId);

            string faculty = @"select FacultyId, isLoad FROM ed.ExamsVed where ExamsVed.Id = @Id ";
            DataRow r = Util.BDC.GetDataTable(faculty, dic).Rows[0];
            _faculty = r.Field<int?>("FacultyId").ToString();
            bool isLoad = r.Field<bool>("isLoad");

            string query = @"
select " +
(isLoad ? " FIO, " : "") +
@" PersonVedNumber 
 FROM ed.ExamsVedHistory " +
(isLoad ? " join ed.extPerson on extPerson.Id = PersonId " : "") +
@" where ExamsVedHistory.ExamsVedId = @Id and PersonId = @PersonId ";

            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;
            r = tbl.Rows[0];
            lbPersonNumber.Text = (isLoad ? r.Field<string>("FIO").ToString() + "/ " : "") + r.Field<int?>("PersonVedNumber").ToString();

            if (!isMain)
            {
                query = @"select MarkValue, Comment FROM ed.ExamsVedMarkDetails where ExamsVedMarkDetails.Id = @gDetailsId";
                dic.Add("gDetailsId", gMarkDetailsId);
                tbl = Util.BDC.GetDataTable(query, dic);
                if (tbl.Rows.Count != 0)
                {
                    r = tbl.Rows[0];
                    tbExamMark.Text = r.Field<decimal?>("MarkValue").HasValue ? r.Field<decimal?>("MarkValue").ToString() : "";
                    tbComment.Text = r.Field<string>("Comment");
                }
                else
                {
                    tbExamMark.Text = "";
                    tbComment.Text = "";
                }
                tbExamMark.ReadOnly = !String.IsNullOrEmpty(tbExamMark.Text);
            }
            else
            {
                query = @"select ExamsVedHistoryMark.MarkValue, ExamsVedMarkDetails.Comment FROM ed.ExamsVedHistoryMark 
join ed.ExamsVedMarkDetails on ExamsVedHistoryMark.Id = ExamsVedMarkDetails.ExamsVedHistoryMarkId
where ExamsVedMarkDetails.Id = @gDetailsId";
                dic.Add("gDetailsId", gMarkDetailsId);
                tbl = Util.BDC.GetDataTable(query, dic);
                if (tbl.Rows.Count != 0)
                {
                    r = tbl.Rows[0];
                    tbExamMark.Text = r.Field<decimal?>("MarkValue").HasValue ? r.Field<decimal?>("MarkValue").ToString() : "";
                    tbComment.Text = r.Field<string>("Comment");
                }
                else
                {
                    tbExamMark.Text = "";
                    tbComment.Text = "";
                }
                tbExamMark.ReadOnly = false;
            }
            

            query = @"select Barcode FROM ed.Person where Id = @PersonId";
            tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;
            r = tbl.Rows[0];
            int Barcode = r.Field<int>("Barcode");
            _Barcode = Barcode;

            string vis_query = @" select Id, ExamsVedId from ed.ExamsVedPersonClosedView where PersonId = @PersonId";
            DataTable vis_tbl = Util.BDC.GetDataTable(vis_query, new Dictionary<string, object>() { { "@PersonId", _PersonId } });
            if (vis_tbl.Rows.Count == 0)
                IsVisible = true;

            foreach (DataRow rw in vis_tbl.Rows)
            {
                if (rw.Field<Guid?>("ExamsVedId").HasValue)
                {
                    if (rw.Field<Guid?>("ExamsVedId") == _VedomostId)
                    {
                        IsVisible = false;
                    }
                }
                else
                {
                    IsVisible = false;
                }
            }

            if (Barcode != 0)
            {
                FillCard(Barcode);
            }
            else
            {
                MessageBox.Show("Данная карточка не создана в ЛК и не имеет прикреплённых файлов");
                return;
            }
        }
        private void FillCard(int Barcode)
        {
            string query = @"SELECT qFiles.[Id]
                                  ,qFiles.[FileName] as 'Название файла'
                                  ,qFiles.[Comment] as 'Комментарий'
                                  ,SP_Faculty.Name as 'Факультет'
                                  ,Entry.FacultyId
                                  ,(case when (qFiles.ApplicationId is null and qFiles.CommitId is null) then 'общ.файл' else 'к заявлению ('+Entry.ObrazProgramName+')' end) as 'Тип файла'
                                  ,(case when IsApproved IS NULL then '" + IsApprovedNULL + @"' else (case when IsApproved = 'True' then '" + IsApprovedTrue + @"' else '" + IsApprovedFalse + @"' end )end) AS 'Статус'
                                  , EntryId
                              FROM [dbo].[extEverExistedFiles] as qFiles
                              inner join Person on Person.Id = qFiles.PersonId 
                              left join Entry on Entry.Id = qFiles.EntryId
                              left join SP_Faculty on SP_Faculty.Id = Entry.FacultyId 
                              ";
            string filters = "where Person.Barcode = @Barcode ";
            // поиск мотивационного письма
            if (_type == CardType.Motivation)
            {
                filters += "and ( FileName LIKE '%мотив%' OR FileName LIKE '%motiv%' OR Comment LIKE '%мотив%' OR Comment LIKE '%motiv%' )";
            }
            // поиск эссе
            else if (_type == CardType.Essay)
            {
                filters +=
                @"
                       and (
                          (FileName LIKE '%esse%' AND FileName NOT LIKE '%compressed%' AND Comment NOT LIKE '%compressed%') OR
                          FileName LIKE '%essay%' OR
                          (Comment LIKE '%esse%' AND FileName NOT LIKE '%compressed%' AND Comment NOT LIKE '%compressed%') OR
                          Comment LIKE '%essay%' OR
                          FileName LIKE '%эсс%' OR
                          FileName LIKE '%эсе%' OR
                          Comment LIKE '%эсс%' OR
                          Comment LIKE '%эсе%' OR
                          FileName LIKE '%эсэ%' OR
                          Comment LIKE '%эсэ%'
                          ";
                if (_faculty == "21")
                {
                    filters +=
                        @" OR
                        FileName LIKE '%proek%' OR FileName LIKE '%proje%' OR FileName LIKE '%proe%' OR
                        FileName LIKE '%курат%прое%' OR FileName LIKE '%kurat%pr%' OR FileName LIKE '%kurators%' OR
                        FileName LIKE '%сurat%pr%' OR FileName LIKE '%сurators%' OR
                        FileName LIKE '%реценз%' OR FileName LIKE '%recenz%' OR FileName LIKE '%retsenz%' OR  FileName LIKE '%rezens%' OR 
                        FileName LIKE '%выстав%' OR FileName LIKE '%vystav%' OR 
                        Comment LIKE '%proek%' OR Comment LIKE '%proje%' OR Comment LIKE '%proe%' OR
                        Comment LIKE '%курат%прое%' OR Comment LIKE '%kurat%pr%' OR Comment LIKE '%kurators%' OR
                        Comment LIKE '%реценз%' OR Comment LIKE '%recenz%' OR Comment LIKE '%retsenz%' OR  Comment LIKE '%rezens%' OR 
                        Comment LIKE '%выстав%' OR Comment LIKE '%vystav%'  
                        )";
                }
                else
                    filters += ") ";
            }
            else if (_type == CardType.Portfolio)
            {
                query = @"
SELECT qFiles.Id  
,qFiles.[FileName] as 'Название файла'
,qFiles.[Comment] as 'Комментарий'
,SP_Faculty.Name as 'Факультет'
,Entry.FacultyId 
,(case when (qFiles.ApplicationId is null and qFiles.CommitId is null) then 'общ.файл' else 'к заявлению ('+Entry.ObrazProgramName+')' end) as 'Тип файла'
,(case when IsApproved IS NULL then '" + IsApprovedNULL + @"' else (case when IsApproved = 'True' then '" + IsApprovedTrue + @"' else '" + IsApprovedFalse + @"' end )end) AS 'Статус'
, Entry.Id as EntryId
FROM [OnlinePriem2015].[dbo].[qAbitFiles_AllExceptPassport] as qFiles
LEFT JOIN Application ON ApplicationId = Application.Id 
LEFT JOIN Application_LOG ON ApplicationId = Application_LOG.Id 
LEFT JOIN Entry ON Entry.Id = Application.EntryId OR Entry.Id = Application_LOG.EntryId INNER JOIN
Person ON Person.Id = qFiles.PersonId 
left join SP_Faculty on SP_Faculty.Id = Entry.FacultyId 
";
                filters +=
                    @"
                       and NOT (
                          (FileName LIKE '%esse%' AND FileName NOT LIKE '%compressed%' AND Comment NOT LIKE '%compressed%') OR
                          FileName LIKE '%essay%' OR
                          (Comment LIKE '%esse%' AND FileName NOT LIKE '%compressed%' AND Comment NOT LIKE '%compressed%') OR
                          Comment LIKE '%essay%' OR
                          FileName LIKE '%эсс%' OR
                          FileName LIKE '%эсе%' OR
                          Comment LIKE '%эсс%' OR
                          Comment LIKE '%эсе%' OR
                          FileName LIKE '%эсэ%' OR
                          Comment LIKE '%эсэ%'
                          ";
                if (_faculty == "21")
                {
                    filters +=
                        @" OR
                        FileName LIKE '%proek%' OR FileName LIKE '%proje%' OR FileName LIKE '%proe%' OR
                        FileName LIKE '%курат%прое%' OR FileName LIKE '%kurat%pr%' OR FileName LIKE '%kurators%' OR
                        FileName LIKE '%сurat%pr%' OR FileName LIKE '%сurators%' OR
                        FileName LIKE '%реценз%' OR FileName LIKE '%recenz%' OR FileName LIKE '%retsenz%' OR  FileName LIKE '%rezens%' OR 
                        FileName LIKE '%выстав%' OR FileName LIKE '%vystav%' OR 
                        Comment LIKE '%proek%' OR Comment LIKE '%proje%' OR Comment LIKE '%proe%' OR
                        Comment LIKE '%курат%прое%' OR Comment LIKE '%kurat%pr%' OR Comment LIKE '%kurators%' OR
                        Comment LIKE '%реценз%' OR Comment LIKE '%recenz%' OR Comment LIKE '%retsenz%' OR  Comment LIKE '%rezens%' OR 
                        Comment LIKE '%выстав%' OR Comment LIKE '%vystav%'  
                        )";
                }
                else
                    filters += ") ";
                filters += "and NOT ( FileName LIKE '%мотив%' OR FileName LIKE '%motiv%' OR Comment LIKE '%мотив%' OR Comment LIKE '%motiv%' )";

            }
            else if (_type == CardType.PhilosophyEssay)
            {
                filters +=
@" and (
                           FileName LIKE '%phil_essay%' OR
                          Comment LIKE '%философ%' 
                          ";
                 
                filters += ") ";
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (cbFaculty.Checked)
            {
                filters += " and Entry.FacultyId=@FacultyId ";
                dic.AddVal("@FacultyId", _faculty);
            }

            if (cbEntry.Checked)
            {
                string exam = @"select EntryId FROM ed.[extExamInEntry] inner join ed.ExamsVed on ExamsVed.ExamId = extExamInEntry.ExamId where ExamsVed.Id = @Id ";
                DataTable tbl_f = Util.BDC.GetDataTable(exam, new Dictionary<string, object> { { "@Id", _VedomostId } });
                exam = "";

                foreach (DataRow rw in tbl_f.Rows)
                    exam += "'" + rw.Field<Guid?>("EntryId") + "',";

                if (!String.IsNullOrEmpty(exam))
                    filters += " and Entry.Id in ( " + exam.Substring(0, exam.Length-1) + " )";
            }

            dic.AddVal("@Barcode", Barcode);
            dic.AddVal("@FileTypeId", _type);
            DataTable tbl = Util.ADC.GetDataTable(query + filters, dic);
            dgvAbitFiles.DataSource = tbl;

            foreach (string s in new List<string>() { "Id", "EntryId", "FacultyId", })
                if (dgvAbitFiles.Columns.Contains(s))
                    dgvAbitFiles.Columns[s].Visible = false;

            if (dgvAbitFiles.Columns.Contains("Статус"))
                dgvAbitFiles.Columns["Статус"].Width = 70;
            if (dgvAbitFiles.Columns.Contains("Комментарий"))
                dgvAbitFiles.Columns["Комментарий"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void GdvAddColumns()
        {
            DataGridViewButtonColumn dgvColBtn = new DataGridViewButtonColumn();
            dgvColBtn.Name = "Show";
            dgvColBtn.HeaderText = "Просмотр";
            dgvColBtn.Text = "Открыть";
            dgvColBtn.UseColumnTextForButtonValue = true;
            dgvAbitFiles.Columns.Insert(dgvAbitFiles.Columns.Count,dgvColBtn); 

            foreach (DataGridViewColumn col in dgvAbitFiles.Columns)
            {
                col.ReadOnly = true;
            }
            dgvAbitFiles.ReadOnly = true;
            //dgvAbitFiles.Columns["Проверено"].ReadOnly = false;
        }
        private void dgvAbitFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) 
                return;
            else
                if (e.ColumnIndex == dgvAbitFiles.Columns["Show"].Index)
                {
                    if (!IsVisible)
                    {
                        throw new NullReferenceException();
                    }

                    int row = this.dgvAbitFiles.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvAbitFiles.Rows[row].Cells["Id"].Value;

                    string query = "SELECT FileData FROM extAbitFiles_All WHERE Id=@Id";

                    string filename = dgvAbitFiles.Rows[e.RowIndex].Cells["Название файла"].Value.ToString();

                    string flattened =  
                        (filename ?? "_").Replace('?', '_').Replace(':', '_').Replace('\\', '_').Replace('/', '_').Replace('*', '_').Replace('|', '_').Replace('<', '_').Replace('>', '_').Replace('"', '_');

                    filename = flattened;

                    int lastSlashPos = filename.LastIndexOf('\\');
                    if (lastSlashPos > 0)
                        filename = filename.Substring(lastSlashPos);

                    filename = Util.TemplateFolder + filename;
                    int ind = filename.LastIndexOf('.');
                    if (ind > 0)
                    {
                        string extension = filename.Substring(ind + 1);
                        string template_filename = filename.Substring(0, ind);
                        int i = 1;
                        while (File.Exists(filename))
                        {
                            filename = template_filename + "(" + i.ToString() + ")."+extension;
                            i++;
                        }
                    }
                    else
                    {
                        if (File.Exists(filename))
                            MessageBox.Show("Закройте открытые файлы.");
                    }

                    byte[] data = (byte[])Util.ADC.GetValue(query, new Dictionary<string, object>() { { "@Id", id } });
                    if (data != null)
                    {
                        using (FileStream fs = new FileStream(filename, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                bw.Write(data);
                                bw.Flush();
                                bw.Close();
                            }
                            fs.Close();
                        }
                        System.Diagnostics.Process.Start(filename);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Файл был приложен к заявлению, которое было удалено, или был удален. Продолжить открытие файл?", "?", MessageBoxButtons.YesNo))
                        {
                            query = "SELECT FileData FROM AllFiles WHERE Id=@Id";
                            data = (byte[])Util.ADC.GetValue(query, new Dictionary<string, object>() { { "@Id", id } });
                            if (data != null)
                            {
                                using (FileStream fs = new FileStream(filename, FileMode.Create))
                                {
                                    using (BinaryWriter bw = new BinaryWriter(fs))
                                    {
                                        bw.Write(data);
                                        bw.Flush();
                                        bw.Close();
                                    }
                                    fs.Close();
                                }
                                System.Diagnostics.Process.Start(filename);
                            }
                        }
                        else
                            return;
                    }
                    
                }
                else
                    if (e.ColumnIndex == dgvAbitFiles.Columns["Статус"].Index)
                    {
                        int row = this.dgvAbitFiles.CurrentCell.RowIndex;
                        Guid id = (Guid)dgvAbitFiles.Rows[row].Cells["Id"].Value;
                        String value = (String)dgvAbitFiles.Rows[e.RowIndex].Cells["Статус"].Value;

                        bool? IsApproved;
                        if (value != null && value == IsApprovedTrue)
                        {
                            value = IsApprovedFalse;
                            IsApproved = false;
                        }
                        else if (value != null && value == IsApprovedFalse)
                        {
                            value = IsApprovedNULL;
                            IsApproved = null;
                        }
                        else
                        {
                            value = IsApprovedTrue;
                            IsApproved = true;
                        }

                        try
                        {
                            string query = "Update ApplicationFile set IsApproved = @IsApproved where Id = @Id";
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("@Id", id);
                            dic.AddVal("@IsApproved", IsApproved);
                            if (Util.ADC.ExecuteQuery(query, dic) == 0)
                            {
                                query = "Update PersonFile set IsApproved = @IsApproved where Id = @Id";
                                Util.ADC.ExecuteQuery(query, dic);
                            }

                            dgvAbitFiles.Rows[e.RowIndex].Cells["Статус"].Value = value;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        return;
                    }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private bool Save()
        {
            if (CheckFields())
            {
                string Mark = tbExamMark.Text.Replace('.',',');
                decimal iMark = 0;
                if (!decimal.TryParse(Mark, out iMark))
                {
                    MessageBox.Show("Неверный формат оценки","Ошибка");
                    return false;
                }
                try
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("@Mark", iMark);
                    dic.Add("@ExaminerName", _ExaminerName);
                    dic.Add("@_VedomostId", _VedomostId);
                    dic.Add("@PersonId", _PersonId);
                    dic.Add("@Id", gMarkDetailsId);
                    dic.Add("@Date", DateTime.Now);
                    dic.Add("@HistoryMarkId", gHistoryMarkId);
                    dic.Add("@Comment", tbComment.Text);

                    if (!Util.IsTest)
                    {
                        //блокируем от удаления файлы с метками (одобрен-не одобрен)
                        foreach (DataGridViewRow rw in dgvAbitFiles.Rows)
                        {
                            if (rw.Cells["Статус"].Value.ToString() != IsApprovedNULL)
                            {
                                string query = "UPDATE ApplicationFile SET IsReadOnly=1 WHERE Id=@Id";
                                if (Util.ADC.ExecuteQuery(query, new Dictionary<string, object> { { "@Id", (Guid)rw.Cells["Id"].Value } }) == 0)
                                {
                                    query = "UPDATE PersonFile SET IsReadOnly=1 WHERE Id=@Id";
                                    Util.ADC.ExecuteQuery(query, new Dictionary<string, object> { { "@Id", (Guid)rw.Cells["Id"].Value } });
                                }
                            }
                        }
                    }

                    //обновить оценку за себя
                    Util.BDC.ExecuteQuery(@"update ed.ExamsVedMarkDetails set MarkValue=@Mark, Date=@Date, Comment =@Comment where Id = @Id ", dic);
                    //если ты не главный, 
                    if (!isMain)
                    {
                        // если оценка не заблокирована 
                        //string Ischecked = (Util.BDC.GetValue(@"select ISNULL(MarkIsChecked, 0) from ed.ExamsVedHistoryMark where Id = @ExamsVedHistoryMarkId ", new Dictionary<string, object>() { { "@ExamsVedHistoryMarkId", gHistoryMarkId } }).ToString();
                        bool MarkIsChecked =(bool)Util.BDC.GetValue(@"select ISNULL(MarkIsChecked, 0) from ed.ExamsVedHistoryMark where Id = @ExamsVedHistoryMarkId ", new Dictionary<string, object>() { { "@ExamsVedHistoryMarkId", gHistoryMarkId } });
                        if (!MarkIsChecked)
                        {
                            // обновить среднюю оценку по всей работе
                            Util.BDC.ExecuteQuery(@"update ed.ExamsVedHistoryMark set MarkValue = 
                            (select AVG(MarkValue) from ed.ExamsVedMarkDetails where ExamsVedMarkDetails.ExamsVedHistoryMarkId = ExamsVedHistoryMark.Id) where ExamsVedHistoryMark.Id = @ExamsVedHistoryMarkId", 
                             new Dictionary<string, object>() { { "@ExamsVedHistoryMarkId", gHistoryMarkId } });
                        }
                        else
                        {
                            MessageBox.Show("Общая оценка за работу уже утверждена", "Невозможно обновить оценку за всю работу");
                        }
                    }
                    // обновить всю оценку за работу, поставить отметку о том, что она проверена
                    else
                    {
                        Util.BDC.ExecuteQuery(@"update ed.ExamsVedHistoryMark set MarkIsChecked = 1, MarkValue = @MarkValue
                        where ExamsVedHistoryMark.Id = @ExamsVedHistoryMarkId",
                        new Dictionary<string, object>() { { "@ExamsVedHistoryMarkId", gHistoryMarkId }, { "@MarkValue", iMark } });
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _handlerUpdate();
                tbExamMark.ReadOnly = true;
                return true;
            }
            return false;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbExamMark.Text) && !tbExamMark.ReadOnly)
                {
                    if (DialogResult.Yes == MessageBox.Show("Вы ввели оценку, но не сохранили результат. Сохранить оценку?","Вопрос",MessageBoxButtons.YesNo))
                    {
                        if (!Save())
                            return;
                    }
                }
            }
            catch
            {
            }
            try
            {
                ShowNext();
            }
            catch (Exception )
            {
            }
        }
        private void ShowNext()
        {
            string cardId = _handlerOpenNext(ref ownerRowIndex);
            if (!String.IsNullOrEmpty(cardId))
            {
                _PersonId = Guid.Parse(cardId);
                FillCombos();
                if (String.IsNullOrEmpty(tbExamMark.Text))
                    tbExamMark.ReadOnly = false;
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbExamMark.Text) && !tbExamMark.ReadOnly)
                {
                    if (DialogResult.Yes == MessageBox.Show("Вы ввели оценку, но не сохранили результат. Сохранить оценку?", "Вопрос", MessageBoxButtons.YesNo))
                    {
                        if (!Save())
                            return;
                    }
                }
            }
            catch
            {
            }
            try
            {
                string cardId = _handlerOpenPrev(ref ownerRowIndex);
                if (!String.IsNullOrEmpty(cardId))
                {
                    _PersonId = Guid.Parse(cardId);
                    FillCombos();
                }
            }
            catch (Exception )
            {
            }
        }
        private void cbFaculty_CheckedChanged(object sender, EventArgs e)
        {
            FillCard(_Barcode);
        }
        private bool CheckFields()
        {
            if (tbExamMark.Text.Length <= 0)
            {
                epErrorInput.SetError(tbExamMark, "Отсутствует оценка");
                return false;
            }
            else
                epErrorInput.Clear();

            return true;
        }
        private void dgvAbitFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        { 
            if (e.RowIndex < 0)
                return;

            DataGridViewRow rw = dgvAbitFiles.Rows[e.RowIndex];

            var value = dgvAbitFiles.Rows[e.RowIndex].Cells["Статус"].Value;
            if (value != null && (String)value == IsApprovedTrue)
            {
                rw.DefaultCellStyle.BackColor = Color.LightGreen;
                rw.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (value != null && (String)value == IsApprovedFalse)
            {
                rw.DefaultCellStyle.BackColor = Color.Tomato;
                rw.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                rw.DefaultCellStyle.BackColor = Color.LightYellow;
                rw.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void cbEntry_CheckedChanged(object sender, EventArgs e)
        {
            FillCard(_Barcode);
        }
    }
}
