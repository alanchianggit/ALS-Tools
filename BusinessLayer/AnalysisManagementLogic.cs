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
        public DataTable ImportSampleListToDataTable(DataSet ds, List<WebviewSampleParameterEntity> listSamples)
        {
            int DisplayOrder;
            int GroupID;
            int SampleID;

            //set output datatable to sample parameter table
            DataTable dt = ds.Tables["SampleParameter"];
            //find GroupID for unknown sample
            try
            {
                //Enumerate sample group table
                var rowColl = ds.Tables["SampleGroup"].AsEnumerable();
                string gID = (from r in rowColl
                              where r.Field<string>("SampleGroupName") == "Unknown Samples"
                              select r.Field<string>("GroupID")).First<string>();
                GroupID = int.Parse(gID);
            }
            catch (ArgumentNullException ee)
            {
                Console.WriteLine(ee.Message);
                Console.WriteLine("Default to 1 (after 0=Calibration standards)");
                GroupID = 1;
            }

            //find display order
            try
            {
                var rowCol1 = ds.Tables["SampleParameter"].AsEnumerable();
                string _displayOrder = (from r in rowCol1
                                        where r.Field<string>("GroupID") == GroupID.ToString()
                                        select r.Field<string>("SampleListDisplayOrder")).Max();
                DisplayOrder = int.Parse(_displayOrder);
            }
            catch (ArgumentNullException ANex)
            {
                Console.WriteLine(ANex.Message);
                Console.WriteLine("Default Display order to '-1'");
                DisplayOrder = -1;
            }

            //Find SampleID
            try
            {
                var rowCol1 = ds.Tables["SampleParameter"].AsEnumerable();
                string _sampleid = (from r in rowCol1
                                        where r.Field<string>("GroupID") == GroupID.ToString()
                                        select r.Field<string>("SampleID")).Max();
                SampleID = int.Parse(_sampleid);
            }
            catch (ArgumentNullException ANex)
            {
                Console.WriteLine(ANex.Message);
                Console.WriteLine("Default SampleID to '-1'");
                SampleID = -1;
            }

            //Loop through list of samples to import into datatable
            foreach (WebviewSampleParameterEntity wsp in listSamples)
            {
                //Iterators for each sample
                DisplayOrder += 1;
                SampleID += 1;
                
                SampleParameter sp = new SampleParameter(wsp);
                sp.GroupID = GroupID;
                sp.SampleListDisplayOrder = DisplayOrder;
                sp.SamplePosition = "T0V2";
                sp.SampleID = SampleID;
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
