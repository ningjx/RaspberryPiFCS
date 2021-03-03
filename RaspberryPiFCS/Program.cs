using RaspberryPiFCS.Drivers;
using RaspberryPiFCS.Main;
using System;
using System.Diagnostics;

namespace RaspberryPiFCS
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //读取配置信息
                ConfigService.ReadConfig();
                //注册系统//启动远程通信；
                EquipmentBus.ControllerRegister.Register(Enum.RegisterType.Sys, true);
        
                //启动其它设备
                //EquipmentBus.Lunch();
                //启动function
                FunctionWatcher.Lunch();
        
        
            }
            catch (Exception ex)
            {
                LogService.Add(Enum.LogType.Error, "系统异常", ex);
            }
            finally
            {
                ConfigService.SaveConfig();
            }
        }
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        //var driver = DriversFactory.GetUARTDriver("ttyUSB0");
        //        //driver.RecEvent += Driver_RecEvent;
        //        //Console.Read();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //}
        //
        //private static void Driver_RecEvent(byte[] bytes)
        //{
        //    Console.WriteLine("________________");
        //    Console.WriteLine(bytes.BytesToStr());
        //}
    }
}
