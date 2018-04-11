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
using BusinessLayer.Events;
using BusinessLayer.Productions;
using DAL.Productions;
//using DAL.Events;



namespace ALSTools
{
    public partial class Production : Form
    {

        private BindingSource EventBS = new BindingSource();
        private BindingSource ProductionBS = new BindingSource();
        private IDbDataAdapter daProductions;
        private DataSet MasterDS = new DataSet("Master");

        Point mouseDownPoint = Point.Empty;

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
            //dtLogs = EventLogic.GetLogIDs();
            //daEvents = EventLogic.GetEventAdapter();
            daProductions = ProductionLogic.GetProductionAdapter();

            if (MasterDS.Tables.Count != 0) { MasterDS = new DataSet(); }

            //using (DataSet EventDS = new DataSet())
            //{
            //    daEvents.Fill(EventDS);
            //    MasterDS.Merge(EventDS.Tables["Table"], true, MissingSchemaAction.Add);
            //    MasterDS.Tables["Table"].TableName = "tbl_Events";
            //    EventBS.DataSource = MasterDS;
            //    EventBS.DataMember = "tbl_Events";
            //    this.dgv_Events.DataSource = EventBS;
            //    this.dgv_Events.Columns["EventID"].ReadOnly = true;
            //}

            using (DataSet ProductionDS = new DataSet())
            {
                daProductions.Fill(ProductionDS);
                MasterDS.Merge(ProductionDS.Tables["Table"], true, MissingSchemaAction.Add);
                MasterDS.Tables["Table"].TableName = "tbl_Productions";
                ProductionBS.DataSource = MasterDS;
                ProductionBS.DataMember = "tbl_Productions";
                this.dgv_Production.DataSource = ProductionBS;
                this.dgv_Production.Columns["ProductionID"].ReadOnly = true;
            }

            //using (DataTable dt = new DataTable())
            //{

            //    this.cmb_InstrumentFilter.DataSource = dtLogs;
            //    this.cmb_InstrumentFilter.ValueMember = "LogID";
            //    this.cmb_InstrumentFilter.SelectedIndex = -1;
            //}
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

        private void dgv_Production_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView obj = sender as DataGridView;
            frm_Event frm = GetEventForm();
            TextBox ctrl = frm.Controls["txt_productionIDFilter"] as TextBox;

            bool newrow = obj.Rows[e.RowIndex].IsNewRow;
            if (!newrow)
            {
                ctrl.Text = obj["ProductionName", e.RowIndex].Value.ToString();
            }
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

            DataGridView obj = sender as DataGridView;
            ModifiedType mt = UpdateDataSet(obj, e);
            GetData();

            
            frm_Event frm = GetEventForm();

            if (mt.Equals(ModifiedType.Insert))
            {
                //Create events about production
                frm.AddGeneralEvent(string.Format("New production: {0}", obj["ProductionID", e.RowIndex].Value));
            }
            else if (mt.Equals(ModifiedType.Update))
            {
                frm.AddGeneralEvent(string.Format("Modified production {0}", obj["ProductionID", e.RowIndex].Value));
            }
        }
        public enum ModifiedType
        {
            Insert,
            Update
        }

        private ModifiedType UpdateDataSet(DataGridView dgv, DataGridViewCellEventArgs e)
        {

            try
            {
                dgv.EndEdit();
                BindingSource bs = (BindingSource)dgv.DataSource;
                string tablename = bs.DataMember.ToString();
                if (e.RowIndex > MasterDS.Tables[tablename].Rows.Count - 1)
                {
                    //Insert row into dataset, add and update database
                    DataRow dr = MasterDS.Tables[tablename].NewRow();
                    dr[e.ColumnIndex] = dgv[e.ColumnIndex, e.RowIndex].Value;
                    MasterDS.Tables[tablename].Rows.Add(dr);

                    dgv.Rows.RemoveAt(e.RowIndex);
                    dgv.EndEdit();
                    return ModifiedType.Insert;
                }

                if (!HasRowAt(MasterDS.Tables[tablename], e.RowIndex))
                {
                    MasterDS.Tables[tablename].Rows[e.RowIndex].EndEdit();
                }

                return ModifiedType.Update;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message.ToString());
                return ModifiedType.Update;
            }
            finally
            {
                List<IDbDataAdapter> adapters = new List<IDbDataAdapter>();
                adapters.Add(daProductions);

                ProductionLogic.AttachTransaction(adapters);
                for (int i = 0; i < MasterDS.Tables.Count; i++)
                {
                    using (DataSet DS = new DataSet())
                    {
                        DS.Merge(MasterDS.Tables[i], true, MissingSchemaAction.Add);
                        DS.Tables[0].TableName = "Table";
                        adapters[i].Update(DS);
                    }
                }

                ProductionLogic.TryCommit();

            }

        }
        private bool HasRowAt(DataTable dt, int index)
        {
            return dt.Rows.Count <= index;
        }
    }
}
