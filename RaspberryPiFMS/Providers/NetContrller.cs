using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Models;
using System.Threading;
using Newtonsoft.Json;

namespace RaspberryPiFMS.Providers
{
    /// <summary>
    /// 包括数据传输、内网穿透、延迟检测三部分
    /// </summary>
    public class NetContrller
    {
        public double ping;
        RemoteDataModel remoteData = new RemoteDataModel();

        private string remoteIp = string.Empty;
        private string remotePort = string.Empty;
        
        public IpInfoModel IpInfo
        {
            get { return new IpInfoModel(remoteIp, remotePort, ping); }
            set {
                IpInfo = value;
                Console.WriteLine($"初始化远程IP[{remoteIp}:{remotePort}]");
                dataSocket = new SocketHelper(IpInfo.ip, IpInfo.port);
                this.remoteIp = IpInfo.ip;
                this.remotePort = IpInfo.port;
            }
        }

        public string serviceIp = string.Empty;


        private ThreadStart netT;
        private Thread net;

        private ThreadStart ddnsT;
        private Thread ddns;

        private SocketHelper ddnsSocket;
        private SocketHelper dataSocket;

        public NetContrller()
        {
            ddnsSocket = new SocketHelper("106.14.115.249", "6666");
            Console.WriteLine($"连接遥控器...");
            while (string.IsNullOrEmpty(remoteIp))
            {
                DDNS();
                Thread.Sleep(500);
            }
            Console.WriteLine($"连接遥控器成功");

            ddnsT = () => DDNS();
            ddns = new Thread(ddnsT);
            netT = () => Excute();
            net = new Thread(netT);

            net.Start();
            ddns.Start();
        }

        private void Excute()
        {
            string reciveData = dataSocket.ReciveData();
            RemoteDataModel reciveModel = JsonConvert.DeserializeObject<RemoteDataModel>(reciveData);
            if (reciveModel.timeStamp > remoteData.timeStamp)
                remoteData = reciveModel;
        }

        private void DDNS()
        { 
            ddnsSocket.SendData("DDNS");

            string ipInfo = ddnsSocket.ReciveData();
            if (!string.IsNullOrEmpty(ipInfo))
            {
                var info = ipInfo.Split(':');
                if (info[0] != IpInfo.ip || info[1] != IpInfo.port)
                    IpInfo = new IpInfoModel(info[0], info[1], 0);
            }
            Thread.Sleep(1000);
        }
    }
}
