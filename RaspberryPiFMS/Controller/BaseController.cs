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
        /// 初始化基础控制器
        /// </summary>
        /// <param name="baseDriver"></param>
        /// <param name="ms">控制器的轮询间隔时间</param>
        public BaseController(int ms = 20)
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
            _pca9685.SetAngle((int)BaseChannel.Pitch, Bus.CenterData.Pitch);
            _pca9685.SetAngle((int)BaseChannel.RollL, Bus.CenterData.RollL);
            _pca9685.SetAngle((int)BaseChannel.RollR, Bus.CenterData.RollR);
            _pca9685.SetAngle((int)BaseChannel.Yaw, Bus.CenterData.Yaw);
            _pca9685.SetAngle((int)BaseChannel.ThrottelL, Bus.CenterData.ThrottelL);
            _pca9685.SetAngle((int)BaseChannel.ThrottelR, Bus.CenterData.ThrottelR);
            //pca9685.SetPWMAngle((int)BaseChannel.ThrottelR, Bus.CenterData.ThrottelR);
            //pca9685.SetPWMAngle((int)BaseChannel.Trim, Bus.CenterData.Trim);
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
