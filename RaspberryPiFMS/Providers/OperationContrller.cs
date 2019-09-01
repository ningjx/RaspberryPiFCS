using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Models;
using RaspberryPiFMS.Helper;
using Newtonsoft.Json;

namespace RaspberryPiFMS.Providers
{
    /// <summary>
    /// 启动控制器和网络线程，合并远程遥控和自动控制
    /// </summary>
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
        public double baseValue = 0;



        private ThreadStart contrllerT;
        private Thread contrller;

        private BaseContrl baseContrl;
        private ContrlModel contrlData;
        private NetContrller netContrller;

        public string testData = string.Empty;
        public AutoFlightModel autoData;
        public RemoteDataModel remoteData = new RemoteDataModel();

        /// <summary>
        /// 设置控制模式
        /// </summary>
        /// <param name="isVanv">是否自动垂直导航</param>
        /// <param name="isLanv">是否自动水平导航</param>
        /// <param name="isAutoThrottel">是否自动油门</param>
        /// <param name="isAutoTrim">是否自动配平</param>
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
            contrlData = new ContrlModel();
            remoteData = new RemoteDataModel();
            Console.Write($"启动控制器\r\n");
            baseContrl = new BaseContrl();
            Console.Write($"启动控制器完成\r\n");
            Console.Write($"启动远程控制\r\n"); 
            netContrller = new NetContrller();
            Console.Write($"启动远程控制完成\r\n");

            contrllerT = () => Contrller();
            contrller = new Thread(contrllerT);
            contrller.Start();
        }




        private void Contrller()
        {
            while (true)
            {
                
                    //remoteData = netContrller.remoteData;
                testData = netContrller.testData;
                if(testData != "null")
                    remoteData = JsonConvert.DeserializeObject<RemoteDataModel>(testData);
                //自动导航设置
                //isVanv = remoteData.vnav;
                //isLanv = remoteData.lnav;
                //isAutoThrottel = remoteData.autoThrottel;
                //isAutoTrim = remoteData.autoTrim;

                //#region 处理自动导航模式
                //if (isVanv)
                //{
                //    contrlData.pitch = autoData.pitch;
                //}
                //else
                //{
                //    contrlData.pitch = remoteData.pitch + 40;
                //}

                //if (isLanv)
                //{
                //    contrlData.roll = autoData.roll;
                //}
                //else
                //{
                //    contrlData.roll = remoteData.roll+50;
                //}
                //if (isAutoThrottel)
                //{
                //    contrlData.throttel = autoData.Lthrottel;
                //}
                //else
                //{
                //    contrlData.throttel = remoteData.throttle;
                //}
                //if (isAutoTrim)
                //{
                //    contrlData.throttel = autoData.trim;
                //}
                //else
                //{
                //    contrlData.throttel = remoteData.trim;
                //}
                //#endregion

                //contrlData.airBreak = remoteData.airBreak;
                //contrlData.flap = remoteData.flap;
                //if (remoteData.gear)
                //{
                //    contrlData.gear = 90;
                //}
                //if (remoteData.pushBack)
                //{
                //    contrlData.pushBack = 90;
                //}
                #region 灯光组先空
                #endregion
                Console.WriteLine($"遥控数据");
                contrlData.roll = remoteData.roll + 50;
                //if (test == 0)
                //    test = 90;
                //else
                //    test = 0;
                //remoteData.roll = test;
                //testData = remoteData.roll.ToString();
                //contrlData.roll = remoteData.roll;
                
                    baseContrl.contrlData = contrlData;
                    baseValue = baseContrl.contrlData.roll;
                    Console.Clear();

                        Console.WriteLine($"{netContrller.remoteData.roll}\r\n{contrlData.roll}\r\n{baseValue}\r\n{testData}");
                
                Thread.Sleep(100);
            }
        }

    }
}
