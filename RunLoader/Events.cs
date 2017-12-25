using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using BusinessLayer;

namespace RunLoader
{
    public partial class Events : Form
    {
        public Events()
        {
            InitializeComponent();
        }

        private static Events inst;
        public static Events GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new Events();
                return inst;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            using (EventsLogic el = new EventsLogic())
            {
                LogEvent newevent = el.Add(this.cmb_Log.Text, this.txt_Details.Text);
                el.Post(newevent);
            }
                
            
        }
    }
}
