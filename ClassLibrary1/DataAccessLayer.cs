using System.Data;
using System.Data.OleDb;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net;
using System.Data.SQLite;
using System.Configuration;
using Entity;
using System.Data.SqlTypes;
using System.Reflection;
using System.Collections;

namespace DAL
{
    public enum DatabaseType
    {
        AccessACCDB,
        AccessMDB,
        SQLServer,
        Oracle,
        SQLite
        // any other data source type
    }
    public enum DatabaseFileType
    {
        MDB = DatabaseType.AccessMDB,
        ACCDB = DatabaseType.AccessACCDB,
        DB = DatabaseType.SQLite
    }

    public enum ParameterType
    {
        Integer,
        Char,
        VarChar
        // define a common parameter type set
    }

    public static class DataFactory
    {
        public static IDbConnection ActiveConn { get; set; }
        public static string ActiveConnectionString { get; set; }

        public static DatabaseType dbtype { get; set; }

        private static Dictionary<object, IDbConnection> conns = new Dictionary<object, IDbConnection>();

        //private static DataFactory()  {


        //}

        public static IDbConnection CreateConnection(string DBDataSource)
        {
            //Find extension from file path
            string extension = Path.GetExtension(DBDataSource).Replace(".", string.Empty);
            //Find database file type based on extension
            DatabaseFileType dbfiletype = (DatabaseFileType)Enum.Parse(typeof(DatabaseFileType), extension, true);
            //find database type based on file type
            dbtype = (DatabaseType)dbfiletype;
            //create connection using database type
            return DataFactory.CreateConnection(dbtype, DBDataSource);
        }

        public static IDbConnection CreateConnection(DatabaseType dbtype, string DBDataSource)
        {
            //Set connection string
            ActiveConnectionString = BuildString(DBDataSource, dbtype);
            //Lazy load connection type
            conns.Add(DatabaseType.AccessACCDB, new OleDbConnection());
            conns.Add(DatabaseType.AccessMDB, new OleDbConnection());
            conns.Add(DatabaseType.Oracle, new OracleConnection());
            conns.Add(DatabaseType.SQLServer, new SqlConnection());
            conns.Add(DatabaseType.SQLite, new SQLiteConnection());
            //Get type of connection provided datasource and database type 
            IDbConnection Conn = conns[dbtype];
            //Close connection
            if (Conn.State != ConnectionState.Closed) { Conn.Close(); };
            //update connection string
            Conn.ConnectionString = ActiveConnectionString;
            Conn.Open();
            return ActiveConn = Conn;
        }

