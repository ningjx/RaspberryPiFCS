using RaspberryPiFCS.Interface;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspberryPiFCS.Drivers
{
    public class UARTDriver_Socket : IUARTDriver
    {
        public event UARTRecHandler RecEvent;
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private EndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 6666);
        int BufferSize = 50;
        byte[] recBytes = new byte[10000];
        byte[] bytes;
        int currRecCount;
        int restRecCount;
        int currPosition;
        public UARTDriver_Socket(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
        {
            bytes = new byte[BufferSize];
            restRecCount = BufferSize;
            _socket.Bind(new IPEndPoint(IPAddress.Loopback, 7788));
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(1);
                        currRecCount = _socket.ReceiveFrom(recBytes, ref endPoint);
                        if (currRecCount >= BufferSize)
                        {
                            restRecCount = BufferSize;
                            currPosition = 0;
                            RecEvent?.Invoke(recBytes);
                        }
                        else if (restRecCount <= currRecCount)//缓冲区满
                        {
                            for (int i = 0; i < restRecCount; i++)
                            {
                                bytes[currPosition + i] = recBytes[i];
                            }
                            restRecCount = BufferSize;
                            currPosition = 0;
                            RecEvent?.Invoke(bytes);
                        }
                        else
                        {
                            for (int i = 0; i < currRecCount; i++)
                            {
                                bytes[currPosition + i] = recBytes[i];
                            }
                            currPosition += currRecCount;
                            restRecCount = BufferSize - currPosition + 1;
                        }
                    }
                    catch { }
                }
            });
        }

        public void WriteBytes(byte[] bytes)
        {
            _socket.SendTo(bytes, endPoint);
        }
    }
}
