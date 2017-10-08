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
    }
}

namespace DAL
{
    public class CustomersData
    {
        public DataTable GetCustomers()
        {
            string ConnectionString =
               ConfigurationSettings.AppSettings
               ["ConnectionString"];
            DatabaseType dbtype =
               (DatabaseType)Enum.Parse
               (typeof(DatabaseType),
               ConfigurationSettings.AppSettings
               ["DatabaseType"]);

            IDbConnection cnn =
               DataFactory.CreateConnection
               (ConnectionString, dbtype);

            string cmdString = "SELECT CustomerID" +
               ",CompanyName,ContactName FROM Customers";

            IDbCommand cmd =
               DataFactory.CreateCommand(
               cmdString, dbtype, cnn);

            DbDataAdapter da =
               DataFactory.CreateAdapter(cmd, dbtype);

            DataTable dt = new DataTable("Customers");

            da.Fill(dt);

            return dt;
        }
        
    }
}

