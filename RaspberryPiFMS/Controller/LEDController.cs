using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Threading;
using Timer = System.Timers.Timer;

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
        private Timer _timer = new Timer();
        private Timer _ledTimer = new Timer();
        private bool _isExcuting = false;

        public LEDController(int ms = 30)
        {
            ms = Math.Abs(ms);
            _timer.Interval = ms;
            _timer.Elapsed += Excute;

            _ledTimer.Interval = 2200;
            _ledTimer.Elapsed += TwinkleLed;

            _timer.Start();
            _ledTimer.Start();
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isExcuting)
                return;
            _isExcuting = true;
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
                    Cache.LedDriver.SetLedOn((int)LedChannel.TaxiLight);
                    break;
                case false:
                    Cache.LedDriver.SetLedOff((int)LedChannel.TaxiLight);
                    break;
            }
            switch (Cache.CenterControlData.RunwayLight)
            {
                case true:
                    Cache.LedDriver.SetLedOn((int)LedChannel.RunwayLight);
                    break;
                case false:
                    Cache.LedDriver.SetLedOff((int)LedChannel.RunwayLight);
                    break;
            }
            switch (Cache.CenterControlData.TakeOffLight)
            {
                case true:
                    Cache.LedDriver.SetLedOn((int)LedChannel.TakeoffLight);
                    break;
                case false:
                    Cache.LedDriver.SetLedOff((int)LedChannel.TakeoffLight);
                    break;
            }
            switch (Cache.CenterControlData.LandingLight)
            {
                case true:
                    Cache.LedDriver.SetLedOn((int)LedChannel.LandingLight);
                    break;
                case false:
                    Cache.LedDriver.SetLedOff((int)LedChannel.LandingLight);
                    break;
            }
            switch (Cache.CenterControlData.WingInspectionLight)
            {
                case true:
                    Cache.LedDriver.SetLedOn((int)LedChannel.WingInspectionLight);
                    break;
                case false:
                    Cache.LedDriver.SetLedOff((int)LedChannel.WingInspectionLight);
                    break;
            }
            switch (Cache.CenterControlData.PositionLight)
            {
                case true:
                    Cache.LedDriver.SetLedOn((int)LedChannel.AntiCollisionLightWhite);
                    break;
                case false:
                    Cache.LedDriver.SetLedOff((int)LedChannel.AntiCollisionLightWhite);
                    break;
            }
            _isExcuting = false;
        }

        private void TwinkleLed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_flightLight)
            {
                Cache.LedDriver.SetLedOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                Cache.LedDriver.SetLedOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                Cache.LedDriver.SetLedOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                Cache.LedDriver.SetLedOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(300);

                Cache.LedDriver.SetLedOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                Cache.LedDriver.SetLedOff((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                Cache.LedDriver.SetLedOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                Cache.LedDriver.SetLedOff((int)LedChannel.FilghtLightR);
            }
            if (_antiCollisionLight)
            {
                Cache.LedDriver.SetLedOn((int)LedChannel.AntiCollisionLight);
                Thread.Sleep(200);
                Cache.LedDriver.SetLedOff((int)LedChannel.AntiCollisionLight);
            }
        }
    }
}
