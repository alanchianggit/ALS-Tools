using System;
using System.Collections.Generic;
using System.Data;


namespace BusinessLayer.Events
{
    using BusinessLayer;
    using DAL.Events;
    using DAL.Factory;
    public class EventLogic : BaseLogic
    {
        private static string _tableName;
        private static IDbDataAdapter _eventadapter;
        private static IDbTransaction _eventtrans;


        public static IDbDataAdapter Eventadapter
        {
            get
            {
                return _eventadapter;
            }

            set
            {
                _eventadapter = value;
            }
        }

        public static IDbTransaction EventTrans
        {
            get
            {
                return _eventtrans;
            }

            set
            {
                _eventtrans = value;
            }
        }

        public static string TableName
        {
            get
            {
                return _tableName = EventDAL.TableName.Replace("[", string.Empty).Replace("]", string.Empty);
            }
        }

        public static IDbDataAdapter GetEventAdapter()
        {
            IDbDataAdapter da;

            if (Eventadapter == null)
            {

                using (EventDAL eDAL = new EventDAL())
                {

                    da = eDAL.AdaptEvent();

                }

                Eventadapter = da;
                return da;
            }
            else
            {
                return Eventadapter;
            }
        }

        public static IDbDataAdapter GetBackupAdapter()
        {

            IDbDataAdapter da;
            using (EventDAL eDAL = new EventDAL())
            {
                da = eDAL.AdaptEventBackup();
            }
            return da;
        }

        public static DataTable GetLogIDs()
        {

            DataTable da;
            using (EventDAL eDAL = new EventDAL())
            {
                da = eDAL.ReadAvailableLogs();

            }
            return da;
        }
    }
}
