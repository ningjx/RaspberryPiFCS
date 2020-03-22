﻿using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Helper;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using Timer = System.Timers.Timer;
using RaspberryPiFCS.Interface;

namespace RaspberryPiFCS.Controller
{
    public class LEDController : IController
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

        public bool Lunch()
        {
            throw new NotImplementedException();
        }

        private void Excute(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isExcuting)
                return;
            _isExcuting = true;
            switch (StatusDatasBus.CenterSignal.AntiCollisionLight)
            {
                case true:
                    _antiCollisionLight = true;
                    break;
                case false:
                    _antiCollisionLight = false;
                    break;
            }
            switch (StatusDatasBus.CenterSignal.FlightLight)
            {
                case true:
                    _flightLight = true;
                    break;
                case false:
                    _flightLight = false;
                    break;
            }
            switch (StatusDatasBus.CenterSignal.TaxiLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.TaxiLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.TaxiLight);
                    break;
            }
            switch (StatusDatasBus.CenterSignal.RunwayLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.RunwayLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.RunwayLight);
                    break;
            }
            switch (StatusDatasBus.CenterSignal.TakeOffLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.TakeoffLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.TakeoffLight);
                    break;
            }
            switch (StatusDatasBus.CenterSignal.LandingLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.LandingLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.LandingLight);
                    break;
            }
            switch (StatusDatasBus.CenterSignal.WingInspectionLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.WingInspectionLight);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.WingInspectionLight);
                    break;
            }
            switch (StatusDatasBus.CenterSignal.PositionLight)
            {
                case true:
                    EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.AntiCollisionLightWhite);
                    break;
                case false:
                    EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.AntiCollisionLightWhite);
                    break;
            }
            _isExcuting = false;
        }

        private void TwinkleLed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_flightLight)
            {
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(300);

                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightR);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightR);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightR);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightR);
            }
            if (_antiCollisionLight)
            {
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.AntiCollisionLight);
                Thread.Sleep(200);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.AntiCollisionLight);
            }
        }
    }
}