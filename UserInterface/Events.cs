using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using BusinessLayer.Events;
using BusinessLayer.Backup;
using Entity;
using LogicExtensions;
using System.Linq;

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
        private DateTimePicker dtp = new DateTimePicker();
        private DataGridViewCell oldDGC;


        public frm_Event()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            if (daEvents == null) { daEvents = EventLogic.GetAdapter(); }
            if (daAuditTrail == null) { daAuditTrail = EventLogic.GetBackupAdapter(); }

            das.Clear();
            das.Add(daEvents);
            das.Add(daAuditTrail);

            if (MasterDS.Tables.Count != 0) { MasterDS = new DataSet(); }
            //New row doesn't update in bindingsource
            using (DataSet ds = new DataSet())
            {
                DataGridView dgv = this.dgv_Events;
                string tblname = EventLogic.TableName;
                if (MasterDS.Tables.Contains(tblname)) { MasterDS.Tables.Remove(tblname); }
                daEvents.Fill(ds);
                MasterDS.Merge(ds.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = tblname;
                EventBS.DataSource = MasterDS;
                EventBS.DataMember = tblname;
                dgv.DataSource = EventBS;
                dgv.Columns[ID].ReadOnly = true;



                CreateComboColumn(dgv, "LogName", EventLogic.GetLogs());
                CreateComboColumn(dgv, "ProductionName", BusinessLayer.BaseLogLogic.GetProductionNames());
                CreateComboColumn(dgv, "User", EventLogic.GetUsers());

                MasterDS.AcceptChanges();
            }
            using (DataSet ds = new DataSet())
            {
                DataGridView dgv = this.dgv_AuditTrail;
                string tblname = BackupLogic.TableName;
                if (MasterDS.Tables.Contains(tblname)) { MasterDS.Tables.Remove(tblname); }
                daAuditTrail.Fill(ds);

                MasterDS.Merge(ds.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = tblname;
                AuditTrailBS.DataSource = MasterDS;
                AuditTrailBS.DataMember = tblname;
                dgv.DataSource = AuditTrailBS;
                dgv.Columns["TableName"].Visible = false;
                dgv.ReadOnly = true;
                dgv.Columns["AffectedID"].Visible = false;
            }
            using (DataTable dt = new DataTable())
            {
                dtLogs = BusinessLayer.BaseLogLogic.GetLogs();
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
                dr["Terminal"] = Environment.GetEnvironmentVariable("computername");
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

        private void UpdateDataSet(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            UpdateDataSet(dgv);
        }
        private void UpdateDataSet(DataGridView dgv)
        {
            das.Clear();
            das.Add(daEvents);
            das.Add(daAuditTrail);


            bool isNewRecord = false;
            try
            {

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

        private void dgv_Events_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            e.Row.Cells["Terminal"].Value = Environment.GetEnvironmentVariable("computername");
        }

        private void dgv_Events_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Button == MouseButtons.Left)
            {
                if (dgv[e.ColumnIndex, e.RowIndex].OwningColumn.HeaderText.Contains("Time"))
                {
                    DateTime datetime;
                    dtp = new DateTimePicker();
                    dgv.Controls.Add(dtp);
                    System.Drawing.Rectangle rect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                    dtp.MaxDate = DateTime.Now;
                    dtp.MinDate = DateTime.MinValue;

                    dtp.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
                    dtp.Format = DateTimePickerFormat.Custom;
                    dtp.Location = new System.Drawing.Point(rect.X, rect.Y);
                    dtp.Size = new System.Drawing.Size(rect.Width, rect.Height);
                    bool success = DateTime.TryParse(dgv.CurrentCell.Value.ToString(), out datetime);
                    if (success)
                    {
                        dtp.Value = datetime;
                    }

                    dtp.ShowUpDown = false;
                    dtp.Visible = true;
                    dtp.Show();
                    dtp.ValueChanged += Dtp_ValueChanged;
                    dtp.VisibleChanged += Dtp_VisibleChanged;
                }
            }
        }

        private void Dtp_VisibleChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (!dtp.Visible)
            {
                UpdateDataSet(this.dgv_Events);
            }
        }

        private void Dtp_ValueChanged(object sender, EventArgs e)
        {
            oldDGC.Value = dtp.Value.ToString();
        }


        private void dgv_Events_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location != dtp.Location)
            {
                dtp.Visible = false;
            }

            oldDGC = dgv.CurrentCell;
        }

    }
}

