using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

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
    }
}
