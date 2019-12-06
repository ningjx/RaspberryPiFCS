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
    public static class Bus
    {
        #region 各种参数
        //丢失信号延迟时间/秒
        public static int LosingSignalDelay = 3;

        //遥控器数据异常过滤-过滤阈值/角度
        public static int De_Shanking = 20;

        public static double AngleLimit_Roll = 50;
        public static double AngleLimit_Pitch = 50;
        public static double AngleLimit_Yaw = 50;
        #endregion

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

        public static CenterControlModel CenterData = new CenterControlModel();

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

        #region 各种锁
        // 解码器锁，防止多线程修改遥控参数
        public static bool DecodingLock = false;
        public static bool IsRemoteConnected = true;
        #endregion
        public static I2CBus I2CBus;
        private static ControlPolymerize _controlPolymerize;
        static Bus()
        {
            try
            {
                AngleLimit_Roll = 30;
                AngleLimit_Pitch = 50;
                AngleLimit_Yaw = 50;

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
            catch(Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
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
            if (proc == null)
                throw new Exception("遥控器模块启动失败");
            //using (var sr = proc.StandardOutput)
            //{
            //    Console.WriteLine(sr.ReadToEnd());
            //} 
        }
    }
}
