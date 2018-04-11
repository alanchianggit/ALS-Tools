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
using BusinessLayer.Events;
using System.Reflection;


namespace ALSTools
{
    public partial class frm_Event : Form
    {
        //private LogEvent currEvent;
        private BindingSource EventBS = new BindingSource();
        private BindingSource AuditTrailBS = new BindingSource();
        private IDbDataAdapter daEvents;
        private IDbDataAdapter daAuditTrail;
        private DataTable dtLogs;
        private DataSet MasterDS = new DataSet("Master");
        Point mouseDownPoint = Point.Empty;

        public frm_Event()
        {
            InitializeComponent();
            GetData();
        }

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
                this.dgv_Events.Columns["EventID"].ReadOnly = true;
            }
            //New
            using (DataSet AuditDS = new DataSet())
            {
                daAuditTrail.Fill(AuditDS);
                MasterDS.Merge(AuditDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = "tbl_Backup";
                AuditTrailBS.DataSource = MasterDS;
                AuditTrailBS.DataMember = "tbl_Backup";
                this.dgv_AuditTrail.DataSource = AuditTrailBS;
                this.dgv_AuditTrail.Columns["TableName"].Visible = false;   
                this.dgv_AuditTrail.Columns["BackupID"].ReadOnly = true;
                this.dgv_AuditTrail.Columns["AffectedID"].Visible = false;
                
            }
            using (DataTable dt = new DataTable())
            {

                this.cmb_InstrumentFilter.DataSource = dtLogs;
                this.cmb_InstrumentFilter.ValueMember = "LogID";
                this.cmb_InstrumentFilter.SelectedIndex = -1;
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
            TextBox txtbox = sender as TextBox;
            string filter = string.Format("[ProductionID] Like '%{0}%'", txtbox.Text.ToString());

            EventBS.Filter = filter;

        }

        public void AddGeneralEvent(string str)
        {
            try
            {
                DataTable dt = MasterDS.Tables["tbl_Events"];
                DataRow dr = dt.NewRow();
                dr["Details"] = str;
                dr["LogName"] = "General";
                dt.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                TryCommitDB();
            }
            GetData();
        }

        private DateTime GetDateWithoutMilliseconds(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }

        private bool TryCommitDB()
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void DisplayAuditTrail(object sender)
        {
            DataGridView obj = sender as DataGridView;
            DataGridViewCell e = obj.CurrentCell;
            if (obj.SelectedCells.Count == 1)
            {
                DisplayAuditTrail(sender, e);
            }
        }
        private void DisplayAuditTrail(object sender, DataGridViewCell e)
        {

            //NEW Backup
            if (e.RowIndex != -1)
            {
                DataGridView dgv = sender as DataGridView;
                //Can't find backupid from new event row to exisiting row
                string backupid =    dgv[dgv.Columns["ProductionID"].Index, e.RowIndex].Value.ToString();
                string filter = string.Format("[AffectedID] = '{0}'", backupid);
                AuditTrailBS.Filter = filter;
            }
        }

        private void DisplayAuditTrail(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView obj = sender as DataGridView;
            DataGridViewCell cell = obj.CurrentCell;

            if (cell.RowIndex != -1)
            {
                DisplayAuditTrail(sender, cell);
            }
        }

        private void dgv_Events_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            UpdateDataSet(dgv,e);
            GetData();
            DisplayAuditTrail(sender, e);
        }

        private bool HasRowAt(DataTable dt, int index)
        {
            return dt.Rows.Count <= index;
        }

        private void UpdateDataSet(DataGridView dgv, DataGridViewCellEventArgs e)
        {

            dgv.EndEdit();
            BindingSource bs = (BindingSource)dgv.DataSource;
            
            string tablename = bs.DataMember.ToString();


            try
            {
                //if modified cell's row is greater (newer) than current dataset
                if (e.RowIndex > MasterDS.Tables[tablename].Rows.Count - 1)
                //if(HasRowAt(MasterDS.Tables[tablename],e.RowIndex))
                {
                    //Create new row
                    DataRow dr = MasterDS.Tables[tablename].NewRow();
                    //Get old value from datagridview and put into row's cell
                    dr[e.ColumnIndex] = dgv[e.ColumnIndex, e.RowIndex].Value;
                    dr.SetField("TimeCreated", DateTime.Now);
                    // Add row to current dataset
                    MasterDS.Tables[tablename].Rows.Add(dr);
                    //Remove datagridview row
                    dgv.Rows.RemoveAt(e.RowIndex);
                    //End edit
                    dgv.EndEdit();
                }

                if (!HasRowAt(MasterDS.Tables[tablename], e.RowIndex))
                {
                    MasterDS.Tables[tablename].Rows[e.RowIndex].EndEdit();

                    //Audit trail if update succeeds
                    DataTable dt = MasterDS.Tables[tablename].GetChanges();
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i<dt.Columns.Count;i++)
                        {
                            if (dr.HasVersion(DataRowVersion.Original))
                            {

                                //compare current and original versions
                                if (!dr[i, DataRowVersion.Current].Equals(dr[i, DataRowVersion.Original]))
                                {
                                    DataTable dtbackup = MasterDS.Tables["tbl_Backup"];
                                    DataRow drbackup = dtbackup.NewRow();
                                    //Import row values from old table to new
                                    drbackup["TimeLogged"] = GetDateWithoutMilliseconds(DateTime.Now);
                                    drbackup["TableName"] = tablename;
                                    drbackup["ColumnName"] = dt.Columns[i].ColumnName;
                                    drbackup["OldValue"] = dr[i, DataRowVersion.Original].ToString();
                                    drbackup["NewValue"] = dr[i, DataRowVersion.Current].ToString();
                                    drbackup["AffectedID"] = dr["ProductionID"].ToString();
                                    dtbackup.Rows.Add(drbackup);
                                } 
                            }
                        }
                    }
                   
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message.ToString());
            }
            finally
            {
                TryCommitDB();
            }

        }

        private void dgv_Events_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            //Validate data type
            DataGridViewCell dgc = dgv[e.ColumnIndex, e.RowIndex];
            if (dgc.ValueType == typeof(DateTime))
            {
                DateTime result = new DateTime();
                if (!DateTime.TryParse(e.FormattedValue.ToString(), out result))
                {
                    if (e.FormattedValue.ToString() == string.Empty) { dgc.Value = DBNull.Value; }
                    else
                    {
                        e.Cancel = true;
                        dgc.ErrorText = "Not a valid Format.";
                    }
                }
            }
            else if (dgc.ValueType == typeof(int))
            {
                int result = new int();
                if (!int.TryParse(e.FormattedValue.ToString(), out result))
                {
                    if (e.FormattedValue.ToString() == string.Empty) { dgc.Value = DBNull.Value; }
                    else
                    {
                        e.Cancel = true;
                        dgc.ErrorText = "Not a valid Format.";
                    }

                }
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void cmb_InstrumentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox txtbox = sender as ComboBox;
            string value = null;
            if (txtbox.SelectedIndex >= 0)
            {
                value = txtbox.SelectedValue.ToString();
            }
            else
            {
                value = txtbox.Text;
            }

            if (value.Length > 0)
            {
                string filter = string.Format("[LogName] Like '%{0}%'", value);
                EventBS.Filter = filter;
            }
            else
            {
                EventBS.Filter = string.Empty;
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

        
        private void frmMsDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint = new Point(e.X, e.Y);
            }

        }


        private void frmMsMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }

        private void frmMsUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }

        private void dgv_Events_SelectionChanged(object sender, EventArgs e)
        {
            DisplayAuditTrail(sender);
        }

        private void frm_Event_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
