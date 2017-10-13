using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FilesEntity
    {
        protected string _FileName = string.Empty;
        protected string _ID = string.Empty;
        protected int _FileSize = 0;
        protected string _Type = string.Empty;
        protected DateTime _DateUploaded = DateTime.Today;
        protected byte[] _FileBinary;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int FileSize
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
            get
            {
                //Return as file?
                return _FileBinary;
            }
            set { _FileBinary = value; }
        }
    }

}
