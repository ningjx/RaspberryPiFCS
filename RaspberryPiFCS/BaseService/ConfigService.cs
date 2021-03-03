using RaspberryPiFCS.Configs;
using System;
using System.Collections.Generic;
using System.Text;
using HelperLib;
using Newtonsoft.Json;

namespace RaspberryPiFCS
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public static class ConfigService
    {
        private static readonly string[] _sys = new string[] { "Configs", "SystemConfig.json" };//系统配置
        private static readonly string[] _equipment = new string[] { "Configs", "EquipmentConfig.json" };//设备配置
        private static readonly string[] _remoteControl = new string[] { "Configs", "RemoteControlConfig.json" };//遥控器配置

        public static SysConfig SysConfigs;
        public static RemoteConfigs RemoteConfigs;
        public static EquipmentConfig EquipmentConfigs;

        public static bool SaveConfig()
        {
            try
            {
                _sys.Write(JsonConvert.SerializeObject(SysConfigs));
                _equipment.Write(JsonConvert.SerializeObject(RemoteConfigs));
                _remoteControl.Write(JsonConvert.SerializeObject(EquipmentConfigs));
                return true;
            }
            catch (Exception ex)
            {
                LogService.Add(Enum.LogType.Debug, "读取配置信息失败", ex);
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
                LogService.Add(Enum.LogType.Error, "未能成功初始化配置信息", ex);
                return false;
            }
            return true;
        }

        private static void ReadSysConfig(string[] path)
        {
            string data = path.Read();
            if (string.IsNullOrEmpty(data))
            {
                SysConfigs = new SysConfig();
                path.Write(JsonConvert.SerializeObject(SysConfigs));
            }
            else
            {
                try
                {
                    SysConfigs = JsonConvert.DeserializeObject<SysConfig>(data);
                }
                catch (Exception ex)
                {
                    LogService.Add(Enum.LogType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
                    SysConfigs = new SysConfig();
                    path.Write(JsonConvert.SerializeObject(SysConfigs));
                }
            }
        }

        private static void ReadEquConfig(string[] path)
        {
            string data = path.Read();
            if (string.IsNullOrEmpty(data))
            {
                EquipmentConfigs = new EquipmentConfig();
                path.Write(JsonConvert.SerializeObject(EquipmentConfigs));
            }
            else
            {
                try
                {
                    EquipmentConfigs = JsonConvert.DeserializeObject<EquipmentConfig>(data);
                }
                catch (Exception ex)
                {
                    LogService.Add(Enum.LogType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
                    EquipmentConfigs = new EquipmentConfig();
                    path.Write(JsonConvert.SerializeObject(EquipmentConfigs));
                }
            }
            if (EquipmentConfigs.Count == 0)
                EquipmentConfigs.InitRemote();
        }

        private static void ReadRemConfig(string[] path)
        {
            string data = path.Read();
            if (string.IsNullOrEmpty(data))
            {
                RemoteConfigs = new RemoteConfigs();
                path.Write(JsonConvert.SerializeObject(RemoteConfigs));
            }
            else
            {
                try
                {
                    RemoteConfigs = JsonConvert.DeserializeObject<RemoteConfigs>(data);
                }
                catch (Exception ex)
                {
                    LogService.Add(Enum.LogType.Debug, $"未能成功初始化“{path[1].Replace(".json", "")}”的配置信息", ex);
                    RemoteConfigs = new RemoteConfigs();
                    path.Write(JsonConvert.SerializeObject(RemoteConfigs));
                }
            }
            if (RemoteConfigs.Count == 0)
                RemoteConfigs.InitRemote();
        }
    }
}
