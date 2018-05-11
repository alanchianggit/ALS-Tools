using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.SettingsLogic
{
    using DAL.Factory;

    public static class SettingsLogic
    {
        public static void ChangeFactorySettings(string argName, string val)
        {
            DataLayer.ChangeSettings(argName, val);

        }

        public static string GetFactorySettings(string arg)
        {
            string result = DataLayer.GetSettings(arg);
            return result;
        }
    }
}
