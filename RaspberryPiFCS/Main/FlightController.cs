using RaspberryPiFCS.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiFCS.Models;
using RaspberryPiFCS.Configs;

namespace RaspberryPiFCS.Main
{
    public class FlightController
    {

        /// <summary>
        /// 将<see cref="OriginSignal"/>转换成<see cref="RemoteSignal"/>
        /// </summary>
        private void TranslateSignal()
        {
            foreach (var channel in SignalBus.ControlConfig.Channels)
            {
                if (channel.ChannelType == ChannelType.Switch)
                {
                    if (SignalBus.OriginSignal[channel.ChannelNum] == channel.MinValue)
                        SignalBus.RemoteSignal[channel.RemoteMapping.GetHashCode()] = Switch.Off;
                    else if (SignalBus.OriginSignal[channel.ChannelNum] == channel.MaxValue)
                        SignalBus.RemoteSignal[channel.RemoteMapping.GetHashCode()] = Switch.On;
                    else if (SignalBus.OriginSignal[channel.ChannelNum] == channel.MidValue)
                        SignalBus.RemoteSignal[channel.RemoteMapping.GetHashCode()] = Switch.MId;
                }
                else if (channel.ChannelType == ChannelType.Rocker)
                {
                    SignalBus.RemoteSignal[channel.RemoteMapping.GetHashCode()] = (SignalBus.OriginSignal[channel.ChannelNum] - channel.MinValue) / (channel.MaxValue - channel.MinValue) * (channel.MaxAngle - channel.MinAngle) + channel.MinAngle;
                }
            }
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
            SignalBus.CenterSignal.Gear = (Switch)SignalBus.RemoteSignal.Channel05 == Switch.On ? true : false;
            SignalBus.CenterSignal.PushBack = (Switch)SignalBus.RemoteSignal.Channel11 == Switch.On ? true : false;
            switch (SignalBus.RemoteSignal.Channel09)
            {
                case Switch.Off:
                    SignalBus.CenterSignal.Flap = FlapMode.FlapUp;
                    break;
                case Switch.MId:
                    SignalBus.CenterSignal.Flap = FlapMode.TakeOff;
                    break;
                case Switch.On:
                    SignalBus.CenterSignal.Flap = FlapMode.Landing;
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
            if ((Switch)SignalBus.RemoteSignal.Channel08 == Switch.Off)
            {
                if ((Switch)SignalBus.RemoteSignal.Channel07 == Switch.Off)//航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                {
                    SignalBus.CenterSignal.TaxiLight = false;
                    SignalBus.CenterSignal.RunwayLight = false;
                    SignalBus.CenterSignal.TakeOffLight = false;
                    SignalBus.CenterSignal.LandingLight = false;
                    SignalBus.CenterSignal.WingInspectionLight = false;
                    SignalBus.CenterSignal.PositionLight = false;

                    SignalBus.CenterSignal.FlightLight = true;
                    SignalBus.CenterSignal.AntiCollisionLight = true;
                    SignalBus.CenterSignal.LogoLight = SignalBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)SignalBus.RemoteSignal.Channel07 == Switch.MId)// 滑行灯开(根据起落架状态)
                {
                    SignalBus.CenterSignal.RunwayLight = false;
                    SignalBus.CenterSignal.TakeOffLight = false;
                    SignalBus.CenterSignal.LandingLight = false;
                    SignalBus.CenterSignal.WingInspectionLight = false;
                    SignalBus.CenterSignal.PositionLight = false;

                    SignalBus.CenterSignal.FlightLight = true;
                    SignalBus.CenterSignal.AntiCollisionLight = true;
                    SignalBus.CenterSignal.LogoLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.TaxiLight = SignalBus.CenterSignal.Gear ? true : false;
                }
                else//跑道脱离灯
                {
                    SignalBus.CenterSignal.TakeOffLight = false;
                    SignalBus.CenterSignal.LandingLight = false;
                    SignalBus.CenterSignal.WingInspectionLight = false;
                    SignalBus.CenterSignal.PositionLight = false;

                    SignalBus.CenterSignal.FlightLight = true;
                    SignalBus.CenterSignal.AntiCollisionLight = true;
                    SignalBus.CenterSignal.LogoLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.TaxiLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.RunwayLight = true;
                }
            }
            else
            {
                if ((Switch)SignalBus.RemoteSignal.Channel07 == Switch.On)//起飞灯(根据起落架状态)
                {
                    SignalBus.CenterSignal.LandingLight = false;
                    SignalBus.CenterSignal.WingInspectionLight = false;
                    SignalBus.CenterSignal.PositionLight = false;

                    SignalBus.CenterSignal.FlightLight = true;
                    SignalBus.CenterSignal.AntiCollisionLight = true;
                    SignalBus.CenterSignal.LogoLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.TaxiLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.RunwayLight = true;
                    SignalBus.CenterSignal.TakeOffLight = SignalBus.CenterSignal.Gear ? true : false;
                }
                else if ((Switch)SignalBus.RemoteSignal.Channel07 == Switch.MId)//着陆灯(根据起落架状态)
                {
                    SignalBus.CenterSignal.WingInspectionLight = false;
                    SignalBus.CenterSignal.PositionLight = false;

                    SignalBus.CenterSignal.FlightLight = true;
                    SignalBus.CenterSignal.AntiCollisionLight = true;
                    SignalBus.CenterSignal.LogoLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.TaxiLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.RunwayLight = true;
                    SignalBus.CenterSignal.TakeOffLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.LandingLight = SignalBus.CenterSignal.Gear ? true : false;
                }
                else//高亮度白色防撞灯 机翼检查灯开
                {
                    SignalBus.CenterSignal.FlightLight = true;
                    SignalBus.CenterSignal.AntiCollisionLight = true;
                    SignalBus.CenterSignal.LogoLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.TaxiLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.RunwayLight = true;
                    SignalBus.CenterSignal.TakeOffLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.LandingLight = SignalBus.CenterSignal.Gear ? true : false;
                    SignalBus.CenterSignal.PositionLight = true;
                    SignalBus.CenterSignal.WingInspectionLight = true;
                }
            }
            #endregion
        }
    }
}
