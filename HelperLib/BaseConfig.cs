using HelperLib.Interface;
using Newtonsoft.Json;
using System;

namespace HelperLib
{
    public static class BaseConfig
    {
        public static IConfig ConfigData;
        public static string[] Path = new string[] { "Config" };
        public static Type Type { get; set; }

        public static bool ReadConfig()
        {
            var str = FileHelper.Read(Path);
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    ConfigData = (IConfig)JsonConvert.DeserializeObject(str, Type);
                    return true;
                }
                catch (Exception ex)
                {
                    FileHelper.Write_Append(new string[] { "Log.txt" }, ex.ToString());
                    ConfigData = (IConfig)Activator.CreateInstance(Type);
                    return false;
                }
            }
            else
            {
                ConfigData = (IConfig)Activator.CreateInstance(Type);
                return false;
            }
        }
        public static bool SaveConfig()
        {
            try
            {
                FileHelper.Write(Path, JsonConvert.SerializeObject(ConfigData));
                return true;
            }
            catch (Exception ex)
            {
                FileHelper.Write_Append(new string[] { "Log.txt" }, ex.ToString());
                return false;
            }
        }
    }
}
