using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiFMS.Configs;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.SystemMessage;

namespace RaspberryPiFMS
{
    public static class Lunch
    {
        public static Tuple<bool, string> StartUp()
        {
            try
            {
                Config.InitConfig();//初始化配置信息
                ControllerBus.SysLaunch();
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, $"系统启动失败------\n失败消息：{ex.Message}\n--------------------------------------------------------------------------\n失败位置：{ex.StackTrace}\n--------------------------------------------------------------------------\n");
            }
            return new Tuple<bool, string>(true, "启动完成");
        }
    }
}
