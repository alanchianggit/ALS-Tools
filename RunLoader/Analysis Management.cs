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
using BusinessLayer;
using System.IO.Compression;
using DAL;

namespace RunLoader
{
    public partial class Analysis_Management : Form
    {
        private bool FirstConnect = true;
        private static Analysis_Management inst;
        public static Analysis_Management GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)  inst = new Analysis_Management(); 
                return inst;
            }
        }


        List<Sample> listSamples;

        public Analysis_Management()
        {
            InitializeComponent();
        }

        private XElement CSVtoXML()
        {

            string[] lines = File.ReadAllLines(GetPath());

            XElement xml = new XElement("SampleSequence", lines
                .Select(line => new XElement("Item", line.Split(',')
                .Select((column, index) => new XElement("Column" + index, column)))));

            return xml;

        }

        private void Connect()
        {
            try
            {
                if (FirstConnect)
                {
                    try
                    {
                        //Test connection and store in datafactory
                        IDbConnection conn = DataFactory.CreateConnection(this.txt_FileLocation.Text);
                        FirstConnect = false;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                else
                {
                    try
                    {
                        DataFactory.ActiveConn.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Connection Failed due to error '{0}'", ex.Message));
            }
            //conn = DataFactory.CreateConnection(DatabaseType.Oracle,"192.168.1.252:1521/ORCL");
        }


        private void CreateSampleList()
        {
            listSamples = new List<Sample>();
            string strPath = string.Empty;
            if (File.Exists(GetPath()) == true) { strPath = GetPath(); } else { return; }

            using (StreamReader sr = new StreamReader(strPath))
            {
                //Store full text
                string FullText = sr.ReadToEnd().ToString();
                //Parse FullText to string array
                string[] textRows = FullText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string row in textRows)
                {
                    //Parse each line in string array to each column
                    string[] SampleArray = row.Split(new[] { ',' }, StringSplitOptions.None);
                    //Create sample
                    Sample currSample = new Sample(SampleArray);
                    //Add sample to list
                    listSamples.Add(currSample);
                }
            }
        }
        private void cmd_LoadRun_Click(object sender, EventArgs e)
        {
            //CreateSampleList();
            Connect();
            GetMethodFile();
        }

        private void GetMethodFile()
        {
            //Stream unzippedEntrystream;
            using (MethodFile mf = new MethodFile())
            {
                
                string outputPath = Path.GetFullPath(string.Format(@"{0}\{1}", this.txt_Output.Text,this.cmb_RunNum.Text));
                FileInfo fi = new FileInfo(outputPath);
                if (Directory.Exists(outputPath.Replace(fi.Extension, string.Empty))==false)
                {
                    Directory.CreateDirectory(outputPath.Replace(fi.Extension,string.Empty));
                }
                
                string methodfile = mf.GetFileName(this.cmb_Method.Text);
                MemoryStream ms = new MemoryStream(mf[methodfile].FileContent);
                ZipArchive archive = new ZipArchive(ms);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    //NEed to redo file structure
                    entry.ExtractToFile(string.Format(@"{0}\{1}", outputPath.Replace(fi.Extension, string.Empty), entry.FullName), overwrite: true);
                    //unzippedEntrystream = entry.Open();
                }
                
            }
        }
        private string GetPath()
        {
            string path = string.Empty;
            try
            {
                path = Path.GetFullPath(string.Format(@"{0}{1}", this.txt_Directory.Text, this.cmb_RunNum.Text));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return path;
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
