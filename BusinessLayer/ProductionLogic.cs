using System;
using System.Collections.Generic;

using System.Data;

namespace BusinessLayer.Productions
{
    using DAL.Productions;
    using DAL.Factory;
    public class ProductionLogic: BaseLogLogic
    {
        private static string _tableName;

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
                return _tableName = ProductionDAL.TableName.Replace("[", string.Empty).Replace("]", string.Empty);
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
