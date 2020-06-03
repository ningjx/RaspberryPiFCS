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
        public static Pca9685 BasePca = new Pca9685();
        public static Pca9685 LEDPca = new Pca9685();
        public static E34_2G4D20D E34_2G4D20D = new E34_2G4D20D("");
        public static RemoteController RemoteController=new RemoteController();
        static EquipmentBus()
        {
            RemoteController.Lunch();
            BasePca.Lunch();
            
        }
    }
}
