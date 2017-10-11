using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using DAL;
using Entity;
using BusinessLayer;
using System.Collections.Generic;

namespace RunLoader
{
    public partial class FileAccessForm : Form
    {
        public FileAccessForm()
        {
            InitializeComponent();
        }

        private enum OrderBy
        {
            FileName = 1,
            Size = 2,
            Type = 3,
            DateUploaded = 4
        }

        public IDbConnection conn { get; private set; }

        private const string FileDialogFilter = "Microsoft Access (*.accdb , *.mdb)|*.accdb;*.mdb|SQLite (*.db)|*.db|All Files (*.*)|*.*";
        private const string DATA_FILE_FILTER = "All Files (*.*)|*.*";

        private void btn_BrowseAccess_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AutoUpgradeEnabled = true;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dlg.Filter = FileDialogFilter;
            if (txt_FileLocation.Text.Length > 0)
            {
                dlg.FileName = txt_FileLocation.Text;
            }
            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            txt_FileLocation.Text = dlg.FileName;
            //PopulateListView();
        }

        private void UpdateStatusConsole(string message)
        {
            this.BeginInvoke
                        (new Action(() =>
                        {
                            //add message to status console
                            this.txt_status.AppendText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + " - " + message);
                            //add new line to status console
                            this.txt_status.AppendText(Environment.NewLine);
                        }
                        ));

        }


        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //Test connection and store in datafactory
            conn = DataFactory.CreateConnection(this.txt_FileLocation.Text);
            if (conn.State == ConnectionState.Open)
            {
                UpdateStatusConsole(string.Format("Connection opened with {0}", this.txt_FileLocation.Text));
            }

            //
            
            //conn = DataFactory.CreateConnection(DatabaseType.Oracle,"192.168.1.252:1521/ORCL");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Files obj = new Files();
            DataTable oFilesdt = obj.getFileDataTable();
            List<FilesEntity> oFiles = obj.getFilesList();
            DataTable dt = oFilesdt;
            dt.Columns.Remove("FileContent");
            dataGridView1.DataSource = dt;
            DataGridViewCheckBoxColumn dgc = new DataGridViewCheckBoxColumn();
            dgc.Name = "CheckColumn";
            dgc.ReadOnly = false;

            dataGridView1.Columns.Add(dgc);
        }
    }
}
