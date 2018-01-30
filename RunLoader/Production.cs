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


namespace RunLoader
{
    public partial class Production : Form
    {
        public Productions currProd;
        //private DateTime defaultTime = DateTime.Now;

        public Production()
        {
            InitializeComponent();
            currProd = new Productions();
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
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            currProd.CreateNew(currProd);
        }

        private void ShowProduction()
        {
            txt_Ender.Text = currProd.Ender;
            txt_Starter.Text = currProd.Starter;
            dateTime_StartTime.Text = currProd.StartTime.ToString();
            dateTime_EndTime.Text = currProd.EndTime.ToString();
            cmb_Method.Text = currProd.Method;
            cmb_ProductionName.Text = currProd.ProductionName;
            cmb_Type.Text = currProd.Type;
            txt_Quantity.Text = currProd.Quantity.ToString();
            cmb_EqpName.Text = currProd.EqpName;

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                switch (ctrl.Name.Substring(0,4))
                {
                    case "cmb_":
                    case "txt_":
                        ctrl.ResetText();
                        break;
                    case "date":
                        ctrl.Text = DateTime.Now.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
