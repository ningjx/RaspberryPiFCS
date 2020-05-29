using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RaspberryPiFCS.Drivers
{
    public class SocketDriver
    {
        public IPEndPoint BindIP { get; }
        public IPEndPoint TargetIP { get; }
        public EndPoint OriginIP => _endPoint;

        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private EndPoint _endPoint;
        private byte[] Buffer { get; } = new byte[10000];

        /// <summary>
        /// 向目标IP发送字节
        /// </summary>
        public byte[] ByteForTarget { set { _socket.SendTo(value, TargetIP); } }

        /// <summary>
        /// 向来源IP发送字节
        /// </summary>
        public byte[] ByteForOrigin { set { _socket.SendTo(value, OriginIP); } }

        public SocketDriver(int bindPort, int sendPort = 0)
        {
            BindIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), bindPort);
            if (sendPort != 0)
                TargetIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), sendPort);
        }

        public byte[] ReciveBytes()
        {
            _socket.ReceiveFrom(Buffer, ref _endPoint);
            return Buffer;
        }
    }
}
