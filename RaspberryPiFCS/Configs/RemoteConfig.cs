using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RaspberryPiFCS.Enum;

namespace RaspberryPiFCS.Configs
{
    /// <summary>
    /// 遥控器列表
    /// </summary>
    public class RemoteConfigs : Dictionary<string, RemoteConfig>
    {

    }

    public class RemoteConfig
    {
        /// <summary>
        /// 遥控器名称
        /// </summary>
        [JsonProperty("遥控器名称")]
        public string ControlName { get; set; }

        /// <summary>
        /// 通道数量
        /// </summary>
        [JsonProperty("通道数量")]
        public int ChannelCount { get { return Channels == null ? 0 : Channels.Count; } }

        /// <summary>
        /// 通道信息
        /// </summary>
        [JsonProperty("通道信息")]
        public List<Channel> Channels;

        /// <summary>
        /// 生效时间
        /// </summary>
        [JsonProperty("生效时间")]
        public DateTime EffctiveTime;
    }

    /// <summary>
    /// 通道
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// 通道序号
        /// </summary>
        [JsonProperty("通道序号(禁止修改)")]
        public int ChannelNum;

        /// <summary>
        /// 通道类型
        /// </summary>
        [JsonProperty("通道类型")]
        public ChannelType ChannelType;

        /// <summary>
        /// 通道映射
        /// </summary>
        [JsonProperty("通道映射")]
        public RemoteMapping RemoteMapping;

        /// <summary>
        /// 通道中值
        /// </summary>
        [JsonProperty("通道中值")]
        public long MidValue;

        /// <summary>
        /// 通道最大值
        /// </summary>
        [JsonProperty("通道最大值")]
        public long MaxValue;

        /// <summary>
        /// 通道最小值
        /// </summary>
        [JsonProperty("通道最小值")]
        public long MinValue;

        /// <summary>
        /// 最大角度
        /// </summary>
        [JsonProperty("最大角度")]
        public float MaxAngle;

        /// <summary>
        /// 最小角度
        /// </summary>
        [JsonProperty("最小角度")]
        public float MinAngle;

        /// <summary>
        /// 是否翻转
        /// </summary>
        [JsonProperty("是否翻转")]
        public bool Reverse;
    }

    /// <summary>
    /// 通道类型
    /// </summary>
    public enum ChannelType
    {
        /// <summary>
        /// 开关型
        /// </summary>
        [JsonProperty("开关型")]
        Switch,

        /// <summary>
        /// 摇杆型
        /// </summary>
        [JsonProperty("摇杆型")]
        Rocker
    }

    public static class RemoteExtension
    {
        public static void InitRemote(this RemoteConfigs RemoteConfigs)
        {

        }
    }
}
