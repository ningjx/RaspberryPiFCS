using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using RaspberryPiFCS;

namespace TestConsole
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string portname = Extends.GetPorts()[0];
    //        UARTHelper uart = new UARTHelper(portname, 115200);
    //        uart.ReceivedEvent += Uart_ReceivedEvent;
    //        uart.BufSize = 10000;
    //        uart.ReceiveTimeoutEnable = false;
    //        uart.Open();
    //        while (true)
    //        {
    //            Console.Clear();
    //            Console.WriteLine($"航向  {StatusDatasBus.FlightData.Attitude.Angle_Z.ToString("f2")}°");
    //            Console.WriteLine($"Roll  {StatusDatasBus.FlightData.Attitude.Angle_X.ToString("f2")}°");
    //            Console.WriteLine($"Pitch  {StatusDatasBus.FlightData.Attitude.Angle_Y.ToString("f2")}°");
    //            Console.WriteLine($"经度  {(StatusDatasBus.FlightData.GPSData.Longitude / 10000000).ToString("f0")}°{((StatusDatasBus.FlightData.GPSData.Longitude % 10000000)/1e5).ToString("f5")}\'");
    //            Console.WriteLine($"纬度  {(StatusDatasBus.FlightData.GPSData.Latitude / 10000000).ToString("f0")}°{((StatusDatasBus.FlightData.GPSData.Latitude % 10000000)/1e5).ToString("f5")}\'\n");
    //            Console.WriteLine($"气压高度  {StatusDatasBus.FlightData.Attitude.BarometricAltitude.ToString("f2")}M");
    //            Console.WriteLine($"GPS高度  {StatusDatasBus.FlightData.GPSData.GPSAltitude.ToString("f2")}M");
    //            Console.WriteLine($"GPSYaw  {StatusDatasBus.FlightData.GPSData.GPSYaw.ToString("f2")}M");
    //            Console.WriteLine($"地速  {StatusDatasBus.FlightData.GPSData.GPSSpeed.ToString("f2")}M/S");
    //            Thread.Sleep(50);
    //        }
    //        Console.ReadKey();
    //    }

    //    private static void Uart_ReceivedEvent(object sender, byte[] bytes)
    //    {
    //        //Console.WriteLine(bytes[1].ByteArrToHexStr());
    //        //GPSHelper.DecodeData(bytes);
    //    }
    //}
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
           
        }
    }
}
