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
        private CenterControlModel _centerData = new CenterControlModel();
        public BaseController()
        {
            _timer.Interval = 15;
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

        private void ManualControl()
        {
            if (Cache.RemoteSignal.Channel01 != _centerData.Yaw)
            {
                Cache.BaseDriver.SetPWMAngle(1, Cache.RemoteSignal.Channel01);
                _centerData.Yaw = Cache.RemoteSignal.Channel01;
            }
            if (Cache.RemoteSignal.Channel02 != _centerData.Pitch)
            {
                Cache.BaseDriver.SetPWMAngle(2, Cache.RemoteSignal.Channel02);
                _centerData.Pitch = Cache.RemoteSignal.Channel02;
            }
            if (Cache.RemoteSignal.Channel03!= _centerData.Throttel)
            {
                Cache.BaseDriver.SetPWMAngle(3, Cache.RemoteSignal.Channel03);
                _centerData.Throttel = Cache.RemoteSignal.Channel03;
            }
            if (Cache.RemoteSignal.Channel04!= _centerData.Roll)
            {
                Cache.BaseDriver.SetPWMAngle(4, Cache.RemoteSignal.Channel04);
                _centerData.Roll = Cache.RemoteSignal.Channel04;
            }
        }
    }
}
