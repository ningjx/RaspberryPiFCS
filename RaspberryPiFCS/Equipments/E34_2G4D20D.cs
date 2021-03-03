using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RaspberryPiFCS.Main;
using RaspberryPiFCS.Drivers;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using MavLink;

namespace RaspberryPiFCS.Equipments
{
    /// <summary>
    /// 亿佰特E34_2G4D20D，全双工2.4G射频模块,2km/250kbps
    /// </summary>
    public class E34_2G4D20D : IEquipment_UART
    {
        public EquipmentData EquipmentData { get; } = new EquipmentData("E34_2G4D20D");

        public string ComName { get; } = string.Empty;

        private IUARTDriver UART;

        public event DataHandler ReciveEvent;


        public List<byte[]> SendBytes { get; set; } = new List<byte[]>();

        public RelyEquipment RelyEquipment { get; set; } = new RelyEquipment
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
                if (!EquipmentBus.ControllerRegister.CheckRely(RelyEquipment))
                {
                    throw new Exception($"依赖设备尚未启动{string.Join("、", RelyEquipment)}");
                }

                UART = DriversFactory.GetUARTDriver(ComName);
                UART.RecEvent += ReceivedEvent;

                EquipmentData.IsEnable = true;
                EquipmentBus.ControllerRegister.Register(Enum.RegisterType.E34_2G4D20D, true);

                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1);
                        try
                        {
                            lock (SendBytes)
                            {
                                if (SendBytes.Count != 0)
                                {
                                    if (SendBytes[0] != null)
                                        UART.WriteBytes(SendBytes[0]);
                                    SendBytes.RemoveAt(0);
                                }
                            }
                        }
                        catch { }
                    }
                });
            }
            catch (Exception ex)
            {
                EquipmentData.AddError(Enum.LogType.Error, "启动E34_2G4D20D失败！", ex);
                LogService.Add(Enum.LogType.Error, "启动E34_2G4D20D失败！", ex);
                EquipmentData.IsEnable = false;
                return false;
            }
            return true;
        }

        private void ReceivedEvent(byte[] bytes)
        {
            ReciveEvent?.Invoke(bytes);
        }
    }
}
