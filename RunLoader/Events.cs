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
    public partial class frm_Event : Form
    {
        public LogEvent currEvent;   

        public frm_Event()
        {
            InitializeComponent();
            currEvent = new LogEvent();
        }

        private static frm_Event inst;
        public static frm_Event GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new frm_Event();
                return inst;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            using (LogEvent el = new LogEvent())
            {
                
            }
                
            
        }

        private void txt_Time_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_Details_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
