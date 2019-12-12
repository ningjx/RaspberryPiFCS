using Newtonsoft.Json;
using RaspberryPiFMS.Helper;
using System;

namespace RaspberryPiFMS.Models
{
    public class SysConfig
    {
        private readonly string[] path = new string[] { "Configs", "SystemConfig.json" };

        /// <summary>
        /// 遥控信号丢失后自动控制的延迟时间
        /// </summary>
        [JsonProperty("LosingSignalDelay")]
        public int LosingSignalDelay;

        /// <summary>
        /// 副翼最大角度阈值
        /// </summary>
        [JsonProperty("AngleLimit_Roll")]
        public float AngleLimit_Roll;

        /// <summary>
        /// 升降舵最大角度阈值
        /// </summary>
        [JsonProperty("AngleLimit_Pitch")]
        public float AngleLimit_Pitch;

        /// <summary>
        /// 方向舵最大角度阈值
        /// </summary>
        [JsonProperty("AngleLimit_Yaw")]
        public float AngleLimit_Yaw;

        /// <summary>
        /// 配平舵最大角度阈值
        /// </summary>
        [JsonProperty("AngleLimit_Trim")]
        public float AngleLimit_Trim;

        /// <summary>
        /// 不要使用这个，要初始化配置文件请调用<see cref="Config.InitConfig()"/>
        /// </summary>
        [Obsolete("不要使用这个直接初始化配置文件，要初始化请调用Config.InitConfig()")]
        public SysConfig()
        {
            LosingSignalDelay = 3;
            AngleLimit_Roll = 50;
            AngleLimit_Pitch = 50;
            AngleLimit_Yaw = 50;
            AngleLimit_Trim = 50;
        }
    }

    /// <summary>
    /// 配置文件
    /// </summary>
    public static class Config
    {
        public
        /// <summary>
        /// 初始化载入配置文件
        /// </summary>
        /// <returns></returns>
        public static SysConfig InitConfig()
        {
            string[] path = new string[] { "Configs", "SystemConfig.json" };
            string data = path.Read();
            if (string.IsNullOrEmpty(data))
            {
                SysConfig config = new SysConfig();
                path.Write(JsonConvert.SerializeObject(config));
                return config;
            }
            return JsonConvert.DeserializeObject<SysConfig>(data);
        }

        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configName">参数名</param>
        /// <param name="value">要修改的值</param>
        public static void ChangeConfig(this SysConfig config, string configName, object value)
        {
            var currentValue = typeof(SysConfig).GetField(configName).GetValue(config);
            if (currentValue == null)
                throw new Exception("没有这个参数");
            if (value == currentValue)
                return;
            typeof(SysConfig).GetField(configName).SetValue(config, value);
            string[] path = new string[] { "Configs", "SystemConfig.json" };
            path.Write(JsonConvert.SerializeObject(config));
        }
    }
}
