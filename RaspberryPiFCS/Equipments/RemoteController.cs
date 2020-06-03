using RaspberryPiFCS.Main;
using RaspberryPiFCS.Drivers;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Net;

namespace RaspberryPiFCS.Equipments
{
    public class RemoteController : IEquipment_Socket
    {
        public EquipmentData EquipmentData { get; } = new EquipmentData("RemoteController");

        private SocketDriver SocketDriver;
        public SBusDriver SBus;

        public RelyEquipment RelyEquipment { get; set; } = new RelyEquipment
        {
            Enum.RegisterType.Sys
        };

        public IPEndPoint BindIP => throw new NotImplementedException();

        public IPEndPoint TargetIP => throw new NotImplementedException();

        public EndPoint OriginIP => throw new NotImplementedException();

        public bool Lunch()
        {
            try
            {
                if (!EquipmentBus.ControllerRegister.CheckRely(RelyEquipment))
                {
                    throw new Exception($"依赖设备尚未启动{string.Join("、", RelyEquipment)}");
                }

                SBus = DriversFactory.GetSBusDriver(3);
                SBus.SetSignal += SBus_SetSignal;

                SocketDriver = DriversFactory.GetSocketDriver(5000, 5001);

                EquipmentData.IsEnable = true;
                EquipmentBus.ControllerRegister.Register(Enum.RegisterType.Socket, true);
            }
            catch (Exception ex)
            {
                EquipmentData.AddError(Enum.LogType.Error, "启动遥控器失败！", ex);
                Logger.Add(Enum.LogType.Error, "启动遥控器失败！", ex);
                EquipmentData.IsEnable = false;
                return false;
            }
            return true;
        }

        public void Excute()
        {
            SBus.DecodeSignal(SocketDriver.ReciveBytes());
        }

        private void Socket_ReceivedEvent(byte[] bytes)
        {
            SBus.DecodeSignal(bytes);
        }

        private void SBus_SetSignal(long[] signals)
        {
            SignalBus.OriginSignal.Channel01 = signals[0];
            SignalBus.OriginSignal.Channel02 = signals[1];
            SignalBus.OriginSignal.Channel03 = signals[2];
            SignalBus.OriginSignal.Channel04 = signals[3];
            SignalBus.OriginSignal.Channel05 = signals[4];
            SignalBus.OriginSignal.Channel06 = signals[5];
            SignalBus.OriginSignal.Channel07 = signals[6];
            SignalBus.OriginSignal.Channel08 = signals[7];
            SignalBus.OriginSignal.Channel09 = signals[8];
            SignalBus.OriginSignal.Channel10 = signals[9];
            SignalBus.OriginSignal.Channel11 = signals[10];
            SignalBus.OriginSignal.Channel12 = signals[11];
            SignalBus.OriginSignal.Channel13 = signals[12];
            SignalBus.OriginSignal.Channel14 = signals[13];
            SignalBus.OriginSignal.Channel15 = signals[14];
            SignalBus.OriginSignal.Channel16 = signals[15];
        }
    }
}
