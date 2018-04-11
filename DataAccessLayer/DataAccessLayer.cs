﻿using System.Data;
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
using System.Data.Odbc;


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

        private const string defaultDB = @"C:\Users\Alan\Documents\BackEnd1.accdb";
        public static DataLayer Instance = new DataLayer();
        //public IDbConnection AlternateConn;

        public IDbTransaction trans;

        public void Reset()
        {
            Instance = new DataLayer();
        }


        public DataLayer()
        {
            //CreateConnection(defaultDB);
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
            //return DataFactory.CreateConnection(dbtype, defaultDB);
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
            //((IDisposable)Instance).Dispose();
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
    using DAL.Factory;

    public class ProductionDAL :IDisposable
    {

        public static ProductionDAL Instance = new ProductionDAL();
        public void Reset()
        {
            Instance = new ProductionDAL();
        }

        public ProductionDAL()
        {
            if (DataLayer.ActiveConn == null)
            {
                DataLayer.CreateConnection();
            }

            DataLayer.ExceptionFields.Clear();
            DataLayer.FieldValues.Clear();
            DataLayer.FieldNames.Clear();

        }
        private const string tablename = "[tbl_Production]";
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

        public IDbDataAdapter AdaptProduction()
        {
            ProductionEntity obj = new ProductionEntity ();
            IDbDataAdapter da = DataLayer.CreateAdapter();


            #region ProductionAdapter Select
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("ProductionID");
            IDbCommand selectcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            selectcmd.CommandText = string.Format("SELECT [ProductionID], {1} FROM {0}", tablename, string.Join(",", DataLayer.FieldNames.ToArray()));
            selectcmd.CommandType = CommandType.Text;
            #endregion


            #region ProductionAdapter Insert

            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("ProductionID");
            IDbCommand insertcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            insertcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", tablename, string.Join(",", DataLayer.FieldNames.ToArray()), string.Join(",", DataLayer.FieldValues.ToArray()));
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

            string strSQL = string.Format("DELETE * FROM {0} WHERE [ProductionID]=@ProductionID", tablename);
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

        //public IDbDataAdapter AdaptBackupData()
        //{
        //    EventEntity obj = new EventEntity();
        //    IDbDataAdapter da = CreateAdapter();

        //    #region BackupAdapter Select
        //    ExceptionFields.Clear();
        //    ExceptionFields.Add("BackupID");

        //    //IDbCommand selectcmd = ExtractParameters(obj, ExceptionFields, true, "@");

        //    IDbCommand selectcmd = ExtractParameters(obj, ExceptionFields, true, "@");
        //    selectcmd.CommandText = string.Format("SELECT [BackupID], {1} FROM {0}", "[tbl_EventsBackup]", string.Join(",", FieldNames.ToArray()));
        //    selectcmd.CommandType = CommandType.Text;
        //    da.SelectCommand = selectcmd;

        //    #endregion

        //    #region BackupAdapter Insert
        //    ExceptionFields.Clear();
        //    ExceptionFields.Add("BackupID");
        //    IDbCommand insertcmd = ExtractParameters(obj, ExceptionFields, true, "@");
        //    insertcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", "[tbl_EventsBackup]", string.Join(",", FieldNames.ToArray()), string.Join(",", FieldValues.ToArray()));
        //    insertcmd.CommandType = CommandType.Text;

        //    da.InsertCommand = insertcmd;

        //    #endregion


        //    return da;
        //}

        public DataTable ReadAvailableLogs()
        {
            DataTable dt = new DataTable("AvailableLogs");
            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dr[i] = null;
            }
            dt.Rows.Add(dr);
            dr.EndEdit();

            string strSQL = string.Format("SELECT [LogID],[Department] FROM {0} ORDER BY [LogID]", "[tbl_AvailableLogs]");
            IDbCommand dbcmd = DataLayer.CreateCommand(strSQL);
            using (IDataReader reader = dbcmd.ExecuteReader(CommandBehavior.Default))
            {
                reader.Read();
                dt.Load(reader, LoadOption.PreserveChanges);
            }
            return dt;

        }


        //public ProductionEntity RetrieveProductionData(ProductionEntity pe)
        //{
        //    return pe = RetrieveProductionData(pe.ProductionName);
        //}

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
                        pi.SetValue(pe, null);
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
            string strSQL = string.Format("SELECT * FROM {0} WHERE [ProductionName]='{1}'", tablename, prod.ProductionName);
            dt = DataLayer.QueryTable(strSQL);
            return dt;

        }

        //public void Add(ProductionEntity obj)
        //{
        //    List<IDbCommand> cmds = new List<IDbCommand>();
        //    if (!CheckExistence(obj))
        //    {
        //        ExceptionFields.Clear();
        //        ExceptionFields.Add("ID");
        //        IDbCommand cmd = ExtractParameters(obj, ExceptionFields);
        //        cmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", tablename, string.Join(",", FieldNames.ToArray()), string.Join(",", FieldValues.ToArray()));
        //        cmds.Add(cmd);
        //        RunNonQuery(cmds);
        //    }
        //}

        //public void Update(ProductionEntity obj)
        //{
        //    List<IDbCommand> cmds = new List<IDbCommand>();
        //    if (CheckExistence(obj))
        //    {
        //        ExceptionFields.Clear();
        //        ExceptionFields.Add("ID");
        //        ExceptionFields.Add("ProductionName");

        //        IDbCommand cmd = ExtractParameters(obj, ExceptionFields);

        //        cmd.CommandText = string.Format("{0} WHERE ProductionName='{1}'", UpdateClause, obj.ProductionName);
        //        cmds.Add(cmd);
        //        RunNonQuery(cmds);
        //    }
        //}



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

        public bool CheckExistence(ProductionEntity obj)
        {
            bool boolExist;
            if (DataLayer.ActiveConn.State != ConnectionState.Open) { DataLayer.ActiveConn.Open(); }
            using (IDbCommand cmdcheck = DataLayer.CreateCommand(string.Empty))
            {
                IDbDataParameter pmchk = cmdcheck.CreateParameter();
                pmchk.ParameterName = "@ProductionName";
                pmchk.Value = obj.ProductionName;
                cmdcheck.Parameters.Add(pmchk);

                string strCheckExist = string.Format("SELECT [ProductionName] FROM [tbl_Production] WHERE [ProductionName] = {0}", "@ProductionName");
                cmdcheck.CommandText = strCheckExist;
                var result = cmdcheck.ExecuteScalar();

                if (result != null) { boolExist = true; }
                else { boolExist = false; }

                //Clean up
                cmdcheck.Dispose();
                pmchk = null;
                strCheckExist = string.Empty;
                obj = null;
                DataLayer.ActiveConn.Close();
            }
            return boolExist;
        }



        public DataTable GetAvailableLogs()
        {
            DataTable dt = new DataTable();
            dt = DataLayer.QueryTable(string.Format("SELECT [ID],[LogID] FROM [tbl_AvailableLogs] WHERE [Department]='ICP-MS'"));
            return dt;
        }

        public DataTable GetProductionIDs()
        {
            DataTable dt = new DataTable();
            dt = GetProductionIDs(string.Empty);
            return dt;
        }

        public DataTable GetProductionIDs(string EqpFilter)
        {
            DataTable dt = new DataTable();
            if (EqpFilter.Equals(string.Empty))
            {
                EqpFilter = string.Empty;
            }
            else
            {
                EqpFilter = string.Format(" AND [EqpName] = '{0}'", EqpFilter);
            }

            string strSQL = string.Format("SELECT [ProductionName] FROM [tbl_Production] WHERE [ProductionName] IN (SELECT [ProductionName] FROM [tbl_Production]){0} ORDER BY [ProductionName]", EqpFilter);
            dt = DataLayer.QueryTable(strSQL);

            return dt;
        }

        public DataTable GetMethods()
        {
            //List<string> methods = new List<string>();
            DataTable dt = new DataTable();
            string strSQL = "SELECT [Method] FROM [tbl_Method]";
            dt = DataLayer.QueryTable(strSQL);

            //methods = dt.AsEnumerable().Select(r => r.Field<string>("Method")).ToList();


            return dt;
        }
    }
    //public class EventsDAL : DataLayer, IDisposable
    //{
    //    private const string tablename = "[tbl_Events]";
    //    public EventsDAL()
    //    {
    //        if (DataLayer.ActiveConn == null)
    //        {
    //            DataLayer.CreateConnection();
    //        }
    //        ExceptionFields.Clear();
    //        FieldValues.Clear();
    //        FieldNames.Clear();
    //    }

    //    public void Add(EventEntity obj)
    //    {
    //        List<IDbCommand> cmds = new List<IDbCommand>();
    //        if (!CheckExistence(obj))
    //        {
    //            ExceptionFields.Clear();
    //            ExceptionFields.Add("EventID");
    //            IDbCommand cmd = ExtractParameters(obj, ExceptionFields);

    //            cmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", tablename, string.Join(",", FieldNames.ToArray()), string.Join(",", FieldValues.ToArray()));
    //            cmds.Add(cmd);
    //            RunNonQuery(cmds);
    //        }
    //    }
    //    private string UpdateClause
    //    {
    //        get
    //        {
    //            //need to build update statement
    //            string strSQL = string.Format("UPDATE {0} SET ", tablename);
    //            for (int i = 0; i <= FieldNames.Count - 1; i++)
    //            {
    //                if (i > 0)
    //                {
    //                    strSQL += ",";
    //                }
    //                strSQL += string.Format("{0}={1}", FieldNames[i], FieldValues[i]);
    //            }

    //            return strSQL;
    //        }
    //    }

    //    private IDbCommand BackupCMD(EventEntity obj)
    //    {

    //        ExceptionFields.Clear();
    //        IDbCommand cmd = ExtractParameters(obj, ExceptionFields);

    //        cmd.CommandText = string.Format("INSERT INTO {0} SELECT * FROM {1} WHERE [EventID]={2}", "[tbl_EventsBackup]", tablename, obj.EventID);
    //        return cmd;
    //    }

    //    public void Update(EventEntity obj)
    //    {
    //        List<IDbCommand> cmds = new List<IDbCommand>();
    //        string strSQL = string.Empty;
    //        if (CheckExistence(obj))
    //        {


    //            IDbCommand cmdbackup = BackupCMD(obj);
    //            cmds.Add(cmdbackup);

    //            ExceptionFields.Clear();
    //            ExceptionFields.Add("EventID");

    //            IDbCommand cmd = ExtractParameters(obj, ExceptionFields);

    //            cmd.CommandText = string.Format("{0} WHERE [EventID]={1}", UpdateClause, obj.EventID);
    //            cmds.Add(cmd);



    //        }
    //        RunNonQuery(cmds);
    //    }

    //    public bool CheckExistence(EventEntity obj)
    //    {
    //        bool boolExist;
    //        if (DataLayer.ActiveConn.State != ConnectionState.Open) { DataLayer.ActiveConn.Open(); }
    //        using (IDbCommand cmdcheck = DataLayer.CreateCommand(string.Empty))
    //        {
    //            IDbDataParameter pmchk = cmdcheck.CreateParameter();

    //            pmchk = cmdcheck.CreateParameter();
    //            pmchk.ParameterName = "@EventID";
    //            pmchk.Value = obj.EventID;
    //            cmdcheck.Parameters.Add(pmchk);

    //            string strCheckExist = string.Format("SELECT [EventID] FROM {0} WHERE [ID] = {1}", tablename, "@EventID");
    //            cmdcheck.CommandText = strCheckExist;
    //            var result = cmdcheck.ExecuteScalar();

    //            if (result != null) { boolExist = true; }
    //            else { boolExist = false; }

    //            //Clean up

    //            cmdcheck.Dispose();
    //            pmchk = null;
    //            strCheckExist = string.Empty;
    //            obj = null;
    //            DataLayer.ActiveConn.Close();


    //        }
    //        return boolExist;
    //    }

    //    public DataTable GetDataTable(EventEntity obj)
    //    {
    //        DataTable dt = new DataTable();
    //        string strSQL = string.Format("SELECT * FROM {0} WHERE [ProductionID]='{1}'", tablename, obj.ProductionID);
    //        dt = QueryTable(strSQL);
    //        return dt;

    //    }

    //    #region IDisposable Support
    //    private bool disposedValue = false; // To detect redundant calls

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!disposedValue)
    //        {
    //            if (disposing)
    //            {
    //                // TODO: dispose managed state (managed objects).
    //            }

    //            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
    //            // TODO: set large fields to null.

    //            disposedValue = true;
    //        }
    //    }

    //    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    //    // ~EventsDAL() {
    //    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //    //   Dispose(false);
    //    // }

    //    // This code added to correctly implement the disposable pattern.
    //    void IDisposable.Dispose()
    //    {
    //        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //        Dispose(true);
    //        // TODO: uncomment the following line if the finalizer is overridden above.
    //        // GC.SuppressFinalize(this);
    //    }
    //    #endregion
    //}
}


