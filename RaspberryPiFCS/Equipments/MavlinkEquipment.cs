using MavLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RaspberryPiFCS.Equipments
{
    public class MavlinkEquipment
    {
        private E34_2G4D20D e34_2G4D20D = new E34_2G4D20D("");
        private Mavlink Mavlink = new Mavlink();
        Type[] messageTypes;
        public Timer Timer { get; set; } = new Timer(30);
        public bool Lock { get; set; } = false;
        public MavlinkEquipment()
        {
            e34_2G4D20D.Lunch();
            e34_2G4D20D.ReciveEvent += E34_2G4D20D_ReciveEvent;
            Mavlink.SetMessage += Mavlink_SetMessage;
            Mavlink.PacketReceived += Mavlink_PacketReceived;
            messageTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType.Name == "MavlinkMessage").ToArray();
            Timer.AutoReset = true;
            Timer.Elapsed += Excute;
            Timer.Start();
        }

        public void SendMessage(MavlinkMessage message)
        {
            MavlinkPacket packet = new MavlinkPacket(message);
            Mavlink.Send(packet);
        }

        public void SendByType(Type messageType)
        {
            e34_2G4D20D.SendBytes.Add(Mavlink.Send(messageType));
        }

        private void Mavlink_PacketReceived(object sender, MavlinkPacket e)
        {
            RecivePacket?.Invoke(e);
        }

        private void E34_2G4D20D_ReciveEvent(byte[] bytes)
        {
            Mavlink.ParseBytes(bytes);
        }

        private void Excute(object sender, ElapsedEventArgs e)
        {
            if (Lock)
                return;
            else
                Lock = true;

            try
            {
                foreach (var type in messageTypes)
                {
                    e34_2G4D20D.SendBytes.Add(Mavlink.Send(type));
                }
            }
            catch { }
            Lock = false;
        }

        private bool Mavlink_SetMessage(MavlinkMessage message, Type type)
        {
            switch (type.Name)
            {
                case "Msg_controlmode":
                    type.GetField("contrlmode").SetValue(message, (byte)DataBus.FlightStatus.ContrlMode);
                    break;

                case "Msg_speedmode":
                    type.GetField("speedmode").SetValue(message, (byte)DataBus.FlightStatus.SpeedMode);
                    break;

                case "Msg_gps_status":
                    type.GetField("satellites_visible").SetValue(message, (byte)DataBus.FlightData.GPSData.SatellitesCount);
                    break;


                case "Msg_attitude":
                    type.GetField("roll").SetValue(message, DataBus.FlightData.Attitude.Angle_X);
                    type.GetField("pitch").SetValue(message, DataBus.FlightData.Attitude.Angle_Y);
                    type.GetField("yaw").SetValue(message, DataBus.FlightData.Attitude.Angle_Z);
                    type.GetField("rollspeed").SetValue(message, DataBus.FlightData.Attitude.Palstance_X);
                    type.GetField("pitchspeed").SetValue(message, DataBus.FlightData.Attitude.Palstance_Y);
                    type.GetField("yawspeed").SetValue(message, DataBus.FlightData.Attitude.Palstance_Z);
                    break;

                case "Msg_attitude_ext":
                    type.GetField("Aacceleration_X").SetValue(message, DataBus.FlightData.Attitude.Aacceleration_X);
                    type.GetField("Aacceleration_Y").SetValue(message, DataBus.FlightData.Attitude.Aacceleration_Y);
                    type.GetField("Aacceleration_Z").SetValue(message, DataBus.FlightData.Attitude.Aacceleration_Z);
                    type.GetField("BarometricAltitude").SetValue(message, DataBus.FlightData.Attitude.BarometricAltitude);
                    type.GetField("Pressure").SetValue(message, DataBus.FlightData.Attitude.Pressure);
                    type.GetField("MicroAltitude").SetValue(message, DataBus.FlightData.Attitude.MicroAltitude);
                    break;

                case "Msg_global_position_int":
                    type.GetField("lat").SetValue(message, DataBus.FlightData.GPSData.Latitude);
                    type.GetField("lon").SetValue(message, DataBus.FlightData.GPSData.Longitude);
                    type.GetField("alt").SetValue(message, DataBus.FlightData.GPSData.GPSAltitude);
                    type.GetField("relative_alt").SetValue(message, DataBus.FlightData.GPSData.GPSAltitude - DataBus.FlightData.GPSData.GroundAlt);
                    type.GetField("hdg").SetValue(message, DataBus.FlightData.GPSData.GPSHeading);
                    break;

                case "Msg_global_position_int_ext":
                    type.GetField("GPSSpeed").SetValue(message, DataBus.FlightData.GPSData.GPSSpeed);
                    type.GetField("GPSYaw").SetValue(message, DataBus.FlightData.GPSData.GPSYaw);
                    type.GetField("SatellitesCount").SetValue(message, DataBus.FlightData.GPSData.SatellitesCount);
                    type.GetField("PositionalAccuracy").SetValue(message, DataBus.FlightData.GPSData.PositionalAccuracy);
                    type.GetField("HorizontalAccuracy").SetValue(message, DataBus.FlightData.GPSData.HorizontalAccuracy);
                    type.GetField("VerticalAccuracy").SetValue(message, DataBus.FlightData.GPSData.VerticalAccuracy);
                    break;
                case "Msg_functionstatus":
                    break;
                case "Msg_setfunctionstatus":
                    break;
                case "Msg_log":
                    break;
                case "Msg_home_position":
                    break;
                default: return false;
            }
            return true;
        }

        /// <summary>
        /// 订阅此事件以接收mavlink消息
        /// </summary>
        public event MavlinkHandler RecivePacket;
    }
}
