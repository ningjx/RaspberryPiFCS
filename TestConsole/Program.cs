using System;
using System.Threading;
using RaspberryPiFCS;
using RaspberryPiFCS.Helper;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string portname = Extends.GetPorts()[0];
            UARTHelper uart = new UARTHelper(portname, 115200);
            uart.ReceivedEvent += Uart_ReceivedEvent;
            uart.BufSize = 10000;
            uart.ReceiveTimeoutEnable = false;
            uart.Open();
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"航向  {StateDatasBus.FlightData.NavData.Attitude.Angle_Z.ToString("f2")}°");
                Console.WriteLine($"Roll  {StateDatasBus.FlightData.NavData.Attitude.Angle_X.ToString("f2")}°");
                Console.WriteLine($"Pitch  {StateDatasBus.FlightData.NavData.Attitude.Angle_Y.ToString("f2")}°");
                Console.WriteLine($"经度  {(StateDatasBus.FlightData.NavData.GPSData.Longitude / 10000000).ToString("f0")}°{((StateDatasBus.FlightData.NavData.GPSData.Longitude % 10000000)/1e5).ToString("f5")}\'");
                Console.WriteLine($"纬度  {(StateDatasBus.FlightData.NavData.GPSData.Latitude / 10000000).ToString("f0")}°{((StateDatasBus.FlightData.NavData.GPSData.Latitude % 10000000)/1e5).ToString("f5")}\'\n");
                Console.WriteLine($"气压高度  {StateDatasBus.FlightData.NavData.Attitude.BarometricAltitude.ToString("f2")}M");
                Console.WriteLine($"GPS高度  {StateDatasBus.FlightData.NavData.GPSData.GPSAltitude.ToString("f2")}M");
                Console.WriteLine($"GPSYaw  {StateDatasBus.FlightData.NavData.GPSData.GPSYaw.ToString("f2")}M");
                Console.WriteLine($"地速  {StateDatasBus.FlightData.NavData.GPSData.GroundSpeed.ToString("f2")}M/S");
                Thread.Sleep(50);
            }
            Console.ReadKey();
        }

        private static void Uart_ReceivedEvent(object sender, byte[] bytes)
        {
            //Console.WriteLine(bytes[1].ByteArrToHexStr());
            GPSHelper.DecodeData(bytes);
        }
    }
}
