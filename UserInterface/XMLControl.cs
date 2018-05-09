using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace ALSTools
{
    public partial class XMLControl : BaseOperationForm
    {
        private static XMLControl inst;
        private static DataSet dataset;
        //Point mouseDownPoint = Point.Empty;

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
            dataset = new DataSet();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Clear();
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
            }
            CreateDataGridViews();
        }

        private void CreateDataGridViews()
        {
            foreach (DataTable dt in dataset.Tables)
            {
                TabPage tp = new TabPage(dt.TableName);
                DataGridView dgv = new DataGridView();
                tp.Controls.Add(dgv);
                dgv.Location = this.tabControl1.Location;
                Size newsize = new Size(this.tabControl1.Width - 50, this.tabControl1.Height - 50);
                dgv.Size = newsize;
                dgv.DataSource = dataset;
                dgv.DataMember = dt.TableName;
                dgv.ReadOnly = true;
                dgv.Show();
                this.tabControl1.TabPages.Add(tp);

            }

        }
        
    }


}


