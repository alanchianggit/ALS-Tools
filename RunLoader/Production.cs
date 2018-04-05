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



namespace RunLoader
{
    public partial class Production : Form
    {
        private string oldvalue;
        //public Productions currProd;

        private BindingSource EventBS = new BindingSource();
        private BindingSource ProductionBS = new BindingSource();
        //private IDbDataAdapter daEvents;
        private IDbDataAdapter daProductions;
        private DataSet MasterDS = new DataSet("Master");

        public Production()
        {
            InitializeComponent();

            GetData();
            //currProd = new Productions();
            //UpdateLogs();
            //UpdateProductionIDs();
            //UpdateMethodList();




            //DataBinding
            //this.txt_Ender.DataBindings.Add("Text", currProd, "Ender", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.txt_StartTime.DataBindings.Add("Text", currProd, "StartTime", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.txt_EndTime.DataBindings.Add("Text", currProd, "EndTime", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.cmb_EqpName.DataBindings.Add("Text", currProd, "EqpName", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.txt_Starter.DataBindings.Add("Text", currProd, "Starter", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.cmb_Method.DataBindings.Add("Text", currProd, "Method", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.txt_Quantity.DataBindings.Add("Text", currProd, "Quantity", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.cmb_Type.DataBindings.Add("Text", currProd, "Type", true, DataSourceUpdateMode.OnPropertyChanged);
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

        //private void ControlTextChanged(object sender, EventArgs e)
        //{
        //    //currProd.UpdateProperty(sender);
        //    //if (sender == this.cmb_ProductionName)
        //    //{
        //    //    currProd.GetProduction();
        //    //}
        //}

        //private void btn_Create_Click(object sender, EventArgs e)
        //{
        //    //currProd.CreateNew();
        //    //UpdateProductionIDs();

        //}


        //private void btn_Clear_Click(object sender, EventArgs e)
        //{
        //    //currProd.GetProduction();
        //    //if (currProd.ID == int.MinValue)
        //    //{
        //    //    ClearFields();
        //    //}
        //}

        //private void ClearFields()
        //{
        //    foreach (Control ctrl in tableLayoutPanel1.Controls)
        //    {
        //        switch (ctrl.Name.Substring(0, 4))
        //        {
        //            case "cmb_":
        //            case "txt_":
        //                ctrl.ResetText();
        //                break;
        //            case "date":
        //                ctrl.Text = DateTime.MinValue.ToString();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //private void btn_Update_Click(object sender, EventArgs e)
        //{
        //    //currProd.UpdateDB();

        //}

        //private void UpdateLogs()
        //{
        //    using (ProductionDAL pdal = new ProductionDAL())
        //    {
        //        DataTable dt = pdal.GetAvailableLogs();
        //        this.cmb_EqpName.DataSource = dt;
        //        this.cmb_EqpName.DisplayMember = "LogID";
        //        this.cmb_EqpName.ValueMember = "LogID";

        //        DataTable dt1 = new DataTable();
        //        dt1 = dt.Copy();
        //        this.cmb_EqpFilter.DataSource = dt1;
        //        this.cmb_EqpFilter.DisplayMember = "LogID";
        //        this.cmb_EqpFilter.ValueMember = "LogID";
        //    }

        //    this.cmb_EqpFilter.Text = string.Empty;

        //}



        //private void UpdateProductionIDs()
        //{
        //    List<string> _prods = new List<string>();
        //    using (ProductionDAL pdal = new ProductionDAL())
        //    {
        //        DataTable dt = new DataTable();
        //        if (this.cmb_EqpFilter.Text == string.Empty)
        //        {
        //            dt = pdal.GetProductionIDs();
        //        }
        //        else
        //        {
        //            dt = pdal.GetProductionIDs(cmb_EqpFilter.Text);

        //        }
        //        this.cmb_ProductionName.DataSource = dt;
        //        this.cmb_ProductionName.DisplayMember = "ProductionName";
        //        this.cmb_ProductionName.ValueMember = "ProductionName";
        //    }
        //}

        //private void UpdateMethodList()
        //{
        //    using (ProductionDAL pdal = new ProductionDAL())
        //    {
        //        DataTable dt = new DataTable();
        //        dt = pdal.GetMethods();
        //        this.cmb_Method.DataSource = dt;
        //        this.cmb_Method.DisplayMember = "Method";
        //        this.cmb_Method.ValueMember = "Method";
        //    }
        //}

        //private void cmb_EqpFilter_TextChanged(object sender, EventArgs e)
        //{
        //    UpdateProductionIDs();
        //}


        //private void btn_AddEvent_Click(object sender, EventArgs e)
        //{
        //    //List<EventEntity> le = currProd.Events;
        //}

        //private void cmb_ProductionName_Leave(object sender, EventArgs e)
        //{
        //    //currProd.GetProduction();
        //}

        //private void TimePicker(object sender, EventArgs e)
        //{
        //    TextBox txtb = (TextBox)sender;
        //    txtb.Text = DateTime.Now.ToString();
        //}

        //private void txtDateTimeLeave(object sender, EventArgs e)
        //{
        //    DateTime datetime;
        //    TextBox txt = (TextBox)sender;
        //    if (DateTime.TryParse(txt.Text, out datetime))
        //    {
        //        switch (txt.Name)
        //        {
        //            case "txt_StartTime":
        //                //currProd.StartTime = datetime;
        //                break;
        //            case "txt_EndTime":
        //                //currProd.EndTime = datetime;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        txt.Text = oldvalue;
        //    }
        //}

        //private void txtEnter(object sender, EventArgs e)
        //{
        //    TextBox txt = (TextBox)sender;
        //    oldvalue = txt.Text;
        //}

        Point mouseDownPoint = Point.Empty;
        private void frmMsDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint = new Point(e.X, e.Y);
                //Extension.GetMDIChildLocation(this.MdiParent, this.Location);
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
            if (this.MdiParent.MdiChildren.OfType<frm_Event>().Count() == 0)
            {
                Operations.Instance.ClickEvent();
            }
            //otherForm = new frm_Event();
            frm_Event otherForm;
            otherForm = this.MdiParent.MdiChildren.OfType<frm_Event>().Single();

            foreach (Control ctrl in otherForm.Controls)
            {
                if (ctrl.Name.Equals("txt_ProductionIDFilter"))
                {

                    ctrl.Text = obj["ProductionName", e.RowIndex].Value.ToString() ;
                }
            }
        }

