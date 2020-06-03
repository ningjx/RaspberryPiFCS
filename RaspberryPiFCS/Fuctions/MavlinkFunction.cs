using MavLink;
using MavLink.Message;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using System;
using System.Reflection;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RaspberryPiFCS.Fuctions
{
    public class MavlinkFunction : IFunction
    {
        Mavlink mavlink;
        static int SequenceNumber = 0;

        Timer timer = new Timer(10);
        Type[] messageTypes;
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(10);
        public bool Lock { get; set; } = false;

        public FunctionStatus FunctionStatus { get; set; } = FunctionStatus.Online;
        public MavlinkFunction()
        {
            mavlink = new Mavlink();
            mavlink.SetMessage += Mavlink_SetMessage;
            messageTypes = Assembly.GetExecutingAssembly().GetTypes();
            EquipmentBus.ControllerRegister.Register(Enum.RegisterType.MavlinkController, false);
            Timer.AutoReset = true;
            Timer.Elapsed += Excute;
            Timer.Start();
        }

        private void Excute(object sender, ElapsedEventArgs e)
        {
            if (Lock)
                return;
            else
                Lock = true;

            try
            {
                //根据控制信号操作

                foreach (var type in messageTypes)
                {
                    if (type.BaseType.Name != "MavlinkMessage")
                        continue;
                    EquipmentBus.E34_2G4D20D.SendBytes = mavlink.Send(type);
                    Thread.Sleep(10);
                }



            }
            catch (Exception ex)
            {
                RetryTime++;
                if (RetryTime > 10)
                {
                    FunctionStatus = FunctionStatus.Failure;
                    //日志
                }
            }
            Lock = false;
        }


        public void Dispose()
        {

        }

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

        private void Mavlink_SetMessage(MavlinkMessage message, Type type)
        {
            switch (type.Name)
            {
                case "Msg_attitude":
                    type.GetField("roll").SetValue(message, DataBus.FlightData.Attitude.Angle_X);
                    type.GetField("pitch").SetValue(message, DataBus.FlightData.Attitude.Angle_Y);
                    type.GetField("yaw").SetValue(message, DataBus.FlightData.Attitude.Angle_Z);
                    type.GetField("rollspeed").SetValue(message, DataBus.FlightData.Attitude.Palstance_X);
                    type.GetField("pitchspeed").SetValue(message, DataBus.FlightData.Attitude.Palstance_Y);
                    type.GetField("yawspeed").SetValue(message, DataBus.FlightData.Attitude.Palstance_Z);
                    type.GetField("Aacceleration_X").SetValue(message, DataBus.FlightData.Attitude.Aacceleration_X);
                    type.GetField("Aacceleration_Y").SetValue(message, DataBus.FlightData.Attitude.Aacceleration_Y);
                    type.GetField("Aacceleration_Z").SetValue(message, DataBus.FlightData.Attitude.Aacceleration_Z);
                    type.GetField("BarometricAltitude").SetValue(message, DataBus.FlightData.Attitude.BarometricAltitude);
                    type.GetField("Pressure").SetValue(message, DataBus.FlightData.Attitude.Pressure);
                    type.GetField("MicroAltitude").SetValue(message, DataBus.FlightData.MicroAltitude);
                    break;
                case "Msg_gpsdata":
                    type.GetField("Latitude").SetValue(message, DataBus.FlightData.GPSData.Latitude);
                    type.GetField("Longitude").SetValue(message, DataBus.FlightData.GPSData.Longitude);
                    type.GetField("GPSAltitude").SetValue(message, DataBus.FlightData.GPSData.GPSAltitude);
                    type.GetField("GPSSpeed").SetValue(message, DataBus.FlightData.GPSData.GPSSpeed);
                    type.GetField("GPSHeading").SetValue(message, DataBus.FlightData.GPSData.GPSHeading);
                    type.GetField("GPSYaw").SetValue(message, DataBus.FlightData.GPSData.GPSYaw);
                    type.GetField("SatellitesCount").SetValue(message, DataBus.FlightData.GPSData.SatellitesCount);
                    type.GetField("PositionalAccuracy").SetValue(message, DataBus.FlightData.GPSData.PositionalAccuracy);
                    type.GetField("HorizontalAccuracy").SetValue(message, DataBus.FlightData.GPSData.HorizontalAccuracy);
                    type.GetField("VerticalAccuracy").SetValue(message, DataBus.FlightData.GPSData.VerticalAccuracy);
                    break;
            }
        }
    }
}
