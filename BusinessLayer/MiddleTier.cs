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
        public List<FilesEntity> getFilesList()
        {
            FileDataDAL obj = new FileDataDAL();
            return obj.GetList();
        }

        public DataTable getFileDataTable()
        {
            FileDataDAL obj = new FileDataDAL();

            return obj.Datatable;
        }

    }
}
