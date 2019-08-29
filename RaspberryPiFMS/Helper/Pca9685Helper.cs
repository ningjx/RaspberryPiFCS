using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Unosquare.RaspberryIO.Abstractions;

namespace RaspberryPiFMS.Helper
{
    public class Pca9685
    {
        /// <summary>
        /// I2C Device
        /// </summary>
        private II2CDevice _device;

        private double _pwmFrequency;

        private byte _prescale;

        /// <summary>
        /// Get default clock rate. Set if you are using external clock.
        /// </summary>
        public double ClockRate
        {
            get;
            set;
        } = 25000000.0;


        /// <summary>
        /// Set PWM frequency or get effective value.
        /// </summary>
        public double PwmFrequency
        {
            get
            {
                return _pwmFrequency;
            }
            set
            {
                Prescale = GetPrescale(value);
            }
        }

        /// <summary>
        /// Set PWM frequency using prescale value or get the value.
        /// </summary>
        public byte Prescale
        {
            get
            {
                return _prescale;
            }
            set
            {
                byte b = (byte)((value < 3) ? 3 : value);
                SetPwmFrequency(b);
                _prescale = b;
                _pwmFrequency = GetFreq(b);
            }
        }

        /// <summary>
        /// Initialize PCA9685
        /// </summary>
        /// <param name="i2cDevice">The I2C device to be used</param>
        public Pca9685(II2CDevice i2cDevice)
        {
            _device = i2cDevice;
            SetPwm(0, 0);
            byte[] span = new byte[2] { 1, 4 };

            _device.Write(span);

            span = new byte[2] { 0, 1 };

            _device.Write(span);
            Thread.Sleep(5);
            int num = _device.Read();
            num &= -17;
            span = span = new byte[2] { 0, (byte)num };

            _device.Write(span);
            Thread.Sleep(5);
        }

        /// <summary>
        /// Set a single PWM channel
        /// </summary>
        /// <param name="on">The turn-on time of specfied channel</param>
        /// <param name="off">The turn-off time of specfied channel</param>
        /// <param name="channel">target channel</param>
        public void SetPwm(int on, int off, int channel)
        {
            on &= 0xFFF;
            off &= 0xFFF;
            channel &= 0xF;

            byte[] span = new byte[2] { (byte)(6 + 4 * channel), (byte)on };

            _device.Write(span);

            span = new byte[2] { (byte)(7 + 4 * channel), (byte)(on >> 8) };

            _device.Write(span);

            span = new byte[2] { (byte)(8 + 4 * channel), (byte)off };

            _device.Write(span);

            span = new byte[2] { (byte)(9 + 4 * channel), (byte)(off >> 8) };

            _device.Write(span);
        }

        /// <summary>
        /// Set all PWM channels
        /// </summary>
        /// <param name="on">The turn-on time of all channels</param>
        /// <param name="off">The turn-on time of all channels</param>
        public void SetPwm(int on, int off)
        {
            on &= 0xFFF;
            off &= 0xFFF;


            byte[] span = new byte[2] { 250, (byte)on };

            _device.Write(span);

            span = new byte[2] { 251,
                (byte)(on >> 8) };


            _device.Write(span);

            span = new byte[2] {  252,
                (byte)off};

            _device.Write(span);

            span = new byte[2] {  253,
                (byte)(off >> 8)};

            _device.Write(span);
        }

        /// <summary>
        /// Get prescale of specified PWM frequency
        /// </summary>
        private byte GetPrescale(double freq_hz)
        {
            return (byte)Math.Round(ClockRate / 4096.0 / freq_hz - 1.0);
        }

        /// <summary>
        /// Get PWM frequency of specified prescale
        /// </summary>
        private double GetFreq(byte prescale)
        {
            return ClockRate / 4096.0 / (double)(prescale + 1);
        }

        /// <summary>
        /// Set PWM frequency by using prescale
        /// </summary>
        private void SetPwmFrequency(byte prescale)
        {
            byte b = _device.Read();
            int num = (sbyte)b | 0x10;

            byte[] span = new byte[2] { 0, (byte)num };

            _device.Write(span);
            span = new byte[2] { 254, prescale };
            ;
            _device.Write(span);
            span = new byte[2] { 0, b };

            _device.Write(span);
            Thread.Sleep(5);
            span = new byte[2] { 0, (byte)(b | 0x80) };

            _device.Write(span);
        }

        public byte GetByte()
        {
            return _device.Read();
        }

        /// <summary>
        /// 角度转换成off
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public int ConvertAngle(double angle)
        {
            double ms = 0.5 + (60 / 180) * (2.5 - 0.5);
            return Convert.ToInt32(4096 * ms / 20);
        }
    }
}