        private static string BuildString(string DBDataSource, DatabaseType dbtype)
        {

            switch (dbtype)
            {
                case DatabaseType.AccessACCDB:
                    {
                        OleDbConnectionStringBuilder bs = new OleDbConnectionStringBuilder();
                        bs.DataSource = Path.GetFullPath(DBDataSource).ToString();
                        bs.Provider = "Microsoft.ACE.OLEDB.12.0";
                        bs.OleDbServices = -1;
                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.AccessMDB:
                    {
                        OleDbConnectionStringBuilder bs = new OleDbConnectionStringBuilder();
                        bs.DataSource = Path.GetFullPath(DBDataSource).ToString();
                        bs.Provider = "Microsoft.Jet.OLEDB.4.0";
                        bs.OleDbServices = -1;
                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.Oracle:
                    {
                        OracleConnectionStringBuilder bs = new OracleConnectionStringBuilder();
                        bs.DataSource = DBDataSource;
                        bs.UserID = "sys";
                        bs.Password = "oracle";
                        bs.DBAPrivilege = "SYSDBA";
                        bs.SelfTuning = true;
                        bs.Pooling = true;
                        bs.ConnectionTimeout = 60;
                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.SQLite:
                    {
                        SQLiteConnectionStringBuilder bs = new SQLiteConnectionStringBuilder();
                        bs.DataSource = DBDataSource;
                        bs.Password = "sql";
                        bs.Pooling = true;
                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.SQLServer:
                    {
                        SqlConnectionStringBuilder bs = new SqlConnectionStringBuilder();
                        bs.DataSource = IPAddress.Parse(DBDataSource).ToString();
                        bs.Pooling = true;
                        //bs.UserID = "admin";
                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
                default:
                    {
                        OleDbConnectionStringBuilder bs = new OleDbConnectionStringBuilder();
                        bs.DataSource = DBDataSource;
                        bs.OleDbServices = -1;

                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
            }

            return ActiveConnectionString;
        }

        public static IDbCommand CreateCommand(string CommandText, DatabaseType dbtype, IDbConnection cnn)
        {
            IDbCommand cmd;
            switch (DataFactory.dbtype)
            {
                case DatabaseType.AccessACCDB:
                case DatabaseType.AccessMDB:
                    cmd = new OleDbCommand(CommandText, (OleDbConnection)cnn);
                    break;

                case DatabaseType.SQLServer:
                    cmd = new SqlCommand(CommandText, (SqlConnection)cnn);
                    break;

                case DatabaseType.Oracle:
                    cmd = new OracleCommand(CommandText, (OracleConnection)cnn);
                    break;
                case DatabaseType.SQLite:
                    cmd = new SQLiteCommand(CommandText, (SQLiteConnection)cnn);
                    break;
                default:
                    cmd = new OleDbCommand(CommandText, (OleDbConnection)cnn);
                    break;
            }

            return cmd;
        }


        public static DbDataAdapter CreateAdapter(IDbCommand cmd)
        {
            DbDataAdapter da;

            switch (DataFactory.dbtype)
            {
                case DatabaseType.AccessACCDB:
                case DatabaseType.AccessMDB:
                    da = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;

                case DatabaseType.SQLServer:
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    break;

                case DatabaseType.Oracle:
                    da = new OracleDataAdapter((OracleCommand)cmd);
                    break;
                case DatabaseType.SQLite:
                    da = new SQLiteDataAdapter((SQLiteCommand)cmd);
                    break;
                default:
                    da = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;
            }

            return da;
        }
    }

    public class FileDataDAL
    {
        public DataTable Datatable { get; set; }

        List<FilesEntity> listFE = new List<FilesEntity>();

        public FileDataDAL()
        {
            string strSQL = "SELECT * FROM tbl_Files";
            IDbConnection conn;
            if (DataFactory.ActiveConn != null && DataFactory.ActiveConn.State == ConnectionState.Open)
            {
                conn = DataFactory.ActiveConn;
                IDbCommand cmd = DataFactory.CreateCommand(strSQL, DataFactory.dbtype, conn);
                DbDataAdapter da = DataFactory.CreateAdapter(cmd);
                DataTable dt = new DataTable("FileData");
                da.Fill(dt);
                this.Datatable = dt;
            }
            else
            {
                Console.WriteLine("Connection is no longer active");
            }           
        }



        public List<FilesEntity> GetList()
        {
            
            
            foreach (DataRow dr in this.Datatable.Rows)
            {
                FilesEntity obj = new FilesEntity();
                //Reflection method
                foreach (PropertyInfo pi in typeof(FilesEntity).GetProperties())
                {
                    pi.SetValue(obj, dr[pi.Name]);   
                }
                //Hardcoded method
                //obj.FileName = dr["Filename"].ToString();
                //obj.Size = dr["Size"].Equals(DBNull.Value) ? (int)dr["Size"] : 0 ;
                //obj.DateUploaded = DateTime.Parse(dr["Date Uploaded"].ToString());
                //obj.Type = dr["Type"].ToString();

                //obj.FileContent = (byte[])dr["FileContent"];
                listFE.Add(obj);
            }
            return listFE;
        }
        public void Add(FilesEntity obj)
        {
            if (DataFactory.ActiveConn.State == ConnectionState.Open)
            {
                // get properties from entity class
                PropertyInfo[] PIs = typeof(FilesEntity).GetProperties();
                //Create table of data according to properties so it can be adapted to connection
                var cmd = DataFactory.CreateCommand(string.Empty, DataFactory.dbtype, DataFactory.ActiveConn);
                List<string> columnNames = new List<string>();
                List<string> columnValues = new List<string>();
                foreach (PropertyInfo pi in PIs)
                {
                    IDbDataParameter pm = cmd.CreateParameter();
                    pm.ParameterName = string.Format("@{0}",pi.Name.ToString());
                    pm.Value = pi.GetValue(obj);
                    cmd.Parameters.Add(pm);
                    

                    columnValues.Add(pm.ParameterName);
                    columnNames.Add("[" + pi.Name.ToString() + "]");
                }

                cmd.CommandText = string.Format("INSERT INTO [tbl_Files] ({0}) VALUES ({1});",string.Join(",",columnNames.ToArray()),string.Join(",",columnValues.ToArray()));
                Console.WriteLine(cmd.CommandText);

                cmd.ExecuteNonQuery();
                
                
            }
            else
            {
                
            }
        }
    }


}

