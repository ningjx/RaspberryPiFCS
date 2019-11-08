using RaspberryPiFMS.Models;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMS
{
    public static class Config
    {
        /// <summary>
        /// AP模式
        /// </summary>
        public static ContrlMode ContrlMode;

        /// <summary>
        /// 遥控数据
        /// </summary>
        public static SbusModel RemoteSignal;

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

        public static bool DecodingLock;
        static Config()
        {
            IsRemoteConnected = true;
            DecodingLock = false;
            //BaseDriver = new Pca9685();
            //LedAndPushbackDriver = new Pca9685();
            LosingSignalDelay = 3;
            RemoteSignal = new SbusModel();
            //LedContorl = new LEDController();
            //BaseContorl = new BaseController();
        }
    }
}
