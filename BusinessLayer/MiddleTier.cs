using System.Collections.Generic;
using System.Data;
using Entity;
using DAL.Files;
using System;
using Microsoft.Win32.SafeHandles;
using System.Reflection;
using DAL;
using DAL.Factory;



namespace BusinessLayer
{
    public class MethodFile : FileEntity, IDisposable
    {
        private Dictionary<string, FileEntity> _FilesList = new Dictionary<string, FileEntity>();

        public MethodFile() { }

        public Dictionary<string, FileEntity> getFilesList()
        {
            using (FileDataDAL obj = new FileDataDAL())
            { return _FilesList = obj.GetList(); }
        }

        public FileEntity this[string key]
        {
            // returns value if exists
            get
            {
                getFilesList();
                return _FilesList[key];
            }

            // updates if exists, adds if doesn't exist
            set { _FilesList[key] = value; }
        }

        public string GetFileName(string method)
        {
            string fn = string.Empty;
            using (FileDataDAL obj = new FileDataDAL())
            { return fn = obj.GetFileName(method); }
        }
    }




    public class Files : FileEntity, IDisposable
    {
        private Dictionary<string, FileEntity> _FilesList = new Dictionary<string, FileEntity>();
        private Dictionary<string, string> _FileNameList = new Dictionary<string, string>();
        public Files() { }

        public FileEntity this[string key]
        {
            // returns value if exists
            get { return _FilesList[key]; }

            // updates if exists, adds if doesn't exist
            set { _FilesList[key] = value; }
        }


        public Dictionary<string, FileEntity> getFilesList()
        {
            using (FileDataDAL obj = new FileDataDAL())
            { return _FilesList = obj.GetList(); }
        }
        public Dictionary<string, string> getFileNameList()
        {
            using (FileDataDAL obj = new FileDataDAL())
            {
                _FileNameList = obj.GetNameList();
                return _FileNameList;
            }
        }

        public DataTable getFileDataTable()
        {
            using (FileDataDAL obj = new FileDataDAL())

            {
                return obj.Datatable;
            }
        }

        public void Add()
        {
            using (FileDataDAL obj = new FileDataDAL())
            { obj.Add(this); }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        override protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _FileNameList = null;
                    _FilesList = null;
                    base.Dispose(disposing);

                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;

            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Files() {
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

    public class SampleParameter : SampleParameterEntity
    {
        public SampleParameter()
        {
            AcqID = -1;
            ListID = 0;
            GroupID = 2;
            SampleID = 5;
            SampleListDisplayOrder = 4;
            SampleType = "Sample";
            SampleName = "Undefined";
            FunctionUsedFlag = "false";
            TotalDilution = 1;
            CalibrationLevel = null;
            AcquisitionDataSet_ID = 0;
        }
        public SampleParameter(WebviewSampleParameterEntity wsp)
        {
            foreach (PropertyInfo pi in typeof(SampleParameter).GetProperties())
            {
                typeof(SampleParameter).GetProperty(pi.Name).SetValue(this, typeof(WebviewSampleParameterEntity).GetProperty(pi.Name).GetValue(wsp));
                
            }
            //Default values
            this.AcqID = -1;
            this.GroupID = 1;
            this.FunctionUsedFlag = "false";
            this.TotalDilution = 1;
        }
        public DataRow ToDataRow(DataRow dr)
        {
            //DataTable dt = new DataTable();
            //DataRow dr = dt.NewRow();

            foreach (PropertyInfo pi in typeof(SampleParameter).GetProperties())
            {
                //dt.Columns.Add(pi.Name, pi.PropertyType);
                dr[pi.Name] = pi.GetValue(this);
            }

            return dr;
        }


    }

}


namespace BusinessLayer
{
    public class BaseLogLogic
    {
        public static DataSet MasterDS = new DataSet("Master");
        public static List<IDbDataAdapter> listDA = new List<IDbDataAdapter>();
        public static void AttachTransaction(List<IDbDataAdapter> objs)
        {
            if (DataLayer.Instance.trans != null) { DataLayer.Instance.trans = null; }

            DataLayer.Instance.trans = DataLayer.ActiveConn.BeginTransaction();
            foreach (IDbDataAdapter obj in objs)
            {
                AttachTransaction(obj);
            }
        }

        public static void AttachTransaction(IDbDataAdapter obj)
        {
            if (obj.InsertCommand != null) { obj.InsertCommand.Transaction = DataLayer.Instance.trans; }
            if (obj.DeleteCommand != null) { obj.DeleteCommand.Transaction = DataLayer.Instance.trans; }
            if (obj.UpdateCommand != null) { obj.UpdateCommand.Transaction = DataLayer.Instance.trans; }

        }

        public static bool TryCommitDB()
        {
            try
            {
                AttachTransaction(listDA);
                for (int i = 0; i < MasterDS.Tables.Count; i++)
                {
                    using (DataSet DS = new DataSet())
                    {
                        DS.Merge(MasterDS.Tables[i], true, MissingSchemaAction.Add);
                        DS.Tables[0].TableName = "Table";
                        listDA[i].Update(DS);
                    }
                }

                CommitTrans();
                MasterDS.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                RollbackTrans();
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public static void CommitTrans()
        {
            try
            {
                DataLayer.Instance.trans.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DataLayer.Instance.trans.Rollback();
            }
        }

        private static void RollbackTrans()
        {
            try
            {
                DataLayer.Instance.trans.Rollback();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
