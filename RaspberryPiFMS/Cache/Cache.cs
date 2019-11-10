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

        /// <summary>
        /// 基础动作驱动器
        /// </summary>
        public static Pca9685 BaseDriver;

        /// <summary>
        /// 灯光/反推驱动器
        /// </summary>
        public static Pca9685 LedAndPushbackDriver;

        /// <summary>
        /// 实时的遥控信号连接状态
        /// </summary>
        public static bool IsRemoteConnected;

        /// <summary>
        /// 解码器锁，防止多线程修改遥控参数
        /// </summary>
        public static bool DecodingLock;

        
        static Cache()
        {
            ContrlMode = ContrlMode.Manual;
            IsRemoteConnected = true;
            DecodingLock = false;
            Console.WriteLine("初始化Pca9685");
            BaseDriver = new Pca9685();
            //LedAndPushbackDriver = new Pca9685();
            LosingSignalDelay = 3;
            RemoteSignal = new RemoteControlModel();
            AutoControlData = new AutoControlModel();
            //LedContorl = new LEDController();
            Console.WriteLine("初始化基础控制器");
            BaseContorl = new BaseController();
            Console.WriteLine("初全局缓存初始化完成");
        }
    }
}
