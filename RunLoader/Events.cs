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
using Auth;
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
            currEvent.User = AuthEntity.Username;
            //currEvent.ProductionID = "test";
            Databinding();
        }
        
        private void Databinding()
        {
            //DataBinding
            //this.txt_Details.DataBindings.Clear();
            //this.txt_Time.DataBindings.Clear();
            //this.cmb_Log.DataBindings.Clear();
            //this.txt_ProductionID.DataBindings.Clear();
            //this.cmb_ID.DataBindings.Clear();

            this.txt_Details.DataBindings.Add("Text", currEvent, "Details", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txt_Time.DataBindings.Add("Text", currEvent, "TimeCreated", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmb_Log.DataBindings.Add("Text", currEvent, "LogName", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txt_ProductionID.DataBindings.Add("Text", currEvent, "ProductionID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmb_ID.DataBindings.Add("Text", currEvent, "ID", true, DataSourceUpdateMode.OnPropertyChanged);
            
            
        }
        
        private void GetEvents()
        {
            currEvent.ProductionID = this.txt_ProductionID.Text;
            IDbDataAdapter da;
            using (EventLogic elogic = new EventLogic())
            {
                da = elogic.GetAdapter(currEvent);
                DataSet ds = new DataSet();
                da.Fill(ds);
                this.dgv_Events.DataSource = ds.Tables[0];
            }
            
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
            currEvent.Add();
            GetEvents();
        }

        private void txt_ProductionID_TextChanged(object sender, EventArgs e)
        {
            GetEvents();
        }
    }
}
