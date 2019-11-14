using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RaspberryPiFMS.Controller
{
    public class LEDController
    {
        /// <summary>
        /// 航行灯（翼尖闪烁）
        /// </summary>
        private bool _flightLight = true;
        /// <summary>
        /// 红色信标灯（闪烁）
        /// </summary>
        private bool _antiCollisionLight = true;
        private MicroTimer _timer = new MicroTimer();
        private MicroTimer _ledTimer = new MicroTimer();
        private Pca9685 _ledDriver;
        private readonly List<LedChannel> _mode1 = new List<LedChannel>
        {
            LedChannel.TaxiLight,
            LedChannel.RunwayLight,
            LedChannel.TakeoffLight,
            LedChannel.LandingLight,
            LedChannel.AntiCollisionLightWhite,
            LedChannel.WingInspectionLight
        };
        private readonly List<LedChannel> _mode2 = new List<LedChannel>
        {
            LedChannel.RunwayLight,
            LedChannel.TakeoffLight,
            LedChannel.LandingLight,
            LedChannel.AntiCollisionLightWhite,
            LedChannel.WingInspectionLight
        };
        private readonly List<LedChannel> _mode3 = new List<LedChannel>
        {
            LedChannel.TakeoffLight,
            LedChannel.LandingLight,
            LedChannel.AntiCollisionLightWhite,
            LedChannel.WingInspectionLight

        };
        private readonly List<LedChannel> _mode4 = new List<LedChannel>
        {
            LedChannel.LandingLight,
            LedChannel.AntiCollisionLightWhite,
            LedChannel.WingInspectionLight
        };
        private readonly List<LedChannel> _mode5 = new List<LedChannel>
        {
            LedChannel.AntiCollisionLightWhite,
            LedChannel.WingInspectionLight
        };

        public LEDController(Pca9685 ledDriver, int ms = 15)
        {
            ms = Math.Abs(ms);
            _ledDriver = ledDriver;
            _timer.Interval = ms;
            _timer.Elapsed += Excute;

            _ledTimer.Interval = 1500;
            _ledTimer.Elapsed += TwinkleLed;
            _timer.Start();
            _ledTimer.Start();
        }

        //使用频道7(三挡)/8(两档)
        //8-off
        //      7-off:航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
        //      7-mid:滑行灯开(根据起落架状态)
        //      7-on:跑道脱离灯
        //8-on
        //      7-on:起飞灯(根据起落架状态)
        //      7-mid:着陆灯(根据起落架状态)
        //      7-off:高亮度白色防撞灯 机翼检查灯开
        //7个灯光控制
        private void Excute()
        {
            if (Cache.RemoteSignal.Channel08 == Switch.Off)
            {
                if (Cache.RemoteSignal.Channel07 == Switch.Off)//航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
                {
                    _mode1.ForEach(t => _ledDriver.SetLedOff((int)t));
                    if (Cache.CenterControlData.Gear)
                        _ledDriver.SetLedOn((int)LedChannel.LogoLight);
                    else
                        _ledDriver.SetLedOff((int)LedChannel.LogoLight);
                }
                else if (Cache.RemoteSignal.Channel07 == Switch.MId)// 滑行灯开(根据起落架状态)
                {
                    _mode2.ForEach(t => _ledDriver.SetLedOff((int)t));
                    if (Cache.CenterControlData.Gear)
                        _ledDriver.SetLedOn((int)LedChannel.TaxiLight);
                    else
                        _ledDriver.SetLedOff((int)LedChannel.TaxiLight);
                }
                else//跑道脱离灯
                {
                    _mode3.ForEach(t => _ledDriver.SetLedOff((int)t));
                    _ledDriver.SetLedOn((int)LedChannel.RunwayLight);
                }
            }
            else
            {
                if (Cache.RemoteSignal.Channel07 == Switch.On)//起飞灯(根据起落架状态)
                {
                    _mode4.ForEach(t => _ledDriver.SetLedOff((int)t));
                    if (Cache.CenterControlData.Gear)
                        _ledDriver.SetLedOn((int)LedChannel.TakeoffLight);
                    else
                        _ledDriver.SetLedOff((int)LedChannel.TakeoffLight);
                }
                else if (Cache.RemoteSignal.Channel07 == Switch.MId)//着陆灯(根据起落架状态)
                {
                    _mode5.ForEach(t => _ledDriver.SetLedOff((int)t));
                    if (Cache.CenterControlData.Gear)
                        _ledDriver.SetLedOn((int)LedChannel.LandingLight);
                    else
                        _ledDriver.SetLedOff((int)LedChannel.LandingLight);
                }
                else//高亮度白色防撞灯 机翼检查灯开
                {
                    _mode1.ForEach(t => _ledDriver.SetLedOn((int)t));
                    if (Cache.CenterControlData.Gear)
                        _ledDriver.SetLedOn((int)LedChannel.LogoLight);
                    else
                        _ledDriver.SetLedOff((int)LedChannel.LogoLight);
                }
            }
        }

        private void TwinkleLed()
        {
            if (_flightLight)
            {
                _ledDriver.SetLedOn((int)LedChannel.FilghtLight);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.FilghtLight);
                Thread.Sleep(200);
                _ledDriver.SetLedOn((int)LedChannel.FilghtLight);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.FilghtLight);
            }
            if (_antiCollisionLight)
            {
                _ledDriver.SetLedOn((int)LedChannel.AntiCollisionLight);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.AntiCollisionLight);
            }
        }
    }
}
