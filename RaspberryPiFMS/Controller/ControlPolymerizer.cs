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
        private readonly PIDHelper _pid = new PIDHelper();

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
            StateDatasBus.CenterSignal.ThrottelL1 = (float)value;
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (StateDatasBus.FlightState.ContrlMode)
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
            StateDatasBus.CenterSignal.Yaw = StateDatasBus.RemoteSignal.Yaw;
            StateDatasBus.CenterSignal.ThrottelL1 = StateDatasBus.RemoteSignal.Throttel;
            StateDatasBus.CenterSignal.ThrottelR1 = StateDatasBus.RemoteSignal.Throttel;
            //对油门进行PID控制
            //_pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, (float)Cache.RemoteSignal.Channel03);
            StateDatasBus.CenterSignal.RollL = StateDatasBus.RemoteSignal.Roll;
            StateDatasBus.CenterSignal.RollR = StateDatasBus.RemoteSignal.Roll;
            StateDatasBus.CenterSignal.PitchL =StateDatasBus.RemoteSignal.Pitch;
            StateDatasBus.CenterSignal.PitchR = StateDatasBus.RemoteSignal.Pitch;

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
            StateDatasBus.CenterSignal.Gear = (Switch)StateDatasBus.RemoteSignal.Channel05 == Switch.On ? true : false;
            StateDatasBus.CenterSignal.PushBack = (Switch)StateDatasBus.RemoteSignal.Channel11 == Switch.On ? true : false;
            switch (StateDatasBus.RemoteSignal.Channel09)
            {
                case Switch.Off:
                    StateDatasBus.CenterSignal.Flap = FlapMode.FlapUp;
                    break;
                case Switch.MId:
                    StateDatasBus.CenterSignal.Flap = FlapMode.TakeOff;
                    break;
                case Switch.On:
                    StateDatasBus.CenterSignal.Flap = FlapMode.Landing;
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
            if ((Switch)StateDatasBus.RemoteSignal.Channel08 == Switch.Off)
            {
                if ((Switch)StateDatasBus.RemoteSignal.Channel07 == Switch.Off)//航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                {
                    StateDatasBus.CenterSignal.TaxiLight = false;
                    StateDatasBus.CenterSignal.RunwayLight = false;
                    StateDatasBus.CenterSignal.TakeOffLight = false;
                    StateDatasBus.CenterSignal.LandingLight = false;
                    StateDatasBus.CenterSignal.WingInspectionLight = false;
                    StateDatasBus.CenterSignal.PositionLight = false;

                    StateDatasBus.CenterSignal.FlightLight = true;
                    StateDatasBus.CenterSignal.AntiCollisionLight = true;
                    StateDatasBus.CenterSignal.LogoLight = StateDatasBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)StateDatasBus.RemoteSignal.Channel07 == Switch.MId)// 滑行灯开(根据起落架状态)
                {
                    StateDatasBus.CenterSignal.RunwayLight = false;
                    StateDatasBus.CenterSignal.TakeOffLight = false;
                    StateDatasBus.CenterSignal.LandingLight = false;
                    StateDatasBus.CenterSignal.WingInspectionLight = false;
                    StateDatasBus.CenterSignal.PositionLight = false;

                    StateDatasBus.CenterSignal.FlightLight = true;
                    StateDatasBus.CenterSignal.AntiCollisionLight = true;
                    StateDatasBus.CenterSignal.LogoLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.TaxiLight = StateDatasBus.CenterSignal.Gear ? true : false;
                }
                else//跑道脱离灯
                {
                    StateDatasBus.CenterSignal.TakeOffLight = false;
                    StateDatasBus.CenterSignal.LandingLight = false;
                    StateDatasBus.CenterSignal.WingInspectionLight = false;
                    StateDatasBus.CenterSignal.PositionLight = false;

                    StateDatasBus.CenterSignal.FlightLight = true;
                    StateDatasBus.CenterSignal.AntiCollisionLight = true;
                    StateDatasBus.CenterSignal.LogoLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.TaxiLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.RunwayLight = true;
                }
            }
            else
            {
                if ((Switch)StateDatasBus.RemoteSignal.Channel07 == Switch.On)//起飞灯(根据起落架状态)
                {
                    StateDatasBus.CenterSignal.LandingLight = false;
                    StateDatasBus.CenterSignal.WingInspectionLight = false;
                    StateDatasBus.CenterSignal.PositionLight = false;

                    StateDatasBus.CenterSignal.FlightLight = true;
                    StateDatasBus.CenterSignal.AntiCollisionLight = true;
                    StateDatasBus.CenterSignal.LogoLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.TaxiLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.RunwayLight = true;
                    StateDatasBus.CenterSignal.TakeOffLight = StateDatasBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)StateDatasBus.RemoteSignal.Channel07 == Switch.MId)//着陆灯(根据起落架状态)
                {
                    StateDatasBus.CenterSignal.WingInspectionLight = false;
                    StateDatasBus.CenterSignal.PositionLight = false;

                    StateDatasBus.CenterSignal.FlightLight = true;
                    StateDatasBus.CenterSignal.AntiCollisionLight = true;
                    StateDatasBus.CenterSignal.LogoLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.TaxiLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.RunwayLight = true;
                    StateDatasBus.CenterSignal.TakeOffLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.LandingLight = StateDatasBus.CenterSignal.Gear ? true : false;
                }
                else//高亮度白色防撞灯 机翼检查灯开
                {
                    StateDatasBus.CenterSignal.FlightLight = true;
                    StateDatasBus.CenterSignal.AntiCollisionLight = true;
                    StateDatasBus.CenterSignal.LogoLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.TaxiLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.RunwayLight = true;
                    StateDatasBus.CenterSignal.TakeOffLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.LandingLight = StateDatasBus.CenterSignal.Gear ? true : false;
                    StateDatasBus.CenterSignal.PositionLight = true;
                    StateDatasBus.CenterSignal.WingInspectionLight = true;
                }
            }
            #endregion
        }
    }
}
