using System;
using System.Windows.Forms;

namespace RunLoader
{
    public partial class frm_MainMenu : Form
    {
        public frm_MainMenu()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btn_FileManagement_Click(object sender, EventArgs e)
        {
            FileAccessForm.GetForm.Show();
        }

        private void btn_AnalysisLoader_Click(object sender, EventArgs e)
        {
            Analysis_Management.GetForm.Show();
        }

        private void btn_archiver_Click(object sender, EventArgs e)
        {
            Archiver.ArchiverForm.GetForm.Show();
        }
    }
}
