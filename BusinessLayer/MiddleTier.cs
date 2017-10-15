using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entity;
using DAL;

namespace BusinessLayer
{
    public class Files : FilesEntity
    {
        private Dictionary<string, FilesEntity> _FilesList = new Dictionary<string, FilesEntity>();

        public Files()
        {
            FileDataDAL obj = new FileDataDAL();
            _FilesList = obj.GetList();
        }


        public FilesEntity this[string key]
        {
            // returns value if exists
            get { return _FilesList[key]; }

            // updates if exists, adds if doesn't exist
            set { _FilesList[key] = value; }
        }


        public Dictionary<string,FilesEntity> getFilesList()
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
