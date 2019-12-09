using System;
using System.Net;
using System.Net.Sockets;
using RaspberryPiFMS.Helper;
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
            Console.Write("初始化解码器");
            _sbusHelper = new SbusHelper();
            Console.WriteLine("------Finish\r");
            byte[] _buffer = new byte[1000];
            _timer = new Timer() ;
            _timer.Interval = 10;
            _timer.AutoReset = true;
            _timer.Elapsed += ReciveData;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 4664);
            _socket.ReceiveTimeout = 10;
            _socket.Connect(endPoint);
            _timer.Start();
        }

        private void ReciveData(object sender, System.Timers.ElapsedEventArgs e)
        {
            int length = _socket.Receive(_buffer);
            if (length != 0)
                _sbusHelper.DecodeSignal(_buffer);
        }
    }
}
