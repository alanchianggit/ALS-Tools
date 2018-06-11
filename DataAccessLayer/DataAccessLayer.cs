using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Entity;
using Oracle.ManagedDataAccess.Client;




namespace AuthDAL
{
    public class Auth_DAL : IDisposable
    {

        public Auth_DAL()
        {

        }

        public void Signin()
        {

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
        // ~Auth_DAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

namespace DAL.Factory
{
    using System.Configuration;
    using DataAccessLayer.Properties;
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

    public class DataLayer : IDisposable
    {


        //create new Lists for colum names and parameters
        //public static List<string> FieldNames = new List<string>();
        public static List<string> FieldNames = new List<string>();
        public static List<string> FieldValues = new List<string>();
        //create exception fields list
        public static List<string> ExceptionFields = new List<string>();

        private const string defaultDBPath = @"C:\Users\Alan\Documents\BackEnd1.accdb";
        private static string _defaultDB;

        public static void ChangeSettings(string arg, string val)
        {
            //Settings.Default.Properties[arg].DefaultValue = val;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!config.AppSettings.Settings.AllKeys.Contains(arg))
            {
                config.AppSettings.Settings.Add(arg, val);
            }
            else
            {
                config.AppSettings.Settings[arg].Value = val;
            }
            config.Save(ConfigurationSaveMode.Modified);
        }
        public static string GetSetting(string arg)
        {
            string result = string.Empty;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string [] list = config.AppSettings.Settings.AllKeys;
            if (list.Contains(arg))
            {
                result = config.AppSettings.Settings[arg].Value.ToString();
            }
            return result;
        }

        public static DataTable GetSettings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


            DataTable result = new DataTable("Configurations");
            result.Columns.Add("Keys", typeof(string));
            result.Columns.Add("Values", typeof(string));
            
            foreach (KeyValueConfigurationElement o in config.AppSettings.Settings)
            {
                result.Rows.Add(o.Key, o.Value);                
            }
            return result;
        }


        public static string defaultDB
        {
            get
            {
                return _defaultDB = Settings.Default.DbPath;
            }
            set
            {

                Settings.Default.DbPath = value;
            }
        }


        public static DataLayer Instance = new DataLayer();
        //public IDbConnection AlternateConn;

        public IDbTransaction trans;

        public void Reset()
        {
            Instance = new DataLayer();
            if (string.IsNullOrEmpty(GetSetting("DbPath")))
            {
                ChangeSettings("DbPath", defaultDBPath);
            }
        }


        public DataLayer()
        {
            

        }




        public static IDbConnection ActiveConn;/*{ get; set; }*/
        public static string ActiveConnectionString { get; set; }

        public static DatabaseType dbtype { get; set; }

        private static Dictionary<object, IDbConnection> conns = new Dictionary<object, IDbConnection>();


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
            IDbConnection conn = DataLayer.Instance.CreateConnection(dbtype, defaultDB);
            return conn;

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
            IDbConnection conn = DataLayer.Instance.CreateConnection(dbtype, DBDataSource);
            return conn;

        }

        public static DataTable QueryTable(string strSQL)
        {
            DataSet ds = new DataSet();

            if (DataLayer.ActiveConn != null && DataLayer.ActiveConn.State != ConnectionState.Open) { DataLayer.ActiveConn.Open(); }
            try
            {
                IDbCommand cmd = DataLayer.CreateCommand(strSQL);
                IDbDataAdapter da = DataLayer.CreateAdapter(cmd);

                da.Fill(ds);

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ds.Tables[0];
        }
        public static void RunNonQuery(List<IDbCommand> cmds)
        {
            if (DataLayer.ActiveConn.State != ConnectionState.Open) { DataLayer.ActiveConn.Open(); }
            IDbTransaction trans = ActiveConn.BeginTransaction();
            try
            {
                foreach (IDbCommand cmd in cmds)
                {
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();

                }
                trans.Commit();


            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
            DataLayer.ActiveConn.Close();
        }

        public static void RunNonQuery(IDbCommand cmd, string strSQL)
        {
            List<IDbCommand> cmds = new List<IDbCommand>();
            cmd.CommandText = strSQL;
            cmds.Add(cmd);
            RunNonQuery(cmds);
        }

        public static IDbCommand ExtractParameters(object obj, List<string> excepfields, bool extractnulls, string paramchar)
        {
            //if (string.IsNullOrEmpty(paramchar)) { paramchar = "@"; };
            FieldValues.Clear();
            FieldNames.Clear();
            // get properties from entity class

            PropertyInfo[] PIs = obj.GetType().GetProperties();
            //Create table of data according to properties so it can be adapted to connection
            IDbCommand cmd = DataLayer.CreateCommand(string.Empty);


            //Iterate through each prorperty to coerce a parameter
            foreach (PropertyInfo pi in PIs)
            {

                if (!excepfields.Contains(pi.Name))
                {
                    if (!extractnulls) { break; }

                    //Create new parameter object
                    IDbDataParameter pm = cmd.CreateParameter();
                    //Set Parameter name from property name
                    pm.ParameterName = string.Format("{1}{0}", pi.Name.ToString(), paramchar);
                    //Set value from property of object
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.DateTime":
                            try
                            {
                                if ((DateTime)pi.GetValue(obj) == DateTime.MinValue)
                                {
                                    pm.Value = DBNull.Value;
                                    pm.DbType = DbType.DateTime;
                                    pm.SourceColumn = pi.Name;
                                    break;
                                }
                                else
                                {
                                    pm.DbType = DbType.DateTime;
                                    pm.Value = pi.GetValue(obj);
                                    pm.SourceColumn = pi.Name;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case "System.Int32":
                            try
                            {
                                if ((int)pi.GetValue(obj) == int.MinValue || (int)pi.GetValue(obj) == 0)
                                {
                                    pm.Value = DBNull.Value;
                                    pm.DbType = DbType.Int32;
                                    pm.SourceColumn = pi.Name;
                                }
                                else
                                {
                                    pm.DbType = DbType.Int32;
                                    pm.Value = pi.GetValue(obj);
                                    pm.SourceColumn = pi.Name;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        default:
                            pm.Value = pi.GetValue(obj);
                            pm.DbType = DbType.String;
                            pm.SourceColumn = pi.Name;
                            break;
                    }

                    //Add parameter to command
                    cmd.Parameters.Add(pm);
                    //Add to list for generating string
                    FieldValues.Add(pm.ParameterName);
                    FieldNames.Add("[" + pi.Name.ToString() + "]");
                    //FieldNames.Sort();
                    //FieldValues.Sort();
                    //clean up
                    pm = null;
                    PIs = null;
                }
            }
            return cmd;
        }

        public static IDbCommand ExtractParameters(object obj, List<string> excepfields)
        {
            IDbCommand cmd = ExtractParameters(obj, excepfields, true, "@");
            return cmd;
        }
        public static IDbCommand ExtractParameters(object obj, List<string> excepfields, bool extractnulls)
        {
            IDbCommand cmd = ExtractParameters(obj, excepfields, extractnulls, string.Empty);
            return cmd;
        }

        public IDbConnection CreateConnection(DatabaseType dbtype, string DBDataSource)
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
            ActiveConn = Conn;

            return Conn;

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
                        OdbcConnectionStringBuilder bs = new OdbcConnectionStringBuilder();
                        bs.Add("Dbq", DBDataSource);


                        ActiveConnectionString = bs.ConnectionString;
                    }
                    break;
            }

            return ActiveConnectionString;
        }

        public static IDbCommand CreateCommand(string CommandText)
        {
            IDbCommand cmd;
            switch (DataLayer.dbtype)
            {
                case DatabaseType.AccessACCDB:
                case DatabaseType.AccessMDB:
                    cmd = new OleDbCommand(CommandText, (OleDbConnection)DataLayer.ActiveConn);
                    break;

                case DatabaseType.SQLServer:
                    cmd = new SqlCommand(CommandText, (SqlConnection)DataLayer.ActiveConn);
                    break;

                case DatabaseType.Oracle:
                    cmd = new OracleCommand(CommandText, (OracleConnection)DataLayer.ActiveConn);
                    break;
                case DatabaseType.SQLite:
                    cmd = new SQLiteCommand(CommandText, (SQLiteConnection)DataLayer.ActiveConn);
                    break;
                default:
                    cmd = new OdbcCommand(CommandText, (OdbcConnection)DataLayer.ActiveConn);
                    break;
            }

            return cmd;
        }

        public static IDbDataAdapter CreateAdapter()
        {
            IDbDataAdapter da;

            switch (DataLayer.dbtype)
            {
                case DatabaseType.AccessACCDB:
                case DatabaseType.AccessMDB:
                    da = new OleDbDataAdapter();
                    break;

                case DatabaseType.SQLServer:
                    da = new SqlDataAdapter();
                    break;

                case DatabaseType.Oracle:
                    da = new OracleDataAdapter();
                    break;
                case DatabaseType.SQLite:
                    da = new SQLiteDataAdapter();
                    break;
                default:
                    da = new OdbcDataAdapter();
                    break;
            }

            return da;
        }

        public static IDbDataAdapter CreateAdapter(IDbCommand cmd)
        {
            DbDataAdapter da;

            switch (DataLayer.dbtype)
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
                    da = new OdbcDataAdapter((OdbcCommand)cmd);
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
        }
    }
}

namespace DAL.Files
{
    using DAL.Factory;
    public class FileDataDAL : IDisposable
    {
        public DataTable Datatable = new DataTable();

        private Dictionary<string, string> dictFileName = new Dictionary<string, string>();
        private Dictionary<string, FileEntity> dictFE = new Dictionary<string, FileEntity>();
        private List<FileEntity> listFE = new List<FileEntity>();

        public FileDataDAL()
        {
            if (DataLayer.ActiveConn != null)
            {
                DataLayer.CreateConnection();
            }

        }

        private DataTable GetDatatable()
        {
            string strSQL = "SELECT * FROM tbl_Files";
            if (DataLayer.ActiveConn != null && DataLayer.ActiveConn.State != ConnectionState.Open) { DataLayer.ActiveConn.Open(); }
            try
            {
                IDbCommand cmd = DataLayer.CreateCommand(strSQL);
                IDbDataAdapter da = DataLayer.CreateAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cmd.Dispose();
                Datatable = ds.Tables[0];
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
            using (IDbCommand cmd = DataLayer.CreateCommand(strSQL))
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
            if (DataLayer.ActiveConn.State != ConnectionState.Open) { DataLayer.ActiveConn.Open(); }
            try
            {

                using (IDbCommand cmdcheck = DataLayer.CreateCommand(string.Empty))
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
                        PropertyInfo[] PIs = obj.GetType().GetProperties();

                        //Create table of data according to properties so it can be adapted to connection
                        IDbCommand cmdInsert = DataLayer.CreateCommand(string.Empty);

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
                    DataLayer.ActiveConn.Close();
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
                    DataLayer.ActiveConn.Close();
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
}


namespace DAL.Productions
{
    using DAL.Backup;
    using DAL.Factory;

    public class ProductionDAL : BaseDAL, IDisposable
    {
        private const string _tableName = "[tbl_Production]";
        public static new ProductionDAL Instance = new ProductionDAL();
        public new void Reset()
        {
            Instance = new ProductionDAL();
        }

        public ProductionDAL()
        {
            Initialize();
        }

        private string UpdateClause
        {
            get
            {
                //need to build update statement
                string strSQL = string.Format("UPDATE {0} SET ", "[tbl_Production]");
                for (int i = 0; i <= DataLayer.FieldNames.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        strSQL += ",";
                    }
                    strSQL += string.Format("{0}={1}", DataLayer.FieldNames[i], DataLayer.FieldValues[i]);
                }

                return strSQL;
            }
        }

        public static string TableName
        {
            get
            {
                return _tableName;
            }
        }

        public IDbDataAdapter GetAdapter()
        {
            ProductionEntity obj = new ProductionEntity();
            IDbDataAdapter da = DataLayer.CreateAdapter();


            #region ProductionAdapter Select
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("ProductionID");
            IDbCommand selectcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            selectcmd.CommandText = string.Format("SELECT [ProductionID], {1} FROM {0}", _tableName, string.Join(",", DataLayer.FieldNames.ToArray()));
            selectcmd.CommandType = CommandType.Text;
            #endregion


            #region ProductionAdapter Insert

            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("ProductionID");
            IDbCommand insertcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            insertcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", _tableName, string.Join(",", DataLayer.FieldNames.ToArray()), string.Join(",", DataLayer.FieldValues.ToArray()));
            insertcmd.CommandType = CommandType.Text;
            #endregion

            #region ProductionAdapter Update
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("ProductionID");

            IDbCommand updatecmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");

            updatecmd.CommandText = string.Format("{0} WHERE [ProductionID]=@ProductionID", UpdateClause);
            IDbDataParameter param = updatecmd.CreateParameter();
            param.ParameterName = "@ProductionID";
            param.SourceColumn = "ProductionID";
            updatecmd.Parameters.Add(param);

            #endregion


            #region ProductionAdapter Delete
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("ProductionID");

            //IDbCommand deletecmd = ExtractParameters(obj, ExceptionFields, true, "@");

            string strSQL = string.Format("DELETE * FROM {0} WHERE [ProductionID]=@ProductionID", _tableName);
            IDbCommand deletecmd = DataLayer.CreateCommand(strSQL);
            IDbDataParameter delparam = deletecmd.CreateParameter();
            delparam.ParameterName = "@ProductionID";
            delparam.SourceColumn = "ProductionID";
            deletecmd.Parameters.Add(delparam);
            #endregion

            da.DeleteCommand = deletecmd;
            da.UpdateCommand = updatecmd;
            da.SelectCommand = selectcmd;
            da.InsertCommand = insertcmd;
            return da;
        }
        public IDbDataAdapter AdaptEventBackup()
        {
            IDbDataAdapter da = BackupDAL.Instance.GetAdapter();
            return da;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
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


        //public DataTable GetAvailableLogs()
        //{
        //    DataTable dt = new DataTable();
        //    dt = DataLayer.QueryTable(string.Format("SELECT [ID],[LogID] FROM [tbl_AvailableLogs] WHERE [Department]='ICP-MS'"));
        //    return dt;
        //}

        //public DataTable GetProductionIDs(string EqpFilter)
        //{
        //    DataTable dt = new DataTable();
        //    if (EqpFilter.Equals(string.Empty))
        //    {
        //        EqpFilter = string.Empty;
        //    }
        //    else
        //    {
        //        EqpFilter = string.Format(" AND [EqpName] = '{0}'", EqpFilter);
        //    }

        //    string strSQL = string.Format("SELECT [ProductionName] FROM [tbl_Production] WHERE [ProductionName] IN (SELECT [ProductionName] FROM [tbl_Production]){0} ORDER BY [ProductionName]", EqpFilter);
        //    dt = DataLayer.QueryTable(strSQL);

        //    return dt;
        //}

    }

}
namespace DAL
{
    using DAL.Factory;

    public class BaseDAL : IDisposable
    {
        public static BaseDAL Instance = new BaseDAL();
        public void Reset()
        {
            Instance = new BaseDAL();
        }

        public void Initialize()
        {
            if (DataLayer.ActiveConn == null)
            {
                DataLayer.CreateConnection();
            }

            DataLayer.ExceptionFields.Clear();
            DataLayer.FieldValues.Clear();
            DataLayer.FieldNames.Clear();
        }
        public BaseDAL()
        {
            Initialize();
        }
        public DataTable GetMethods()
        {
            DataTable dt = new DataTable("Methods");


            string strSQL = string.Format("SELECT DISTINCT [Method] FROM {0} ORDER BY [Method]", "[tbl_Method]");
            IDbCommand dbcmd = DataLayer.CreateCommand(strSQL);
            using (IDataReader reader = dbcmd.ExecuteReader(CommandBehavior.Default))
            {
                reader.Read();
                dt.Load(reader, LoadOption.PreserveChanges);
                //Add empty value
                DataRow drc = dt.NewRow();
                dt.Rows.InsertAt(drc, 0);
            }
            return dt;

        }

        public DataTable GetUsers()
        {
            DataTable dt = new DataTable("Users");

            //Returns all empty values, even select *
            string strSQL = string.Format("SELECT DISTINCT [Initials] FROM {0} ORDER BY [Initials]", "[tbl_Employee]");

            IDbCommand dbcmd = DataLayer.CreateCommand(strSQL);
            using (IDataReader reader = dbcmd.ExecuteReader(CommandBehavior.Default))
            {
                dt.Load(reader, LoadOption.PreserveChanges);
                //Add empty value
                DataRow drc = dt.NewRow();
                dt.Rows.InsertAt(drc, 0);
            }
            return dt;

        }

        public DataTable ReadAvailableProductionNames()
        {
            DataTable dt = new DataTable("AvailableProductionNames");

            string strSQL = string.Format("SELECT DISTINCT [ProductionName] FROM {0} ORDER BY [ProductionName]", "[tbl_Production]");
            IDbCommand dbcmd = DataLayer.CreateCommand(strSQL);
            using (IDataReader reader = dbcmd.ExecuteReader(CommandBehavior.Default))
            {
                dt.Load(reader, LoadOption.PreserveChanges);
                //Add empty value
                DataRow drc = dt.NewRow();
                dt.Rows.InsertAt(drc, 0);
            }


            return dt;

        }

        public DataTable ReadLogs()
        {
            DataTable dt = new DataTable("AvailableLogs");

            string strSQL = string.Format("SELECT [LogID],[Department] FROM {0} ORDER BY [LogID]", "[tbl_AvailableLogs]");
            IDbCommand dbcmd = DataLayer.CreateCommand(strSQL);
            using (IDataReader reader = dbcmd.ExecuteReader(CommandBehavior.Default))
            {
                dt.Load(reader, LoadOption.PreserveChanges);
                //Add empty value
                DataRow drc = dt.NewRow();
                dt.Rows.InsertAt(drc, 0);
            }
            return dt;
        }


        public DataTable ReadAvailableLogs()
        {
            DataTable dt = new DataTable("AvailableLogs");

            string strSQL = string.Format("SELECT [LogID],[Department] FROM {0} ORDER BY [LogID]", "[tbl_AvailableLogs]");
            IDbCommand dbcmd = DataLayer.CreateCommand(strSQL);
            using (IDataReader reader = dbcmd.ExecuteReader(CommandBehavior.Default))
            {
                dt.Load(reader, LoadOption.PreserveChanges);
                //Add empty value
                DataRow drc = dt.NewRow();
                dt.Rows.InsertAt(drc, 0);
            }
            return dt;

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
        // ~BaseDAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }

}


namespace DAL.Events
{

    using DAL.Backup;
    using DAL.Factory;

    public class EventDAL : BaseDAL, IDisposable
    {
        public const string TableName = "[tbl_Events]";
        public static new EventDAL Instance = new EventDAL();
        public new void Reset()
        {
            Instance = new EventDAL();
        }

        public EventDAL()
        {
            Initialize();
        }

        private string UpdateClause
        {
            get
            {
                string strSQL = string.Format("UPDATE {0} SET ", TableName);
                for (int i = 0; i <= DataLayer.FieldNames.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        strSQL += ",";
                    }
                    strSQL += string.Format("{0}={1}", DataLayer.FieldNames[i], DataLayer.FieldValues[i]);
                }

                return strSQL;
            }
        }

        public IDbDataAdapter GetAdapter()
        {
            EventEntity obj = new EventEntity();
            IDbDataAdapter da = DataLayer.CreateAdapter();


            #region EventsAdapter Select
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("EventID");
            IDbCommand selectcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            selectcmd.CommandText = string.Format("SELECT [EventID], {1} FROM {0} ORDER BY [EventID]", TableName, string.Join(",", DataLayer.FieldNames.ToArray()));
            selectcmd.CommandType = CommandType.Text;
            #endregion


            #region EventsAdapter Insert

            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("EventID");

            IDbCommand insertcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            insertcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", TableName, string.Join(",", DataLayer.FieldNames.ToArray()), string.Join(",", DataLayer.FieldValues.ToArray()));
            insertcmd.CommandType = CommandType.Text;
            #endregion

            #region EventsAdapter Update
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("EventID");
            IDbCommand updatecmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");

            updatecmd.CommandText = string.Format("{0} WHERE [EventID]=@EventID", UpdateClause);
            IDbDataParameter param = updatecmd.CreateParameter();
            param.ParameterName = "@EventID";
            param.SourceColumn = "EventID";
            updatecmd.Parameters.Add(param);

            #endregion


            #region EventsAdapter Delete
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("EventID");

            //IDbCommand deletecmd = ExtractParameters(obj, ExceptionFields, true, "@");

            string strSQL = string.Format("DELETE * FROM {0} WHERE [EventID]=@EventID", TableName);
            IDbCommand deletecmd = DataLayer.CreateCommand(strSQL);
            IDbDataParameter delparam = deletecmd.CreateParameter();
            delparam.ParameterName = "@EventID";
            delparam.SourceColumn = "EventID";
            deletecmd.Parameters.Add(delparam);
            #endregion

            da.DeleteCommand = deletecmd;
            da.UpdateCommand = updatecmd;
            da.SelectCommand = selectcmd;
            da.InsertCommand = insertcmd;
            return da;
        }
        public IDbDataAdapter AdaptEventBackup()
        {
            IDbDataAdapter da = BackupDAL.Instance.GetAdapter();
            return da;
        }




        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
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


}

namespace DAL.Backup
{
    using DAL.Factory;

    public class BackupDAL : IDisposable
    {
        public const string TableName = "[tbl_Backup]";
        public static BackupDAL Instance = new BackupDAL();
        public void Reset()
        {
            Instance = new BackupDAL();
        }

        public BackupDAL()
        {
            if (DataLayer.ActiveConn == null)
            {
                DataLayer.CreateConnection();
            }

            DataLayer.ExceptionFields.Clear();
            DataLayer.FieldValues.Clear();
            DataLayer.FieldNames.Clear();

        }

        private string UpdateClause
        {
            get
            {
                string strSQL = string.Format("UPDATE {0} SET ", TableName);
                for (int i = 0; i <= DataLayer.FieldNames.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        strSQL += ",";
                    }
                    strSQL += string.Format("{0}={1}", DataLayer.FieldNames[i], DataLayer.FieldValues[i]);
                }

                return strSQL;
            }
        }

        public IDbDataAdapter GetAdapter()
        {
            BackupEntity obj = new BackupEntity();
            IDbDataAdapter da = DataLayer.CreateAdapter();


            #region BackupAdapter Select
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("BackupID");
            IDbCommand selectcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            selectcmd.CommandText = string.Format("SELECT [BackupID], {1} FROM {0} ORDER BY [BackupID]", TableName, string.Join(",", DataLayer.FieldNames.ToArray()));
            selectcmd.CommandType = CommandType.Text;
            #endregion


            #region BackupAdapter Insert

            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("BackupID");

            IDbCommand insertcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            insertcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", TableName, string.Join(",", DataLayer.FieldNames.ToArray()), string.Join(",", DataLayer.FieldValues.ToArray()));
            insertcmd.CommandType = CommandType.Text;
            #endregion

            #region BackupAdapter Update
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("BackupID");
            IDbCommand updatecmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");

            updatecmd.CommandText = string.Format("{0} WHERE [BackupID]=@BackupID", UpdateClause);
            IDbDataParameter param = updatecmd.CreateParameter();
            param.ParameterName = "@BackupID";
            param.SourceColumn = "BackupID";
            updatecmd.Parameters.Add(param);

            #endregion


            #region BackupAdapter Delete
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("BackupID");

            //IDbCommand deletecmd = ExtractParameters(obj, ExceptionFields, true, "@");

            string strSQL = string.Format("DELETE * FROM {0} WHERE [BackupID]=@BackupID", TableName);
            IDbCommand deletecmd = DataLayer.CreateCommand(strSQL);
            IDbDataParameter delparam = deletecmd.CreateParameter();
            delparam.ParameterName = "@BackupID";
            delparam.SourceColumn = "BackupID";
            deletecmd.Parameters.Add(delparam);
            #endregion

            da.DeleteCommand = deletecmd;
            da.UpdateCommand = updatecmd;
            da.SelectCommand = selectcmd;
            da.InsertCommand = insertcmd;
            return da;
        }

        public IDbDataAdapter AdaptEventBackup()
        {
            BackupEntity obj = new BackupEntity();
            IDbDataAdapter da = DataLayer.CreateAdapter();

            #region BackupAdapter Select
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("BackupID");
            IDbCommand selectcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            selectcmd.CommandText = string.Format("SELECT [BackupID], {1} FROM {0} WHERE [TableName]='{2}'", "[tbl_Backup]", string.Join(",", DataLayer.FieldNames.ToArray()), TableName.Replace("[", string.Empty).Replace("]", string.Empty));
            selectcmd.CommandType = CommandType.Text;
            da.SelectCommand = selectcmd;

            #endregion

            #region BackupAdapter Insert
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("BackupID");
            IDbCommand insertcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            insertcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", "[tbl_Backup]", string.Join(",", DataLayer.FieldNames.ToArray()), string.Join(",", DataLayer.FieldValues.ToArray()));
            insertcmd.CommandType = CommandType.Text;

            da.InsertCommand = insertcmd;

            #endregion


            return da;
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


}

