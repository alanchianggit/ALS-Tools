using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class FileEntity
    {
        protected string _FileName = string.Empty;
        //protected Int32 _ID = int.MinValue;
        protected int _FileSize = 0;
        protected string _Type = string.Empty;
        protected DateTime _DateUploaded = DateTime.Now;
        protected byte[] _FileBinary;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        //public Int32 ID
        //{
        //    get { return _ID; }
        //    set { _ID = value; }
        //}

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
            get
            {
                return _FileBinary;
            }
            set { _FileBinary = value; }
        }


    }

}
