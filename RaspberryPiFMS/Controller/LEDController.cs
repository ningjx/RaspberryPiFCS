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
        private Pca9685 pca9685 = new Pca9685();
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
            switch (Bus.CenterData.AntiCollisionLight)
            {
                case true:
                    _antiCollisionLight = true;
                    break;
                case false:
                    _antiCollisionLight = false;
                    break;
            }
            switch (Bus.CenterData.FlightLight)
            {
                case true:
                    _flightLight = true;
                    break;
                case false:
                    _flightLight = false;
                    break;
            }
            switch (Bus.CenterData.TaxiLight)
            {
                case true:
                    pca9685.SetOn((int)LedChannel.TaxiLight);
                    break;
                case false:
                    pca9685.SetOff((int)LedChannel.TaxiLight);
                    break;
            }
            switch (Bus.CenterData.RunwayLight)
            {
                case true:
                    pca9685.SetOn((int)LedChannel.RunwayLight);
                    break;
                case false:
                    pca9685.SetOff((int)LedChannel.RunwayLight);
                    break;
            }
            switch (Bus.CenterData.TakeOffLight)
            {
                case true:
                    pca9685.SetOn((int)LedChannel.TakeoffLight);
                    break;
                case false:
                    pca9685.SetOff((int)LedChannel.TakeoffLight);
                    break;
            }
            switch (Bus.CenterData.LandingLight)
            {
                case true:
                    pca9685.SetOn((int)LedChannel.LandingLight);
                    break;
                case false:
                    pca9685.SetOff((int)LedChannel.LandingLight);
                    break;
            }
            switch (Bus.CenterData.WingInspectionLight)
            {
                case true:
                    pca9685.SetOn((int)LedChannel.WingInspectionLight);
                    break;
                case false:
                    pca9685.SetOff((int)LedChannel.WingInspectionLight);
                    break;
            }
            switch (Bus.CenterData.PositionLight)
            {
                case true:
                    pca9685.SetOn((int)LedChannel.AntiCollisionLightWhite);
                    break;
                case false:
                    pca9685.SetOff((int)LedChannel.AntiCollisionLightWhite);
                    break;
            }
            _isExcuting = false;
        }

        private void TwinkleLed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_flightLight)
            {
                pca9685.SetOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                pca9685.SetOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                pca9685.SetOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                pca9685.SetOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(300);

                pca9685.SetOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                pca9685.SetOff((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                pca9685.SetOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                pca9685.SetOff((int)LedChannel.FilghtLightR);
            }
            if (_antiCollisionLight)
            {
                pca9685.SetOn((int)LedChannel.AntiCollisionLight);
                Thread.Sleep(200);
                pca9685.SetOff((int)LedChannel.AntiCollisionLight);
            }
        }
    }
}
