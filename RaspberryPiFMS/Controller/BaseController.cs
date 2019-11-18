using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Models;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.Controller
{
    public class BaseController
    {
        private Timer _timer = new Timer();
        private bool _excuteLock = false;

        /// <summary>
        /// 初始化基础控制器
        /// </summary>
        /// <param name="baseDriver"></param>
        /// <param name="ms">控制器的轮询间隔时间</param>
        public BaseController(int ms = 10)
        {
            ms = Math.Abs(ms);
            _timer.Interval = ms;
            _timer.Elapsed += Excute; 
            _timer.AutoReset = true;
            _timer.Start();
        }

      

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_excuteLock)
                return;
            _excuteLock = true;
            SetControl();
            _excuteLock = false;
        }

        /// <summary>
        /// 映射所有遥控器可以控制的
        /// </summary>
        private void SetControl()
        {
            #region 最基本的四个通道
            Cache.BaseDriver.SetPWMAngle((int)BaseChannel.Pitch, Cache.CenterControlData.Pitch);
            Cache.BaseDriver.SetPWMAngle((int)BaseChannel.Roll, Cache.CenterControlData.Roll);
            Cache.BaseDriver.SetPWMAngle((int)BaseChannel.Yaw, Cache.CenterControlData.Yaw);
            Cache.BaseDriver.SetPWMAngle((int)BaseChannel.Throttel, Cache.CenterControlData.Throttel);
            #endregion
        }
    }
}
