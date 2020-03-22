using RaspberryPiFCS.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using RaspberryPiFCS.Interface;
using MavLink;
using System.Timers;
using System.Reflection;
using MavLink.Message;
using System.Threading;
using Timer = System.Timers.Timer;

namespace RaspberryPiFCS.Controller
{
    public class MavlinkController : IController
    {
        Mavlink mavlink;
        static int SequenceNumber = 0;

        Timer timer = new Timer(10);
        Type[] messageTypes;

        public MavlinkController()
        {
            
        }

        private void Excute(object sender, ElapsedEventArgs e)
        {
            foreach (var type in messageTypes)
            {
                if (type.BaseType.Name != "MavlinkMessage")
                    continue;
                EquipmentBus.E34_2G4D20D.SendBytes = mavlink.Send(type);
                Thread.Sleep(10);
            }
        }

        public bool Lunch()
        {
            try
            {
                timer.Elapsed += Excute;
                timer.AutoReset = true;
                mavlink = new Mavlink();
                messageTypes = Assembly.GetExecutingAssembly().GetTypes();
                timer.Start();
                StatusDatasBus.ControllerRegister.Register(Enum.RegisterType.MavlinkController, false);
            }
            catch (Exception ex)
            {
                SystemMessage.ErrorMessage.Add(Enum.ErrorType.Error, "启动MavLink控制器时失败", ex);
                return false;
            }
            return true;
        }

        #region 填充不同类型的消息:废弃
        private static MavlinkPacket GetPacket(MavlinkMessage message)
        {
            MavlinkPacket packet = new MavlinkPacket(message);
            packet.SystemId = 1;
            packet.ComponentId = 1;
            packet.SequenceNumber = (byte)((SequenceNumber >> 24) & 0xFF);
            if (SequenceNumber == 255)
                SequenceNumber = 0;
            else
                SequenceNumber++;
            return packet;
        }

        
        private void SetMessage(object message,Type type)
        {
            switch (type.Name)
            {
                case "Msg_attitude":
                    type.GetField("roll").SetValue(message, StatusDatasBus.FlightData.Attitude.Angle_X);
                    type.GetField("pitch").SetValue(message, StatusDatasBus.FlightData.Attitude.Angle_Y);
                    type.GetField("yaw").SetValue(message, StatusDatasBus.FlightData.Attitude.Angle_Z);
                    type.GetField("rollspeed").SetValue(message, StatusDatasBus.FlightData.Attitude.Palstance_X);
                    type.GetField("pitchspeed").SetValue(message, StatusDatasBus.FlightData.Attitude.Palstance_Y);
                    type.GetField("yawspeed").SetValue(message, StatusDatasBus.FlightData.Attitude.Palstance_Z);
                    type.GetField("Aacceleration_X").SetValue(message, StatusDatasBus.FlightData.Attitude.Aacceleration_X);
                    type.GetField("Aacceleration_Y").SetValue(message, StatusDatasBus.FlightData.Attitude.Aacceleration_Y);
                    type.GetField("Aacceleration_Z").SetValue(message, StatusDatasBus.FlightData.Attitude.Aacceleration_Z);
                    type.GetField("BarometricAltitude").SetValue(message, StatusDatasBus.FlightData.Attitude.BarometricAltitude);
                    type.GetField("Pressure").SetValue(message, StatusDatasBus.FlightData.Attitude.Pressure);
                    type.GetField("MicroAltitude").SetValue(message, StatusDatasBus.FlightData.MicroAltitude);
                    break;
                case "Msg_gpsdata":
                    type.GetField("Latitude").SetValue(message, StatusDatasBus.FlightData.GPSData.Latitude);
                    type.GetField("Longitude").SetValue(message, StatusDatasBus.FlightData.GPSData.Longitude);
                    type.GetField("GPSAltitude").SetValue(message, StatusDatasBus.FlightData.GPSData.GPSAltitude);
                    type.GetField("GPSSpeed").SetValue(message, StatusDatasBus.FlightData.GPSData.GPSSpeed);
                    type.GetField("GPSHeading").SetValue(message, StatusDatasBus.FlightData.GPSData.GPSHeading);
                    type.GetField("GPSYaw").SetValue(message, StatusDatasBus.FlightData.GPSData.GPSYaw);
                    type.GetField("SatellitesCount").SetValue(message, StatusDatasBus.FlightData.GPSData.SatellitesCount);
                    type.GetField("PositionalAccuracy").SetValue(message, StatusDatasBus.FlightData.GPSData.PositionalAccuracy);
                    type.GetField("HorizontalAccuracy").SetValue(message, StatusDatasBus.FlightData.GPSData.HorizontalAccuracy);
                    type.GetField("VerticalAccuracy").SetValue(message, StatusDatasBus.FlightData.GPSData.VerticalAccuracy);
                    break;
            }
        }
        #endregion
    }
}
