using System.Collections.Generic;
using System.Data;
using Entity;
using DAL;

namespace BusinessLayer
{
    public class Files : FileEntity
    {
        private Dictionary<string, FileEntity> _FilesList = new Dictionary<string, FileEntity>();

        public Files()
        {
            FileDataDAL obj = new FileDataDAL();
            _FilesList = obj.GetList();
        }

        public FileEntity this[string key]
        {
            // returns value if exists
            get { return _FilesList[key]; }

            // updates if exists, adds if doesn't exist
            set { _FilesList[key] = value; }
        }


        public Dictionary<string,FileEntity> getFilesList()
        {
            FileDataDAL obj = new FileDataDAL();
            return _FilesList = obj.GetList();
        }

        public DataTable getFileDataTable()
        {
            FileDataDAL obj = new FileDataDAL();

            return obj.Datatable;
        }

        public void Add()
        {
            FileDataDAL obj = new FileDataDAL();
            obj.Add(this);
        }
    }
}
