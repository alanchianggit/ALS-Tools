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
using DAL;



namespace RunLoader
{
    public partial class Production : Form
    {
        private string oldvalue;
        public Productions currProd;

        public Production()
        {
            InitializeComponent();
            currProd = new Productions();
            UpdateLogs();
            UpdateProductionIDs();
            UpdateMethodList();
            

            //DataBinding
            this.txt_Ender.DataBindings.Add("Text", currProd, "Ender", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txt_StartTime.DataBindings.Add("Text", currProd, "StartTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txt_EndTime.DataBindings.Add("Text", currProd, "EndTime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmb_EqpName.DataBindings.Add("Text", currProd, "EqpName", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txt_Starter.DataBindings.Add("Text", currProd, "Starter", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmb_Method.DataBindings.Add("Text", currProd, "Method", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txt_Quantity.DataBindings.Add("Text", currProd, "Quantity", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmb_Type.DataBindings.Add("Text", currProd, "Type", true, DataSourceUpdateMode.OnPropertyChanged);
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

        private void ControlTextChanged(object sender, EventArgs e)
        {
            currProd.UpdateProperty(sender);
            if (sender == this.cmb_ProductionName)
            {
                currProd.GetProduction();
            }
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            currProd.CreateNew();
            UpdateProductionIDs();

        }


        private void btn_Clear_Click(object sender, EventArgs e)
        {
            currProd.GetProduction();
            if (currProd.ID == int.MinValue)
            {
                ClearFields();
            }
        }

        private void ClearFields()
        {
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                switch (ctrl.Name.Substring(0, 4))
                {
                    case "cmb_":
                    case "txt_":
                        ctrl.ResetText();
                        break;
                    case "date":
                        ctrl.Text = DateTime.MinValue.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            currProd.UpdateDB();

        }

        private void UpdateLogs()
        {
            using (ProductionDAL pdal = new ProductionDAL())
            {
                DataTable dt = pdal.GetAvailableLogs();
                this.cmb_EqpName.DataSource = dt;
                this.cmb_EqpName.DisplayMember = "LogID";
                this.cmb_EqpName.ValueMember = "LogID";

                DataTable dt1 = new DataTable();
                dt1 = dt.Copy();
                this.cmb_EqpFilter.DataSource = dt1;
                this.cmb_EqpFilter.DisplayMember = "LogID";
                this.cmb_EqpFilter.ValueMember = "LogID";
            }

            this.cmb_EqpFilter.Text = string.Empty;

        }

        private void UpdateProductionIDs()
        {
            List<string> _prods = new List<string>();
            using (ProductionDAL pdal = new ProductionDAL())
            {
                DataTable dt = new DataTable();
                if (this.cmb_EqpFilter.Text == string.Empty)
                {
                    dt = pdal.GetProductionIDs();
                }
                else
                {
                    dt = pdal.GetProductionIDs(cmb_EqpFilter.Text);

                }
                this.cmb_ProductionName.DataSource = dt;
                this.cmb_ProductionName.DisplayMember = "ProductionName";
                this.cmb_ProductionName.ValueMember = "ProductionName";
            }
        }

        private void UpdateMethodList()
        {
            using (ProductionDAL pdal = new ProductionDAL())
            {
                DataTable dt = new DataTable();
                dt = pdal.GetMethods();
                this.cmb_Method.DataSource = dt;
                this.cmb_Method.DisplayMember = "Method";
                this.cmb_Method.ValueMember = "Method";
            }
        }

        private void cmb_EqpFilter_TextChanged(object sender, EventArgs e)
        {
            UpdateProductionIDs();
        }


        private void btn_AddEvent_Click(object sender, EventArgs e)
        {
            List<EventEntity> le = currProd.Events;
        }

        private void cmb_ProductionName_Leave(object sender, EventArgs e)
        {
            currProd.GetProduction();
        }

        private void TimePicker(object sender, EventArgs e)
        {
            TextBox txtb = (TextBox)sender;
            txtb.Text = DateTime.Now.ToString();
        }

        private void txtDateTimeLeave(object sender, EventArgs e)
        {
            DateTime datetime;
            TextBox txt = (TextBox)sender;
            if (DateTime.TryParse(txt.Text, out datetime))
            {
                switch (txt.Name)
                {
                    case "txt_StartTime":
                        currProd.StartTime = datetime;
                        break;
                    case "txt_EndTime":
                        currProd.EndTime = datetime;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                txt.Text = oldvalue;
            }
        }

        private void txtEnter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            oldvalue = txt.Text;
        }
    }
}
