using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunLoader
{
    public partial class Signin : Form
    {
        private bool _Connected;
        protected string _UserName;
        protected string _Password;

        public string Username
        {
            get
            {
                return Username = _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        public string Password
        {
            get
            {
                return Password = _Password;
            }
            set
            {
                _Password = value;
            }
        }
        public bool Connected
        {
            get
            {
                return Connected = _Connected;
            }
            set
            {
                if (value == true)
                {
                    toolStripStatusLabel1.Text = "Connected";
                }
                else
                {
                    toolStripStatusLabel1.Text = "Not Connected";
                }
                _Connected = value;
            }
        }


        public Signin()
        {
            InitializeComponent();
        }

        private static Signin inst;
        public static Signin GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new Signin();
                return inst;
            }
        }

        private void Signin_Load(object sender, EventArgs e)
        {

        }

        private void Signin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //In case windows is trying to shut down, don't hold the process up
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {

            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == DialogResult.None)
                {
                    // Assume that X has been clicked and act accordingly.
                    // Confirm user wants to close
                    switch (MessageBox.Show(this, "Are you sure?", "Do you still want ... ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        //Stay on this form
                        case DialogResult.No:
                            e.Cancel = true;
                            break;
                        default:
                            break;
                    }
                }
            }


        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {
            TestConnection();
        }

        private void TestConnection()
        {
            WebClient client = new WebClient { Credentials = new NetworkCredential(Username, Password) };
            string response = client.DownloadString("https://accounts.google.com/ServiceLogin");
            if (!response.Contains("Not Authorized"))
            {
                _Connected = true;
            }
            else
            {
                _Connected = false;
            }
        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {
            Username = sender.ToString();
        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            Password = sender.ToString();
        }
    }
}
