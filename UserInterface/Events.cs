using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using BusinessLayer.Events;
using BusinessLayer.Backup;
using Entity;
using LogicExtensions;



namespace ALSTools
{
    //using LogicExtensions;
    public partial class frm_Event : BaseOperationForm
    {
        private BindingSource EventBS = new BindingSource();
        private BindingSource AuditTrailBS = new BindingSource();
        private static List<IDbDataAdapter> das = EventLogic.listDA;
        private static IDbDataAdapter daEvents;
        private static IDbDataAdapter daAuditTrail;
        private DataTable dtLogs;
        private DataSet MasterDS = EventLogic.MasterDS;
        private const string ID = EventLogic.ID;

        public frm_Event()
        {
            InitializeComponent();
            GetData();
        }
        
        private void GetData()
        {
            

            dtLogs = EventLogic.GetLogIDs();
            if (daEvents == null)
            {
                daEvents = EventLogic.GetAdapter();
            }
            if (daAuditTrail == null)
            {
                daAuditTrail = EventLogic.GetBackupAdapter();
            }

            das.Clear();
            das.Add(daEvents);
            das.Add(daAuditTrail);

            if (MasterDS.Tables.Count != 0) { MasterDS = new DataSet(); }
            //New row doesn't update in bindingsource
            using (DataSet EventDS = new DataSet())
            {
                string tblname = EventLogic.TableName;
                if (MasterDS.Tables.Contains(tblname)) { MasterDS.Tables.Remove(tblname); }
                daEvents.Fill(EventDS);
                MasterDS.Merge(EventDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName =tblname;
                EventBS.DataSource = MasterDS;
                EventBS.DataMember = tblname;
                this.dgv_Events.DataSource = EventBS;
                this.dgv_Events.Columns[ID].ReadOnly = true;

                fillcombo();
            }
            using (DataSet AuditDS = new DataSet())
            {
                string tblname = BackupLogic.TableName;
                if (MasterDS.Tables.Contains(tblname)) { MasterDS.Tables.Remove(tblname); }
                daAuditTrail.Fill(AuditDS);

                MasterDS.Merge(AuditDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = tblname;
                AuditTrailBS.DataSource = MasterDS;
                AuditTrailBS.DataMember = tblname;
                this.dgv_AuditTrail.DataSource = AuditTrailBS;
                this.dgv_AuditTrail.Columns["TableName"].Visible = false;
                this.dgv_AuditTrail.ReadOnly = true;
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




        public void AddGeneralEvent(string str)
        {
            AddGeneralEvent(str, string.Empty);
        }

        public void AddGeneralEvent(string str, string ProdName)
        {
            das.Clear();
            das.Add(daEvents);
            das.Add(daAuditTrail);
            try
            {
                DataTable dt = MasterDS.Tables[EventLogic.TableName];
                DataRow dr = dt.NewRow();
                dr["ProductionName"] = ProdName;
                dr["Details"] = str;
                dr["LogName"] = "General";
                dr["TimeCreated"] = DateTimeExtension.GetDateWithoutMilliseconds(DateTime.Now);
                dt.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                EventLogic.TryCommitDB(MasterDS);
                
                //TryCommitDB();
            }
            GetData();

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

            if (e.RowIndex != -1)
            {
                DataGridView dgv = sender as DataGridView;
                //Can't find backupid from new event row to exisiting row
                string backupid = dgv[dgv.Columns[ID].Index, e.RowIndex].Value.ToString();
                string filter = string.Format("[AffectedID] = '{0}'", backupid);
                AuditTrailBS.Filter = filter;
            }
            else
            {
                AuditTrailBS.RemoveFilter();
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
            UpdateDataSet(dgv, e);
            GetData();
            DisplayAuditTrail(sender, e);
        }

        //private bool HasRowAt(DataTable dt, int index)
        //{
        //    return dt.Rows.Count <= index;
        //}

        private void UpdateDataSet(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            das.Clear();
            das.Add(daEvents);
            das.Add(daAuditTrail);
            

            bool isNewRecord = false;
            try
            {
                //dgv.EndEdit();
                BindingSource bs = (BindingSource)dgv.DataSource;
                string backuptblName = AuditTrailBS.DataMember.ToString();
                string targetTblName = bs.DataMember.ToString();

                //if modified cell's row is greater (newer) than current dataset

                DataRowView obj = (DataRowView)bs.Current;
                DataRow currDR = obj.Row;

                if (obj.IsNew)
                {
                    isNewRecord = true;

                    if (currDR["TimeCreated"].ToString() == string.Empty)
                    {
                        currDR["TimeCreated"] = DateTimeExtension.GetDateWithoutMilliseconds(DateTime.Now);
                    }

                    currDR.Table.Rows.Add(currDR);
                    currDR.EndEdit();
                }
                else
                {
                    List<DataRow> toDelete = new List<DataRow>();
                    isNewRecord = false;
                    //currDR.Table.AcceptChanges();
                    currDR.EndEdit();
                    foreach (DataRow dr in currDR.Table.Rows)
                    {

                        if (dr.RowState == DataRowState.Added)
                        {
                            toDelete.Add(dr);
                        }
                    }

                    foreach (DataRow dr in toDelete)
                    {
                        currDR.Table.Rows.Remove(dr);
                    }
                }

                DataTable dt = new DataTable();
                if (isNewRecord)
                {
                    dt = currDR.Table.GetChanges(DataRowState.Added);
                }
                else
                {
                    dt = currDR.Table.GetChanges(DataRowState.Modified);
                }

                //Audit trail if update succeeds
                if (dt != null)
                {
                    //BackupLogic.AddDiffRows(dt, targetTblName, out MasterDS);
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dr.HasVersion(DataRowVersion.Original))
                            {

                                //compare current and original versions
                                if (!dr[i, DataRowVersion.Current].Equals(dr[i, DataRowVersion.Original]))
                                {
                                    DataTable dtbackup = MasterDS.Tables[backuptblName];
                                    DataRow drbackup = dtbackup.NewRow();
                                    //Import row values from old table to new
                                    drbackup["TimeLogged"] = DateTimeExtension.GetDateWithoutMilliseconds(DateTime.Now);
                                    drbackup["TableName"] = targetTblName;
                                    drbackup["ColumnName"] = dt.Columns[i].ColumnName;
                                    drbackup["OldValue"] = dr[i, DataRowVersion.Original].ToString();
                                    drbackup["NewValue"] = dr[i, DataRowVersion.Current].ToString();
                                    drbackup["AffectedID"] = dr[ID].ToString();
                                    dtbackup.Rows.Add(drbackup);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            finally
            {
                EventLogic.TryCommitDB(MasterDS);
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

        private void fillcombo()
        {
            //ADD COMBOBOX
            DataGridViewComboBoxColumn combocol = new DataGridViewComboBoxColumn();
            this.dgv_Events.Columns["ProductionName"].Name = "txtProductionName";
            
            combocol.HeaderText = "ProductionName";
            combocol.Name = "ProductionName";
            combocol.DataSource = EventLogic.GetLogIDs();
            combocol.ValueMember = "LogID";
            this.dgv_Events.Columns.Add(combocol);
            List<string> rowval = new List<string>();

            
            foreach (DataGridViewRow dr in this.dgv_Events.Rows)
            {
                dr.Cells["ProductionName"].Value = dr.Cells["txtProductionName"].Value;
                //rowval.Add(dr["ProductionName"].ToString());
            }
            //combocol.Items.AddRange(rowval.ToArray());
            

            //Need to trigger change value to original productioname column
            
        }

        private void txt_ProductionID_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            string strFilter = null;
            if (!txtbox.Text.ToString().Equals(string.Empty))
            {
                strFilter = string.Format("[ProductionName] Like '%{0}%'", txtbox.Text.ToString());
            }
            FilterEvents(strFilter);
        }

        private void cmb_InstrumentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox txtbox = sender as ComboBox;
            //string value = null;
            //if (txtbox.SelectedText.Length > 0)
            //{
            //    value = txtbox.SelectedValue.ToString();
            //}
            //else
            //{
            //    value = txtbox.SelectedText;
            //}

            //if (value.Equals(string.Empty))
            //{
            //    string strFilter = string.Format("[LogName] Like '%{0}%'", value);
            //    FilterEvents(strFilter);
            //}


        }

        private void FilterEvents(string value)
        {
            if (value == null)
            {
                EventBS.RemoveFilter();
                return;
            }
            else if (value.Length > 0)
            {
                EventBS.Filter = value;
            }
            else
            {
                EventBS.RemoveFilter();
            }

        }


        private void txt_SearchPhrase_TextChanged(object sender, EventArgs e)
        {
            using (EventEntity obj = new EventEntity())
            {
                TextBox txtbox = sender as TextBox;
                PropertyInfo[] pis = obj.GetType().GetProperties();
                string strFilter = string.Empty;

                foreach (PropertyInfo pi in pis)
                {
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.String":
                            strFilter += string.Format("[{0}] LIKE '%{1}%'", pi.Name, txtbox.Text.ToString());
                            if (pis[pis.Length - 1].Name != pi.Name)
                            {
                                strFilter += " OR ";
                            }
                            break;
                        default:
                            break;
                    }
                }

                FilterEvents(strFilter);

            }
        }

        private void dgv_Events_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayAuditTrail(sender, e);
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
