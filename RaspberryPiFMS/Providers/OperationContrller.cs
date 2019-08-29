using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Models;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMS.Providers
{
    public class OperationContrller
    {
        private bool isVanv;
        private bool isLanv;
        private bool isAutoThrottel;
        private bool isAutoTrim;

        private ThreadStart contrllerT;
        private Thread contrller;

        private BaseContrl baseContrl;
        private ContrlModel contrlData;

        public AutoFlightModel autoData;
        public RemoteDataModel remoteData;
        /// <summary>
        /// 设置控制模式
        /// </summary>
        /// <param name="isVanv">是否自动垂直导航</param>
        /// <param name="isLanv">是否自动水平导航</param>
        /// <param name="isAutoThrottel">是否自动油门</param>
        public void SetContrlMode(bool isVanv,bool isLanv,bool isAutoThrottel,bool isAutoTrim)
        {
            this.isVanv = isVanv;
            this.isLanv = isLanv;
            this.isAutoThrottel = isAutoThrottel;
            this.isAutoTrim = isAutoTrim;
        }

        public OperationContrller()
        {
            autoData = new AutoFlightModel();
            remoteData = new RemoteDataModel();
            baseContrl = new BaseContrl();
            contrlData = new ContrlModel();

            remoteData.SetDefault();
            contrllerT = () => Contrller();
            contrller = new Thread(contrllerT);
            contrller.Start();
        }

        private void Contrller()
        {
            while (true)
            {








                baseContrl.contrlData = contrlData;
            }
        }

    }
}
