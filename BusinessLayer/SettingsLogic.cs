namespace BusinessLayer.SettingsLogic
{
    using System.Data;
    using DAL.Factory;

    public static class SettingsLogic
    {
        public static void SetFactorySettings(string argName, string val)
        {
            DataLayer.ChangeSettings(argName, val);

        }

        public static string GetFactorySetting(string arg)
        {
            string result = DataLayer.GetSetting(arg);
            return result;
        }

        public static DataTable GetFactorySettings()
        {
            DataTable dt = new DataTable("Configurations");
            dt = DataLayer.GetSettings();

            return dt;

        }
    }
}
