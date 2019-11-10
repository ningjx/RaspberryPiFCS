using System;
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
            _timer.Interval = 20;
            _timer.Enabled = true;
            _timer.Elapsed += Excute;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("定时任务");
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
            Console.WriteLine("进入手动控制");
            if(Cache.RemoteSignal.Channel04!= _centerData.Roll)
            {
                Console.WriteLine("设置角度");
                Cache.BaseDriver.SetPWMAngle(1,Cache.RemoteSignal.Channel04);
                _centerData.Roll = Cache.RemoteSignal.Channel04;
            }
            if (Cache.RemoteSignal.Channel03!= _centerData.Throttel)
            {
                Cache.BaseDriver.SetPWMAngle(2, Cache.RemoteSignal.Channel03);
                _centerData.Throttel = Cache.RemoteSignal.Channel03;
            }
        }
    }
}
