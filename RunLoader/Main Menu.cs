using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Form newform = new FileAccessForm();
            newform.Show();
        }

        private void btn_AnalysisLoader_Click(object sender, EventArgs e)
        {
            Form newform = new Analysis_Management();
            newform.Show();
        }
    }
}
