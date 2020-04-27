using HelperLib;
using HelperLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryPiClient
{
    public static class Config
    {
        static Config()
        {
            BaseConfig.ConfigData = new ConfigData();
            BaseConfig.Type = typeof(ConfigData);
        }

        public static void ReadConfig()
        {
            BaseConfig.ReadConfig();
        }

        public static void SaveConfig()
        {
            BaseConfig.ReadConfig();
        }
    }

    public class ConfigData : IConfig
    {

    }
}
