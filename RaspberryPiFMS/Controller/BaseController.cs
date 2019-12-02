using System;
using RaspberryPiFMS.Enum;
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

        private void SetControl()
        {
            #region 最基本的四个通道
            Bus.BaseDriver.SetPWMAngle((int)BaseChannel.Pitch, Bus.CenterControlData.Pitch);
            Bus.BaseDriver.SetPWMAngle((int)BaseChannel.RollL, Bus.CenterControlData.Roll);
            Bus.BaseDriver.SetPWMAngle((int)BaseChannel.RollR, Bus.CenterControlData.Roll);
            Bus.BaseDriver.SetPWMAngle((int)BaseChannel.Yaw, Bus.CenterControlData.Yaw);
            Bus.BaseDriver.SetPWMAngle((int)BaseChannel.ThrottelL, Bus.CenterControlData.ThrottelL);
            Bus.BaseDriver.SetPWMAngle((int)BaseChannel.ThrottelR, Bus.CenterControlData.ThrottelR);
            Bus.BaseDriver.SetPWMAngle((int)BaseChannel.Trim, Bus.CenterControlData.Trim);
            #endregion
        }
    }
}
