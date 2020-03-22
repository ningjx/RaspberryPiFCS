﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RaspberryPiFCS.Helper
{
    public class SocketHelper
    {
        private System.Net.Sockets.Socket socket;
        private string ipAddress;
        private int port;
        private IPEndPoint ipe;
        private EndPoint ep;

        public SocketHelper()
        {
            socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            //IPEndPoint iep1 = new IPEndPoint(IPAddress.Broadcast, 4567);
            ipAddress = "";
            port = 35415;
            ipe = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            ep = ipe;
        }

        public SocketHelper(string ip, string point)
        {
            socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //socket.ReceiveTimeout = 500;
            ipAddress = ip;
            port = Convert.ToInt32(point);
            ipe = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            ep = ipe;
        }
        public void CloseCon()
        {
            socket.Close();
        }

        public void SendData(string data)
        {
            var byteData = new byte[1000];
            byteData = Encoding.ASCII.GetBytes(data);
            socket.SendTo(byteData, ipe);
        }

        public string ReciveData()
        {
            string data = string.Empty;
            byte[] buffer = new byte[2];
            int length = socket.ReceiveFrom(buffer, ref ep);
            data = Encoding.ASCII.GetString(buffer);
            return data;//.Substring(0,length);
        }
    }
}