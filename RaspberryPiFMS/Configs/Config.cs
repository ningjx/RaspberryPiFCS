using Newtonsoft.Json;
using RaspberryPiFMS.Helper;
using System;

namespace RaspberryPiFMS.Configs
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class Config
    {
        public static SysConfig SysConfig;
        static Config()
        {
            string[] path = new string[] { "Configs", "SystemConfig.json" };
            string data = path.Read();
            if (string.IsNullOrEmpty(data))
            {
                SysConfig = new SysConfig();
                path.Write(JsonConvert.SerializeObject(SysConfig));
            }
            else
            {
                try
                {
                    SysConfig = JsonConvert.DeserializeObject<SysConfig>(data);
                }
                catch (Exception)
                {
                    SysConfig = new SysConfig();
                    path.Write(JsonConvert.SerializeObject(SysConfig));
                }
            }
        }

        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <param name="configName">参数名</param>
        /// <param name="value">要修改的值</param>
        public static void ChangeConfig(string configName, object value)
        {
            var currentValue = typeof(SysConfig).GetField(configName).GetValue(SysConfig);
            if (currentValue == null)
                throw new Exception("没有这个参数");
            if (value == currentValue)
                return;
            typeof(SysConfig).GetField(configName).SetValue(SysConfig, value);
            string[] path = new string[] { "Configs", "SystemConfig.json" };
            path.Write(JsonConvert.SerializeObject(SysConfig));
        }

        public static void SaveConfig()
        {
            string[] path = new string[] { "Configs", "SystemConfig.json" };
            path.Write(JsonConvert.SerializeObject(SysConfig));
        }
    }
}
