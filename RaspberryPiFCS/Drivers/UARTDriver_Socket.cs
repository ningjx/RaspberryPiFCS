using RaspberryPiFCS.Interface;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RaspberryPiFCS.Drivers
{
    public class UARTDriver_Socket : IUARTDriver
    {
        public event UARTRecHandler RecEvent;
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public UARTDriver_Socket(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
        {
            _socket.Bind( new IPEndPoint( IPAddress.Loopback,7788 ) );
        }


        public void WriteBytes(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
