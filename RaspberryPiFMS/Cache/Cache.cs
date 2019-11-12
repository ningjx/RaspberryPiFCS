using RaspberryPiFMS.Models;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;
using System;

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
        public static ContrlMode ContrlMode;

        /// <summary>
        /// 遥控数据
        /// </summary>
        public static RemoteControlModel RemoteSignal;

        /// <summary>
        /// 自动数据
        /// </summary>
        public static AutoControlModel AutoControlData;

        /// <summary>
        /// 丢失信号延迟时间
        /// </summary>
        public static int LosingSignalDelay;

        /// <summary>
        /// 灯光控制器
        /// </summary>
        public static LEDController LedContorl;

        /// <summary>
        /// 基础动作控制器
        /// </summary>
        public static BaseController BaseContorl;

        public static PushBackController PushBackControl;

        /// <summary>
        /// 实时的遥控信号连接状态
        /// </summary>
        public static bool IsRemoteConnected;

        /// <summary>
        /// 解码器锁，防止多线程修改遥控参数
        /// </summary>
        public static bool DecodingLock;

        public static RemoteController RemoteController;

        /// <summary>
        /// 基础动作驱动器
        /// </summary>
        private static Pca9685 _BaseDriver;

        /// <summary>
        /// 灯光驱动器
        /// </summary>
        private static Pca9685 _LedDriver;

        /// <summary>
        /// 反推驱动器
        /// </summary>
        private static Pca9685 _PushbackDriver;



        /// <summary>
        /// 遥控器数据异常过滤-过滤阈值（角度）
        /// </summary>
        public static int De_Shanking;
        static Cache()
        {
            ContrlMode = ContrlMode.Manual;
            IsRemoteConnected = true;
            DecodingLock = false;
            LosingSignalDelay = 3;
            RemoteSignal = new RemoteControlModel();
            AutoControlData = new AutoControlModel();

            Console.Write("初始化基础驱动器");
            _BaseDriver = new Pca9685(0x40);
            Console.WriteLine("------Finish\r");

            Console.Write("初始化基础控制器");
            BaseContorl = new BaseController(_BaseDriver);
            Console.WriteLine("------Finish\r");
            #region 未实现的控制器
            Console.Write("初始化灯光驱动器");
            _LedDriver = new Pca9685(0x60);
            Console.WriteLine("------Finish\r");

            Console.Write("初始化灯光控制器");
            LedContorl = new LEDController(_LedDriver);
            Console.WriteLine("------Finish\r");

            Console.Write("初始化反推驱动器");
            _PushbackDriver = new Pca9685(0x42);
            Console.WriteLine("------Finish\r");

            Console.Write("初始化反推控制器");
            PushBackControl = new PushBackController(_PushbackDriver);
            Console.WriteLine("------Finish\r");
            #endregion
            Console.Write("初始化遥控接收器");
            RemoteController = new RemoteController();
            Console.WriteLine("------Finish\r");
            Console.WriteLine("全局缓存初始化完成");
        }
    }
}
