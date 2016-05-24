using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.Data;

namespace MotiveMailEssay
{
    static class Util
    {
        public static BDClass BDC { get; private set; }
        public static BDClass ADC { get; private set; }
        public static string TemplateFolder { get; private set; }
        public static Form MainForm { get; set; }
        public static int CampaignYear { get; private set; }
        public static int CountryRussiaId { get; private set; }
        public static bool IsTest { get; private set; }

        static Util()
        {
            string connStr = "Data Source = srveducation; Initial Catalog=Priem;Integrated Security=True;Connect Timeout=300";
            //"Data Source=81.89.183.103;Initial Catalog=OnlinePriem2012;Integrated Security=False;User ID=OnlinePriem2012Inspector;Password=372639BE-888B-4FF4-8D17-0E86B364566C;Connect Timeout=300";
            BDC = new BDClass(connStr);

            if (connStr.Contains("TEST"))
                IsTest = true;
            else
                IsTest = false;

            connStr = "Data Source = srvpriem; Initial Catalog=OnlinePriem2012;Integrated Security=False; User=MotivMailFileReader;Password=$MotivMail4FileReader.;Connect Timeout=300";
            //"Data Source=81.89.183.103;Initial Catalog=OnlinePriem2012;Integrated Security=False;User ID=OnlinePriem2012Inspector;Password=372639BE-888B-4FF4-8D17-0E86B364566C;Connect Timeout=300";
            ADC = new BDClass(connStr);

            TemplateFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MotiveMailEssay_TempFiles\";
            CampaignYear = 2014;
            CountryRussiaId = 193;

            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(TemplateFolder))
                    Directory.CreateDirectory(TemplateFolder);
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось создать директорию.\r\n" + e.Message);
            }
        }

        public static void ClearTempFolder()
        {
            string[] files = Directory.GetFiles(TemplateFolder);
            foreach (string filename in files)
            {
                try
                {
                    File.Delete(filename);
                }
                catch { }
            }
        }

        internal static void OpenVedList() 
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is VedList)
                {
                    f.Close();
                }
            }
            var VdList = new VedList();
            VdList.MdiParent = MainForm;
            VdList.Show();
        }
        public static void AddVal(this Dictionary<string, object> dic, string Key, object Value)
        {
            if (Value == null)
                Value = DBNull.Value;

            dic.Add(Key, Value);
        }

        public static void OpenVedCard(Form parent, Guid id, UpdateHandler handler)
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is VedCard)
                {
                    f.Close();
                }
            }
            var pcard = new VedCard(id);
            pcard._handler = handler;
            pcard.MdiParent = MainForm;
            pcard.Show();
        }
        public static void OpenExamMarkCard(Form parent, Guid id, Guid VedId, int type, int row, UpdateHandler handlerUpdate, OpenHandler handlerOpenNext, OpenHandler handlerOpenPrev)
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is ExamMarkCard)
                {
                    f.Close();
                }
            }
            var pcard = new ExamMarkCard(id, VedId, type, row);
            pcard._handlerUpdate = handlerUpdate;
            pcard._handlerOpenNext = handlerOpenNext;
            pcard._handlerOpenPrev = handlerOpenPrev;
            pcard.MdiParent = MainForm;
            pcard.Show();
        }


        public static string GetUserName()
        {
            return GetADUserName(System.Environment.UserName);
        }
        public static string GetADUserName(string userName)
        {
            try
            {
                var ADPrincipal = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ADPrincipal, userName);

                if (user != null)
                    return user.DisplayName + " (" + userName + ")";
            }
            catch { }

            return userName;
        }
    }
}
