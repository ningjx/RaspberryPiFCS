using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Models;
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

        /// <summary>
        /// LED控制器
        /// </summary>
        /// <param name="ms">轮询时间</param>
        /// <param name="i2cAddr">IIC设备地址</param>
        public LEDController(int ms = 30, int i2cAddr = 0x40)
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
            switch (StateDatasBus.CenterSignal.AntiCollisionLight)
            {
                case true:
                    _antiCollisionLight = true;
                    break;
                case false:
                    _antiCollisionLight = false;
                    break;
            }
            switch (StateDatasBus.CenterSignal.FlightLight)
            {
                case true:
                    _flightLight = true;
                    break;
                case false:
                    _flightLight = false;
                    break;
            }
            switch (StateDatasBus.CenterSignal.TaxiLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)LedChannel.TaxiLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)LedChannel.TaxiLight);
                    break;
            }
            switch (StateDatasBus.CenterSignal.RunwayLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)LedChannel.RunwayLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)LedChannel.RunwayLight);
                    break;
            }
            switch (StateDatasBus.CenterSignal.TakeOffLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)LedChannel.TakeoffLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)LedChannel.TakeoffLight);
                    break;
            }
            switch (StateDatasBus.CenterSignal.LandingLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)LedChannel.LandingLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)LedChannel.LandingLight);
                    break;
            }
            switch (StateDatasBus.CenterSignal.WingInspectionLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)LedChannel.WingInspectionLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)LedChannel.WingInspectionLight);
                    break;
            }
            switch (StateDatasBus.CenterSignal.PositionLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)LedChannel.AntiCollisionLightWhite);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)LedChannel.AntiCollisionLightWhite);
                    break;
            }
            _isExcuting = false;
        }

        private void TwinkleLed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_flightLight)
            {
                EquipmentBus.LEDPca.SetOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOn((int)LedChannel.FilghtLightL);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)LedChannel.FilghtLightL);
                Thread.Sleep(300);

                EquipmentBus.LEDPca.SetOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOn((int)LedChannel.FilghtLightR);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)LedChannel.FilghtLightR);
            }
            if (_antiCollisionLight)
            {
                EquipmentBus.LEDPca.SetOn((int)LedChannel.AntiCollisionLight);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)LedChannel.AntiCollisionLight);
            }
        }
    }
}
