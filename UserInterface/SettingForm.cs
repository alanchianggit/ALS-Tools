using System;
using System.Data;
using System.Windows.Forms;



namespace RunLoader
{
    using BusinessLayer.SettingsLogic;

    public partial class SettingForm : Form
    {
        private static BindingSource settingsBS = new BindingSource();
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

            DataSet ds = new DataSet();
            ds.Tables.Add(SettingsLogic.GetFactorySettings());
            settingsBS.DataSource = ds;
            settingsBS.DataMember = "Configurations";
            this.dataGridView1.DataSource = settingsBS;

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            try
            {
                BindingSource bs = (BindingSource)dgv.DataSource;
                string targetTblName = bs.DataMember.ToString();

                DataRowView obj = (DataRowView)bs.Current;
                DataRow currDR = obj.Row;

                SettingsLogic.SetFactorySettings(currDR["Keys"].ToString(), currDR["Values"].ToString());

            }
            catch (NullReferenceException nullEX)
            {
                MessageBox.Show(nullEX.Message);
            }


        }
    }
}
