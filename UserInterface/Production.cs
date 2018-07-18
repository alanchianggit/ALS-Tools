using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BusinessLayer.Backup;
using BusinessLayer.Productions;
using LogicExtensions;


namespace ALSTools
{
    public partial class Production : BaseOperationForm
    {
        private bool CancelEdit = false;
        private BindingSource ProductionBS = new BindingSource();
        private BindingSource AuditTrailBS = new BindingSource();
        private static IDbDataAdapter daAuditTrail;
        private static IDbDataAdapter daProductions;
        private static List<IDbDataAdapter> das = ProductionLogic.listDA;
        private DataSet MasterDS = ProductionLogic.MasterDS;
        private const string ID = ProductionLogic.ID;
        private enum StatusEnum
        {
            Created,
            Running,
            Completed,
            Redo,
        }
        public Production()
        {
            InitializeComponent();
            GetData();
        }

        private static Production inst;
        public static Production GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new Production();
                return inst;
            }
        }

        private void GetData()
        {


            daProductions = ProductionLogic.GetAdapter();
            daAuditTrail = ProductionLogic.GetBackupAdapter();

            das.Clear();
            das.Add(daProductions);
            das.Add(daAuditTrail);

            if (MasterDS.Tables.Count != 0) { MasterDS = new DataSet(); }
            using (DataSet ProductionDS = new DataSet())
            {
                DataGridView dgv = this.dgv_Production;
                string tblname = ProductionLogic.TableName;
                if (MasterDS.Tables.Contains(tblname)) { MasterDS.Tables.Remove(tblname); }
                daProductions.Fill(ProductionDS);
                MasterDS.Merge(ProductionDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = tblname;
                MasterDS.Tables[tblname].Columns["EqpName"].DefaultValue = DAL.Factory.DataLayer.GetSetting("DefaultLogID");

                ProductionBS.DataSource = MasterDS;
                ProductionBS.DataMember = tblname;
                dgv.DataSource = ProductionBS;
                dgv.Columns[ID].ReadOnly = true;


                CreateComboColumn(dgv, "EqpName", ProductionLogic.GetLogs());
                CreateComboColumn(dgv, "Starter", ProductionLogic.GetStarter());
                CreateComboColumn(dgv, "Ender", ProductionLogic.GetEnder());
                CreateComboColumn(dgv, "Method", ProductionLogic.GetMethods());
                CreateComboColumn(dgv, "Type", ProductionLogic.GetTypes());

                MasterDS.AcceptChanges();
            }

            using (DataSet AuditDS = new DataSet())
            {
                DataGridView dgv = this.dgv_AuditTrail;
                string tblname = BackupLogic.TableName;
                if (MasterDS.Tables.Contains(tblname)) { MasterDS.Tables.Remove(tblname); }
                daAuditTrail.Fill(AuditDS);

                MasterDS.Merge(AuditDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = tblname;
                AuditTrailBS.DataSource = MasterDS;
                AuditTrailBS.DataMember = tblname;
                dgv.DataSource = AuditTrailBS;

                dgv.Columns["TableName"].Visible = false;
                dgv.Columns["AffectedID"].Visible = false;

            }
        }


        private void ShowEvent(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridView obj = sender as DataGridView;

            //Show event in event form
            frm_Event frm = GetEventForm();
            TextBox ctrl = frm.Controls["txt_productionIDFilter"] as TextBox;

            bool newrow = obj.Rows[e.RowIndex].IsNewRow;
            if (!newrow)
            {
                ctrl.Text = obj["ProductionName", e.RowIndex].Value.ToString();
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

        private void dgv_Production_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DisplayAuditTrail(sender, e);
        }

        private frm_Event GetEventForm()
        {
            //Interact with Events form
            //Show events under that production
            if (this.MdiParent.MdiChildren.OfType<frm_Event>().Count() == 0)
            {
                Operations.Instance.ClickEvent();
            }
            frm_Event otherForm;
            otherForm = this.MdiParent.MdiChildren.OfType<frm_Event>().Single();
            otherForm.Show();

            return otherForm;
        }


        private void dgv_Production_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (CancelEdit) { GetData(); return; }

            int rowindex = e.RowIndex;

            DataGridView obj = sender as DataGridView;
            ModifiedType mt = UpdateDataSet(obj);
            GetData();

            UpdateEvent(obj, mt, rowindex);

        }
        public enum ModifiedType
        {
            Insert,
            Update
        }

        private void UpdateEvent(DataGridView obj, ModifiedType type, int rowIndex)
        {
            frm_Event frm = GetEventForm();
            DataGridViewCell productionName = obj["ProductionName", rowIndex];
            if (productionName.Value != null)
            {
                string EventType = string.Empty;
                if (type.Equals(ModifiedType.Insert))
                {
                    EventType = "New";
                }
                else if (type.Equals(ModifiedType.Update))
                {
                    EventType = "Modified";
                }

                if (!string.IsNullOrEmpty(productionName.Value.ToString()))
                {
                    frm.AddGeneralEvent(string.Format("{1} production: {0}", obj[ID, rowIndex].Value, EventType), productionName.Value.ToString());
                }
                else
                {
                    frm.AddGeneralEvent(string.Format("{1} production: {0}", obj[ID, rowIndex].Value, EventType));
                }

            }
        }


        private ModifiedType UpdateDataSet(DataGridView dgv)
        {
            das.Clear();
            das.Add(daProductions);
            das.Add(daAuditTrail);

            bool isNewRecord = false;
            ModifiedType modType;
            try
            {
                BindingSource bs = (BindingSource)dgv.DataSource;
                string targettblName = bs.DataMember.ToString();
                string backuptblName = AuditTrailBS.DataMember.ToString();


                DataRowView obj = (DataRowView)bs.Current;
                DataRow currDR = obj.Row;
                if (obj.IsNew)
                {
                    isNewRecord = true;

                    currDR.Table.Rows.Add(currDR);
                    currDR.EndEdit();
                    modType = ModifiedType.Insert;
                }
                else
                {
                    isNewRecord = false;
                    List<DataRow> toDelete = new List<DataRow>();
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

                    modType = ModifiedType.Update;
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

                if (dt != null)
                {
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
                                    drbackup["TableName"] = targettblName;
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
                MessageBox.Show(excep.Message.ToString());
                return ModifiedType.Update;
            }
            finally
            {
                ProductionLogic.TryCommitDB(MasterDS);
            }
            return modType;

        }



        private void dgv_Production_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.Rows[e.RowIndex] != null && !dgv.Rows[e.RowIndex].IsNewRow && dgv.IsCurrentRowDirty)
            {
                CancelEdit = false;
            }
            else
            {
                CancelEdit = true;
                return;
            }

        }

        private void dgv_Production_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowEvent(sender, e);
        }

        private void ProductionButtonActions(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            
            DateTime newdatetime = DateTimeExtension.GetDateWithoutMilliseconds(DateTime.Now);
            DataRowView drv = this.dgv_Production.CurrentCell.OwningRow.DataBoundItem as DataRowView;
            int rowindex = this.dgv_Production.CurrentCell.OwningRow.Index;
            try
            {
                switch (btn.Name)
                {
                    case "btn_AddComment":

                        break;
                    case "btn_EndRun":
                    case "btn_StartRun":
                    case "btn_RedoRun":

                        string newStatus = GetStatus(btn, drv.Row.Field<string>("Status"));
                        StatusEnum Status;
                        Enum.TryParse(newStatus, out Status);

                        if (!string.IsNullOrEmpty(newStatus))
                        {
                            //Change Time stamps
                            switch (Status)
                            {
                                case StatusEnum.Completed:
                                    drv.Row.SetField<DateTime>("EndTime", newdatetime);

                                    break;
                                case StatusEnum.Created:
                                case StatusEnum.Redo:
                                    drv.Row.SetField<string>("StartTime", null);
                                    drv.Row.SetField<string>("EndTime", null);
                                    break;
                                case StatusEnum.Running:
                                    drv.Row.SetField<DateTime>("StartTime", newdatetime);

                                    break;
                                default:
                                    break;
                            }
                            //Change Status
                            drv.Row.SetField<string>("Status", newStatus);

                        }
                        else
                        {
                            MessageBox.Show(string.Format("Cannot change status because the production is in '{0}' state.", drv.Row.Field<string>("Status")));
                        }

                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ModifiedType mt = UpdateDataSet(dgv_Production);
                GetData();
                UpdateEvent(dgv_Production, mt, rowindex);
            }

        }


        private string GetStatus(Button btn, string status)
        {
            string btnname = btn.Name;


            //Initialize decision table
            DataTable dt = new DataTable();

            DataColumn dcstatus = dt.Columns.Add("Status", typeof(string));
            DataColumn dcstart = dt.Columns.Add(this.btn_StartRun.Name, typeof(string));
            DataColumn dcend = dt.Columns.Add(this.btn_EndRun.Name, typeof(string));
            DataColumn dcredo = dt.Columns.Add(this.btn_RedoRun.Name, typeof(string));

            dt.Rows.Add("Completed", null, null, "Redo");
            dt.Rows.Add("Running", null, "Completed", "Redo");
            dt.Rows.Add("Redo", "Running", null, null);
            dt.Rows.Add(null, "Running", null, null);
            dt.Rows.Add(string.Empty, "Running", null, null);
            dt.Rows.Add("Created", "Running", null, null);

            string result = dt.AsEnumerable().Where(x => x.Field<string>("Status") == status).Select(q => q.Field<string>(btnname)).Single<string>();

            return result;
        }
    }
}
