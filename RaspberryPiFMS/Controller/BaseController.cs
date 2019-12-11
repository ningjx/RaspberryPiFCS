using System;
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
        private Pca9685 _pca9685 = new Pca9685(0x42);
        private bool _gearStatus = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ms">控制器的轮询间隔时间</param>
        /// <param name="i2cAddr">Pca9685地址</param>
        public BaseController(int ms = 20,int i2cAddr = 0x40)
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
            _pca9685.SetAngle((int)BaseChannel.PitchL, CenterData.PitchL);
            _pca9685.SetAngle((int)BaseChannel.PitchR, CenterData.PitchR);
            _pca9685.SetAngle((int)BaseChannel.RollL, CenterData.RollL);
            _pca9685.SetAngle((int)BaseChannel.RollR, CenterData.RollR);
            _pca9685.SetAngle((int)BaseChannel.Yaw, CenterData.Yaw);
            _pca9685.SetAngle((int)BaseChannel.ThrottelL, CenterData.ThrottelL);
            _pca9685.SetAngle((int)BaseChannel.ThrottelR, CenterData.ThrottelR);
            #endregion
        }

        private void Gear(bool gear)
        {
            if (_gearStatus == gear)
                return;
            if (LandingGearEvent == null)
            {

            }
            else
            {
                _gearStatus = gear;
                LandingGearEvent.Invoke();
            }
        }

        public delegate void BaseControlEventHandler();

        public event BaseControlEventHandler LandingGearEvent;

        public event BaseControlEventHandler PushBackEvent;

        public event BaseControlEventHandler FlapEvent;

    }
}
