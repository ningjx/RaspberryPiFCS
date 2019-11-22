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
            Cache.CenterControlData.ThrottelL = value;
            Cache.CenterControlData.ThrottelR = value;
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (Cache.ContrlMode)
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
            Cache.CenterControlData.Pitch = Cache.RemoteSignal.Channel02;
            Cache.CenterControlData.Yaw = Cache.RemoteSignal.Channel01;
            //对油门进行PID控制
            _pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, (float)Cache.RemoteSignal.Channel03);
            //Cache.CenterControlData.Throttel = Cache.RemoteSignal.Channel03;
            Cache.CenterControlData.Roll = Cache.RemoteSignal.Channel04;
            Cache.CenterControlData.Pitch = Cache.RemoteSignal.Channel02;
            Cache.CenterControlData.Trim = Cache.RemoteSignal.Channel12;
            //Cache.RemoteSignal.Channel06
            CommonOperation();
        }
        private void LateralNavigation()
        {
            Cache.CenterControlData.Pitch = Cache.RemoteSignal.Channel02;
            Cache.CenterControlData.Trim = Cache.RemoteSignal.Channel12;

            Cache.CenterControlData.Roll = Cache.AutoControlData.Roll;

            _pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, Cache.AutoControlData.ThrottelL);

        }
        private void VerticalNavigation()
        {
            Cache.CenterControlData.Roll = Cache.RemoteSignal.Channel04;

            Cache.CenterControlData.Pitch = Cache.AutoControlData.Pitch;
            Cache.CenterControlData.Trim = Cache.AutoControlData.Trim;

            _pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, Cache.AutoControlData.ThrottelL);
        }
        private void APOn()
        {
            Cache.CenterControlData.Roll = Cache.AutoControlData.Roll;
            Cache.CenterControlData.Pitch = Cache.AutoControlData.Pitch;
            Cache.CenterControlData.Trim = Cache.AutoControlData.Trim;

            _pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, Cache.AutoControlData.ThrottelL);
        }
        private void AutoSpeed()
        {
            Cache.CenterControlData.Pitch = Cache.RemoteSignal.Channel02;
            Cache.CenterControlData.Roll = Cache.RemoteSignal.Channel04;

            Cache.CenterControlData.Trim = Cache.AutoControlData.Trim;

            _pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, Cache.AutoControlData.ThrottelL);
        }

        private void CommonOperation()
        {
            Cache.CenterControlData.Gear = Cache.RemoteSignal.Channel05 == Switch.On ? true : false;
            Cache.CenterControlData.PushBack = Cache.RemoteSignal.Channel11 == Switch.On ? true : false;
            Cache.CenterControlData.Flap = Cache.RemoteSignal.Channel09 == Switch.On ? FlapMode.Landing : (Cache.RemoteSignal.Channel09 == Switch.MId ? FlapMode.TakeOff : FlapMode.FlapUp);
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
            if (Cache.RemoteSignal.Channel08 == Switch.Off)
            {
                if (Cache.RemoteSignal.Channel07 == Switch.Off)//航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                {
                    Cache.CenterControlData.TaxiLight = false;
                    Cache.CenterControlData.RunwayLight = false;
                    Cache.CenterControlData.TakeOffLight = false;
                    Cache.CenterControlData.LandingLight = false;
                    Cache.CenterControlData.WingInspectionLight = false;
                    Cache.CenterControlData.PositionLight = false;

                    Cache.CenterControlData.FlightLight = true;
                    Cache.CenterControlData.AntiCollisionLight = true;
                    Cache.CenterControlData.LogoLight = Cache.CenterControlData.Gear ? true : false;
                }
                else if (Cache.RemoteSignal.Channel07 == Switch.MId)// 滑行灯开(根据起落架状态)
                {
                    Cache.CenterControlData.RunwayLight = false;
                    Cache.CenterControlData.TakeOffLight = false;
                    Cache.CenterControlData.LandingLight = false;
                    Cache.CenterControlData.WingInspectionLight = false;
                    Cache.CenterControlData.PositionLight = false;

                    Cache.CenterControlData.FlightLight = true;
                    Cache.CenterControlData.AntiCollisionLight = true;
                    Cache.CenterControlData.LogoLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.TaxiLight = Cache.CenterControlData.Gear ? true : false;
                }
                else//跑道脱离灯
                {
                    Cache.CenterControlData.TakeOffLight = false;
                    Cache.CenterControlData.LandingLight = false;
                    Cache.CenterControlData.WingInspectionLight = false;
                    Cache.CenterControlData.PositionLight = false;

                    Cache.CenterControlData.FlightLight = true;
                    Cache.CenterControlData.AntiCollisionLight = true;
                    Cache.CenterControlData.LogoLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.TaxiLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.RunwayLight = true;
                }
            }
            else
            {
                if (Cache.RemoteSignal.Channel07 == Switch.On)//起飞灯(根据起落架状态)
                {
                    Cache.CenterControlData.LandingLight = false;
                    Cache.CenterControlData.WingInspectionLight = false;
                    Cache.CenterControlData.PositionLight = false;

                    Cache.CenterControlData.FlightLight = true;
                    Cache.CenterControlData.AntiCollisionLight = true;
                    Cache.CenterControlData.LogoLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.TaxiLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.RunwayLight = true;
                    Cache.CenterControlData.TakeOffLight = Cache.CenterControlData.Gear ? true : false;
                }
                else if (Cache.RemoteSignal.Channel07 == Switch.MId)//着陆灯(根据起落架状态)
                {
                    Cache.CenterControlData.WingInspectionLight = false;
                    Cache.CenterControlData.PositionLight = false;

                    Cache.CenterControlData.FlightLight = true;
                    Cache.CenterControlData.AntiCollisionLight = true;
                    Cache.CenterControlData.LogoLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.TaxiLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.RunwayLight = true;
                    Cache.CenterControlData.TakeOffLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.LandingLight = Cache.CenterControlData.Gear ? true : false;
                }
                else//高亮度白色防撞灯 机翼检查灯开
                {
                    Cache.CenterControlData.FlightLight = true;
                    Cache.CenterControlData.AntiCollisionLight = true;
                    Cache.CenterControlData.LogoLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.TaxiLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.RunwayLight = true;
                    Cache.CenterControlData.TakeOffLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.LandingLight = Cache.CenterControlData.Gear ? true : false;
                    Cache.CenterControlData.PositionLight = true;
                    Cache.CenterControlData.WingInspectionLight = true;
                }
            }
            #endregion
        }
    }
}
