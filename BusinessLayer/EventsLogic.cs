using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

using System.Data;
using System.Reflection;


namespace BusinessLayer.Events
{
    using DAL.Events;
    using DAL.Factory;
    public static class EventLogic
    {

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

        //public static DataTable GetDataTable(EventEntity obj)
        //{
        //    DataTable dt = new DataTable();
        //    using (EventsDAL eDAL = new EventsDAL())
        //    {
        //        dt = eDAL.GetDataTable(obj);
        //    }

        //    return dt;
        //}

        public static DataTable GetLogIDs()
        {

            DataTable da;
            using (EventDAL eDAL = new EventDAL())
            {
                da = eDAL.ReadAvailableLogs();

            }
            return da;
        }

        //public static LogEvent ConvertToEvent(System.Windows.Forms.DataGridViewRow dr)
        //{
        //    LogEvent le = new LogEvent();
        //    System.Windows.Forms.DataGridViewCellCollection dc = dr.Cells;
        //    foreach (PropertyInfo pi in le.GetType().GetProperties())
        //    {
        //        switch (pi.PropertyType.ToString())
        //        {
        //            case "System.String":
        //                pi.SetValue(le, dc[pi.Name].Value.ToString());
        //                break;
        //            case "System.Int32":
        //                int intresult;
        //                int.TryParse(dc[pi.Name].Value.ToString(), out intresult);
        //                pi.SetValue(le, intresult);
        //                break;
        //            case "System.DateTime":
        //                DateTime datetimeresult;
        //                DateTime.TryParse(dc[pi.Name].Value.ToString(), out datetimeresult);
        //                pi.SetValue(le, datetimeresult);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    return le;
        //}

    }

    //public class LogEvent : EventEntity, IDisposable
    //{

    //    public LogEvent() { }

        //public void AddOrSubmit()
        //{
        //    using (EventsDAL eDAL = new EventsDAL())
        //    {
        //        if (this.EventID == 0 || this.EventID.ToString() == string.Empty)
        //        {
        //            eDAL.Add(this);
        //        }
        //        else
        //        {
        //            eDAL.Update(this);
        //        }

        //    }
        //}




        //#region IDisposable Support
        //private bool disposedValue = false; // To detect redundant calls

        //protected override void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects).
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //        // TODO: set large fields to null.

        //        disposedValue = true;
        //    }
        //}

        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //// ~EventsLogic() {
        ////   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        ////   Dispose(false);
        //// }

        //// This code added to correctly implement the disposable pattern.
        //void IDisposable.Dispose()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(true);
        //    // TODO: uncomment the following line if the finalizer is overridden above.
        //    // GC.SuppressFinalize(this);
        //}
        //#endregion


    //}
}
