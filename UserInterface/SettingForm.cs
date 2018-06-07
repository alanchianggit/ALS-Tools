using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace RunLoader
{
    using BusinessLayer.SettingsLogic;

    public partial class SettingForm : Form
    {

        private static SettingForm inst;
        public static SettingForm GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new SettingForm();
                return inst;
            }
        }

        public SettingForm()
        {
            InitializeComponent();
            
            //SettingsLogic.ChangeFactorySettings("test", "testval" + DateTime.Now.ToString());

            DataTable configurations = SettingsLogic.GetFactorySettings();
            this.dataGridView1.DataSource = configurations;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {

                //cannot get datagridview datasource
                DataTable moddt = (DataTable)this.dataGridView1.DataSource;
                DataTable dt = moddt.GetChanges(DataRowState.Modified);
                if(dt.Rows.Count > 0 )
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        SettingsLogic.ChangeFactorySettings(dr["Key"].ToString(), dr["Value"].ToString());
                    }
                }

                
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}
