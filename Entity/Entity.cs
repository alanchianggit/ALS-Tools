using System;

namespace LogicExtensions
{
    public static class DateTimeExtension
    {

        public static DateTime GetDateWithoutMilliseconds(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }
    }
}

namespace Entity
{
    using LogicExtensions;

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
        protected string _Skip;
        protected string _WebviewSampleName;
        //private string _Comment;
        protected string _Vial;
        protected string _FileName;
        protected string _Dilution;
        protected string _Level;
        protected string _Type;

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
                return _Skip;
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
                return _Type;
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
                return _WebviewSampleName;
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
                return _Vial;
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
                return _FileName;
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
                return _Dilution;
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
                return _Level;
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
                return _AcqID;
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
                return _ListID;
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
                return _GroupID;
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
                return _SampleID;
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
                return _SampleListDisplayOrder;
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
                return _SampleType;
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
                return _SampleName;
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
                return _SamplePosition;
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
                return _TotalDilution;
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
                return _FunctionUsedFlag;
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
                return _CalibrationLevel;
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
                return _AcquisitionDataSet_ID;
            }
            set
            {
                _AcquisitionDataSet_ID = value;
            }
        }
    }

    public class EventEntity : IDisposable
    {
        protected string _LogName;
        protected DateTime _TimeCreated;
        protected string _ProductionName;
        protected int _EventID;
        protected string _User;
        //protected string _Terminal;
        protected string _Details;

        public EventEntity()
        {
        }

        public EventEntity(string argLog, string argDetails)
        {
            this.LogName = argLog;
            this.Details = argDetails;
        }
        

        public string LogName
        {
            get
            {
                return _LogName;
            }
            set
            {
                _LogName = value;
            }
        }


        public DateTime TimeCreated
        {
            get
            {
                return _TimeCreated;
            }
            set
            {
                _TimeCreated = DateTimeExtension.GetDateWithoutMilliseconds(value);
            }
        }

        public string ProductionName
        {
            get
            {
                return _ProductionName;
            }
            set
            {
                _ProductionName = value;
            }
        }

        public int EventID
        {
            get
            {
                return _EventID;
            }
            set
            {
                _EventID = value;
            }
        }


        public string User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
            }
        }

        public string Terminal
        {
            get
            {
                return Environment.GetEnvironmentVariable("computername");
            }
            set
            {
                return;
            }
        }

        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
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
        // ~EventEntity() {
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

    public class BackupEntity : IDisposable
    {
        protected int backupid;
        protected DateTime timelogged;
        protected string table_name;
        protected string column_name;
        protected string oldvalue;
        protected string newvalue;
        protected string affectedid;

        public BackupEntity()
        {

        }

        public int BackupID
        {
            get
            {
                return backupid;
            }

            set
            {
                backupid = value;
            }
        }

        public DateTime TimeLogged
        {
            get
            {
                return timelogged;
            }
            set
            {
                timelogged = DateTimeExtension.GetDateWithoutMilliseconds(value);
            }
        }
        public string TableName
        {
            get
            {
                return table_name;
            }

            set
            {
                table_name = value;
            }
        }

        public string ColumnName
        {
            get
            {
                return column_name;
            }

            set
            {
                column_name = value;
            }
        }

        public string Oldvalue
        {
            get
            {
                return oldvalue;
            }

            set
            {
                oldvalue = value;
            }
        }

        public string Newvalue
        {
            get
            {
                return newvalue;
            }

            set
            {
                newvalue = value;
            }
        }

        public string AffectedID
        {
            get
            {
                return affectedid;
            }

            set
            {
                affectedid = value;
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
        // ~BackupEntity() {
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

   
    public class ProductionEntity : IDisposable
    {
        protected string _name;
        protected DateTime? _startTime;
        protected DateTime? _endTime;
        protected string _starter;
        protected string _ender;
        protected int _ProductionID;
        protected string _type;
        protected int _quantity;
        protected string _method;
        protected string _eqpname;
        protected string _status;


        

        public DateTime StartTime
        {
            get
            {

                if (_startTime.HasValue)
                {
                    return _startTime.Value;
                }
                else
                {
                    return DateTime.MinValue;
                }

            }

            set
            {
                DateTime datetime;

                if (!DateTime.TryParse(value.ToString(), out datetime))
                {
                    return;
                }
                _startTime = DateTimeExtension.GetDateWithoutMilliseconds(value);
            }
        }



        public string ProductionName
        {
            get
            {
                return _name;
            }

            set
            {
                if (value == ProductionName) return;
                _name = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                if (_endTime.HasValue)
                {
                    return _endTime.Value;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }

            set
            {
                DateTime datetime;

                if (!DateTime.TryParse(value.ToString(), out datetime))
                {
                    return;
                }
                _endTime = DateTimeExtension.GetDateWithoutMilliseconds(value);
            }
        }

        public string Starter
        {
            get
            {
                return _starter;
            }

            set
            {
                if (value == Starter) return;
                _starter = value;
            }
        }

        public string Ender
        {
            get
            {
                return _ender;
            }

            set
            {
                if (value == Ender) return;
                _ender = value;
            }
        }

        public int ProductionID
        {
            get
            {
                return _ProductionID;
            }

            set
            {
                if (value == ProductionID) return;
                _ProductionID = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                if (value == Type) return;
                _type = value;
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                int val;
                if (value == Quantity) return;
                if (!int.TryParse(value.ToString(), out val)) return;
                _quantity = value;
            }
        }

        public string Method
        {
            get
            {
                return _method;
            }

            set
            {
                if (value == Method) return;
                _method = value;
            }
        }

        public string EqpName
        {
            get
            {
                return _eqpname;
            }

            set
            {
                if (value == EqpName) return;
                _eqpname = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public ProductionEntity(string name)
        {
            ProductionName = name;
        }
        public ProductionEntity()
        {
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            Quantity = int.MinValue;
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
        // ~ProductionEntity() {
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
namespace Auth
{
    public class AuthEntity
    {
        private static string _username;
        private static string _password;
        private static bool _authenticated;

        public static AuthEntity Instance = new AuthEntity();

        public void Reset()
        {
            Instance = new AuthEntity();
        }

        public AuthEntity()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
        public static string Username
        {
            get
            {
                if (string.IsNullOrEmpty(_username)) return string.Empty;
                return _username;
            }

            set
            {
                _username = value;
            }
        }

        public static string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password)) return string.Empty;
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public static bool Authenticated
        {
            get
            {
                return _authenticated;
            }

            set
            {
                _authenticated = value;
            }
        }

    }
}