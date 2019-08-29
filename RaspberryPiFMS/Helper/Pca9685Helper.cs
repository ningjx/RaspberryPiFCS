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
        #region 变量
        const int SUBADR1 = 0x02;
        const int SUBADR2 = 0x03;
        const int SUBADR3 = 0x04;
        const int MODE1 = 0x00;
        const int PRESCALE = 0xFE;
        const int LED0_ON_L = 0x06;
        const int LED0_ON_H = 0x07;
        const int LED0_OFF_L = 0x08;
        const int LED0_OFF_H = 0x09;
        const int ALLLED_ON_L = 0xFA;
        const int ALLLED_ON_H = 0xFB;
        const int ALLLED_OFF_L = 0xFC;
        const int ALLLED_OFF_H = 0xFD;

        private II2CDevice device;
        #endregion

        public Pca9685(int addr = 0x40, double freq = 50)
        {
            I2CBus bus = new I2CBus();
            device = bus.AddDevice(addr);
            device.WriteAddressByte(MODE1, 0x00);
            SetPWMFreq(freq);
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

        /// <summary>
        /// 设置PWM频率
        /// </summary>
        /// <param name="freq"></param>
        private void SetPWMFreq(double freq)
        {
            double prescaleval = 25000000.0;   // 25MHz
            prescaleval /= 4096.0;       // 12-bit
            prescaleval /= (float)freq;
            prescaleval -= 1.0;

            var prescale = Math.Floor(prescaleval + 0.5);

            var oldmode = Read(MODE1);
            var newmode = (oldmode & 0x7F) | 0x10;
            Write(MODE1, Convert.ToByte(newmode));
            Write(PRESCALE, Convert.ToByte(Math.Floor(prescale)));
            Write(MODE1, oldmode);
            Thread.Sleep(5);
            Write(MODE1, Convert.ToByte(oldmode | 0x80));
        }

        private void Write(int reg, byte value)
        {
            device.WriteAddressByte(reg, value);
        }

        private byte Read(int reg)
        {
            return device.ReadAddressByte(reg);
        }

        private int ConvertAngle(double angle)
        {
            double ms = 0.5 + (60 / 180) * (2.5 - 0.5);
            return Convert.ToInt32(4096 * ms / 20);
        }
    }
}
