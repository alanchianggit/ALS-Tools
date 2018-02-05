using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth;

namespace Auth
{
    public partial class frmAuth : Form
    {
        private static frmAuth inst;
        public static frmAuth GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new frmAuth();
                return inst;
            }
        }
        public frmAuth()
        {
            InitializeComponent();
            

        }

        private void StoreProperties(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            switch (txt.Name)
            {
                case "txt_Username":
                    AuthEntity.Username = txt.Text;
                    break;
                case "txt_Password":
                    AuthEntity.Password = txt.Text;
                    break;
                default:
                    break;
            }
        }

        private void Auth_Load(object sender, EventArgs e)
        {

        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {

        }
    }
}
