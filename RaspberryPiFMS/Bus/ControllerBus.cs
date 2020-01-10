using RaspberryPiFMS.Models;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;
using System;
using RaspberryPiFMS.ComputeCenter;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace RaspberryPiFMS
{
    public static class ControllerBus
    {
        public static RemoteController RemoteControl;
        public static LoraController LoraControl;
        public static LEDController LedContorl;
        public static BaseController BaseContorl;
        public static PushBackController PushBackControl;
        public static QIFDController QIFDControl;
        public static TempController TempControl;
        private static ControlPolymerizer _controlPolymerize;
        static ControllerBus()
        {
            Console.Write("启动基础控制器");
            BaseContorl = new BaseController();
            Console.WriteLine("------Finish\r");
            LoraControl = new LoraController();
            //Console.Write("启动灯光控制器");
            //LedContorl = new LEDController();
            //Console.WriteLine("------Finish\r");

            //Console.Write("启动反推控制器");
            //PushBackControl = new PushBackController();
            //Console.WriteLine("------Finish\r");


            Console.Write("启动控制数据聚合");
            _controlPolymerize = new ControlPolymerizer();
            Console.WriteLine("------Finish\r");

            //Console.Write("初始化超声波测距");
            //QIFDControl = new QIFDController(28, 29);
            //Console.WriteLine("------Finish\r");

            //Console.Write("初始化温度传感01");
            //TempControl = new TempController();
            //Console.WriteLine("------Finish\r");
        }

        public static void SysLaunch()
        {
            Console.WriteLine("系统启动完毕\r");
        }

    }
}
