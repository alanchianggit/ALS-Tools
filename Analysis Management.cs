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

namespace RunLoader
{
    public partial class Analysis_Management : Form
    {
        public Analysis_Management()
        {
            InitializeComponent();
        }

        private void cmd_LoadRun_Click(object sender, EventArgs e)
        {
            List<Sample> listSamples = new List<Sample>();
            string xxx = string.Format(@"{0}{1}",this.txt_Directory.Text,this.cmb_RunNum.Text);
            using (StreamReader sr = new StreamReader(xxx))
            {
                string FullText = sr.ReadToEnd().ToString();
                string[] textRows = FullText.Split('\n');
                foreach (string row in textRows)
                    {
                        string[] SampleArray = row.Split(',');   
                        if (SampleArray.Length > 0)
                        {
                            Sample currSample = new Sample(SampleArray);
                        // If (currSample.SampleName != "")
                        // {
                            listSamples.Add(currSample);
                        // }
                        }
                        
                    }
                
                 MessageBox.Show(listSamples.Count.ToString());
            }
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
            for (int i = 0; i<10;i++) 
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

    public class Sample 
    {
        private string _Skip;
        private string _SampleName;
        private string _Comment;
        private string _Vial;
        private string _FileName;
        private string _Dilution;
        private string _Level;
        private string _Type;


        public Sample (string[] arr)
        {
            try
            {
                Skip = arr[0]==string.Empty ? string.Empty : arr[0];
                Type = arr[1]==string.Empty ? string.Empty : arr[1];
                Vial = arr[2]==string.Empty ? string.Empty : arr[2];
                FileName = arr[3]==string.Empty ? string.Empty : arr[3];
                SampleName = arr[4]==string.Empty ? string.Empty : arr[4];
                Level = arr[5]==string.Empty ? string.Empty : arr[5];
                Dilution = arr[6]==string.Empty ? string.Empty : arr[6];                
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }
        public string Skip 
        {
            get
            {
                return Skip = _Skip;
            }
            set
            {
                _Skip = value;
            }
        }

        public string Type
        {
            get
            {
                return Type = _Type;
            }

            set
            {
                _Type = value;
            }
        } 


        public string SampleName 
        {
            get
            {
                return SampleName = _SampleName;
            }
            set
            {
                _SampleName = value;
            }
        }

        public string Vial
        {
            get
            {
                return Vial = _Vial;
            }
            set
            {
                _Vial = value;
            }
        }

        public string FileName 
        {
            get
            {
                return FileName = _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        public string Dilution
        {
            get
            {
                return Dilution = _Dilution;
            }
            set
            {
                _Dilution = value;
            }
        }

        public string Level
        {
            get
            {
                return Level = _Level;
            }
            set
            {
                _Level = value;
            }
        }
    }
}
