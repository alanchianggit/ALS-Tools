using System;
using System.Collections.Generic;
using System.Data;


namespace BusinessLayer.Events
{
    using BusinessLayer;
    using DAL.Events;
    using DAL.Factory;
    public  class EventLogic:BaseLogic
    {
        private static string _tableName = EventDAL.TableName.Replace("[", string.Empty).Replace("]", string.Empty);
        private static IDbDataAdapter _eventadapter;
        private static IDbTransaction _eventtrans;


        public static IDbDataAdapter Eventadapter
        {
            get
            {
                return _eventadapter;
            }

            set
            {
                _eventadapter = value;
            }
        }

        public static IDbTransaction EventTrans
        {
            get
            {
                return _eventtrans;
            }

            set
            {
                _eventtrans = value;
            }
        }

        public static string TableName
        {
            get
            {
                return _tableName;
            }

            set
            {
                _tableName = value;
            }
        }

        public static IDbDataAdapter GetEventAdapter()
        {
            IDbDataAdapter da;

            if (Eventadapter == null)
            {

                using (EventDAL eDAL = new EventDAL())
                {

                    da = eDAL.AdaptEvent();

                }

                Eventadapter = da;
                return da;
            }
            else
            {
                return Eventadapter;
            }
        }
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

        public static void TryCommit()
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

        public static void RollbackTrans()
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

        public static IDbDataAdapter GetBackupAdapter()
        {

            IDbDataAdapter da;
            using (EventDAL eDAL = new EventDAL())
            {
                da = eDAL.AdaptEventBackup();
            }
            return da;
        }

        public static DataTable GetLogIDs()
        {

            DataTable da;
            using (EventDAL eDAL = new EventDAL())
            {
                da = eDAL.ReadAvailableLogs();

            }
            return da;
        }
    }
}
