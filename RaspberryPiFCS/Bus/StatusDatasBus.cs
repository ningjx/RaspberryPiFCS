using RaspberryPiFCS.Configs;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Models;
using RaspberryPiFCS.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaspberryPiFCS
{
    public static class StatusDatasBus
    {
        #region 挂载控制信号
        /// <summary>
        /// 遥控配置信息
        /// 配置信息中，日期最大的遥控器配置为当前生效配置
        /// </summary>
        public static RemoteControl ControlConfig
        {
            get
            {
                if (Config.RemoteConfigs.Count == 0)
                    return null;
                var time = Config.RemoteConfigs.Values.Max(t => t.EffctiveTime);
                return Config.RemoteConfigs.Values.Where(t => t.EffctiveTime == time).FirstOrDefault();
            }
            set
            {
                string controlName = value.ControlName;
                value.EffctiveTime = DateTime.Now;
                Config.RemoteConfigs.AddOrUpdate(controlName, value); Config.SaveConfig();
            }
        }

        /// <summary>
        /// 原始遥控信号
        /// </summary>
        public static OriginSignal OriginSignal = new OriginSignal();

        /// <summary>
        /// 转换后的遥控信号
        /// </summary>
        public static RemoteSignal RemoteSignal = new RemoteSignal();

        /// <summary>
        /// 中心控制信号
        /// </summary>
        public static CenterSignal CenterSignal = new CenterSignal();

        /// <summary>
        /// 自动控制信号
        /// </summary>
        public static AutoSignal AutoSignal = new AutoSignal();
        #endregion

        /// <summary>
        /// 数据
        /// </summary>
        public static FlightData FlightData = new FlightData();

        public static FlightStatus FlightStatus = new FlightStatus();
    }
}
