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

//using System.Data.OracleClient;


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
        MDB=DatabaseType.AccessMDB,
        ACCDB=DatabaseType.AccessACCDB,
        DB=DatabaseType.SQLite
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
        private static Dictionary<object, IDbConnection> conns = new Dictionary<object, IDbConnection>();
        
        static DataFactory()
        {
            conns.Add(DatabaseType.AccessACCDB, new OleDbConnection());
            conns.Add(DatabaseType.AccessMDB, new OleDbConnection());
            conns.Add(DatabaseType.Oracle, new OracleConnection());
            conns.Add(DatabaseType.SQLServer, new SqlConnection());
            conns.Add(DatabaseType.SQLite, new SQLiteConnection());
        }
        public static IDbConnection CreateConnection(string DBDataSource)
        {
            //Find extension from file path
            string extension = Path.GetExtension(DBDataSource).Replace(".", string.Empty);
            //Find database file type based on extension
            DatabaseFileType dbfiletype = (DatabaseFileType)Enum.Parse(typeof(DatabaseFileType), extension,true);
            //find database type based on file type
            DatabaseType dbtype = (DatabaseType)dbfiletype;

            

            //create connection using database type
            return DataFactory.CreateConnection(dbtype,DBDataSource);
        }

        public static IDbConnection CreateConnection(DatabaseType dbtype, string DBDataSource)
        {
            //Get type of connection from provided datasource and database type 
            IDbConnection Conn = conns[dbtype];
            if (Conn.State != ConnectionState.Closed) { Conn.Close(); }
            Conn.ConnectionString = BuildString(DBDataSource, dbtype);
            Conn.Open();
            return Conn;
        }

        

        private static string BuildString(string DBDataSource,DatabaseType dbtype)
        {
            string connectionstring;
            switch(dbtype)
            {
                case DatabaseType.AccessACCDB:
                    {
                        OleDbConnectionStringBuilder bs = new OleDbConnectionStringBuilder();
                        bs.DataSource = Path.GetFullPath(DBDataSource).ToString();
                        bs.Provider = "Microsoft.ACE.OLEDB.12.0";
                        bs.OleDbServices = -1;
                        connectionstring = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.AccessMDB:
                    {
                        OleDbConnectionStringBuilder bs = new OleDbConnectionStringBuilder();
                        bs.DataSource = Path.GetFullPath(DBDataSource).ToString() ;
                        bs.Provider = "Microsoft.Jet.OLEDB.4.0";
                        bs.OleDbServices = -1;
                        connectionstring = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.Oracle:
                    {
                        OracleConnectionStringBuilder bs = new OracleConnectionStringBuilder();
                        bs.DataSource = IPAddress.Parse(DBDataSource).ToString();
                        bs.SelfTuning = true;
                        bs.Pooling = true;
                        bs.ConnectionTimeout = 60;
                        connectionstring = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.SQLite:
                    {
                        SQLiteConnectionStringBuilder bs = new SQLiteConnectionStringBuilder();
                        bs.DataSource = Path.GetFullPath(DBDataSource);
                        bs.Pooling = true;
                        connectionstring = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.SQLServer:
                    {
                        SqlConnectionStringBuilder bs = new SqlConnectionStringBuilder();
                        bs.DataSource = IPAddress.Parse(DBDataSource).ToString();
                        bs.Pooling = true;
                        bs.UserID = "admin";
                        connectionstring = bs.ConnectionString;
                    }
                    break;
                default:
                    {
                        OleDbConnectionStringBuilder bs = new OleDbConnectionStringBuilder();
                        bs.DataSource = DBDataSource;
                        bs.OleDbServices = -1;
                        
                        connectionstring = bs.ConnectionString;
                    }
                    break;
            }

            return connectionstring;
        }

        //public static IDbCommand CreateCommand(string CommandText, DatabaseType dbtype,IDbConnection cnn)
        //{
        //    IDbCommand cmd;
        //    switch (dbtype)
        //    {
        //        case DatabaseType.Access:
        //            cmd = new OleDbCommand(CommandText, (OleDbConnection)cnn);
        //            break;

        //        case DatabaseType.SQLServer:
        //            cmd = new SqlCommand(CommandText, (SqlConnection)cnn);
        //            break;

        //        case DatabaseType.Oracle:
        //            cmd = new OracleCommand(CommandText, (OracleConnection)cnn);
        //            break;
        //        default:
        //            cmd = new SqlCommand(CommandText, (SqlConnection)cnn);
        //            break;
        //    }

        //    return cmd;
        //}


        //public static DbDataAdapter CreateAdapter(IDbCommand cmd, DatabaseType dbtype)
        //{
        //    DbDataAdapter da;
        //    switch (dbtype)
        //    {
        //        case DatabaseType.Access:
        //            da = new OleDbDataAdapter((OleDbCommand)cmd);
        //            break;

        //        case DatabaseType.SQLServer:
        //            da = new SqlDataAdapter((SqlCommand)cmd);
        //            break;

        //        case DatabaseType.Oracle:
        //            da = new OracleDataAdapter((OracleCommand)cmd);
        //            break;

        //        default:
        //            da = new SqlDataAdapter((SqlCommand)cmd);
        //            break;
        //    }

        //    return da;
        //}
    }
}


namespace DatabaseManagement
{
    class MSAccess
    {
        private OleDbConnectionStringBuilder OledbConString
        {
            get
            {

                OleDbConnectionStringBuilder strAccess = new OleDbConnectionStringBuilder();
                string accessProvider = "Microsoft.ACE.OLEDB.12.0";
                string accessDataSource = @"C:\Users\Alan\Documents\Database1.accdb";
                strAccess.Provider = accessProvider;
                strAccess.OleDbServices = -1;
                strAccess.DataSource = accessDataSource;
                return strAccess;
            }
        }
    }
}
