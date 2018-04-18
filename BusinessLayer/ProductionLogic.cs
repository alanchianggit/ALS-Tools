using System;
using System.Collections.Generic;

using System.Data;

namespace BusinessLayer.Productions
{
    using DAL.Productions;
    using DAL.Factory;
    public class ProductionLogic: BaseLogic
    {
        private static string _tableName = ProductionDAL.TableName.Replace("[",string.Empty).Replace("]",string.Empty);

        private static IDbDataAdapter _productionadapter;
        private static IDbTransaction _productiontrans;


        public static IDbDataAdapter ProductionAdapter
        {
            get
            {
                return _productionadapter;
            }

            set
            {
                _productionadapter = value;
            }
        }

        public static IDbTransaction EventTrans
        {
            get
            {
                return _productiontrans;
            }

            set
            {
                _productiontrans = value;
            }
        }

        public static string TableName
        {
            get
            {
                return _tableName;
            }
        }
        
        public static void AttachTransaction(List<IDbDataAdapter> objs)
        {
            if (DataLayer.Instance.trans != null) { DataLayer.Instance.trans = null; }

            DataLayer.Instance.trans = DataLayer.ActiveConn.BeginTransaction();
            foreach (IDbDataAdapter obj in objs)
            {
                AttachTransaction(obj);
            }
        }
        public static void AttachTransaction(IDbDataAdapter obj)
        {
            if (obj.InsertCommand != null) { obj.InsertCommand.Transaction = DataLayer.Instance.trans; }
            if (obj.DeleteCommand != null) { obj.DeleteCommand.Transaction = DataLayer.Instance.trans; }
            if (obj.UpdateCommand != null) { obj.UpdateCommand.Transaction = DataLayer.Instance.trans; }

        }

        public static void TryCommit()
        {
            try
            {
                DataLayer.Instance.trans.Commit();
                //DataLayer.Instance.trans.Rollback();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DataLayer.Instance.trans.Rollback();
            }
        }

        public static IDbDataAdapter GetProductionAdapter()
        {
            IDbDataAdapter da;

            if (ProductionAdapter == null)
            {

                using (ProductionDAL pDAL = new ProductionDAL())
                {

                    da = pDAL.AdaptProduction();

                }

                ProductionAdapter = da;
                return da;
            }
            else
            {
                return ProductionAdapter;
            }
        }


        public static IDbDataAdapter GetBackupAdapter()
        {

            IDbDataAdapter da;
            using (ProductionDAL pDAL = new ProductionDAL())
            {
                da = pDAL.AdaptEventBackup();
            }
            return da;
        }

    }
}
