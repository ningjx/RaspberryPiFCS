using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RaspberryPiFCS.Helper.Tests
{
    [TestClass()]
    public class GPSHelperTests
    {
        [TestMethod()]
        public void DecodeDataTest()
        {
            try
            {
                string portname = Extends.GetPorts()[0];
                UARTHelper uart = new UARTHelper(portname, 9600);
                uart.ReceivedEvent += Uart_ReceivedEvent;
                uart.BufSize = 1000;
                uart.ReceiveTimeoutEnable = false;
                uart.Open();
                //uart.EventStart();
                Thread.Sleep(1000);

                //while (true)
                //{
                    var a = StateDatasBus.FlightData.PositionData.Attitude.Magnetic_X;
                    var b = StateDatasBus.FlightData.PositionData.Attitude.Magnetic_Y;
                    var c = StateDatasBus.FlightData.PositionData.Attitude.Magnetic_Z;
                //}
                Console.WriteLine(a);
                Console.WriteLine(b);
                Console.WriteLine(c);
            }
            catch (Exception ex)
            {

            }
        }

        private void Uart_ReceivedEvent(object sender, byte[] bytes)
        {
            GPSHelper.DecodeData(bytes);
        }
    }
}