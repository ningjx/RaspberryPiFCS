using RaspberryPiFMS.Models;
using RaspberryPiFMS.Enum;

namespace RaspberryPiFMS
{
    public static class Config
    {
        public static ContrlMode ContrlMode;

        public static SbusModel RemoteSignal;

        public static int LosingSignalDelay = 3;
    }
}
