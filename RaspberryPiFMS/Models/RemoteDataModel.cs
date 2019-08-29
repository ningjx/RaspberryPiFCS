using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RaspberryPiFMS.Models
{
    /// <summary>
    /// 用于网络传输遥控数据的数据结构
    /// </summary>
    public class RemoteDataModel
    {
        /// <summary>
        /// 偏航数据
        /// </summary>
        [JsonProperty("Yaw")]
        public double yaw;

        /// <summary>
        /// 滚转
        /// </summary>
        [JsonProperty("Roll")]
        public double roll;

        /// <summary>
        /// 俯仰
        /// </summary>
        [JsonProperty("Pitch")]
        public double pitch;

        /// <summary>
        /// 襟翼
        /// </summary>
        [JsonProperty("Flap")]
        public double flap;

        /// <summary>
        /// 节流阀
        /// </summary>
        [JsonProperty("Throttle")]
        public double throttle;

        /// <summary>
        /// 起落架
        /// </summary>
        [JsonProperty("Gear")]
        public bool gear;

        /// <summary>
        /// 减速板
        /// </summary>
        [JsonProperty("AirBreak")]
        public double airBreak;

        /// <summary>
        /// 反推
        /// </summary>
        [JsonProperty("PushBack")]
        public bool pushBack;

        /// <summary>
        /// 垂直导航
        /// </summary>
        [JsonProperty("VerticalNavigation")]
        public bool vnav;

        /// <summary>
        /// 水平导航
        /// </summary>
        [JsonProperty("LateralNavigation")]
        public bool lnav;

        /// <summary>
        /// 数据时间戳
        /// </summary>
        [JsonProperty("TimeStamp")]
        public long timeStamp;

        [JsonProperty("Trim")]
        public double trim;
        //[JsonProperty("CustomB")]
        //public bool customB;

        //[JsonProperty("CustomA")]
        //public bool customA;

        //[JsonProperty("MenuL")]
        //public bool menuL;

        //[JsonProperty("MenuR")]
        //public bool menuR;


        public void SetDefault()
        {
            yaw = 0;
            roll = 0;
            pitch = 0;
            flap = 0;
            throttle = 0;

            gear = true;
            airBreak = 0;
            pushBack = false;
            trim = 0;

            lnav = false;
            vnav = false;
        }
    }
}
