using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using flyfire.IO.Ports;
using RaspberryPiFMS.Helper;
using RJCP.IO.Ports;


namespace RaspberryPiFMS.Controller
{
    public class RemoteController
    {
        public byte[] data;

        public RemoteController(string portName)
        {
            CustomSerialPort sbus = new CustomSerialPort(portName, 115200, Parity.Even, 8, StopBits.Two);
            sbus.Open();
            sbus.ReceivedEvent += Sbus_ReceivedEvent;
        }

        public void Dispose()
        {
            Dispose();
        }

        private void Sbus_ReceivedEvent(object sender, byte[] bytes)
        {
            Console.Write("哈哈哈哈哈哈哈哈哈哈哈");
            data = bytes;
        }
    }
}
