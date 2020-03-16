using RaspberryPiFCS.Models;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Controller;
using RaspberryPiFCS.Helper;
using System;
using RaspberryPiFCS.ComputeCenter;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using RaspberryPiFCS.SystemMessage;

namespace RaspberryPiFCS
{
    public static class ControllerBus
    {
        public static RemoteController RemoteControl;
        public static SensorController SensorController;
        public static LoraController LoraControl;
        public static LEDController LedContorl;
        public static BaseController BaseContorl;
        public static PushBackController PushBackControl;
        public static QIFDController QIFDControl;
        public static TempController TempControl;
        private static ControlPolymerizer _controlPolymerize;
        static ControllerBus()
        {
            LunchLora();
            //SensorController = new SensorController();
            //Console.Write("启动基础控制器");
            //BaseContorl = new BaseController();
            //Console.WriteLine("------Finish\r");
            
            //Console.Write("启动灯光控制器");
            //LedContorl = new LEDController();
            //Console.WriteLine("------Finish\r");

            //Console.Write("启动反推控制器");
            //PushBackControl = new PushBackController();
            //Console.WriteLine("------Finish\r");


            //Console.Write("启动控制数据聚合");
            //_controlPolymerize = new ControlPolymerizer();
            //Console.WriteLine("------Finish\r");

            //Console.Write("初始化超声波测距");
            //QIFDControl = new QIFDController(28, 29);
            //Console.WriteLine("------Finish\r");

            //Console.Write("初始化温度传感01");
            //TempControl = new TempController();
            //Console.WriteLine("------Finish\r");
        }

        public static void SysLaunch()
        {
            
        }

        private static bool LunchLora()
        {
            try
            {
                LoraControl = new LoraController();
                ErrorMessage.LoraEvent += LoraControl.SendErrorData;
            }
            catch (Exception ex)
            {
                ErrorMessage.Add(Enum.ErrorType.Error, "启动远程数传时失败", ex);
                return false;
            }
            return true;
        }
    }
}
