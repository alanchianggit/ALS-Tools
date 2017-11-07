using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace BusinessLayer
{
    public class AnalysisManagementLogic : IDisposable
    {
        public AnalysisManagementLogic() { }
        public DataTable ImportDataTable(DataTable dt, List<WebviewSampleParameterEntity> listSamples,int gID)
        {
            foreach (WebviewSampleParameterEntity wsp in listSamples)
            {
                //object instantiation
                SampleParameter sp = new SampleParameter(wsp);
                sp.AcqID = -1;
                sp.ListID = 0;
                
                sp.GroupID = gID;
                DataRow dr = dt.NewRow();
                sp.ToDataRow(dr);
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataSet DataSetReadXML(string XMLPath)
        {
            using (DataSet ds = new DataSet())
            {
                ds.ReadXml(XMLPath, XmlReadMode.InferSchema);
                return ds;
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
        // ~AnalysisManagementLogic() {
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
