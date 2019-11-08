using flyfire.IO.Ports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFMS.Helper;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMSTest.HelperTest
{
    [TestClass]
    public class SbusHelperTest
    {
        [TestMethod]
        public void drivertest()
        {
            try
            {
                CustomSerialPort sbus = new CustomSerialPort("COM3", 100000, Parity.Even, 8, StopBits.Two);
                sbus.ReceiveTimeoutEnable = false;
                //sbus.ReceiveTimeout = 1;
                sbus.ReceivedEvent += Sbus_ReceivedEvent;
                sbus.Open();
            }
            catch(Exception eeee0)
            {

            }
        }

        private void Sbus_ReceivedEvent(object sender, byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
