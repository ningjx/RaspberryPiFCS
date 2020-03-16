using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.WiringPi;
//using Unosquare.RaspberryIO;
using flyfire.IO.Ports;
using RJCP.IO.Ports;

namespace RaspberryPiFCS.Helper
{
    public class UARTHelper : CustomSerialPort
    {

        public UARTHelper(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One) : base(portName, baudRate, parity, databits, stopBits)
        {

        }

        public void EventStart()
        {
            Sp_DataReceived(new object(), new SerialDataReceivedEventArgs(SerialData.Eof));
        }
    }
}
