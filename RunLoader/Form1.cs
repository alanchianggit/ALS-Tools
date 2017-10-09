using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using DAL;

namespace RunLoader
{
    public partial class Form1 : Form
    {
        public Form1()
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

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            conn = DataFactory.CreateConnection(this.txt_FileLocation.Text);
        }
    }
}

//namespace DAL
//{
//    public class CustomersData
//    {
//        public DataTable GetCustomers()
//        {
//            string ConnectionString =
//               ConfigurationSettings.AppSettings
//               ["ConnectionString"];
//            DatabaseType dbtype =
//               (DatabaseType)Enum.Parse
//               (typeof(DatabaseType),
//               ConfigurationSettings.AppSettings
//               ["DatabaseType"]);

//            IDbConnection cnn =
//               DataFactory.CreateConnection
//               (ConnectionString, dbtype);

//            string cmdString = "SELECT CustomerID" +
//               ",CompanyName,ContactName FROM Customers";

//            IDbCommand cmd =
//               DataFactory.CreateCommand(
//               cmdString, dbtype, cnn);

//            DbDataAdapter da =
//               DataFactory.CreateAdapter(cmd, dbtype);

//            DataTable dt = new DataTable("Customers");

//            da.Fill(dt);

//            return dt;
//        }
        
//    }
//}

