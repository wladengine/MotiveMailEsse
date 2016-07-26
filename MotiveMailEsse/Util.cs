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
            //string connStr = "Data Source = srveducation; Initial Catalog=Priem2016;Integrated Security=False; User=MotivMailFileReader;Password=$MotivMail4FileReader.;Connect Timeout=300";
            string connStr = "Data Source = srveducation; Initial Catalog=Priem2016_TEST;Integrated Security=True; Connect Timeout=300";

            BDC = new BDClass(connStr);

            if (connStr.Contains("TEST"))
                IsTest = true;
            else
                IsTest = false;

            connStr = "Data Source = srvpriem1; Initial Catalog=OnlinePriem2015;Integrated Security=False; User=MotivMailFileReader;Password=$MotivMail4FileReader.;Connect Timeout=300";

            ADC = new BDClass(connStr);

            TemplateFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MotiveMailEssay_TempFiles\";
            CampaignYear = 2016;
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

        public static void OpenVedCard(Form parent, Guid id, bool isMain, UpdateHandler handler)
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is VedCard)
                {
                    f.Close();
                }
            }
            var pcard = new VedCard(id, isMain);
            pcard._handler = handler;
            pcard.MdiParent = MainForm;
            pcard.Show();
        }
        public static void OpenExamMarkCard(Form parent, Guid id, Guid VedId, CardType type, int row, bool isMain, UpdateHandler handlerUpdate, OpenHandler handlerOpenNext, OpenHandler handlerOpenPrev)
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is ExamMarkCard)
                {
                    f.Close();
                }
            }
            var pcard = new ExamMarkCard(id, VedId, type, row, isMain, handlerUpdate, handlerOpenNext, handlerOpenPrev);
            if (!pcard.isClosed)
                pcard.Show();
        }

        public static string GetUserNameRectorat()
        {
            return System.Environment.UserName;
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

    public enum CardType
    {
        Uknown,
        Motivation,
        Essay,
        Portfolio,
        PhilosophyEssay
    }
}
