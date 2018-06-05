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

            MessageBox.Show(SettingsLogic.GetFactorySettings("test"));
            SettingsLogic.ChangeFactorySettings("test", "testval" + DateTime.Now.ToString());
            MessageBox.Show(SettingsLogic.GetFactorySettings("test"));


        }

        
    }
}
