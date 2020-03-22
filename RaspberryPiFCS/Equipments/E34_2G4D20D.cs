using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiFCS.Helper;
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
        public EquipmentData EquipmentData { get; } = new EquipmentData();

        public string ComName { get; } = string.Empty;

        private UARTHelper uart;

        public byte[] SendBytes { set { uart.Write(value); } }

        public E34_2G4D20D(string comName)
        {
            ComName = comName;
        }

        public bool Lunch()
        {
            try
            {
                uart = new UARTHelper(ComName);
                uart.ReceivedEvent += ReceivedEvent;
                uart.Open();
                EquipmentData.IsEnable = true;
                ControllerBus.ControllerRegister.Register(Enum.RegisterType.E34_2G4D20D, false);
            }
            catch (Exception ex)
            {
                ErrorMessage.Add(Enum.ErrorType.Error, "启动WT901B失败！", ex);
                EquipmentData.IsEnable = false;
                return false;
            }
            return true;
        }

        private void ReceivedEvent(object sender, byte[] bytes)
        {

        }
    }
}
