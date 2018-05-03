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
        private static DataSet dataset = new DataSet();
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

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                ofd.Filter = "XML Files|*.xml";
                ofd.FilterIndex = 0;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dataset.ReadXml(ofd.FileName);
                        this.dataGridView1.DataSource = dataset;
                        this.dataGridView1.DataMember = dataset.Tables[0].TableName;
                        this.dataGridView1.ReadOnly = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        
                    }
                }
            }
        }
    }
}

