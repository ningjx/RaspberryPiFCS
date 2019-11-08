using RaspberryPiFMS.Models;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMS
{
    public static class Config
    {
        public static ContrlMode ContrlMode;

        public static SbusModel RemoteSignal;

        public static int LosingSignalDelay;

        public static LEDController Led;

        public static Pca9685 BaseControl;

        public static Pca9685 LedAndPushback;

        static Config()
        {
            BaseControl = new Pca9685();
            LedAndPushback = new Pca9685();
            LosingSignalDelay = 3;
            RemoteSignal = new SbusModel();
            Led = new LEDController();
        }
    }
}
