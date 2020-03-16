using Newtonsoft.Json;
using RaspberryPiFCS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaspberryPiFCS.Configs
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SysConfig
    {
        /// <summary>
        /// 遥控信号丢失后自动控制的延迟时间
        /// </summary>
        [JsonProperty("丢失信号检测时间（秒）")]
        public int LosingSignalDelay;

        /// <summary>
        /// 副翼最大角度阈值
        /// </summary>
        [JsonProperty("副翼最大阈值")]
        public float AngleLimit_Roll;

        /// <summary>
        /// 升降舵最大角度阈值
        /// </summary>
        [JsonProperty("升降舵最大阈值")]
        public float AngleLimit_Pitch;

        /// <summary>
        /// 方向舵最大角度阈值
        /// </summary>
        [JsonProperty("方向舵最大阈值")]
        public float AngleLimit_Yaw;

        /// <summary>
        /// 配平最大角度阈值
        /// </summary>
        [JsonProperty("配平最大阈值")]
        public float AngleLimit_Trim;

        /// <summary>
        /// 系统刷新频率（毫秒）
        /// </summary>
        [JsonProperty("系统刷新频率（毫秒）")]
        public int SysFrequency;

        public SysConfig()
        {
            LosingSignalDelay = 3;
            AngleLimit_Roll = 50;
            AngleLimit_Pitch = 50;
            AngleLimit_Yaw = 50;
            AngleLimit_Trim = 50;
            SysFrequency = 20;
        }
    }
}
