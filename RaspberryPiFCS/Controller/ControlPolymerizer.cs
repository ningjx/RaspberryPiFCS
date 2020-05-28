using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Helper;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Timer = System.Timers.Timer;
using RaspberryPiFCS.Interface;

namespace RaspberryPiFCS.ComputeCenter
{
    public class ControlPolymerizer : IController
    {
        private readonly Timer _timer = new Timer();
        private readonly PIDDriver _pid = new PIDDriver();

        public ControlPolymerizer()
        {
            _pid.PIDOutEvent_Int += PIDOutEvent;

            _timer.Interval = 20;
            _timer.AutoReset = true;
            _timer.Elapsed += Excute;
            _timer.Start();
        }

        private void PIDOutEvent(float value)
        {
            StatusDatasBus.CenterSignal.ThrottelL1 = (float)value;
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (StatusDatasBus.FlightStatus.ContrlMode)
            {
                case ContrlMode.Manual:
                    ManualPolymerize();
                    break;
                case ContrlMode.LateralNavigation:
                    LateralNavigation();
                    break;
                case ContrlMode.VerticalNavigation:
                    VerticalNavigation();
                    break;
                case ContrlMode.APOn:
                    APOn();
                    break;
                case ContrlMode.AutoSpeed:
                    AutoSpeed();
                    break;
            }
        }

        private void ManualPolymerize()
        {
            StatusDatasBus.CenterSignal.Yaw = StatusDatasBus.RemoteSignal.Yaw;
            StatusDatasBus.CenterSignal.ThrottelL1 = StatusDatasBus.RemoteSignal.Throttel;
            StatusDatasBus.CenterSignal.ThrottelR1 = StatusDatasBus.RemoteSignal.Throttel;
            //对油门进行PID控制
            //_pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, (float)Cache.RemoteSignal.Channel03);
            StatusDatasBus.CenterSignal.RollL = StatusDatasBus.RemoteSignal.Roll;
            StatusDatasBus.CenterSignal.RollR = StatusDatasBus.RemoteSignal.Roll;
            StatusDatasBus.CenterSignal.PitchL =StatusDatasBus.RemoteSignal.Pitch;
            StatusDatasBus.CenterSignal.PitchR = StatusDatasBus.RemoteSignal.Pitch;

            CommonOperation();
        }
        private void LateralNavigation()
        {

        }
        private void VerticalNavigation()
        {

        }
        private void APOn()
        {

        }
        private void AutoSpeed()
        {

        }

