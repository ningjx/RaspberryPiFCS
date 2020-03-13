using System;
using RaspberryPiFMS.Channels;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Interface;
using RaspberryPiFMS.Models;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.Controller
{
    public class BaseController: IController
    {
        private readonly Timer _timer = new Timer();
        private bool _excuteLock = false;

        /// <summary>
        /// 
        /// </summary>
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
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.PitchL, StateDatasBus.CenterSignal.PitchL);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.PitchR, StateDatasBus.CenterSignal.PitchR);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.RollL, StateDatasBus.CenterSignal.RollL);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.RollR, StateDatasBus.CenterSignal.RollR);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.Yaw, StateDatasBus.CenterSignal.Yaw);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.ThrottelL, StateDatasBus.CenterSignal.ThrottelL1);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.ThrottelR, StateDatasBus.CenterSignal.ThrottelR1);
            #endregion
        }


        public delegate void BaseControlEventHandler();

        public event BaseControlEventHandler LandingGearEvent;

        public event BaseControlEventHandler PushBackEvent;

        public event BaseControlEventHandler FlapEvent;

    }
}
