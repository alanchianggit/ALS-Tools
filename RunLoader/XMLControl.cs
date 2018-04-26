using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALSTools
{
    public partial class XMLControl : Form
    {
        private static XMLControl inst;
        private static DataSet dataset;
        private const string path = @"\\alvncws008\groups\minerals\spectroscopy\icp-ms\ms logs\Performance report RAW\Agilent 32\(2018-04-18) PerformanceReport.xml";
        public static XMLControl GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new XMLControl();
                return inst;
            }
        }
        public XMLControl()
        {
            InitializeComponent();
        }

        private DataSet LoadXML()
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(path);
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }



        }

    }
}

