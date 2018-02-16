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
using System.Reflection;

namespace RunLoader
{
    public partial class frm_Event : Form
    {
        private LogEvent currEvent;
        private DataSet dataset;
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
            using (EventLogic el = new EventLogic())
            {
                da = el.GetEventsAdapter(currEvent);
                dataset = new DataSet();
                da.Fill(dataset);
                this.dgv_Events.DataSource = dataset.Tables[0];
                

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
            currEvent.AddOrSubmit();
            GetEvents();
        }

        private void txt_ProductionID_TextChanged(object sender, EventArgs e)
        {
            GetEvents();
        }

        private void DisplayEvent(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_Events.SelectedRows.Count == 1 )
            {
                
                using (EventLogic el = new EventLogic())
                {
                    DataGridViewRow dgr = this.dgv_Events.SelectedRows[0];
                    LogEvent selectedevent = el.ConvertToEvent(dgr);
                    foreach (PropertyInfo pi in selectedevent.GetType().GetProperties())
                    {
                        pi.SetValue(currEvent, pi.GetValue(selectedevent));
                    }

                    
                    dataset = new DataSet();
                    IDbDataAdapter da = el.GetBackupAdapter(currEvent);
                    da.Fill(dataset);
                    this.dgv_AuditTrail.DataSource = dataset.Tables[0];
                }
            }
        }
        
    }
}
