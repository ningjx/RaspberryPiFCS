using RaspberryPiFMS.Models;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;
using System;
using RaspberryPiFMS.ComputeCenter;
using Unosquare.WiringPi;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RaspberryPiFMS
{
    public static class Bus
    {
        /// <summary>
        /// AP模式
        /// </summary>
        public static ContrlMode ContrlMode = ContrlMode.Manual;

        #region Controller
        public static RemoteController RemoteControl;

        public static LEDController LedContorl;

        public static BaseController BaseContorl;

        public static PushBackController PushBackControl;

        public static QIFDController QIFDControl;

        public static TempController TempControl;
        #endregion 
        public static I2CBus I2CBus;
        private static ControlPolymerize _controlPolymerize;
        static Bus()
        {
            Console.Write("启动IIC总线");
            I2CBus = new I2CBus();
            Console.WriteLine("------Finish\r");

            Console.Write("启动基础控制器");
            BaseContorl = new BaseController();
            Console.WriteLine("------Finish\r");

            //Console.Write("启动灯光控制器");
            //LedContorl = new LEDController();
            //Console.WriteLine("------Finish\r");

            //Console.Write("启动反推控制器");
            //PushBackControl = new PushBackController();
            //Console.WriteLine("------Finish\r");

            Console.Write("启动遥控接收器");
            StartRemote();
            RemoteControl = new RemoteController();
            Console.WriteLine("------Finish\r");

            Console.Write("启动控制数据聚合");
            _controlPolymerize = new ControlPolymerize();
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

        private static void StartRemote()
        {
            var psi = new ProcessStartInfo("python", Path.Combine("PythonScripts", "ss.py")) { RedirectStandardOutput = true };
            //启动
            var proc = Process.Start(psi);
            Thread.Sleep(10);
            if (proc == null)
                throw new Exception("遥控器模块启动失败");
            //using (var sr = proc.StandardOutput)
            //{
            //    Console.WriteLine(sr.ReadToEnd());
            //} 
        }
    }
}
