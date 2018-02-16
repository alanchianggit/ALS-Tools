using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
using System.Data;
using System.Reflection;


namespace BusinessLayer
{
    public class EventLogic:IDisposable
    {
        public IDbDataAdapter GetEventsAdapter(EventEntity obj)
        {

            IDbDataAdapter da;
            using (EventsDAL eDAL = new EventsDAL())
            {
                da = eDAL.AdaptEventData(obj);
            }
            return da;
        }

        public IDbDataAdapter GetBackupAdapter(EventEntity obj)
        {

            IDbDataAdapter da;
            using (EventsDAL eDAL = new EventsDAL())
            {
                da = eDAL.AdaptBackupData(obj);
            }
            return da;
        }

        public DataTable GetDataTable(EventEntity obj)
        {
            DataTable dt = new DataTable();
            using (EventsDAL eDAL = new EventsDAL())
            {
                dt = eDAL.GetDataTable(obj);
            }

                return dt;
        }

        public LogEvent ConvertToEvent(System.Windows.Forms.DataGridViewRow dr)
        {
            LogEvent le = new LogEvent();
            System.Windows.Forms.DataGridViewCellCollection dc = dr.Cells;
            foreach (PropertyInfo pi in le.GetType().GetProperties())
            {
                switch (pi.PropertyType.ToString())
                {
                    case "System.String":
                        pi.SetValue(le, dc[pi.Name].Value.ToString());
                        break;
                    case "System.Int32":
                        int intresult;
                        int.TryParse(dc[pi.Name].Value.ToString(), out intresult);
                        pi.SetValue(le, intresult);
                        break;
                    case "System.DateTime":
                        DateTime datetimeresult;
                        DateTime.TryParse(dc[pi.Name].Value.ToString(), out datetimeresult);
                        pi.SetValue(le, datetimeresult);
                        break;
                    default:
                        break;
                }

            }
            return le;
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
        // ~EventLogic() {
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

    public class LogEvent: EventEntity, IDisposable
    {
        
        public LogEvent() { }

        public void AddOrSubmit()
        {
            using (EventsDAL eDAL = new EventsDAL())
            {
                if (this.ID == 0 || this.ID.ToString() == string.Empty)
                {
                    eDAL.Add(this);
                }
                else
                {
                    eDAL.Update(this);
                }
                
            }
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
        // ~EventsLogic() {
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
