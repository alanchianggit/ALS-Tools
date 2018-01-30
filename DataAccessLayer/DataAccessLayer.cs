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
using Entity;
using System.Reflection;
using System.Linq;

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

    public class DataFactory : IDisposable
    {
        private const string defaultDB = @"C:\Users\Alan\Documents\BackEnd1.accdb";
        public static DataFactory Instance = new DataFactory();
        public void Reset()
        {
            Instance = new DataFactory();
        }

        public DataFactory()
        {
            //CreateConnection(defaultDB);
        }

        public static IDbConnection ActiveConn;/*{ get; set; }*/
        public static string ActiveConnectionString { get; set; }

        public static DatabaseType dbtype { get; set; }

        private static Dictionary<object, IDbConnection> conns = new Dictionary<object, IDbConnection>();

        //private static DataFactory()  {


        //}
        //Create Connection based on file extension type: e.g. *.db for SQLite, *.MDB or *.ACCDB for M$ Access
        public static IDbConnection CreateConnection()
        {
            //Find extension from file path
            string extension = Path.GetExtension(defaultDB).Replace(".", string.Empty);
            //Find database file type based on extension
            DatabaseFileType dbfiletype = (DatabaseFileType)Enum.Parse(typeof(DatabaseFileType), extension, true);
            //find database type based on file type
            dbtype = (DatabaseType)dbfiletype;
            //create connection using database type
            return DataFactory.CreateConnection(dbtype, defaultDB);

        }
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
            if (conns.Count == 0)
            {
                conns.Add(DatabaseType.AccessACCDB, new OleDbConnection());
                conns.Add(DatabaseType.AccessMDB, new OleDbConnection());
                conns.Add(DatabaseType.Oracle, new OracleConnection());
                conns.Add(DatabaseType.SQLServer, new SqlConnection());
                conns.Add(DatabaseType.SQLite, new SQLiteConnection());
            }

            //Get type of connection provided datasource and database type 
            IDbConnection Conn = conns[dbtype];
            //Close connection
            if (Conn.State != ConnectionState.Closed) { Conn.Close(); };
            //update connection string if not exist
            if (ActiveConnectionString != Conn.ConnectionString) { Conn.ConnectionString = ActiveConnectionString; }
            //Open connection
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
                        bs.OleDbServices = -13;
                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
                case DatabaseType.AccessMDB:
                    {
                        OleDbConnectionStringBuilder bs = new OleDbConnectionStringBuilder();
                        bs.DataSource = Path.GetFullPath(DBDataSource).ToString();
                        bs.Provider = "Microsoft.Jet.OLEDB.4.0";
                        bs.OleDbServices = -13;
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

        public static IDbCommand CreateCommand(string CommandText)
        {
            IDbCommand cmd;
            switch (DataFactory.dbtype)
            {
                case DatabaseType.AccessACCDB:
                case DatabaseType.AccessMDB:
                    cmd = new OleDbCommand(CommandText, (OleDbConnection)DataFactory.ActiveConn);
                    break;

                case DatabaseType.SQLServer:
                    cmd = new SqlCommand(CommandText, (SqlConnection)DataFactory.ActiveConn);
                    break;

                case DatabaseType.Oracle:
                    cmd = new OracleCommand(CommandText, (OracleConnection)DataFactory.ActiveConn);
                    break;
                case DatabaseType.SQLite:
                    cmd = new SQLiteCommand(CommandText, (SQLiteConnection)DataFactory.ActiveConn);
                    break;
                default:
                    cmd = new OleDbCommand(CommandText, (OleDbConnection)DataFactory.ActiveConn);
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

        public void Dispose()
        {
            if (ActiveConn != null)
            {
                ActiveConn.Dispose();
                ActiveConn = null;
                Instance.Reset();
            }
            //((IDisposable)Instance).Dispose();
        }
    }

    public class EventsDAL : IDisposable
    {


        public void Add(LogEvent LE)
        {
            if (DataFactory.ActiveConn.State != ConnectionState.Open) { DataFactory.ActiveConn.Open(); }
            try
            {

                using (IDbCommand cmdcheck = DataFactory.CreateCommand(string.Empty))
                {
                    IDbDataParameter pmchk = cmdcheck.CreateParameter();

                    pmchk = cmdcheck.CreateParameter();
                    pmchk.ParameterName = "@LogName";
                    pmchk.Value = LE.LogName;
                    cmdcheck.Parameters.Add(pmchk);

                    pmchk = cmdcheck.CreateParameter();
                    pmchk.ParameterName = "@TimeCreated";
                    pmchk.Value = LE.TimeCreated;
                    cmdcheck.Parameters.Add(pmchk);

                    //string strCheckExist = string.Format("SELECT [FileName] FROM [tbl_Files] WHERE [FileName] = {0} OR [FileContent] = {1}", "@FileName", "@FileContent");
                    string strCheckExist = string.Format("SELECT [ID] FROM [tbl_Events] WHERE [LogName] = {0} AND [TimeCreated] = {1}", "@LogName", "@TimeCreated");
                    cmdcheck.CommandText = strCheckExist;
                    var result = cmdcheck.ExecuteScalar();

                    if (result == null)
                    {
                        // get properties from entity class
                        PropertyInfo[] PIs = typeof(LogEvent).GetProperties();

                        //Create table of data according to properties so it can be adapted to connection
                        IDbCommand cmdInsert = DataFactory.CreateCommand(string.Empty);

                        //create new Lists for colum names and parameters
                        List<string> InsertColumnNames = new List<string>();
                        List<string> InsertColumnValues = new List<string>();
                        //Iterate through each prorperty to coerce a parameter
                        foreach (PropertyInfo pi in PIs)
                        {
                            //Create new parameter object
                            IDbDataParameter pm = cmdInsert.CreateParameter();
                            //Set Parameter name from property name
                            pm.ParameterName = string.Format("@{0}", pi.Name.ToString());
                            //Set value from property of object
                            pm.Value = pi.GetValue(LE);
                            //Add parameter to command
                            cmdInsert.Parameters.Add(pm);
                            //Add to list for generating string
                            InsertColumnValues.Add(pm.ParameterName);
                            InsertColumnNames.Add("[" + pi.Name.ToString() + "]");
                            //clean up
                            pm = null;
                        }
                        string strInsert = string.Format("INSERT INTO [tbl_Events] ({0}) VALUES ({1});", string.Join(",", InsertColumnNames.ToArray()), string.Join(",", InsertColumnValues.ToArray()));
                        cmdInsert.CommandText = strInsert;
                        cmdInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        result = null;
                    }

                    //Clean up

                    cmdcheck.Dispose();
                    pmchk = null;
                    strCheckExist = string.Empty;
                    LE = null;
                    DataFactory.ActiveConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EventsDAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }


    public class FileDataDAL : IDisposable
    {
        public DataTable Datatable = new DataTable();

        private Dictionary<string, string> dictFileName = new Dictionary<string, string>();
        private Dictionary<string, FileEntity> dictFE = new Dictionary<string, FileEntity>();
        private List<FileEntity> listFE = new List<FileEntity>();

        public FileDataDAL()
        {
            if (DataFactory.ActiveConn != null)
            {
                DataFactory.CreateConnection();
            }
            
        }

        private DataTable GetDatatable()
        {
            string strSQL = "SELECT * FROM tbl_Files";
            if (DataFactory.ActiveConn != null && DataFactory.ActiveConn.State != ConnectionState.Open) { DataFactory.ActiveConn.Open(); }
            try
            {
                //conn = DataFactory.ActiveConn;
                IDbCommand cmd = DataFactory.CreateCommand(strSQL);
                using (DbDataAdapter da = DataFactory.CreateAdapter(cmd))
                {
                    //DataTable dt = new DataTable("FileData");
                    da.Fill(this.Datatable);
                    //this.Datatable = dt;
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Datatable;

        }

        public string GetFileName(string method)
        {
            string fn = string.Empty;
            string strSQL = "SELECT tbl_Files.FileName FROM tbl_Method INNER JOIN tbl_Files ON tbl_Method.FileName = tbl_Files.FileName WHERE(((tbl_Method.Method) = @Method));";
            using (IDbCommand cmd = DataFactory.CreateCommand(strSQL))
            {
                IDbDataParameter pm = cmd.CreateParameter();
                pm.ParameterName = "@Method";
                pm.Value = method;
                cmd.Parameters.Add(pm);
                fn = cmd.ExecuteScalar().ToString();
            }


            return fn;
        }


        public Dictionary<string, string> GetNameList()
        {
            dictFileName = new Dictionary<string, string>();
            using (this.Datatable = GetDatatable())
            {
                foreach (DataRow dr in this.Datatable.Rows)
                {
                    using (FileEntity obj = new FileEntity())
                    {
                        //Reflection method
                        obj.FileName = dr["FileName"].ToString();
                        dictFileName.Add(obj.FileName, obj.FileName);
                    }
                }
            }

            return dictFileName;
        }

        public Dictionary<string, FileEntity> GetList()
        {
            dictFE = new Dictionary<string, FileEntity>();
            using (this.Datatable = GetDatatable())
            {
                foreach (DataRow dr in this.Datatable.Rows)
                {
                    FileEntity obj = new FileEntity();

                    //Reflection method
                    foreach (PropertyInfo pi in typeof(FileEntity).GetProperties())
                    {
                        pi.SetValue(obj, dr[pi.Name]);
                    }
                    dictFE.Add(obj.FileName, obj);


                }
            }
            return dictFE;
        }

        public void Add(FileEntity obj)
        {
            if (DataFactory.ActiveConn.State != ConnectionState.Open) { DataFactory.ActiveConn.Open(); }
            try
            {

                using (IDbCommand cmdcheck = DataFactory.CreateCommand(string.Empty))
                {
                    IDbDataParameter pmchk = cmdcheck.CreateParameter();
                    pmchk.ParameterName = "@FileName";
                    pmchk.Value = obj.FileName;
                    cmdcheck.Parameters.Add(pmchk);

                    pmchk = cmdcheck.CreateParameter();
                    pmchk.ParameterName = "@DateModified";
                    pmchk.Value = obj.DateModified;
                    cmdcheck.Parameters.Add(pmchk);


                    //string strCheckExist = string.Format("SELECT [FileName] FROM [tbl_Files] WHERE [FileName] = {0} OR [FileContent] = {1}", "@FileName", "@FileContent");
                    string strCheckExist = string.Format("SELECT [FileName] FROM [tbl_Files] WHERE [FileName] = {0}", "@FileName", "@DateModified");
                    cmdcheck.CommandText = strCheckExist;
                    var result = cmdcheck.ExecuteScalar();

                    if (result == null)
                    {
                        // get properties from entity class
                        PropertyInfo[] PIs = typeof(FileEntity).GetProperties();

                        //Create table of data according to properties so it can be adapted to connection
                        IDbCommand cmdInsert = DataFactory.CreateCommand(string.Empty);

                        //create new Lists for colum names and parameters
                        List<string> InsertColumnNames = new List<string>();
                        List<string> InsertColumnValues = new List<string>();
                        //Iterate through each prorperty to coerce a parameter
                        foreach (PropertyInfo pi in PIs)
                        {
                            //Create new parameter object
                            IDbDataParameter pm = cmdInsert.CreateParameter();
                            //Set Parameter name from property name
                            pm.ParameterName = string.Format("@{0}", pi.Name.ToString());
                            //Set value from property of object
                            pm.Value = pi.GetValue(obj);
                            //Add parameter to command
                            cmdInsert.Parameters.Add(pm);
                            //Add to list for generating string
                            InsertColumnValues.Add(pm.ParameterName);
                            InsertColumnNames.Add("[" + pi.Name.ToString() + "]");
                            //clean up
                            pm = null;
                        }
                        string strInsert = string.Format("INSERT INTO [tbl_Files] ({0}) VALUES ({1});", string.Join(",", InsertColumnNames.ToArray()), string.Join(",", InsertColumnValues.ToArray()));
                        cmdInsert.CommandText = strInsert;
                        cmdInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        result = null;
                    }

                    //Clean up

                    cmdcheck.Dispose();
                    pmchk = null;
                    strCheckExist = string.Empty;
                    obj = null;
                    DataFactory.ActiveConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dictFE = null;
                    dictFileName = null;
                    listFE = null;
                    Datatable.Dispose();
                    //((IDisposable)Datatable).Dispose();
                    DataFactory.ActiveConn.Close();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FileDataDAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class ProductionDAL : IDisposable
    {

        public ProductionDAL()
        {
            if (DataFactory.ActiveConn == null)
            {
                DataFactory.CreateConnection();
            }
        }

        public ProductionEntity RetrieveProductionData(ProductionEntity pe)
        {
            return pe = RetrieveProductionData(pe.ProductionName);

        }

        public ProductionEntity RetrieveProductionData(string productionName)
        {
            ProductionEntity pe = new ProductionEntity();
            pe.ProductionName = productionName;

            DataTable dt = new DataTable();
            dt = GetDataTable(pe);
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                //Reflection method
                foreach (PropertyInfo pi in typeof(ProductionEntity).GetProperties())
                {
                    if (!DBNull.Value.Equals(dr[pi.Name]))
                    {
                        pi.SetValue(pe, dr[pi.Name]);
                    }
                    else
                    {
                        //do nothing (i.e. not set value)
                    }
                        
                    
                }
            }
            else
            {
                Console.WriteLine("Multiple Record. Abort.");
            }
            return pe;


            
        }
        private DataTable GetDataTable(ProductionEntity prod)
        {
            DataTable dt = new DataTable();
            string strSQL = string.Format("SELECT * FROM [tbl_Production] WHERE [ProductionName]='{0}'",prod.ProductionName);
            if (DataFactory.ActiveConn != null && DataFactory.ActiveConn.State != ConnectionState.Open) { DataFactory.ActiveConn.Open(); }
            try
            {
                //conn = DataFactory.ActiveConn;
                IDbCommand cmd = DataFactory.CreateCommand(strSQL);
                using (DbDataAdapter da = DataFactory.CreateAdapter(cmd))
                {
                    //DataTable dt = new DataTable("FileData");
                    da.Fill(dt);
                    //this.Datatable = dt;
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;

        }
        public void Add(ProductionEntity obj)
        {
            if (DataFactory.ActiveConn.State != ConnectionState.Open) { DataFactory.ActiveConn.Open(); }
            try
            {

                using (IDbCommand cmdcheck = DataFactory.CreateCommand(string.Empty))
                {
                    IDbDataParameter pmchk = cmdcheck.CreateParameter();
                    pmchk.ParameterName = "@ProductionName";
                    pmchk.Value = obj.ProductionName;
                    cmdcheck.Parameters.Add(pmchk);
                    
                    //string strCheckExist = string.Format("SELECT [FileName] FROM [tbl_Files] WHERE [FileName] = {0} OR [FileContent] = {1}", "@FileName", "@FileContent");
                    string strCheckExist = string.Format("SELECT [ProductionName] FROM [tbl_Production] WHERE [ProductionName] = {0}", "@ProductionName");
                    cmdcheck.CommandText = strCheckExist;
                    var result = cmdcheck.ExecuteScalar();

                    if (result == null)
                    {
                        // get properties from entity class
                        PropertyInfo[] PIs = typeof(ProductionEntity).GetProperties();

                        //Create table of data according to properties so it can be adapted to connection
                        IDbCommand cmdInsert = DataFactory.CreateCommand(string.Empty);

                        //create new Lists for colum names and parameters
                        List<string> InsertColumnNames = new List<string>();
                        List<string> InsertColumnValues = new List<string>();

                        //create exception fields list
                        List<string> ExceptionFields = new List<string>();
                        ExceptionFields.Add("ID");

                        //Iterate through each prorperty to coerce a parameter
                        foreach (PropertyInfo pi in PIs)
                        {
                            if (ExceptionFields.Exists(e => !e.Contains(pi.Name)) && pi.GetValue(obj) != null)
                            {
                                //Create new parameter object
                                IDbDataParameter pm = cmdInsert.CreateParameter();
                                //Set Parameter name from property name
                                pm.ParameterName = string.Format("@{0}", pi.Name.ToString());
                                //Set value from property of object
                                switch (pi.PropertyType.ToString())
                                {
                                    case "System.DateTime":
                                        try
                                        {
                                            if ((DateTime)pi.GetValue(obj) == DateTime.MinValue)
                                            {
                                                pm.Value = DBNull.Value;
                                            }
                                            else
                                            {
                                                pm.Value = pi.GetValue(obj);
                                            }
                                        }
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                    default:
                                        pm.Value = pi.GetValue(obj);
                                        break;
                                }
                                
                                //Add parameter to command
                                cmdInsert.Parameters.Add(pm);
                                //Add to list for generating string
                                InsertColumnValues.Add(pm.ParameterName);
                                InsertColumnNames.Add("[" + pi.Name.ToString() + "]");
                                //clean up
                                pm = null;
                            }
                        }

                        string strInsert = string.Format("INSERT INTO [tbl_Production] ({0}) VALUES ({1});", string.Join(",", InsertColumnNames.ToArray()), string.Join(",", InsertColumnValues.ToArray()));
                        cmdInsert.CommandText = strInsert;
                        cmdInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        result = null;
                    }

                    //Clean up

                    cmdcheck.Dispose();
                    pmchk = null;
                    strCheckExist = string.Empty;
                    obj = null;
                    DataFactory.ActiveConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ProductionDAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public void Update(ProductionEntity pe)
        {

        }
    }
}

