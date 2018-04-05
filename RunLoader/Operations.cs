using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Factory;
using Auth;

namespace RunLoader
{
    public partial class Operations : Form
    {
        
        //public bool Connected
        //{
        //    get
        //    {
        //        return si.Connected;
        //    }
        //}

        private static Operations inst;
        public static Operations Instance
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
            DataLayer.Instance.Reset();
            AuthEntity.Instance.Reset();
            newSigninToolStripMenuItem.PerformClick();
            //newSigninToolStripMenuItem.PerformClick();
            //if (Connected)
            //{
            //    toolStripStatusLabel1.Text = "Connected";
            //}
            //else
            //{
            //    toolStripStatusLabel1.Text = "Not connected";
            //}
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
            string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;

            if (!strType.Contains("Analysis_Management") || frm == null || frm.IsDisposed)
            {
                frm = new Analysis_Management();
                frm = Analysis_Management.GetForm;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
            ShowChildForm(frm);
            
        }

        public void ClickEvent()
        {
            eventsWindowsToolStripMenuItem.PerformClick();
        }

        private void ShowChildForm(Form fm)
        {
            
            fm.MdiParent = this;
            fm.Show();
            fm.BringToFront();
        }

        Form frm;
        private void eventsWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AuthEntity.Authenticated)
            {
                string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
                if (!strType.Contains("Event") || (frm == null || frm.IsDisposed))
                {
                    frm = new frm_Event();
                    frm = frm_Event.GetForm;
                }
                else
                {
                    frm.WindowState = FormWindowState.Normal;
                }
                ShowChildForm(frm);
            }
            else
            {
                MessageBox.Show("Need to sign-in first.");
            }
        }

        private void productionManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AuthEntity.Authenticated)
            {
                string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
                if (!strType.Contains("Production") || (frm == null || frm.IsDisposed))
                {
                    frm = new Production();
                    frm = RunLoader.Production.GetForm;
                }
                else
                {
                    frm.WindowState = FormWindowState.Normal;
                }
                ShowChildForm(frm);
            }
            else
            {
                MessageBox.Show("Need to sign-in first.");
            }
        }

        private void newSigninToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strType = frm != null ? strType = frm.GetType().ToString() : strType = string.Empty;
            if (!strType.Contains("Auth") || (frm == null || frm.IsDisposed))
            {
                frm = new frmAuth();
                frm = frmAuth.GetForm;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
            }
            ShowChildForm(frm);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
