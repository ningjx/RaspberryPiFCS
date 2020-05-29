using System;
using RaspberryPiFCS.Drivers;
using RaspberryPiFCS.Handlers;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using RaspberryPiFCS.SystemMessage;

namespace RaspberryPiFCS.Equipments
{
    /// <summary>
    /// 亿佰特E34_2G4D20D，全双工2.4G射频模块,2km/250kbps
    /// </summary>
    public class E34_2G4D20D : IEquipment_UART
    {
        public EquipmentData EquipmentData { get; } = new EquipmentData("E34_2G4D20D");

        public string ComName { get; } = string.Empty;

        private UARTDriver UART;

        public event DataHandler ReciveEvent;

        public byte[] SendBytes { set { UART.Write(value); } }

        public RelyConyroller RelyConyroller { get; set; } = new RelyConyroller
        {
                Enum.RegisterType.Sys
        };

        public E34_2G4D20D(string comName)
        {
            ComName = comName;
        }

        public bool Lunch()
        {
            try
            {
                if (!EquipmentBus.ControllerRegister.CheckRely(RelyConyroller))
                {
                    throw new Exception($"依赖设备尚未启动{string.Join("、", RelyConyroller)}");
                }

                UART = DriversFactory.GetUARTDriver(ComName);
                UART.ReceivedEvent += ReceivedEvent;
                UART.Open();

                EquipmentData.IsEnable = true;
                EquipmentBus.ControllerRegister.Register(Enum.RegisterType.E34_2G4D20D, true);
            }
            catch (Exception ex)
            {
                EquipmentData.AddError(Enum.ErrorType.Error, "启动E34_2G4D20D失败！", ex);
                Message.Add(Enum.ErrorType.Error, "启动E34_2G4D20D失败！", ex);
                EquipmentData.IsEnable = false;
                return false;
            }
            return true;
        }

        private void ReceivedEvent(object sender, byte[] bytes)
        {
            ReciveEvent?.Invoke(bytes);
        }
    }
}
