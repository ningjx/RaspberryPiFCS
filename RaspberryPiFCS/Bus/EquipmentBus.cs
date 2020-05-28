using RaspberryPiFCS.Equipments;

namespace RaspberryPiFCS
{
    /// <summary>
    /// 设备总线，该总线集成所有连接到飞控的设备，包括iic等
    /// </summary>
    public static class EquipmentBus
    {
        public static Pca9685 BasePca = new Pca9685();
        public static RemoteController RemoteController=new RemoteController();
        static EquipmentBus()
        {
            RemoteController.Lunch();
            BasePca.Lunch();
            
        }
    }
}
