using System;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Models;
using System.Threading;
using Newtonsoft.Json;

namespace RaspberryPiFMS.Controller
{
    /// <summary>
    /// 包括数据传输、内网穿透、延迟检测三部分
    /// </summary>
    public class NetController
    {

        public RemoteControlModel remoteData = new RemoteControlModel();

        public double ping;
        private string remoteIp = string.Empty;
        private string remotePort = string.Empty;

        public string testData = "null";

        public IpInfoModel IpInfo
        {
            get { return new IpInfoModel(remoteIp, remotePort, ping); }
            set
            {
                dataSocket = new SocketHelper(value.ip, value.port);
                this.remoteIp = value.ip;
                this.remotePort = value.port;
                Console.WriteLine($"初始化远程IP[{remoteIp}:{remotePort}]");
            }
        }

        public string serviceIp = string.Empty;


        private ThreadStart netT;
        private Thread net;

        //private ThreadStart ddnsT;
        //private Thread ddns;

        private SocketHelper ddnsSocket;
        private SocketHelper dataSocket;

        public NetController()
        {
            //ddnsSocket = new SocketHelper("", "");
            //Console.WriteLine($"连接遥控器...\r\n");
            //while (string.IsNullOrEmpty(remoteIp))
            //{
            //    DDNS();
            //    Thread.Sleep(500);
            //}
            //Console.WriteLine($"连接遥控器成功\r\n");

            //ddnsT = () => DDNS();
            //ddns = new Thread(ddnsT);
            dataSocket = new SocketHelper("", "");
            netT = () => Excute();
            net = new Thread(netT);

            net.Start();
            //ddns.Start();
        }

        private void Excute()
        {
            var settings = new JsonSerializerSettings
            {
                Error = (obj, args2) =>
                {
                    args2.ErrorContext.Handled = true;
                }
            };//忽略序列化失败
            while (true)
            {
                try
                {
                    dataSocket.SendData("DDNS");
                    //Thread.Sleep(10);
                    string reciveData = dataSocket.ReciveData();
                    if (!string.IsNullOrEmpty(reciveData))
                        testData = reciveData;
                    else
                        testData = "null";
                    //RemoteDataModel reciveModel = new RemoteDataModel();
                    //if (!string.IsNullOrEmpty(reciveData))
                    //{
                    //    reciveModel = JsonConvert.DeserializeObject<RemoteDataModel>(reciveData, settings);
                    //    if (reciveModel != null)//&& reciveModel.timeStamp > remoteData.timeStamp)
                    //        remoteData = reciveModel;
                    //}
                }
                catch (Exception e)
                {

                }
            }

        }



        private void DDNS()
        {
            ddnsSocket.SendData("DDNS");

            string ipInfo = ddnsSocket.ReciveData();
            if (!string.IsNullOrEmpty(ipInfo))
            {
                var info = ipInfo.Split(':');
                if (info[0] != IpInfo.ip || info[1] != IpInfo.port)
                {
                    IpInfo = new IpInfoModel(info[0], info[1], 0);
                    dataSocket.SendData("DDNS");
                }
            }
            //Thread.Sleep(1000);
        }
    }
}
