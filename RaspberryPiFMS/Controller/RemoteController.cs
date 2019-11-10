using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using flyfire.IO.Ports;
using RaspberryPiFMS.Helper;
using RJCP.IO.Ports;
using Timer = System.Timers.Timer;


namespace RaspberryPiFMS.Controller
{
    public class RemoteController
    {
        private SbusHelper _sbusHelper;
        private Socket _socket;
        private Timer _timer;
        private byte[] _buffer = new byte[1000];
        public RemoteController()
        {
            _sbusHelper = new SbusHelper();
            _timer = new Timer(10);
            _timer.AutoReset = true;
            _timer.Elapsed += ReciveData;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 4664);
            _socket.Connect(endPoint);
            _timer.Start();
        }

        private void ReciveData(object sender, System.Timers.ElapsedEventArgs e)
        {
            int length = _socket.Receive(_buffer);
            if(length!=0)
                _sbusHelper.DecodeSignal(_buffer);
        }
    }
}
