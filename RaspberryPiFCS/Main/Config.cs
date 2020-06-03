using RaspberryPiFCS.Configs;
using System;
using System.Collections.Generic;
using System.Text;
using HelperLib;
using Newtonsoft.Json;

namespace RaspberryPiFCS.Main
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class Config
    {
        private static string[] _sys = new string[] { "Configs", "SystemConfig.json" };//系统配置
        private static string[] _equipment = new string[] { "Configs", "EquipmentConfig.json" };//设备配置
        private static string[] _remoteControl = new string[] { "Configs", "RemoteControlConfig.json" };//遥控器配置

        public static SysConfig SysConfig;
        public static RemoteConfigs RemoteConfigs;
        public static Equipment EquipmentConfigs;

        public static bool SaveConfig()
        {
            try
            {
                _sys.Write(JsonConvert.SerializeObject(SysConfig));
                _equipment.Write(JsonConvert.SerializeObject(RemoteConfigs));
                _remoteControl.Write(JsonConvert.SerializeObject(EquipmentConfigs));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Add(Enum.LogType.Debug, "读取配置信息失败", ex);
                return false;
            }
        }

        public static bool ReadConfig()
        {
            try
            {
                ReadSysConfig(_sys);
                ReadEquConfig(_equipment);
                ReadRemConfig(_remoteControl);
            }
            catch (Exception ex)
            {
                Logger.Add(Enum.LogType.Error, "未能成功初始化配置信息", ex);
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
                    Logger.Add(Enum.LogType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
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
                    Logger.Add(Enum.LogType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
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
                    Logger.Add(Enum.LogType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
                    RemoteConfigs = new RemoteConfigs();
                    path.Write(JsonConvert.SerializeObject(SysConfig));
                }
            }
        }
    }
}
