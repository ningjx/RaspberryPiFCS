using RaspberryPiFCS.Models;
using System;
using RaspberryPiFCS.Configs;
using System.Linq;
using RaspberryPiFCS.Main;

namespace RaspberryPiFCS
{
    public static class SignalBus
    {
        #region 挂载控制信号
        /// <summary>
        /// 遥控配置信息
        /// 配置信息中，日期最大的遥控器配置为当前生效配置
        /// </summary>
        public static RemoteConfig ControlConfig
        {
            get
            {
                if (ConfigService.RemoteConfigs.Count == 0)
                    return null;
                var time = ConfigService.RemoteConfigs.Values.Max(t => t.EffctiveTime);
                return ConfigService.RemoteConfigs.Values.FirstOrDefault(t => t.EffctiveTime == time);
            }
            set
            {
                string controlName = value.ControlName;
                value.EffctiveTime = DateTime.Now;
                ConfigService.RemoteConfigs.AddOrUpdate(controlName, value);
                ConfigService.SaveConfig();
            }
        }

        /// <summary>
        /// 原始遥控信号
        /// </summary>
        public static RemoteSignal OriginSignal = new RemoteSignal();

        /// <summary>
        /// 转换后的遥控信号
        /// </summary>
        public static ControlSignal RemoteSignal = new ControlSignal();

        /// <summary>
        /// 中心控制信号
        /// </summary>
        public static CenterSignal CenterSignal = new CenterSignal();

        /// <summary>
        /// 自动控制信号
        /// </summary>
        public static AutoSignal AutoSignal = new AutoSignal();
        #endregion
    }
}
