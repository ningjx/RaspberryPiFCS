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
            switch (Bus.CenterControlData.AntiCollisionLight)
            {
                case true:
                    _antiCollisionLight = true;
                    break;
                case false:
                    _antiCollisionLight = false;
                    break;
            }
            switch (Bus.CenterControlData.FlightLight)
            {
                case true:
                    _flightLight = true;
                    break;
                case false:
                    _flightLight = false;
                    break;
            }
            switch (Bus.CenterControlData.TaxiLight)
            {
                case true:
                    Bus.LedDriver.SetLedOn((int)LedChannel.TaxiLight);
                    break;
                case false:
                    Bus.LedDriver.SetLedOff((int)LedChannel.TaxiLight);
                    break;
            }
            switch (Bus.CenterControlData.RunwayLight)
            {
                case true:
                    Bus.LedDriver.SetLedOn((int)LedChannel.RunwayLight);
                    break;
                case false:
                    Bus.LedDriver.SetLedOff((int)LedChannel.RunwayLight);
                    break;
            }
            switch (Bus.CenterControlData.TakeOffLight)
            {
                case true:
                    Bus.LedDriver.SetLedOn((int)LedChannel.TakeoffLight);
                    break;
                case false:
                    Bus.LedDriver.SetLedOff((int)LedChannel.TakeoffLight);
                    break;
            }
            switch (Bus.CenterControlData.LandingLight)
            {
                case true:
                    Bus.LedDriver.SetLedOn((int)LedChannel.LandingLight);
                    break;
                case false:
                    Bus.LedDriver.SetLedOff((int)LedChannel.LandingLight);
                    break;
            }
            switch (Bus.CenterControlData.WingInspectionLight)
            {
                case true:
                    Bus.LedDriver.SetLedOn((int)LedChannel.WingInspectionLight);
                    break;
                case false:
                    Bus.LedDriver.SetLedOff((int)LedChannel.WingInspectionLight);
                    break;
            }
            switch (Bus.CenterControlData.PositionLight)
            {
                case true:
                    Bus.LedDriver.SetLedOn((int)LedChannel.AntiCollisionLightWhite);
                    break;
                case false:
                    Bus.LedDriver.SetLedOff((int)LedChannel.AntiCollisionLightWhite);
                    break;
            }
            _isExcuting = false;
        }

        private void TwinkleLed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_flightLight)
            {
                Bus.LedDriver.SetLedOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                Bus.LedDriver.SetLedOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                Bus.LedDriver.SetLedOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                Bus.LedDriver.SetLedOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(300);

                Bus.LedDriver.SetLedOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                Bus.LedDriver.SetLedOff((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                Bus.LedDriver.SetLedOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                Bus.LedDriver.SetLedOff((int)LedChannel.FilghtLightR);
            }
            if (_antiCollisionLight)
            {
                Bus.LedDriver.SetLedOn((int)LedChannel.AntiCollisionLight);
                Thread.Sleep(200);
                Bus.LedDriver.SetLedOff((int)LedChannel.AntiCollisionLight);
            }
        }
    }
}
