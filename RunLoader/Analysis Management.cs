using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using Entity;


namespace RunLoader
{
    public partial class Analysis_Management : Form
    {
        List<Sample> listSamples = new List<Sample>();

        public Analysis_Management()
        {
            InitializeComponent();
        }

        private void testproc()
        {

            var lines = File.ReadAllLines(string.Format(@"{0}{1}", this.txt_Directory.Text, this.cmb_RunNum.Text));

            XElement xml = new XElement("SampleSequence", lines
                .Select(line => new XElement("Item", line.Split(',')
                .Select((column, index) => new XElement("Column" + index, column)))));

            xml.Save(@"D:\xmlout.xml");
           
        }

        private void testproc2()
        {
            
            string strPath = string.Format(@"{0}{1}", this.txt_Directory.Text, this.cmb_RunNum.Text);
            using (StreamReader sr = new StreamReader(strPath))
            {
                string FullText = sr.ReadToEnd().ToString();
                string[] textRows = FullText.Split('\n');
                foreach (string row in textRows)
                {
                    string[] SampleArray = row.Split(',');
                    if (SampleArray.Length > 5)
                    {
                        Sample currSample = new Sample(SampleArray);
                        listSamples.Add(currSample);
                    }
                }
            }
        }
        private void cmd_LoadRun_Click(object sender, EventArgs e)
        {
            testproc();
            listSamples = new List<Sample>();
            testproc2();
        }

        private void SerializeDataSet(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataSet));
            // Creates a DataSet; adds a table, column, and ten rows. 
            DataSet ds = new DataSet("myDataSet");
            DataTable t = new DataTable("table1");
            DataColumn c = new DataColumn("thing");
            t.Columns.Add(c);
            ds.Tables.Add(t);
            DataRow r;
            for (int i = 0; i < 10; i++)
            {
                r = t.NewRow();
                r[0] = "Thing " + i;
                t.Rows.Add(r);
                TextWriter writer = new StreamWriter(filename);
                ser.Serialize(writer, ds);
                writer.Close();
            }

        }
    }

    
}
