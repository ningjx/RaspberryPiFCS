using RaspberryPiFCS.Main;
using RaspberryPiFCS.Equipments;
using RaspberryPiFCS.Models;

namespace RaspberryPiFCS
{
    /// <summary>
    /// 设备总线
    /// </summary>
    public static class EquipmentBus
    {
        public static SysRegister ControllerRegister = new SysRegister();//注册
        public static Pca9685 BasePca;
        public static Pca9685 LEDPca;
        public static MavlinkEquipment MavlinkEquipment;
        //public static E34_2G4D20D E34_2G4D20D;
        public static RemoteController RemoteController;
        static EquipmentBus()
        {
            MavlinkEquipment = new MavlinkEquipment();
            Logger.ReadyToSend = true;
        }

        public static void Lunch()
        {
            BasePca = new Pca9685();
        }
    }
}
