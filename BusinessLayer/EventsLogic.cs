using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BusinessLayer
{
    public class LogEvent: EventEntity, IDisposable
    {



        public LogEvent() { }

        public LogEvent Add(string argLog, string argDetails)
        {
            LogEvent LE = new LogEvent();
            LE.LogName = argLog;
            LE.Details = argDetails;
            LE.Terminal = Environment.MachineName;
            LE.Source = base.ToString();
            LE.Level = base.ToString();
            LE.User = Environment.UserName;
            LE.ID = LE.LogName + LE.TimeCreated;
            return LE;
        }


        public void Post()
        {
            using (EventsDAL eDAL = new EventsDAL())
            {
                DataFactory.CreateConnection();
                eDAL.Add(this);
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
