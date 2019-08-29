using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace RaspberryPiFMS.Helper
{
    public class Pca9685
    {
        int SUBADR1 = 0x02;
        int SUBADR2 = 0x03;
        int SUBADR3 = 0x04;
        int MODE1 = 0x00;
        int PRESCALE = 0xFE;
        int LED0_ON_L = 0x06;
        int LED0_ON_H = 0x07;
        int LED0_OFF_L = 0x08;
        int LED0_OFF_H = 0x09;
        int ALLLED_ON_L = 0xFA;
        int ALLLED_ON_H = 0xFB;
        int ALLLED_OFF_L = 0xFC;
        int ALLLED_OFF_H = 0xFD;

        private II2CDevice device;

        public Pca9685(int addr = 0x40)
        {
            I2CBus bus = new I2CBus();
            device = bus.AddDevice(addr);
            device.WriteAddressByte(MODE1, 0x00);
        }

        private void Write(int reg, byte value)
        {
            device.WriteAddressByte(reg, value);
        }

        private byte Read(int reg)
        {
            return device.ReadAddressByte(reg);
        }

        /// <summary>
        /// 设置PWM频率
        /// </summary>
        /// <param name="freq"></param>
        public void SetPWMFreq(double freq)
        {
            double prescaleval = 25000000.0;   // 25MHz
            prescaleval /= 4096.0;       // 12-bit
            prescaleval /= (float)freq;
            prescaleval -= 1.0;

            var prescale = Math.Floor(prescaleval + 0.5);

            var oldmode = Read(MODE1);
            var newmode = (oldmode & 0x7F) | 0x10;       // sleep
            Write(MODE1, Convert.ToByte(newmode));      // go to sleep
            Write(PRESCALE, Convert.ToByte(Math.Floor(prescale)));
            Write(MODE1, oldmode);
            Thread.Sleep(5);
            Write(MODE1, Convert.ToByte(oldmode | 0x80));
        }

        /// <summary>
        /// 设置舵机角度
        /// </summary>
        /// <param name="channel">0-15通道</param>
        /// <param name="angle">角度</param>
        public void SetPWMAngle(int channel, double angle)
        {
            var off = ConvertAngle(angle);
            Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
            Write(LED0_ON_H + 4 * channel, 0 >> 8);
            Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
            Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
        }

        private int ConvertAngle(double angle)
        {
            double ms = 0.5 + (60 / 180) * (2.5 - 0.5);
            return Convert.ToInt32(4096 * ms / 20);
        }
    }
}