        private void dgv_Production_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView obj = sender as DataGridView;
            UpdateDataSet(obj, e);
            GetData();
            //DisplayAuditTrail(sender, e);
        }

        private void UpdateDataSet(DataGridView dgv, DataGridViewCellEventArgs e)
        {

            dgv.EndEdit();
            BindingSource bs = (BindingSource)dgv.DataSource;
            string tablename = bs.DataMember.ToString();
            if (e.RowIndex > MasterDS.Tables[tablename].Rows.Count - 1)
            {
                DataRow dr = MasterDS.Tables[tablename].NewRow();
                dr[e.ColumnIndex] = dgv[e.ColumnIndex, e.RowIndex].Value;
                MasterDS.Tables[tablename].Rows.Add(dr);

                dgv.Rows.RemoveAt(e.RowIndex);
                dgv.EndEdit();
            }

            try
            {
                if (!HasRowAt(MasterDS.Tables[tablename], e.RowIndex))
                {
                    MasterDS.Tables[tablename].Rows[e.RowIndex].EndEdit();

                    ////Audit trail if update succeeds

                    //DataTable dt = MasterDS.Tables["tbl_EventsBackup"];
                    //DataRow dr = dt.NewRow();
                    ////Import row values from old table to new
                    //for (int i = 0; i < MasterDS.Tables["tbl_Events"].Columns.Count; i++)
                    //{
                    //    dr[MasterDS.Tables["tbl_Events"].Columns[i].ColumnName] = MasterDS.Tables["tbl_Events"].Rows[e.RowIndex][i];
                    //}

                    //dt.Rows.Add(dr);
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
                adapters.Add(daProductions);
                //adapters.Add(daAuditTrail);

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
