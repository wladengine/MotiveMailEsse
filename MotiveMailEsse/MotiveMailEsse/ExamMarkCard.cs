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
        private string _ExaminerName;

        private Guid _PersonId;
        private int _Barcode;
        private Guid _VedomostId;
        private int _type;
        private string sMark;
        protected int ownerRowIndex = 0;
        private string IsApprovedTrue = "Проверено";
        private string IsApprovedNULL= "Не проверялось";
        private string IsApprovedFalse = "Отклонено";


        public ExamMarkCard(Guid id, Guid VedId, int type, int row)
        {
            InitializeComponent();
            _PersonId = id;
            _VedomostId = VedId;
            ownerRowIndex = row;
            _type = type;
            _ExaminerName = Util.GetUserName();
            switch (_type)
            {
                case 2: { sMark = @"Mark"; break; }
                case 3: { sMark = @"OralMark"; break; }
            }
            FillCombos();
            GdvAddColumns();
        }
        private void FillCombos()
        {
            this.Text = (_type ==2)?"Мотивационное письмо":"Эссе";
            string query = @"select PersonVedNumber FROM ed.ExamsVedHistory where ExamsVedHistory.ExamsVedId = @Id and PersonId = @PersonId";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@Id", _VedomostId);
            dic.AddVal("@PersonId", _PersonId);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;
            DataRow r = tbl.Rows[0];
            lbPersonNumber.Text = r.Field<int?>("PersonVedNumber").ToString();

            // Mark для мотивационного письма
            // OralMark для эссе 
            query = @"select " + sMark + " FROM ed.ExamsVedHistory where ExamsVedHistory.ExamsVedId = @Id and PersonId = @PersonId";

            tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;
            r = tbl.Rows[0];
            tbExamMark.Text = r.Field<byte?>(sMark).HasValue ? r.Field<byte?>(sMark).ToString() : "";
            if (!String.IsNullOrEmpty(tbExamMark.Text))
            {
                tbExamMark.ReadOnly = true;
            }
            query = @"select Barcode FROM ed.Person where Id = @PersonId";
            tbl = Util.BDC.GetDataTable(query, dic);
            if (tbl.Rows.Count == 0)
                return;
            r = tbl.Rows[0];
            int Barcode = r.Field<int>("Barcode");
            _Barcode = Barcode;
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
            string query = @"select qFiles.[Id], 
                            qFiles.[ApplicationId],
                            [FileName] as 'Название файла', 
                            [Comment] as 'Комментарий' ,
                            SP_Faculty.Name as 'Факультет',
                            (case when (qFiles.ApplicationId is null and qFiles.CommitId is null) then 'общ.файл' else
                            'к заявлению ('+Entry.ObrazProgramName+')' end) as 'Тип файла',
                            --IsReadOnly as 'ReadOnly',
                            --IsApproved as 'Проверено'
                             (case when IsApproved IS NULL then '" + IsApprovedNULL+
                            @"' else (case when IsApproved = 'True' then '" + IsApprovedTrue + @"' else '"+IsApprovedFalse+ @"' end )end) AS 'Статус'
                            FROM qAbitFiles_OnlyEssayMotivLetter as qFiles
                            inner join Person on Person.Id = qFiles.PersonId 
                            left join Application on Application.Id = ApplicationId
                            left join Entry on Application.EntryId = Entry.Id
                            left join SP_Faculty on SP_Faculty.Id = Entry.FacultyId 
                            where Person.Barcode = @Barcode
                            and qFiles.FileTypeId = @FileTypeId
                            ";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (cbFaculty.Checked)
            {
                query += " and Entry.FacultyId=@FacultyId";
                string faculty = @"select FacultyId FROM ed.ExamsVed where ExamsVed.Id = @Id ";
                Dictionary<string, object> dic_f = new Dictionary<string, object>();
                dic_f.AddVal("@Id", _VedomostId);
                DataTable tbl_f = Util.BDC.GetDataTable(faculty, dic_f);
                DataRow r = tbl_f.Rows[0];
                faculty = r.Field<int?>("FacultyId").ToString();
                dic.AddVal("@FacultyId", faculty);
            }
            if (cbEntry.Checked)
            {
                string exam = @"select EntryId FROM ed.[ExamInEntry] inner join ed.ExamsVed on ExamsVed.ExamId = ExamInEntry.ExamId where ExamsVed.Id = @Id ";
                Dictionary<string, object> dic_f = new Dictionary<string, object>();
                dic_f.AddVal("@Id", _VedomostId);

                DataTable tbl_f = Util.BDC.GetDataTable(exam, dic_f);
                exam = "";
                for (int i = 0; i < tbl_f.Rows.Count; i++ )
                {
                    DataRow rw = tbl_f.Rows[i];
                    exam += "'" + rw.Field<Guid?>("EntryId") + "'";
                    if (i < tbl_f.Rows.Count - 1)
                    {
                        exam += ", ";
                    }
                }
                if (!String.IsNullOrEmpty(exam)) 
                    query += " and Entry.Id in ( " + exam + " )";
            }
            dic.AddVal("@Barcode", Barcode);
            dic.AddVal("@FileTypeId", _type); 
            DataTable tbl = Util.ADC.GetDataTable(query, dic);
            dgvAbitFiles.DataSource = tbl;
            dgvAbitFiles.Columns["Id"].Visible = false;
            dgvAbitFiles.Columns["ApplicationId"].Visible = false;
            //dgvAbitFiles.Columns["ReadOnly"].Visible = false;  
            dgvAbitFiles.Columns["Статус"].Width = 70; 
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
                    int row = this.dgvAbitFiles.CurrentCell.RowIndex;
                    Guid id = (Guid)dgvAbitFiles.Rows[row].Cells["Id"].Value;

                    string query = "SELECT FileData FROM extAbitFiles_All WHERE Id=@Id";

                    string filename = dgvAbitFiles.Rows[e.RowIndex].Cells["Название файла"].Value.ToString();

                    //byte[] unicode_bytes = Encoding.Unicode.GetBytes(filename);
                    //byte[] ascii_bytes = Encoding.Convert(Encoding.Unicode, Encoding.Default, unicode_bytes);
                    string flattened = //Encoding.ASCII.GetString(ascii_bytes)
                        (filename ?? "_").Replace('?', '_').Replace(':', '_').Replace('\\', '_').Replace('/', '_').Replace('*', '_').Replace('|', '_').Replace('<', '_').Replace('>', '_').Replace('"', '_');

                    filename = flattened;

                    int lastSlashPos = filename.LastIndexOf('\\');
                    if (lastSlashPos > 0)
                        filename = filename.Substring(lastSlashPos);

                    filename = Util.TemplateFolder + filename;

                    byte[] data = (byte[])Util.ADC.GetValue(query, new Dictionary<string, object>() { { "@Id", id } });
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
            if (CheckFields())
            {
                string Mark = tbExamMark.Text;
                int iMark = 0;
                if (!int.TryParse(Mark, out iMark))
                {
                    iMark = 0;
                }
                try
                {
                    string query = @"update ed.ExamsVedHistory set " + sMark + "=@Mark, ExaminerName = @ExaminerName where ExamsVedHistory.ExamsVedId = @Id and PersonId = @PersonId";
                    Util.BDC.GetValue(query, new Dictionary<string, object> { { "@Mark", iMark }, { "@ExaminerName", _ExaminerName }, { "@Id", _VedomostId }, { "@PersonId", _PersonId } });
                    if (!Util.IsTest)
                    {
                        foreach (DataGridViewRow rw in dgvAbitFiles.Rows)
                        {
                            if (rw.Cells["Статус"].Value.ToString() != IsApprovedNULL)
                            {
                                query = "UPDATE ApplicationFile SET IsReadOnly=1 WHERE Id=@Id";
                                if (Util.ADC.ExecuteQuery(query, new Dictionary<string, object> { { "@Id", (Guid)rw.Cells["Id"].Value } }) == 0)
                                {
                                    query = "UPDATE PersonFile SET IsReadOnly=1 WHERE Id=@Id";
                                    Util.ADC.ExecuteQuery(query, new Dictionary<string, object> { { "@Id", (Guid)rw.Cells["Id"].Value } });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _handlerUpdate();
                tbExamMark.ReadOnly = true;
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string cardId =  _handlerOpenNext(ref ownerRowIndex);
                if (!String.IsNullOrEmpty(cardId))
                {
                    _PersonId = Guid.Parse(cardId);
                    FillCombos();
                    if (String.IsNullOrEmpty(tbExamMark.Text))
                        tbExamMark.ReadOnly = false;
                }
            }
            catch (Exception )
            {
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
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
            
            var value = dgvAbitFiles.Rows[e.RowIndex].Cells["Статус"].Value;
            if (value != null && (String)value == IsApprovedTrue)
            {
                dgvAbitFiles.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                dgvAbitFiles.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (value != null && (String)value == IsApprovedFalse)
            {
                dgvAbitFiles.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Tomato;
                dgvAbitFiles.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                dgvAbitFiles.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvAbitFiles.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        private void cbEntry_CheckedChanged(object sender, EventArgs e)
        {
            FillCard(_Barcode);
        }
    }
}
