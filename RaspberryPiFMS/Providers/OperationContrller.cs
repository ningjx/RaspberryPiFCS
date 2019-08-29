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
        /// <summary>
        /// 是否垂直导航
        /// </summary>
        private bool isVanv;
        /// <summary>
        /// 是否水平导航
        /// </summary>
        private bool isLanv;
        /// <summary>
        /// 是否自动油门
        /// </summary>
        private bool isAutoThrottel;
        /// <summary>
        /// 是否自动配平
        /// </summary>
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
        public void SetContrlMode(bool isVanv, bool isLanv, bool isAutoThrottel, bool isAutoTrim)
        {
            this.isVanv = isVanv;
            this.isLanv = isLanv;
            this.isAutoThrottel = isAutoThrottel;
            this.isAutoTrim = isAutoTrim;
        }

        /// <summary>
        /// 初始化控制器，执行控制单元
        /// </summary>
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
                //自动导航设置
                isVanv = remoteData.vnav;
                isLanv = remoteData.lnav;
                isAutoThrottel = remoteData.autoThrottel;
                isAutoTrim = remoteData.autoTrim;

                #region 处理自动导航模式
                if (isVanv)
                {
                    contrlData.pitch = autoData.pitch;
                }
                else
                {
                    contrlData.pitch = remoteData.pitch + 40;
                }

                if (isLanv)
                {
                    contrlData.roll = autoData.roll;
                }
                else
                {
                    contrlData.roll = remoteData.roll;
                }
                if (isAutoThrottel)
                {
                    contrlData.throttel = autoData.Lthrottel;
                }
                else
                {
                    contrlData.throttel = remoteData.throttle;
                }
                if (isAutoTrim)
                {
                    contrlData.throttel = autoData.trim;
                }
                else
                {
                    contrlData.throttel = remoteData.trim;
                }
                #endregion

                contrlData.airBreak = remoteData.airBreak;
                contrlData.flap = remoteData.flap;
                if (remoteData.gear)
                {
                    contrlData.gear = 90;
                }
                if (remoteData.pushBack)
                {
                    contrlData.pushBack = 90;
                }
                #region 灯光组先空
                #endregion
                lock (baseContrl.contrlData)
                {
                    baseContrl.contrlData = contrlData;
                }
            }
        }

    }
}
