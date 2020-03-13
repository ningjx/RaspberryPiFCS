using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using RaspberryPiFMS.Interface;

namespace RaspberryPiFMS.Controller
{
    public class MavlinkController : IController
    {
        private Socket _socket;
        private MicroTimer _timer;
        private byte[] _buffer = new byte[1000];
        public MavlinkController()
        {
            Console.Write("初始化Mavlink");
            Console.WriteLine("------Finish\r");
            byte[] _buffer = new byte[1000];
            _timer = new MicroTimer(20, true);
            _timer.AutoReset = true;
            _timer.Elapsed += SendData;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 4665);
            _socket.Connect(endPoint);
            _timer.Start();
        }
        public void SendData()
        {

        }
    }
}
