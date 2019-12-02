using System;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.Controller
{
    public class BaseController
    {
        private Timer _timer = new Timer();
        private bool _excuteLock = false;
        private Pca9685 pca9685 = new Pca9685();
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
            pca9685.SetPWMAngle((int)BaseChannel.Pitch, Bus.CenterData.Pitch);
            pca9685.SetPWMAngle((int)BaseChannel.RollL, Bus.CenterData.Roll);
            pca9685.SetPWMAngle((int)BaseChannel.RollR, Bus.CenterData.Roll);
            pca9685.SetPWMAngle((int)BaseChannel.Yaw, Bus.CenterData.Yaw);
            pca9685.SetPWMAngle((int)BaseChannel.ThrottelL, Bus.CenterData.ThrottelL);
            pca9685.SetPWMAngle((int)BaseChannel.ThrottelR, Bus.CenterData.ThrottelR);
            pca9685.SetPWMAngle((int)BaseChannel.Trim, Bus.CenterData.Trim);
            #endregion
        }
    }
}
