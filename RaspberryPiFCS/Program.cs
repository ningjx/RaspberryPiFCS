using RaspberryPiFCS.Main;
using System;

namespace RaspberryPiFCS
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //读取配置信息
                Config.ReadConfig();
                //Config.SaveConfig();
                //注册系统//启动远程通信；
                EquipmentBus.ControllerRegister.Register(Enum.RegisterType.Sys, true);




                //启动其它设备
                EquipmentBus.Lunch();
                //启动function
                FunctionWatcher.Lunch();


            }
            catch (Exception ex)
            {
                Logger.Add(Enum.LogType.Error, "系统异常", ex);
            }
            finally
            {
                Config.SaveConfig();
            }
        }
    }
}
