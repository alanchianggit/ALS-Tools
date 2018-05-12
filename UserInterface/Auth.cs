using System;
using System.Windows.Forms;

namespace Auth
{
    using ALSTools;
    public partial class frmAuth : BaseOperationForm
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
            AuthEntity.Authenticated = true;

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
            //Some signin validation logic
            AuthEntity.Authenticated = true;
            
        }
    }
}
