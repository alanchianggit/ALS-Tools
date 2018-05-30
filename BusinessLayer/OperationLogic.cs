using System;
using System.Collections.Generic;

using System.Data;

namespace BusinessLayer
{
    using System.Linq;
    using DAL;
    using DAL.Factory;
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

        public static bool TryCommitDB(DataSet dataset)
        {
            try
            {
                AttachTransaction(listDA);
                for (int i = 0; i < dataset.Tables.Count; i++)
                {
                    using (DataSet DS = new DataSet())
                    {
                        
                        DS.Merge(dataset.Tables[i], true, MissingSchemaAction.Add);
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


        public static DataTable GetLogs()
        {
            DataTable dt;
            using (BaseDAL obj = new BaseDAL())
            { dt = obj.ReadAvailableLogs(); }
            return dt;
        }
        public static DataTable GetProductionNames()
        {
            DataTable dt;
            using (BaseDAL obj = new BaseDAL())
            { dt = obj.ReadAvailableProductionNames(); }
            return dt;
        }
        public static DataTable GetMethods()
        {
            DataTable dt;
            using (BaseDAL obj = new BaseDAL())
            { dt = obj.GetMethods(); }
            return dt;
        }

        public static DataTable GetUsers()
        {
            DataTable dt;
            using (BaseDAL obj = new BaseDAL())
            { dt = obj.GetUsers(); }
            return dt;
        }

      
    }


}


namespace BusinessLayer.Productions
{
    using BusinessLayer.Backup;
    using DAL.Productions;
    //using DAL.Factory;
    public class ProductionLogic : BaseLogLogic
    {
        private static string _tableName;
        public const string ID = "ProductionID";

        private static IDbDataAdapter _adapter;

        public static IDbDataAdapter Adapter
        {
            get { return _adapter; }

            set { _adapter = value; }
        }

        public static new DataTable GetLogs()
        {
            DataTable dt = BaseLogLogic.GetLogs();
            dt.Columns["LogID"].ColumnName = "EqpName";
            return dt;
        }

        public static DataTable GetEnder()
        {
            DataTable dt = BaseLogLogic.GetUsers();
            dt.Columns["Initials"].ColumnName = "Ender";
            return dt;
        }
        public static DataTable GetStarter()
        {
            DataTable dt = BaseLogLogic.GetUsers();
            dt.Columns["Initials"].ColumnName = "Starter";
            return dt;
        }

        public static new DataTable GetMethods()
        {
            DataTable dt = BaseLogLogic.GetMethods();

            return dt;

        }

        public static string TableName
        {
            get { return _tableName = ProductionDAL.TableName.Replace("[", string.Empty).Replace("]", string.Empty); }
        }

        public static IDbDataAdapter GetAdapter()
        {
            IDbDataAdapter da;

            if (Adapter == null)
            {

                using (ProductionDAL obj = new ProductionDAL())
                {
                    da = obj.GetAdapter();
                }
                Adapter = da;
            }

            return Adapter;
        }


        public static IDbDataAdapter GetBackupAdapter()
        {

            IDbDataAdapter da;
            da = BackupLogic.GetAdapter();
            return da;
        }

    }
}

namespace BusinessLayer.Events
{
    using System.Linq;
    using DAL.Events;
    public class EventLogic : BaseLogLogic
    {
        private static string _tableName;
        private static IDbDataAdapter _adapter;
        public const string ID = "EventID";

        public static IDbDataAdapter Adapter
        {
            get { return _adapter; }

            set { _adapter = value; }
        }

        public static string TableName
        {
            get { return _tableName = EventDAL.TableName.Replace("[", string.Empty).Replace("]", string.Empty); }
        }
        public static new DataTable GetLogs()
        {
            DataTable dt = BaseLogLogic.GetLogs();
            dt.Columns["LogID"].ColumnName = "LogName";
            return dt;
        }

        public static new DataTable GetUsers()
        {
            DataTable dt = BaseLogLogic.GetUsers();
            dt.Columns["Initials"].ColumnName = "User";
            return dt;
        }

        public static IDbDataAdapter GetAdapter()
        {
            IDbDataAdapter da;

            if (Adapter == null)
            {

                using (EventDAL obj = new EventDAL())
                {
                    da = obj.GetAdapter();
                }

                Adapter = da;
                return da;
            }
            else
            {
                return Adapter;
            }
        }

        public static IDbDataAdapter GetBackupAdapter()
        {

            IDbDataAdapter da;
            using (EventDAL obj = new EventDAL())
            { da = obj.AdaptEventBackup(); }
            return da;
        }
        
    }
}

namespace BusinessLayer.Backup
{
    using DAL.Backup;
    using LogicExtensions;

    public class BackupLogic : BaseLogLogic
    {
        private static string _tableName;
        private static IDbDataAdapter _adapter;


        public static IDbDataAdapter Adapter
        {
            get { return _adapter; }

            set { _adapter = value; }
        }

        public static string TableName
        {
            get { return _tableName = BackupDAL.TableName.Replace("[", string.Empty).Replace("]", string.Empty); }
        }



        public static IDbDataAdapter GetAdapter()
        {
            IDbDataAdapter da;

            if (Adapter == null)
            {

                da = BackupDAL.Instance.GetAdapter();

                Adapter = da;
                return da;
            }
            else
            {
                return Adapter;
            }
        }

    }
}