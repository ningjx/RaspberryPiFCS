using RaspberryPiFCS.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.BaseController
{
    public class FlightController
    {
        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            switch (DataBus.FlightStatus.ContrlMode)
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
            DataBus.CenterSignal.Yaw = DataBus.RemoteSignal.Yaw;
            DataBus.CenterSignal.ThrottelL1 = DataBus.RemoteSignal.Throttel;
            DataBus.CenterSignal.ThrottelR1 = DataBus.RemoteSignal.Throttel;
            //对油门进行PID控制
            //_pid.SetWithPID((float)Cache.CenterControlData.ThrottelL, (float)Cache.RemoteSignal.Channel03);
            DataBus.CenterSignal.RollL = DataBus.RemoteSignal.Roll;
            DataBus.CenterSignal.RollR = DataBus.RemoteSignal.Roll;
            DataBus.CenterSignal.PitchL = DataBus.RemoteSignal.Pitch;
            DataBus.CenterSignal.PitchR = DataBus.RemoteSignal.Pitch;

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
        private static void CommonOperation()
        {
            DataBus.CenterSignal.Gear = (Switch)DataBus.RemoteSignal.Channel05 == Switch.On ? true : false;
            DataBus.CenterSignal.PushBack = (Switch)DataBus.RemoteSignal.Channel11 == Switch.On ? true : false;
            switch (DataBus.RemoteSignal.Channel09)
            {
                case Switch.Off:
                    DataBus.CenterSignal.Flap = FlapMode.FlapUp;
                    break;
                case Switch.MId:
                    DataBus.CenterSignal.Flap = FlapMode.TakeOff;
                    break;
                case Switch.On:
                    DataBus.CenterSignal.Flap = FlapMode.Landing;
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
            if ((Switch)DataBus.RemoteSignal.Channel08 == Switch.Off)
            {
                if ((Switch)DataBus.RemoteSignal.Channel07 == Switch.Off)//航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                {
                    DataBus.CenterSignal.TaxiLight = false;
                    DataBus.CenterSignal.RunwayLight = false;
                    DataBus.CenterSignal.TakeOffLight = false;
                    DataBus.CenterSignal.LandingLight = false;
                    DataBus.CenterSignal.WingInspectionLight = false;
                    DataBus.CenterSignal.PositionLight = false;

                    DataBus.CenterSignal.FlightLight = true;
                    DataBus.CenterSignal.AntiCollisionLight = true;
                    DataBus.CenterSignal.LogoLight = DataBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)DataBus.RemoteSignal.Channel07 == Switch.MId)// 滑行灯开(根据起落架状态)
                {
                    DataBus.CenterSignal.RunwayLight = false;
                    DataBus.CenterSignal.TakeOffLight = false;
                    DataBus.CenterSignal.LandingLight = false;
                    DataBus.CenterSignal.WingInspectionLight = false;
                    DataBus.CenterSignal.PositionLight = false;

                    DataBus.CenterSignal.FlightLight = true;
                    DataBus.CenterSignal.AntiCollisionLight = true;
                    DataBus.CenterSignal.LogoLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.TaxiLight = DataBus.CenterSignal.Gear ? true : false;
                }
                else//跑道脱离灯
                {
                    DataBus.CenterSignal.TakeOffLight = false;
                    DataBus.CenterSignal.LandingLight = false;
                    DataBus.CenterSignal.WingInspectionLight = false;
                    DataBus.CenterSignal.PositionLight = false;

                    DataBus.CenterSignal.FlightLight = true;
                    DataBus.CenterSignal.AntiCollisionLight = true;
                    DataBus.CenterSignal.LogoLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.TaxiLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.RunwayLight = true;
                }
            }
            else
            {
                if ((Switch)DataBus.RemoteSignal.Channel07 == Switch.On)//起飞灯(根据起落架状态)
                {
                    DataBus.CenterSignal.LandingLight = false;
                    DataBus.CenterSignal.WingInspectionLight = false;
                    DataBus.CenterSignal.PositionLight = false;

                    DataBus.CenterSignal.FlightLight = true;
                    DataBus.CenterSignal.AntiCollisionLight = true;
                    DataBus.CenterSignal.LogoLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.TaxiLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.RunwayLight = true;
                    DataBus.CenterSignal.TakeOffLight = DataBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)DataBus.RemoteSignal.Channel07 == Switch.MId)//着陆灯(根据起落架状态)
                {
                    DataBus.CenterSignal.WingInspectionLight = false;
                    DataBus.CenterSignal.PositionLight = false;

                    DataBus.CenterSignal.FlightLight = true;
                    DataBus.CenterSignal.AntiCollisionLight = true;
                    DataBus.CenterSignal.LogoLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.TaxiLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.RunwayLight = true;
                    DataBus.CenterSignal.TakeOffLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.LandingLight = DataBus.CenterSignal.Gear ? true : false;
                }
                else//高亮度白色防撞灯 机翼检查灯开
                {
                    DataBus.CenterSignal.FlightLight = true;
                    DataBus.CenterSignal.AntiCollisionLight = true;
                    DataBus.CenterSignal.LogoLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.TaxiLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.RunwayLight = true;
                    DataBus.CenterSignal.TakeOffLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.LandingLight = DataBus.CenterSignal.Gear ? true : false;
                    DataBus.CenterSignal.PositionLight = true;
                    DataBus.CenterSignal.WingInspectionLight = true;
                }
            }
            #endregion
        }
    }
}
