﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Unosquare.RaspberryIO.Abstractions;

namespace RaspberryPiFMS.Controller
{
    public class TempController
    {
        /*
        0x00  Temperature Register      
        0x01  Configuration register  器件模式 写00普通模式(100ms更新一次温度值) 写01为ShutDown模式
        0x02  Hysteresis register
        0x03  Over_temperature shutdown register
        Temp Register 
        MSByte                LSByte
        7   6  5  4  3  2  1  0  7  6  5  4 3 2 1 0
        D10 D9 D8 D7 D6 D5 D4 D3 D2 D1 D0 X X X X X
        D10=0    ℃=+(Temp Data×0.125) 	
        D10=1    ℃=-(Temp Data×0.125)
        Address Table
        MSB          LSB
        1 0 0 1 A2 A1 A0
        1 0 0 1 0  0  1 0/1       =0x92
        */
        private II2CDevice _device;
        private Timer _timer = new Timer(200);

        public TempController(int addr = 0x90)
        {
            _device = Bus.I2CBus.AddDevice(addr);
            _device.WriteAddressByte(0x01, 0x00);
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var tem = GetTemp();
        }

        public double GetTemp()
        {
            float tempture;
            int temp;
            temp = _device.ReadAddressByte(0x00);
            tempture = temp >> 5;
            return tempture * 0.125;
        }
    }
}
