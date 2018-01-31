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
        public Productions currProd;
        private List<string> _logs = new List<string>();
        private List<string> _prods = new List<string>();

        public Production()
        {
            InitializeComponent();
            currProd = new Productions();
            UpdateLogs();
            UpdateProductionIDs();
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
                ShowProduction();
            }
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            currProd.CreateNew(currProd);
            ShowProduction();

        }

        private void ShowProduction()
        {
            txt_Ender.Text = currProd.Ender;
            txt_Starter.Text = currProd.Starter;
            txt_StartTime.Text = currProd.StartTime == DateTime.MinValue ? string.Empty : currProd.StartTime.ToString() ;
            txt_EndTime.Text = currProd.EndTime == DateTime.MinValue ? string.Empty : currProd.EndTime.ToString();
            cmb_Method.Text = currProd.Method;
            cmb_ProductionName.Text = currProd.ProductionName;
            cmb_Type.Text = currProd.Type;
            txt_Quantity.Text = currProd.Quantity == int.MinValue || currProd.Quantity == 0 ? string.Empty : currProd.Quantity.ToString() ;
            cmb_EqpName.Text = currProd.EqpName;

        }

        private  void StoreProduction()
        {
            currProd.Ender = txt_Ender.Text;
            currProd.Starter = txt_Starter.Text;
            currProd.StartTime = string.IsNullOrEmpty(txt_StartTime.Text) ? DateTime.MinValue : DateTime.Parse(txt_StartTime.Text);
            currProd.EndTime = string.IsNullOrEmpty(txt_EndTime.Text) ? DateTime.MinValue : DateTime.Parse(txt_EndTime.Text);
            currProd.Method = cmb_Method.Text;
            currProd.ProductionName = cmb_ProductionName.Text;
            currProd.Type = cmb_Type.Text ;
            currProd.Quantity = string.IsNullOrEmpty(txt_Quantity.Text) ? int.MinValue : int.Parse(txt_Quantity.Text);
            currProd.EqpName = cmb_EqpName.Text;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            currProd.GetProduction();
            if (currProd.ID == int.MinValue)
            {
                ClearFields();
            }
            else
            {
                ShowProduction();
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
            StoreProduction();
            currProd.UpdateDB();
            ShowProduction();
        }

        private void UpdateLogs()
        {
            using (ProductionDAL pdal = new ProductionDAL())
            {
                DataTable dt = pdal.GetAvailableLogs();
                _logs = dt.AsEnumerable().Select(r => r.Field<string>("LogID")).ToList();
            }
            this.cmb_EqpName.Items.AddRange(_logs.ToArray());
            this.cmb_EqpFilter.Items.AddRange(_logs.ToArray());
        }

        private void UpdateProductionIDs()
        {
            using (ProductionDAL pdal = new ProductionDAL())
            {
                if (this.cmb_EqpFilter.Text == string.Empty)
                {
                    DataTable dt = pdal.GetProductionIDs();
                    _prods = dt.AsEnumerable().Select(r => r.Field<string>("ProductionName")).ToList();
                }
                else
                {
                    DataTable dt = pdal.GetProductionIDs(cmb_EqpFilter.Text);
                    _prods = dt.AsEnumerable().Select(r => r.Field<string>("ProductionName")).ToList();
                }
                
            }
            this.cmb_ProductionName.Items.Clear();
            this.cmb_ProductionName.Items.AddRange(_prods.ToArray());
        }

        private void cmb_EqpFilter_TextChanged(object sender, EventArgs e)
        {
            UpdateProductionIDs();
        }
    }
}
