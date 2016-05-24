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
    }
}
