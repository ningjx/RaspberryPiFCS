using System;
using System.Threading;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

namespace RaspberryPiFMS.Helper
{
    public class Pca9685
    {
        #region 变量
        private const int SUBADR1 = 0x02;
        private const int SUBADR2 = 0x03;
        private const int SUBADR3 = 0x04;
        private const int MODE1 = 0x00;
        private const int PRESCALE = 0xFE;
        private const int LED0_ON_L = 0x06;
        private const int LED0_ON_H = 0x07;
        private const int LED0_OFF_L = 0x08;
        private const int LED0_OFF_H = 0x09;
        private const int ALLLED_ON_L = 0xFA;
        private const int ALLLED_ON_H = 0xFB;
        private const int ALLLED_OFF_L = 0xFC;
        private const int ALLLED_OFF_H = 0xFD;

        private II2CDevice _device;
        #endregion

        /// <summary>
        /// Pca9685默认总线地址0x40，焊上A0则0x41，以此类推
        /// </summary>
        /// <param name="addr">IIC总线地址</param>
        /// <param name="freq">PWM频率</param>
        public Pca9685(int addr = 0x40, double freq = 50)
        {
            I2CBus bus = new I2CBus();
            _device = bus.AddDevice(addr);
            _device.WriteAddressByte(MODE1, 0x00);
            SetPWMFreq(freq);
        }

        /// <summary>
        /// 设置舵机角度
        /// </summary>
        /// <param name="channel">0-15通道</param>
        /// <param name="angle">角度</param>
        public void SetPWMAngle(int channel, double angle)
        {
            if (angle >= 0)
            {
                var off = ConvertAngle(angle);
                Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
                Write(LED0_ON_H + 4 * channel, 0 >> 8);
                Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
                Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
            }
        }

        public void SetLedOn(int channel)
        {
            int off = 4096;
            Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
            Write(LED0_ON_H + 4 * channel, 0 >> 8);
            Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
            Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
            LedOnEvent?.Invoke(channel);
        }

        public void SetLedOff(int channel)
        {
            int off = 0;
            Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
            Write(LED0_ON_H + 4 * channel, 0 >> 8);
            Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
            Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
            LedOffEvent?.Invoke(channel);
        }

        /// <summary>
        /// 这个Led被点亮的事件
        /// </summary>
        public event LedEventHandle LedOnEvent;

        /// <summary>
        /// 这个Led被关掉的事件
        /// </summary>
        public event LedEventHandle LedOffEvent;

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
            _device.WriteAddressByte(reg, value);
        }

        private byte Read(int reg)
        {
            return _device.ReadAddressByte(reg);
        }

        private static int ConvertAngle(double angle)
        {
            double ms = 0.5 + (angle / 180) * (2.5 - 0.5);
            return Convert.ToInt32(4096 * ms / 20);
        }

        public delegate void LedEventHandle(int channel);
    }
}