        /// <summary>
        /// 这块需要读取配置文件，看每个通道对应什么功能
        /// </summary>
        private void CommonOperation()
        {
            StatusDatasBus.CenterSignal.Gear = (Switch)StatusDatasBus.RemoteSignal.Channel05 == Switch.On ? true : false;
            StatusDatasBus.CenterSignal.PushBack = (Switch)StatusDatasBus.RemoteSignal.Channel11 == Switch.On ? true : false;
            switch (StatusDatasBus.RemoteSignal.Channel09)
            {
                case Switch.Off:
                    StatusDatasBus.CenterSignal.Flap = FlapMode.FlapUp;
                    break;
                case Switch.MId:
                    StatusDatasBus.CenterSignal.Flap = FlapMode.TakeOff;
                    break;
                case Switch.On:
                    StatusDatasBus.CenterSignal.Flap = FlapMode.Landing;
                    break;
                default:
                    break;
            }
            #region 灯光
            /*使用频道8(两档)/7(三挡)
            8-off
                  7-off:航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                  7-mid:滑行灯开(根据起落架状态)
                  7-on:跑道脱离灯
            8-on  
                  7-on:起飞灯(根据起落架状态)
                  7-mid:着陆灯(根据起落架状态)
                  7-off:高亮度白色防撞灯 机翼检查灯开
            7个灯光控制*/
            if ((Switch)StatusDatasBus.RemoteSignal.Channel08 == Switch.Off)
            {
                if ((Switch)StatusDatasBus.RemoteSignal.Channel07 == Switch.Off)//航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                {
                    StatusDatasBus.CenterSignal.TaxiLight = false;
                    StatusDatasBus.CenterSignal.RunwayLight = false;
                    StatusDatasBus.CenterSignal.TakeOffLight = false;
                    StatusDatasBus.CenterSignal.LandingLight = false;
                    StatusDatasBus.CenterSignal.WingInspectionLight = false;
                    StatusDatasBus.CenterSignal.PositionLight = false;

                    StatusDatasBus.CenterSignal.FlightLight = true;
                    StatusDatasBus.CenterSignal.AntiCollisionLight = true;
                    StatusDatasBus.CenterSignal.LogoLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)StatusDatasBus.RemoteSignal.Channel07 == Switch.MId)// 滑行灯开(根据起落架状态)
                {
                    StatusDatasBus.CenterSignal.RunwayLight = false;
                    StatusDatasBus.CenterSignal.TakeOffLight = false;
                    StatusDatasBus.CenterSignal.LandingLight = false;
                    StatusDatasBus.CenterSignal.WingInspectionLight = false;
                    StatusDatasBus.CenterSignal.PositionLight = false;

                    StatusDatasBus.CenterSignal.FlightLight = true;
                    StatusDatasBus.CenterSignal.AntiCollisionLight = true;
                    StatusDatasBus.CenterSignal.LogoLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.TaxiLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                }
                else//跑道脱离灯
                {
                    StatusDatasBus.CenterSignal.TakeOffLight = false;
                    StatusDatasBus.CenterSignal.LandingLight = false;
                    StatusDatasBus.CenterSignal.WingInspectionLight = false;
                    StatusDatasBus.CenterSignal.PositionLight = false;

                    StatusDatasBus.CenterSignal.FlightLight = true;
                    StatusDatasBus.CenterSignal.AntiCollisionLight = true;
                    StatusDatasBus.CenterSignal.LogoLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.TaxiLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.RunwayLight = true;
                }
            }
            else
            {
                if ((Switch)StatusDatasBus.RemoteSignal.Channel07 == Switch.On)//起飞灯(根据起落架状态)
                {
                    StatusDatasBus.CenterSignal.LandingLight = false;
                    StatusDatasBus.CenterSignal.WingInspectionLight = false;
                    StatusDatasBus.CenterSignal.PositionLight = false;

                    StatusDatasBus.CenterSignal.FlightLight = true;
                    StatusDatasBus.CenterSignal.AntiCollisionLight = true;
                    StatusDatasBus.CenterSignal.LogoLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.TaxiLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.RunwayLight = true;
                    StatusDatasBus.CenterSignal.TakeOffLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)StatusDatasBus.RemoteSignal.Channel07 == Switch.MId)//着陆灯(根据起落架状态)
                {
                    StatusDatasBus.CenterSignal.WingInspectionLight = false;
                    StatusDatasBus.CenterSignal.PositionLight = false;

                    StatusDatasBus.CenterSignal.FlightLight = true;
                    StatusDatasBus.CenterSignal.AntiCollisionLight = true;
                    StatusDatasBus.CenterSignal.LogoLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.TaxiLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.RunwayLight = true;
                    StatusDatasBus.CenterSignal.TakeOffLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.LandingLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                }
                else//高亮度白色防撞灯 机翼检查灯开
                {
                    StatusDatasBus.CenterSignal.FlightLight = true;
                    StatusDatasBus.CenterSignal.AntiCollisionLight = true;
                    StatusDatasBus.CenterSignal.LogoLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.TaxiLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.RunwayLight = true;
                    StatusDatasBus.CenterSignal.TakeOffLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.LandingLight = StatusDatasBus.CenterSignal.Gear ? true : false;
                    StatusDatasBus.CenterSignal.PositionLight = true;
                    StatusDatasBus.CenterSignal.WingInspectionLight = true;
                }
            }
            #endregion
        }

        public bool Lunch()
        {
            throw new NotImplementedException();
        }
    }
}
