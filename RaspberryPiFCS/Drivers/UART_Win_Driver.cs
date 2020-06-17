using System;
using System.Collections.Generic;
using System.Text;
using flyfire.IO.Ports;
using RaspberryPiFCS.Interface;
using RJCP.IO.Ports;

namespace RaspberryPiFCS.Drivers
{
    public class UART_Win_Driver:CustomSerialPort,IUARTDriver
    {
        public UART_Win_Driver(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One): base(portName, baudRate, parity, databits, stopBits)
        {
            BufSize = 10000;
            ReceiveTimeoutEnable = false;
            ReceivedEvent += UART_Win_Driver_ReceivedEvent;
        }

        private void UART_Win_Driver_ReceivedEvent(object sender, byte[] bytes)
        {
            RecEvent?.Invoke(bytes);
        }

        public event UARTRecHandler RecEvent;

        public void WriteBytes(byte[] bytes)
        {
            Write(bytes);
        }
    }
}
