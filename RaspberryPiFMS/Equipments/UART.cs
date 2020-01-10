using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.Helper
{
    /// <summary>
    /// 由于没有找到合适的SerialPort库，这里使用环回地址udp接受Python脚本的数据
    /// </summary>
    public class UART
    {
        private readonly Socket _socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
        private readonly Timer _timer = new Timer(10);
        private EndPoint _endPoint = new IPEndPoint(IPAddress.Any, 0);

        public UART(int port)
        {      
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            _socket.Bind(ip);

            _timer.AutoReset = true;
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }
        public UART(int port,string scriptName)
        {
            var psi = new ProcessStartInfo("python", Path.Combine("PythonScripts", "scriptName")) { RedirectStandardOutput = true };
            //启动
            var proc = Process.Start(psi);
            Thread.Sleep(100);
            if (proc == null)
                throw new Exception("遥控器模块启动失败");

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            _socket.Bind(ip);

            _timer.AutoReset = true;
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }


        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _socket.ReceiveFrom(Bytes,ref _endPoint);
        }

        public byte[] Bytes { get; } = new byte[1000];
    }
}
