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
        #region 操作数据
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
        /// 减速板0-70
        /// </summary>
        [JsonProperty("AirBreak")]
        public double airBreak;

        /// <summary>
        /// 反推
        /// </summary>
        [JsonProperty("PushBack")]
        public bool pushBack;

        /// <summary>
        /// 配平
        /// </summary>
        [JsonProperty("Trim")]
        public double trim;

        #endregion

        /// <summary>
        /// 数据时间戳
        /// </summary>
        [JsonProperty("TimeStamp")]
        public long timeStamp;

        
        #region 导航设定
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
        /// 自动配平
        /// </summary>
        [JsonProperty("AutoTrim")]
        public bool autoTrim;

        /// <summary>
        /// 自动油门
        /// </summary>
        [JsonProperty("AutoThrottel")]
        public bool autoThrottel;
        #endregion

        #region 灯光组

        /// <summary>
        /// 滑行灯
        /// </summary>
        [JsonProperty("TaxiLight")]
        public bool taxiLight;
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        [JsonProperty("RunwayLight")]
        public bool runwayLight;
        /// <summary>
        /// 标志灯
        /// </summary>
        [JsonProperty("LogoLight")]
        public bool logoLight;
        /// <summary>
        /// 着陆灯
        /// </summary>
        [JsonProperty("LandingLight")]
        public bool landingLight;
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        [JsonProperty("WingInspectionLight")]
        public bool wingInspectionLight;
        /// <summary>
        /// 位置灯
        /// </summary>
        [JsonProperty("PositionLight")]
        public bool positionLight;
        /// <summary>
        /// 防撞灯
        /// </summary>
        [JsonProperty("AntiCollisionLight")]
        public bool antiCollisionLight;
        #endregion

        public RemoteDataModel()
        {
            timeStamp = 0;
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
