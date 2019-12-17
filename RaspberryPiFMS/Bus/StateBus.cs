using RaspberryPiFMS.Configs;
using RaspberryPiFMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaspberryPiFMS
{
    public static class StateBus
    {
        public static IPInfo IPInfo = new IPInfo();

        /// <summary>
        /// 配置信息中，日期最大的遥控器配置为当前生效配置
        /// </summary>
        public static RemoteControl ControlConfig
        {
            get
            {
                if (Config.SysConfig.RemoteConfigs.Count == 0)
                    return null;
                var time = Config.SysConfig.RemoteConfigs.Values.Max(t => t.EffctiveTime);
                return Config.SysConfig.RemoteConfigs.Values.Where(t => t.EffctiveTime == time).FirstOrDefault();
            }
            set
            {
                string controlName = value.ControlName;
                value.EffctiveTime = DateTime.Now;
                Config.SysConfig.RemoteConfigs.AddOrUpdate(controlName, value); Config.SaveConfig();
            }
        }

        public static OriginSignal OriginSignal = new OriginSignal();
    }
}
