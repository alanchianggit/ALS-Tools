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
        private BindingSource EventBS = new BindingSource();
        private BindingSource AuditTrailBS = new BindingSource();
        private IDbDataAdapter daEvents;
        private IDbDataAdapter daAuditTrail;
        private DataTable dtLogs;
        private DataSet MasterDS = new DataSet("Master");


        public frm_Event()
        {
            InitializeComponent();

            currEvent = new LogEvent();
            currEvent.User = AuthEntity.Username;
            GetData();
            
        }

        //private void UpdateComboBoxes()
        //{

        //    using (DataSet ds = new DataSet())
        //    {
        //        EventLogic.GetLogIDs().Fill(ds);
        //        this.cmb_Log.DataSource = ds.Tables[0];
        //        this.cmb_Log.DisplayMember = "LogID";
        //        this.cmb_Log.ValueMember = "LogID";
        //    }


        //}

        private void GetData()
        {
            dtLogs = EventLogic.GetLogIDs();
            daEvents = EventLogic.GetEventAdapter();
            daAuditTrail = EventLogic.GetBackupAdapter();

            if (MasterDS.Tables.Count != 0) { MasterDS = new DataSet(); }

            using (DataSet EventDS = new DataSet())
            {
                daEvents.Fill(EventDS);
                MasterDS.Merge(EventDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = "tbl_Events";
                EventBS.DataSource = MasterDS;
                EventBS.DataMember = "tbl_Events";
                this.dgv_Events.DataSource = EventBS;
            }
            using (DataSet AuditDS = new DataSet())
            {
                daAuditTrail.Fill(AuditDS);
                MasterDS.Merge(AuditDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = "tbl_EventsBackup";
                AuditTrailBS.DataSource = MasterDS;
                AuditTrailBS.DataMember = "tbl_EventsBackup";
                this.dgv_AuditTrail.DataSource = AuditTrailBS;
            }
            using (DataTable dt = new DataTable())
            {
                
                this.cmb_InstrumentFilter.DataSource = dtLogs;
                this.cmb_InstrumentFilter.ValueMember = "LogID";
                
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


        private void txt_ProductionID_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = /*(TextBox)*/sender as TextBox;
            string filter = string.Format("[ProductionID] Like '%{0}%'", txtbox.Text.ToString());

            EventBS.Filter = filter;

        }

        private void DisplayAuditTrail(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            int eventid = int.MaxValue;
            int.TryParse(dgv[dgv.Columns["EventID"].Index, e.RowIndex].Value.ToString(), out eventid);
            string filter = string.Format("[EventID] = {0}", eventid);
            AuditTrailBS.Filter = filter;
        }

        private void dgv_Events_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateEvent(e);
            GetData();
            DisplayAuditTrail(sender, e);
        }

        private bool HasRowAt(DataTable dt, int index)
        {
            return dt.Rows.Count <= index;
        }

        private void UpdateEvent(DataGridViewCellEventArgs e)
        {

            this.dgv_Events.EndEdit();

            if (e.RowIndex > MasterDS.Tables["tbl_Events"].Rows.Count - 1)
            {
                DataRow dr = MasterDS.Tables["tbl_Events"].NewRow();
                dr[e.ColumnIndex] = this.dgv_Events[e.ColumnIndex, e.RowIndex].Value;
                MasterDS.Tables["tbl_Events"].Rows.Add(dr);

                this.dgv_Events.Rows.RemoveAt(e.RowIndex);
                this.dgv_Events.EndEdit();
            }

            try
            {
                if (!HasRowAt(MasterDS.Tables["tbl_Events"], e.RowIndex))
                {
                    MasterDS.Tables["tbl_Events"].Rows[e.RowIndex].EndEdit();

                    //Audit trail if update succeeds

                    DataTable dt = MasterDS.Tables["tbl_EventsBackup"];
                    DataRow dr = dt.NewRow();
                    //Import row values from old table to new
                    for (int i = 0; i < MasterDS.Tables["tbl_Events"].Columns.Count; i++)
                    {
                        dr[MasterDS.Tables["tbl_Events"].Columns[i].ColumnName] = MasterDS.Tables["tbl_Events"].Rows[e.RowIndex][i];
                    }

                    //MasterDS.Tables["tbl_EventsBackup"].Rows.Add(dr);
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message.ToString());
            }
            finally
            {
                //adapter - DB interaction
                List<IDbDataAdapter> adapters = new List<IDbDataAdapter>();
                adapters.Add(daEvents);
                adapters.Add(daAuditTrail);

                EventLogic.AttachTransaction(adapters);
                for (int i = 0; i < MasterDS.Tables.Count; i++)
                {
                    using (DataSet DS = new DataSet())
                    {
                        DS.Merge(MasterDS.Tables[i], true, MissingSchemaAction.Add);
                        DS.Tables[0].TableName = "Table";
                        adapters[i].Update(DS);
                    }
                }

                EventLogic.TryCommit();
            }

        }

        private void dgv_Events_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void cmb_InstrumentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox txtbox = sender as ComboBox;

            if (txtbox.SelectedIndex >= 0)
            {
                string filter = string.Format("[LogName] Like '%{0}%'", txtbox.SelectedValue.ToString());
                EventBS.Filter = filter;
            }

            
        }

        private void txt_SearchPhrase_TextChanged(object sender, EventArgs e)
        {
            using (EventEntity obj = new EventEntity())
            {
                TextBox txtbox = sender as TextBox;
                PropertyInfo[] pis = obj.GetType().GetProperties();
                string filter = string.Empty;

                foreach (PropertyInfo pi in pis)
                {
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.String":
                            filter += string.Format("[{0}] LIKE '%{1}%'", pi.Name, txtbox.Text.ToString());
                            if (pis[pis.Length - 1].Name != pi.Name)
                            {
                                filter += " OR ";
                            }
                            break;
                        default:
                            break;
                    }
                }
                EventBS.Filter = filter;

            }
        }

        private void dgv_Events_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayAuditTrail(sender, e);
        }


    }
}
