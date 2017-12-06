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
    public partial class Operations : Form
    {
        
        public bool Connected
        {
            get
            {
                return si.Connected;
            }
        }

        private static Operations inst;
        public static Operations GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new Operations();
                return inst;
            }
        }
        public Operations()
        {
            InitializeComponent();
            newSigninToolStripMenuItem.PerformClick();
            if (Connected)
            {
                toolStripStatusLabel1.Text = "Connected";
            }
            else
            {
                toolStripStatusLabel1.Text = "Not connected";
            }
        }

        private void fileAccessFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileAccessForm.GetForm.Show();
        }

        private void archiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Archiver.ArchiverForm.GetForm.Show();
        }

        private void analysisManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analysis_Management.GetForm.Show();
        }

        Signin si;
        private void newSigninToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (si==null || si.IsDisposed)
            {
                si = new Signin();
                si = Signin.GetForm;
            }
            else
            {
                si.WindowState = FormWindowState.Normal;
            }
            ShowChildForm(si);
        }

        private void ShowChildForm(Form si)
        {
            
            si.MdiParent = this;
            si.Show();
            si.BringToFront();
        }
    }
}
