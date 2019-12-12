using Newtonsoft.Json;
using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaspberryPiFMS.Configs
{
    public class SysConfig
    {
        private readonly string[] path = new string[] { "Configs", "SystemConfig.json" };

        /// <summary>
        /// 温度传感器列表
        /// </summary>
        [JsonProperty("TempEquipment")]
        public Equipment TempEquipment;

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

    
}
