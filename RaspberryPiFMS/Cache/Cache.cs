using RaspberryPiFMS.Models;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;
using System;
using RaspberryPiFMS.ComputeCenter;
using Unosquare.WiringPi;
using System.Diagnostics;
using System.IO;

namespace RaspberryPiFMS
{
    /// <summary>
    /// 全局缓存
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// AP模式
        /// </summary>
        public static ContrlMode ContrlMode = ContrlMode.Manual;

        #region 数据
        /// <summary>
        /// 遥控数据
        /// </summary>
        public static RemoteControlModel RemoteSignal = new RemoteControlModel();

        /// <summary>
        /// 自动数据
        /// </summary>
        public static AutoControlModel AutoControlData = new AutoControlModel();

        /// <summary>
        /// 中心控制数据
        /// 在不同的控制模式下，自动控制数据和遥控数据会整合到中心数据上
        /// </summary>
        public static CenterControlModel CenterControlData = new CenterControlModel();

        public static MavlinkMessage1Hz MavlinkMessage1Hz = new MavlinkMessage1Hz();

        public static MavlinkMessage50Hz MavlinkMessage50Hz = new MavlinkMessage50Hz();
        #endregion
        #region Controller
        public static RemoteController RemoteControl;

        public static LEDController LedContorl;

        public static BaseController BaseContorl;

        public static PushBackController PushBackControl;

        public static QIFDController QIFDControl;

        public static TempController TempControl;
        #endregion

        public static bool IsRemoteConnected = true;

        /// <summary>
        /// 解码器锁，防止多线程修改遥控参数
        /// </summary>
        public static bool DecodingLock = false;

        public static I2CBus I2CBus = new I2CBus();

        public static Pca9685 BaseDriver;

        public static Pca9685 LedDriver;

        public static Pca9685 PushbackDriver;

        /// <summary>
        /// 丢失信号延迟时间
        /// </summary>
        public static int LosingSignalDelay = 3;

        /// <summary>
        /// 遥控器数据异常过滤-过滤阈值（角度）
        /// </summary>
        public static int De_Shanking = 20;

        public static double Distance;

        private static ControlPolymerize _controlPolymerize;

        static Cache()
        {
            //ContrlMode = ContrlMode.Manual;
            //IsRemoteConnected = true;
            //DecodingLock = false;
            //RemoteSignal = new RemoteControlModel();
            //AutoControlData = new AutoControlModel();
            //CenterControlData = new CenterControlModel();
            //De_Shanking = 50;

            Console.Write("初始化基础驱动器");
            BaseDriver = new Pca9685(0x42);
            Console.WriteLine("------Finish\r");

            Console.Write("启动基础控制器");
            BaseContorl = new BaseController();
            Console.WriteLine("------Finish\r");

            //Console.Write("初始化灯光驱动器");
            //_LedDriver = new Pca9685(0x60);
            //Console.WriteLine("------Finish\r");

            //Console.Write("启动灯光控制器");
            //LedContorl = new LEDController();
            //Console.WriteLine("------Finish\r");

            //Console.Write("初始化反推驱动器");
            //_PushbackDriver = new Pca9685(0x42);
            //Console.WriteLine("------Finish\r");

            //Console.Write("启动反推控制器");
            //PushBackControl = new PushBackController();
            //Console.WriteLine("------Finish\r");

            Console.Write("启动遥控接收器");
            StartRemote();
            RemoteControl = new RemoteController();
            Console.WriteLine("------Finish\r");

            //Console.Write("初始化超声波测距");
            //QIFDControl = new QIFDController(28, 29);
            //Console.WriteLine("------Finish\r");

            //Console.Write("初始化温度传感01");
            //TempControl = new TempController();
            //Console.WriteLine("------Finish\r");

            Console.Write("启动控制数据聚合");
            _controlPolymerize = new ControlPolymerize();
            Console.WriteLine("------Finish\r");

            Console.WriteLine("全局缓存初始化完成");
        }

        public static void SysStart()
        {
            Console.WriteLine("系统启动完毕\r");
        }

        private static void StartRemote()
        {
            var psi = new ProcessStartInfo("python", Path.Combine("PythonScripts", "ss.py")) { RedirectStandardOutput = true };
            //启动
            var proc = Process.Start(psi);
            if (proc == null)
                throw new Exception("遥控器模块启动失败");
            //using (var sr = proc.StandardOutput)
            //{
            //    Console.WriteLine(sr.ReadToEnd());
            //} 
        }
    }
}
