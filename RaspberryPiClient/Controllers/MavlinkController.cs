using MavLink;
using FlightDataModel;
using System.IO.Ports;

namespace RaspberryPiClient.Controllers
{
    public static class MavlinkController
    {
        static SerialPort serialPort;
        public static FlightData FlightData;
        static Mavlink mavlink;
        static MavlinkController()
        {
            mavlink = new Mavlink();
            mavlink.PacketReceived += Mavlink_PacketReceived;
            FlightData = new FlightData();

            string portName = SerialPort.GetPortNames()[0];
            serialPort = new SerialPort(portName,115200, Parity.None,8,StopBits.One);
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.Open();
        }

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] readBytes = new byte[serialPort.BytesToRead];
            serialPort.Read(readBytes, 0, readBytes.Length);
            mavlink.ParseBytes(readBytes);
        }


        private static void Mavlink_PacketReceived(object sender, MavlinkPacket e)
        {
            var type = e.Message.GetType();
            switch (type.Name)
            {
                case "Msg_attitude":
                    var message = (Msg_attitude)e.Message;
                    FlightData.Attitude.Angle_X = message.roll;
                    FlightData.Attitude.Angle_Y = message.pitch;
                    FlightData.Attitude.Angle_Z = message.yaw;
                    break;
            }
        }
    }
}
