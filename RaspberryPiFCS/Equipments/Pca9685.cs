using RaspberryPiFCS.Main;
using RaspberryPiFCS.Drivers;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using Unosquare.RaspberryIO.Abstractions;

namespace RaspberryPiFCS.Equipments
{
    /// <summary>
    /// 可用于普通的Pca9685，或者奇果派那个可以驱动电机的9685Plus
    /// </summary>
    public class Pca9685 : IEquipment_IIC
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
        private Dictionary<int, double> _angleBuffer = new Dictionary<int, double>
        {
            {0,-1 },
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
            {15,-1 }
        };
        private Dictionary<int, int> _switchBuffer = new Dictionary<int, int>
        {
            {0,-1 },
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
            {15,-1 }
        };
        #endregion

        public int Addr { get; } = 0;

        public double Freq { get; } = 0;

        public EquipmentData EquipmentData { get; } = new EquipmentData("Pca9685");

        public RelyEquipment RelyEquipment { get; set; } = new RelyEquipment
        {
            RegisterType.Sys,
            RegisterType.IIC
        };
        public II2CDevice I2CDevice { get ; set ; }

        /// <summary>
        /// Pca9685默认设备地址0x40
        /// </summary>
        /// <param name="addr">设备地址</param>
        /// <param name="freq">PWM频率</param>
        public Pca9685(int addr = 0x40, double freq = 50)
        {
            Addr = addr;
            Freq = freq;
        }

        public bool Lunch()
        {
            try
            {
                //检查依赖
                if (!EquipmentBus.ControllerRegister.CheckRely(RelyEquipment))
                {
                    throw new Exception($"依赖设备尚未启动{string.Join("、", RelyEquipment)}");
                }

                I2CDevice = I2CDriver.I2CBus.AddDevice(Addr);
                I2CDevice.WriteAddressByte(MODE1, 0x00);
                SetPWMFreq(Freq);
                EquipmentData.IsEnable = true;
                EquipmentBus.ControllerRegister.Register(RegisterType.Pca9685, false);
            }
            catch (Exception ex)
            {
                EquipmentData.AddError(LogType.Error, $"启动地址为{Addr},频率为{Freq}的PCA9685失败！", ex);
                LogService.Add(LogType.Error, $"启动地址为{Addr},频率为{Freq}的PCA9685失败！", ex);
                EquipmentData.IsEnable = false;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置舵机角度
        /// </summary>
        /// <param name="channel">1-16通道</param>
        /// <param name="angle">角度</param>
        public void SetAngle(int channel, double angle)
        {
            if (channel < 0 || channel > 15)
                return;
            if (_angleBuffer[channel] == angle)
                return;

            if (angle >= 0)
            {
                var off = ConvertAngle(angle);
                Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
                Write(LED0_ON_H + 4 * channel, 0 >> 8);
                Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
                Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
                _angleBuffer[channel] = angle;
            }
        }

        public void SetOn(int channel)
        {
            if (channel < 0 || channel > 15)
                return;
            if (_switchBuffer[channel] == 1)
                return;
            //int off = 4096;
            //Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
            //Write(LED0_ON_H + 4 * channel, 0 >> 8);
            //Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
            //Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
            SetAngle(channel, 360);
            _switchBuffer[channel] = 1;
        }

        public void SetOff(int channel)
        {
            if (channel < 0 || channel > 15)
                return;
            if (_switchBuffer[channel] == 0)
                return;
            //int off = 0;
            //Write(LED0_ON_L + 4 * channel, 0 & 0xFF);
            //Write(LED0_ON_H + 4 * channel, 0 >> 8);
            //Write(LED0_OFF_L + 4 * channel, Convert.ToByte(off & 0xFF));
            //Write(LED0_OFF_H + 4 * channel, Convert.ToByte(off >> 8));
            SetAngle(channel, 0);
            _switchBuffer[channel] = 0;
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
                SetAngle(pwm, Math.Abs(speed > 256 ? 4096 : speed * 16));
                _speed = Math.Abs(speed);
            }
            switch (action)
            {
                case MotorAction.Forward:
                    SetOff(in2);
                    SetOn(in1);
                    break;
                case MotorAction.Break:
                    SetOff(in2);
                    SetOff(in1);
                    break;
                case MotorAction.Backward:
                    SetOff(in1);
                    SetOn(in2);
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
            SetOff(in2);
            SetOff(in1);
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
            I2CDevice.WriteAddressByte(reg, value);
        }

        private byte Read(int reg)
        {
            return I2CDevice.ReadAddressByte(reg);
        }

        private static int ConvertAngle(double angle)
        {
            double ms = 0.5 + (angle / 180) * (2.5 - 0.5);
            return Convert.ToInt32(4096 * ms / 20);
        }
    }
}
