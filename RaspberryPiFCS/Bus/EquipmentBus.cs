using RaspberryPiFCS.Equipments;

namespace RaspberryPiFCS
{
    /// <summary>
    /// 设备总线
    /// </summary>
    public static class EquipmentBus
    {
        public static SysRegister ControllerRegister = new SysRegister();//注册
        public static Pca9685 BasePca = new Pca9685();
        public static RemoteController RemoteController=new RemoteController();
        static EquipmentBus()
        {
            RemoteController.Lunch();
            BasePca.Lunch();
            
        }
    }
}
