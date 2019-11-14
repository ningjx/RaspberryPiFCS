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
        private MicroTimer _timer = new MicroTimer();
        private bool _excuteLock = false;
        //控制数据缓冲
        private Pca9685 _baseDriver;

        /// <summary>
        /// 初始化基础控制器
        /// </summary>
        /// <param name="baseDriver"></param>
        /// <param name="ms">控制器的轮询间隔时间</param>
        public BaseController(Pca9685 baseDriver, int ms = 15)
        {
            ms = Math.Abs(ms);
            _baseDriver = baseDriver;
            _timer.Interval = ms;
            _timer.Elapsed += Excute;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void Excute()
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
            _baseDriver.SetPWMAngle((int)BaseChannel.Pitch, Cache.CenterControlData.Pitch);

            _baseDriver.SetPWMAngle((int)BaseChannel.Roll, Cache.CenterControlData.Roll);

            _baseDriver.SetPWMAngle((int)BaseChannel.Yaw, Cache.CenterControlData.Yaw);

            _baseDriver.SetPWMAngle((int)BaseChannel.Throttel, Cache.CenterControlData.Throttel);
            #endregion
        }
    }
}
