﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Models;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.Controller
{
    public class BaseController
    {
        private Timer _timer = new Timer();
        private bool _excuteLock = false;
        //控制数据缓冲
        private Pca9685 _baseDriver;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseDriver"></param>
        /// <param name="ms">控制器的轮询间隔时间</param>
        public BaseController(Pca9685 baseDriver,int ms = 15)
        {
            ms = Math.Abs(ms);
            _baseDriver = baseDriver;
            _timer.Interval = ms;
            _timer.Enabled = true;
            _timer.Elapsed += Excute;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_excuteLock)
                return;
            _excuteLock = true;
            switch (Cache.ContrlMode)
            {
                case Enum.ContrlMode.Manual:
                    ManualControl();
                    break;
            }
            _excuteLock = false;
        }

        /// <summary>
        /// 映射所有遥控器可以控制的
        /// </summary>
        private void ManualControl()
        {
            #region 最基本的四个通道
            if (Cache.RemoteSignal.Channel01 !=Cache.CenterControlData.Yaw)
            {
                _baseDriver.SetPWMAngle(1, Cache.RemoteSignal.Channel01);
                Cache.CenterControlData.Yaw = Cache.RemoteSignal.Channel01;
            }
            if (Cache.RemoteSignal.Channel02 != Cache.CenterControlData.Pitch)
            {
                _baseDriver.SetPWMAngle(2, Cache.RemoteSignal.Channel02);
                Cache.CenterControlData.Pitch = Cache.RemoteSignal.Channel02;
            }
            if (Cache.RemoteSignal.Channel03 != Cache.CenterControlData.Throttel)
            {
                _baseDriver.SetPWMAngle(3, Cache.RemoteSignal.Channel03);
                Cache.CenterControlData.Throttel = Cache.RemoteSignal.Channel03;
            }
            if (Cache.RemoteSignal.Channel04 != Cache.CenterControlData.Roll)
            {
                _baseDriver.SetPWMAngle(4, Cache.RemoteSignal.Channel04);
                Cache.CenterControlData.Roll = Cache.RemoteSignal.Channel04;
            }
            #endregion


        }
    }
}
