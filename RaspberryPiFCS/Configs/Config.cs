using Newtonsoft.Json;
using RaspberryPiFCS.Helper;
using RaspberryPiFCS.SystemMessage;
using System;

namespace RaspberryPiFCS.Configs
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class Config
    {
        private static string[] _sys = new string[] { "Configs", "SystemConfig.json" };//系统配置
        private static string[] _equipment = new string[] { "Configs", "EquipmentConfig.json" };//设备配置
        private static string[] _remoteControl = new string[] { "Configs", "RemoteControlConfig.json" };//遥控器配置

        public static SysConfig SysConfig;//{ get { return SysConfig; } set { } }
        public static RemoteConfigs RemoteConfigs;
        public static Equipment EquipmentConfigs;

        static Config()
        {

        }

        [Obsolete]
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
            _sys.Write(JsonConvert.SerializeObject(SysConfig));
            _equipment.Write(JsonConvert.SerializeObject(RemoteConfigs));
            _remoteControl.Write(JsonConvert.SerializeObject(EquipmentConfigs));
        }

        public static bool InitConfig()
        {
            try
            {
                ReadSysConfig(_sys);
                ReadEquConfig(_equipment);
                ReadRemConfig(_remoteControl);
            }
            catch(Exception ex)
            {
                ErrorMessage.Add(Enum.ErrorType.Error, "未能成功初始化配置信息", ex);
                return false;
            }
            return true;
        }

        private static void ReadSysConfig(string[] path)
        {
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
                catch (Exception ex)
                {
                    ErrorMessage.Add(Enum.ErrorType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
                    SysConfig = new SysConfig();
                    path.Write(JsonConvert.SerializeObject(SysConfig));
                }
            }
        }

        private static void ReadEquConfig(string[] path)
        {
            string data = path.Read();
            if (string.IsNullOrEmpty(data))
            {
                EquipmentConfigs = new Equipment();
                path.Write(JsonConvert.SerializeObject(SysConfig));
            }
            else
            {
                try
                {
                    EquipmentConfigs = JsonConvert.DeserializeObject<Equipment>(data);
                }
                catch (Exception ex)
                {
                    ErrorMessage.Add(Enum.ErrorType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
                    EquipmentConfigs = new Equipment();
                    path.Write(JsonConvert.SerializeObject(SysConfig));
                }
            }
        }

        private static void ReadRemConfig(string[] path)
        {
            string data = path.Read();
            if (string.IsNullOrEmpty(data))
            {
                RemoteConfigs = new RemoteConfigs();
                path.Write(JsonConvert.SerializeObject(SysConfig));
            }
            else
            {
                try
                {
                    RemoteConfigs = JsonConvert.DeserializeObject<RemoteConfigs>(data);
                }
                catch (Exception ex)
                {
                    ErrorMessage.Add(Enum.ErrorType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
                    RemoteConfigs = new RemoteConfigs();
                    path.Write(JsonConvert.SerializeObject(SysConfig));
                }
            }
        }
    }
}
