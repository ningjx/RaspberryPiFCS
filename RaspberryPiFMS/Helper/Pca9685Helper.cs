using RaspberryPiFMS.Enum;
using System;
using System.Collections.Generic;
using System.Threading;
using Unosquare.RaspberryIO.Abstractions;

namespace RaspberryPiFMS.Helper
{
    /// <summary>
    /// 可用于普通的Pca9685，或者奇果派那个可以驱动电机的9685Plus
    /// </summary>
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
        private int _speed = 0;
        private II2CDevice _device;

        private Dictionary<int, double> _angleBuffer = new Dictionary<int, double>
        {
            {1,-1 },
            {2,-1 },
            {3,-1 },
            {4,-1 },
            {5,-1 },
            {6,-1 },
            {7,-1 },
            {8,-1 },
            {9,-1 },
            {10,-1 },
            {11,-1 },
            {12,-1 },
            {13,-1 },
            {14,-1 },
            {15,-1 },
            {16,-1 }
        };
        private Dictionary<int, int> _ledBuffer = new Dictionary<int, int>
        {
            {1,-1 },
            {2,-1 },
            {3,-1 },
            {4,-1 },
            {5,-1 },
            {6,-1 },
            {7,-1 },
            {8,-1 },
            {9,-1 },
            {10,-1 },
            {11,-1 },
            {12,-1 },
            {13,-1 },
            {14,-1 },
            {15,-1 },
            {16,-1 }
        };
        #endregion

        /// <summary>
        /// Pca9685默认总线地址0x40，焊上A0则0x41，以此类推
        /// </summary>
        /// <param name="addr">IIC总线地址</param>
        /// <param name="freq">PWM频率</param>
        public Pca9685(int addr = 0x40, double freq = 50)
        {
            //I2CBus bus = new I2CBus();
            _device = Bus.I2CBus.AddDevice(addr);
            _device.WriteAddressByte(MODE1, 0x00);
            SetPWMFreq(freq);
        }

        /// <summary>
        /// 设置舵机角度
        /// </summary>
        /// <param name="channel">1-16通道</param>
        /// <param name="angle">角度</param>
        public void SetPWMAngle(int channel, double angle)
        {
            if (channel < 1 || channel > 16)
                return;
            if (_angleBuffer[channel] == angle)
                return;

            if (angle >= 0)
            {
                channel = channel- 1;
                var off = ConvertAngle(angle);
                Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
                Write(LED0_ON_H + 4 * channel, 0 >> 8);
                Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
                Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
                _angleBuffer[channel +1] = angle;
            }
        }

        public void SetLedOn(int channel)
        {
            if (channel < 1 || channel > 16)
                return;
            if (_ledBuffer[channel] == 1)
                return;
            int off = 4096;
            channel =channel- 1;
            Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
            Write(LED0_ON_H + 4 * channel, 0 >> 8);
            Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
            Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
            _ledBuffer[channel+1] = 1;
            LedOnEvent?.Invoke(channel+1);
        }

        public void SetLedOff(int channel)
        {
            if (channel < 1 || channel > 16)
                return;
            if (_ledBuffer[channel] == 0)
                return;
            int off = 0;
            channel -= 1;
            Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
            Write(LED0_ON_H + 4 * channel, 0 >> 8);
            Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
            Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
            _ledBuffer[channel+1] = 0;
            LedOffEvent?.Invoke(channel+1);
        }

        /// <summary>
        /// 设置电机动作/适配奇果派的直流电机驱动，步进电机用不到，暂不支持
        /// </summary>
        /// <param name="channel">1-4</param>
        /// <param name="action">前进后退，刹车就是不通电，不能刹车，英文注释看不懂了不知道咋刹</param>
        /// <param name="speed">我也不知道这速度多快，100随便写的</param>
        public void SetMotor(int channel, MotorAction action, int speed = 100)
        {
            if (channel < 1 || channel > 4)
                return;
            int pwm = 0;
            int in1 = 0;
            int in2 = 0;
            switch (channel)
            {
                case 1:
                    pwm = 8; in2 = 9; in1 = 10;
                    break;
                case 2:
                    pwm = 13; in2 = 12; in1 = 11;
                    break;
                case 3:
                    pwm = 2; in2 = 3; in1 = 4;
                    break;
                case 4:
                    pwm = 7; in2 = 6; in1 = 5;
                    break;
            }
            if (speed != _speed)
            {
                SetPWMAngle(pwm, Math.Abs(speed > 256 ? 4096 : speed * 16));
                _speed = Math.Abs(speed);
            }
            switch (action)
            {
                case MotorAction.Forward:
                    SetLedOff(in2);
                    SetLedOn(in1);
                    break;
                case MotorAction.Break:
                    SetLedOff(in2);
                    SetLedOff(in1);
                    break;
                case MotorAction.Backward:
                    SetLedOff(in1);
                    SetLedOn(in2);
                    break;
            }
            //在这添加限位器/初始化一个限位器并绑定事件吧
            //初始化一个限位器并订阅限位器的事件
            switch (action)
            {
                case MotorAction.Forward:

                    break;
                case MotorAction.Backward:

                    break;
            }
        }

        private void SetMotorStop(int in1, int in2)
        {
            SetLedOff(in2);
            SetLedOff(in1);
        }

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

        /// <summary>
        /// 这个Led被点亮的事件
        /// </summary>
        public event LedEventHandle LedOnEvent;

        /// <summary>
        /// 这个Led被关掉的事件
        /// </summary>
        public event LedEventHandle LedOffEvent;
    }
}
