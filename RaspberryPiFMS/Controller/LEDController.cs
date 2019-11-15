﻿using RaspberryPiFMS.Enum;
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
        private bool isExcuting = false;
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

        public LEDController(Pca9685 ledDriver, int ms = 30)
        {
            ms = Math.Abs(ms);
            _ledDriver = ledDriver;
            _timer.Interval = ms;
            _timer.Elapsed += Excute;

            _ledTimer.Interval = 3000;
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
            if (isExcuting)
                return;
            isExcuting = true;
            switch (Cache.CenterControlData.AntiCollisionLight)
            {
                case true:
                    _antiCollisionLight = true;
                    break;
                case false:
                    _antiCollisionLight = false;
                    break;
            }
            switch (Cache.CenterControlData.FlightLight)
            {
                case true:
                    _flightLight = true;
                    break;
                case false:
                    _flightLight = false;
                    break;
            }
            switch (Cache.CenterControlData.TaxiLight)
            {
                case true:
                    _ledDriver.SetLedOn((int)LedChannel.TaxiLight);
                    break;
                case false:
                    _ledDriver.SetLedOff((int)LedChannel.TaxiLight);
                    break;
            }
            switch (Cache.CenterControlData.RunwayLight)
            {
                case true:
                    _ledDriver.SetLedOn((int)LedChannel.RunwayLight);
                    break;
                case false:
                    _ledDriver.SetLedOff((int)LedChannel.RunwayLight);
                    break;
            }
            switch (Cache.CenterControlData.TakeOffLight)
            {
                case true:
                    _ledDriver.SetLedOn((int)LedChannel.TakeoffLight);
                    break;
                case false:
                    _ledDriver.SetLedOff((int)LedChannel.TakeoffLight);
                    break;
            }
            switch (Cache.CenterControlData.LandingLight)
            {
                case true:
                    _ledDriver.SetLedOn((int)LedChannel.LandingLight);
                    break;
                case false:
                    _ledDriver.SetLedOff((int)LedChannel.LandingLight);
                    break;
            }
            switch (Cache.CenterControlData.WingInspectionLight)
            {
                case true:
                    _ledDriver.SetLedOn((int)LedChannel.WingInspectionLight);
                    break;
                case false:
                    _ledDriver.SetLedOff((int)LedChannel.WingInspectionLight);
                    break;
            }
            switch (Cache.CenterControlData.PositionLight)
            {
                case true:
                    _ledDriver.SetLedOn((int)LedChannel.AntiCollisionLightWhite);
                    break;
                case false:
                    _ledDriver.SetLedOff((int)LedChannel.AntiCollisionLightWhite);
                    break;
            }
            isExcuting = false;
        }

        private void TwinkleLed()
        {
            if (_flightLight)
            {
                _ledDriver.SetLedOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                _ledDriver.SetLedOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(300);

                _ledDriver.SetLedOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                _ledDriver.SetLedOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.FilghtLightR);
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
