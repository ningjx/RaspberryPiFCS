using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.ComputeCenter
{
    public class ControlPolymerize
    {
        private Timer _timer = new Timer();
        private PIDHelper _pid = new PIDHelper();

        public ControlPolymerize()
        {
            _pid.PIDOutEvent += _pid_PIDOutEvent;
            _timer.Interval = 10;
            _timer.AutoReset = true;
            _timer.Elapsed += Excute;
            _timer.Start();
        }

        private void _pid_PIDOutEvent(double value)
        {
            Bus.CenterData.ThrottelL = value;
            Bus.CenterData.ThrottelR = value;
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (Bus.ContrlMode)
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
            Bus.CenterData.Pitch = Bus.RemoteSignal.Channel02;
            Bus.CenterData.Yaw = Bus.RemoteSignal.Channel01;
            //对油门进行PID控制
            //_pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, (float)Cache.RemoteSignal.Channel03);
            Bus.CenterData.ThrottelL = Bus.RemoteSignal.Channel03;
            Bus.CenterData.ThrottelR = Bus.RemoteSignal.Channel03;
            Bus.CenterData.Roll = Bus.RemoteSignal.Channel04;
            Bus.CenterData.Pitch = Bus.RemoteSignal.Channel02;
            Bus.CenterData.Trim = Bus.RemoteSignal.Channel12;
            //Cache.RemoteSignal.Channel06
            CommonOperation();
        }
        private void LateralNavigation()
        {
            Bus.CenterData.Pitch = Bus.RemoteSignal.Channel02;
            Bus.CenterData.Trim = Bus.RemoteSignal.Channel12;

            Bus.CenterData.Roll = Bus.AutoControlData.Roll;

            _pid.SetWithPID((float)Bus.CenterData.ThrottelL, Bus.AutoControlData.ThrottelL);

        }
        private void VerticalNavigation()
        {
            Bus.CenterData.Roll = Bus.RemoteSignal.Channel04;

            Bus.CenterData.Pitch = Bus.AutoControlData.Pitch;
            Bus.CenterData.Trim = Bus.AutoControlData.Trim;

            _pid.SetWithPID((float)Bus.CenterData.ThrottelL, Bus.AutoControlData.ThrottelL);
        }
        private void APOn()
        {
            Bus.CenterData.Roll = Bus.AutoControlData.Roll;
            Bus.CenterData.Pitch = Bus.AutoControlData.Pitch;
            Bus.CenterData.Trim = Bus.AutoControlData.Trim;

            _pid.SetWithPID((float)Bus.CenterData.ThrottelL, Bus.AutoControlData.ThrottelL);
        }
        private void AutoSpeed()
        {
            Bus.CenterData.Pitch = Bus.RemoteSignal.Channel02;
            Bus.CenterData.Roll = Bus.RemoteSignal.Channel04;

            Bus.CenterData.Trim = Bus.AutoControlData.Trim;

            _pid.SetWithPID((float)Bus.CenterData.ThrottelL, Bus.AutoControlData.ThrottelL);
        }

        private void CommonOperation()
        {
            Bus.CenterData.Gear = Bus.RemoteSignal.Channel05 == Switch.On ? true : false;
            Bus.CenterData.PushBack = Bus.RemoteSignal.Channel11 == Switch.On ? true : false;
            Bus.CenterData.Flap = Bus.RemoteSignal.Channel09 == Switch.On ? FlapMode.Landing : (Bus.RemoteSignal.Channel09 == Switch.MId ? FlapMode.TakeOff : FlapMode.FlapUp);
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
            if (Bus.RemoteSignal.Channel08 == Switch.Off)
            {
                if (Bus.RemoteSignal.Channel07 == Switch.Off)//航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                {
                    Bus.CenterData.TaxiLight = false;
                    Bus.CenterData.RunwayLight = false;
                    Bus.CenterData.TakeOffLight = false;
                    Bus.CenterData.LandingLight = false;
                    Bus.CenterData.WingInspectionLight = false;
                    Bus.CenterData.PositionLight = false;

                    Bus.CenterData.FlightLight = true;
                    Bus.CenterData.AntiCollisionLight = true;
                    Bus.CenterData.LogoLight = Bus.CenterData.Gear ? true : false;
                }
                else if (Bus.RemoteSignal.Channel07 == Switch.MId)// 滑行灯开(根据起落架状态)
                {
                    Bus.CenterData.RunwayLight = false;
                    Bus.CenterData.TakeOffLight = false;
                    Bus.CenterData.LandingLight = false;
                    Bus.CenterData.WingInspectionLight = false;
                    Bus.CenterData.PositionLight = false;

                    Bus.CenterData.FlightLight = true;
                    Bus.CenterData.AntiCollisionLight = true;
                    Bus.CenterData.LogoLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.TaxiLight = Bus.CenterData.Gear ? true : false;
                }
                else//跑道脱离灯
                {
                    Bus.CenterData.TakeOffLight = false;
                    Bus.CenterData.LandingLight = false;
                    Bus.CenterData.WingInspectionLight = false;
                    Bus.CenterData.PositionLight = false;

                    Bus.CenterData.FlightLight = true;
                    Bus.CenterData.AntiCollisionLight = true;
                    Bus.CenterData.LogoLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.TaxiLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.RunwayLight = true;
                }
            }
            else
            {
                if (Bus.RemoteSignal.Channel07 == Switch.On)//起飞灯(根据起落架状态)
                {
                    Bus.CenterData.LandingLight = false;
                    Bus.CenterData.WingInspectionLight = false;
                    Bus.CenterData.PositionLight = false;

                    Bus.CenterData.FlightLight = true;
                    Bus.CenterData.AntiCollisionLight = true;
                    Bus.CenterData.LogoLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.TaxiLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.RunwayLight = true;
                    Bus.CenterData.TakeOffLight = Bus.CenterData.Gear ? true : false;
                }
                else if (Bus.RemoteSignal.Channel07 == Switch.MId)//着陆灯(根据起落架状态)
                {
                    Bus.CenterData.WingInspectionLight = false;
                    Bus.CenterData.PositionLight = false;

                    Bus.CenterData.FlightLight = true;
                    Bus.CenterData.AntiCollisionLight = true;
                    Bus.CenterData.LogoLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.TaxiLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.RunwayLight = true;
                    Bus.CenterData.TakeOffLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.LandingLight = Bus.CenterData.Gear ? true : false;
                }
                else//高亮度白色防撞灯 机翼检查灯开
                {
                    Bus.CenterData.FlightLight = true;
                    Bus.CenterData.AntiCollisionLight = true;
                    Bus.CenterData.LogoLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.TaxiLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.RunwayLight = true;
                    Bus.CenterData.TakeOffLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.LandingLight = Bus.CenterData.Gear ? true : false;
                    Bus.CenterData.PositionLight = true;
                    Bus.CenterData.WingInspectionLight = true;
                }
            }
            #endregion
        }
    }
}
