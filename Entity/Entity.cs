using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FileEntity : IDisposable
    {
        protected string _FileName = string.Empty;
        //protected Int32 _ID = int.MinValue;
        protected int _FileSize = 0;
        protected string _Type = string.Empty;
        protected DateTime _DateUploaded = DateTime.Now;
        protected byte[] _FileBinary;
        protected DateTime _DateModified;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public int Size
        {
            get { return _FileSize; }
            set { _FileSize = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public DateTime DateUploaded
        {
            get { return _DateUploaded; }
            set { _DateUploaded = value; }
        }
        public byte[] FileContent
        {
            get { return _FileBinary; }
            set { _FileBinary = value; }
        }
        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
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
                    _FileName = string.Empty;
                    _FileSize = 0;
                    _Type = string.Empty;
                    _DateUploaded = DateTime.Now;
                    _FileBinary = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
                GC.Collect();
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FileEntity() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class WebviewSampleParameterEntity : SampleParameterEntity
    {
        private string _Skip;
        private string _WebviewSampleName;
        //private string _Comment;
        private string _Vial;
        private string _FileName;
        private string _Dilution;
        private string _Level;
        private string _Type;

        public WebviewSampleParameterEntity() { }

        public WebviewSampleParameterEntity(string[] arr)
        {
            Skip = arr[0] == string.Empty ? string.Empty : arr[0];
            Type = arr[1] == string.Empty ? string.Empty : arr[1];
            Vial = arr[2] == string.Empty ? string.Empty : arr[2];
            FileName = arr[3] == string.Empty ? string.Empty : arr[3];
            WebviewSampleName = arr[4] == string.Empty ? string.Empty : arr[4];
            Level = arr[5] == string.Empty ? null : arr[5];
            Dilution = arr[6] == string.Empty ? string.Empty : arr[6];
        }
        public string Skip
        {
            get
            {
                return Skip = _Skip;
            }
            set
            {
                _Skip = value;
            }
        }

        public string Type
        {
            get
            {
                return Type = _Type;
            }

            set
            {
                _Type = value;
                _SampleType = value;
            }
        }


        public string WebviewSampleName
        {
            get
            {
                return WebviewSampleName = _WebviewSampleName;
            }
            set
            {
                _WebviewSampleName = value;
                _SampleName = value;
            }
        }

        public string Vial
        {
            get
            {
                return Vial = _Vial;
            }
            set
            {
                _Vial = value;
            }
        }

        public string FileName
        {
            get
            {
                return FileName = _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        public string Dilution
        {
            get
            {
                return Dilution = _Dilution;
            }
            set
            {
                _Dilution = value;
                int parsednumber;
                bool intParseOK = int.TryParse(value, out parsednumber);
                if (intParseOK) _TotalDilution = parsednumber;

            }
        }

        public string Level
        {
            get
            {
                return Level = _Level;
            }
            set
            {
                _Level = value;
                _CalibrationLevel = value;
            }
        }
    }

    public class SampleParameterEntity
    {
        protected int _AcqID;
        protected int _ListID;
        protected int _GroupID;
        protected int _SampleID;
        protected int _SampleListDisplayOrder;
        protected string _SampleType;
        protected string _SampleName;
        protected string _SamplePosition;
        protected int _TotalDilution;
        protected string _FunctionUsedFlag;
        protected string _CalibrationLevel;
        protected int _AcquisitionDataSet_ID;

        public int AcqID
        {
            get
            {
                return AcqID = _AcqID;
            }
            set
            {
                _AcqID = value;
            }
        }

        public int ListID
        {
            get
            {
                return ListID = _ListID;
            }
            set
            {
                _ListID = value;
            }
        }
        public int GroupID
        {
            get
            {
                return GroupID = _GroupID;
            }
            set
            {
                _GroupID = value;
            }
        }
        public int SampleID
        {
            get
            {
                return SampleID = _SampleID;
            }
            set
            {
                _SampleID = value;
            }
        }
        public int SampleListDisplayOrder
        {
            get
            {
                return SampleListDisplayOrder = _SampleListDisplayOrder;
            }
            set
            {
                _SampleListDisplayOrder = value;
            }
        }
        public string SampleType
        {
            get
            {
                return SampleType = _SampleType;
            }
            set
            {
                _SampleType = value;
            }
        }
        public string SampleName
        {
            get
            {
                return SampleName = _SampleName;
            }
            set
            {
                _SampleName = value;
            }
        }
        public string SamplePosition
        {
            get
            {
                return SamplePosition = _SamplePosition;
            }
            set
            {
                _SamplePosition = value;
            }
        }
        public int TotalDilution
        {
            get
            {
                return TotalDilution = _TotalDilution;
            }
            set
            {
                _TotalDilution = value;
            }
        }
        public string FunctionUsedFlag
        {
            get
            {
                return FunctionUsedFlag = _FunctionUsedFlag;
            }
            set
            {
                _FunctionUsedFlag = value;
            }
        }

        public string CalibrationLevel
        {
            get
            {
                return CalibrationLevel = _CalibrationLevel;
            }
            set
            {
                _CalibrationLevel = value;
            }
        }
        public int AcquisitionDataSet_ID
        {
            get
            {
                return AcquisitionDataSet_ID = _AcquisitionDataSet_ID;
            }
            set
            {
                _AcquisitionDataSet_ID = value;
            }
        }
    }
}