namespace DAL.Events
{
    using DAL.Factory;


    public class EventDAL: IDisposable
    {
        private const string tablename = "[tbl_Events]";
        public static EventDAL Instance = new EventDAL();
        public void Reset()
        {
            Instance = new EventDAL();
        }

        public EventDAL()
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
                string strSQL = string.Format("UPDATE {0} SET ", tablename);
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

        public IDbDataAdapter AdaptEvent()
        {
            EventEntity obj = new EventEntity();
            IDbDataAdapter da = DataLayer.CreateAdapter();


            #region EventsAdapter Select
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("EventID");
            IDbCommand selectcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            selectcmd.CommandText = string.Format("SELECT [EventID], {1} FROM {0}", tablename, string.Join(",", DataLayer.FieldNames.ToArray()));
            selectcmd.CommandType = CommandType.Text;
            #endregion


            #region EventsAdapter Insert

            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("EventID");
            IDbCommand insertcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            insertcmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", tablename, string.Join(",", DataLayer.FieldNames.ToArray()), string.Join(",", DataLayer.FieldValues.ToArray()));
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

            string strSQL = string.Format("DELETE * FROM {0} WHERE [EventID]=@EventID", tablename);
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
            BackupEntity obj = new BackupEntity();
            IDbDataAdapter da = DataLayer.CreateAdapter();

            #region BackupAdapter Select
            DataLayer.ExceptionFields.Clear();
            DataLayer.ExceptionFields.Add("BackupID");
            IDbCommand selectcmd = DataLayer.ExtractParameters(obj, DataLayer.ExceptionFields, true, "@");
            selectcmd.CommandText = string.Format("SELECT [BackupID], {1} FROM {0}", "[tbl_Backup]", string.Join(",", DataLayer.FieldNames.ToArray()));
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

        public DataTable ReadAvailableLogs()
        {
            DataTable dt = new DataTable("AvailableLogs");
            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dr[i] = null;
            }
            dt.Rows.Add(dr);
            dr.EndEdit();

            string strSQL = string.Format("SELECT [LogID],[Department] FROM {0} ORDER BY [LogID]", "[tbl_AvailableLogs]");
            IDbCommand dbcmd = DataLayer.CreateCommand(strSQL);
            using (IDataReader reader = dbcmd.ExecuteReader(CommandBehavior.Default))
            {
                reader.Read();
                dt.Load(reader, LoadOption.PreserveChanges);
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

