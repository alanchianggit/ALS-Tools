using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml.Schema;
using BusinessLayer;
using DAL.Factory;
using Entity;


namespace ALSTools
{
    using BusinessLayer.SettingsLogic;
    public partial class Analysis_Management : Form
    {
        DataSet ds = new DataSet();
        private string filename = string.Empty;
        XmlSchemaSet schemaSet = new XmlSchemaSet();

        List<WebviewSampleParameterEntity> listSamples;

        private bool FirstConnect = true;
        private static Analysis_Management inst;
        public static Analysis_Management GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed) inst = new Analysis_Management();
                return inst;
            }
        }


        

        public Analysis_Management()
        {
            
            InitializeComponent();
            this.txt_FileLocation.Text = SettingsLogic.GetFactorySetting("DbPath");
            //this.txt_Output.Text = @"\\alvncws008\groups\minerals\spectroscopy\userfiles\alan\data\";
            this.cmb_Method.Text = "ME-MS41i";
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
                        IDbConnection conn = DataLayer.CreateConnection(this.txt_FileLocation.Text);
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
                        DataLayer.ActiveConn.Open();
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


        private List<WebviewSampleParameterEntity> GetSampleList()
        {
            listSamples = new List<WebviewSampleParameterEntity>();
            string strPath = string.Empty;
            if (File.Exists(GetPath()) == true) { strPath = GetPath(); } else {return listSamples; }

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
                    WebviewSampleParameterEntity currSample = new WebviewSampleParameterEntity(SampleArray);
                    //Add sample to list
                    listSamples.Add(currSample);
                }
            }
            return listSamples;
        }
        private void cmd_LoadRun_Click(object sender, EventArgs e)
        {
            GetSampleList();
            Connect();
            GetMethodFile();
            ReadXML();
        }

        private void ReadXML()
        {
            using (AnalysisManagementLogic AML = new AnalysisManagementLogic())
            {
                ds = AML.DataSetReadXML(filename);
            }
            dataGridView1.DataSource = ds.Tables["SampleParameter"];
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }


        private void GetMethodFile()
        {
            //Stream unzippedEntrystream;
            using (MethodFile mf = new MethodFile())
            {
                //check if file exists, then store path as string
                string filepath = File.Exists(this.cmb_RunNum.Text) ? Path.GetFullPath(this.cmb_RunNum.Text) : string.Empty;

                FileInfo fi = new FileInfo(filepath);
                //If output directory does not contain new method, then create it
                if (Directory.Exists(Path.Combine(this.txt_Output.Text, fi.Name.Replace(fi.Extension, ".b"))) == false)
                {
                    Directory.CreateDirectory(Path.Combine(this.txt_Output.Text, fi.Name.Replace(fi.Extension, ".b")));
                }
                string outputpath = Path.Combine(this.txt_Output.Text, fi.Name.Replace(fi.Extension, ".b"));
                //query method file based on correlation
                string methodfile = mf.GetFileName(this.cmb_Method.Text);
                //Load zip into memory
                MemoryStream ms = new MemoryStream(mf[methodfile].FileContent);
                //load content as zip archive
                ZipArchive archive = new ZipArchive(ms);
                // iterate through each file
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    //Create fullpath for each file
                    string fullPath = Path.Combine(outputpath, entry.FullName);
                    //if directory doesn't exist
                    if (!Directory.Exists(fullPath))
                    {
                        //create directory after replacing entry name from full path
                        Directory.CreateDirectory(fullPath.Replace(entry.Name, string.Empty));
                        // extract the file
                        entry.ExtractToFile(fullPath, overwrite: true);
                    }
                    else
                    {
                        //extract file when directory exists already
                        entry.ExtractToFile(fullPath, overwrite: true);
                    }
                }
                filename = Path.Combine(outputpath, @"Method\AcqMethod.xml");

            }
        }
        private string GetPath()
        {
            string path = string.Empty;
            try
            {
                path = File.Exists(this.cmb_RunNum.Text) ? Path.GetFullPath(this.cmb_RunNum.Text) : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return path;
        }



        private void btn_LoadRunNum_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = "CSV files|*.csv";
                ofd.InitialDirectory = @"C:\Agilent\ICPMH\1\Sequence\";
                ofd.ReadOnlyChecked = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.cmb_RunNum.Text = ofd.FileName;
                }
            }
        }

        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            using (AnalysisManagementLogic AML = new AnalysisManagementLogic())
            {
                AML.ImportSampleListToDataTable(ds, listSamples);
            }
            //commit changes to dataset
            ds.AcceptChanges();

            ds.WriteXml(filename.Replace(".xml", ".txt"));

        }

        //private XElement CSVtoXML()
        //{

        //    string[] lines = File.ReadAllLines(GetPath());

        //    XElement xml = new XElement("SampleSequence", lines
        //        .Select(line => new XElement("Item", line.Split(',')
        //        .Select((column, index) => new XElement("Column" + index, column)))));

        //    return xml;

        //}

        //private void SerializeDataSet(string filename)
        //{
        //    XmlSerializer ser = new XmlSerializer(typeof(DataSet));
        //    // Creates a DataSet; adds a table, column, and ten rows. 
        //    DataSet ds = new DataSet("myDataSet");
        //    DataTable t = new DataTable("table1");
        //    DataColumn c = new DataColumn("thing");
        //    t.Columns.Add(c);
        //    ds.Tables.Add(t);
        //    DataRow r;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        r = t.NewRow();
        //        r[0] = "Thing " + i;
        //        t.Rows.Add(r);
        //        TextWriter writer = new StreamWriter(filename);
        //        ser.Serialize(writer, ds);
        //        writer.Close();
        //    }

        //}
    }


}


