using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.PiGpio;

namespace RaspberryPiFMS
{
    /// <summary>
    /// 设备总线，该总线集成所有连接到飞控的设备，包括iic等
    /// </summary>
    public static class EquipmentBus
    {
        /// <summary>
        /// IIC总线
        /// </summary>
        public static I2CBus I2CBus = new I2CBus();

        /// <summary>
        /// 基本舵机控制设备
        /// </summary>
        public static Pca9685 BasePca;
        /// <summary>
        /// LED控制设备
        /// </summary>
        public static Pca9685 LEDPca;
        /// <summary>
        /// 反推控制设备
        /// </summary>
        public static Pca9685 PushBackPca;

        public static UART RemoteUart;

        public static UART SensorUart;

        public static UART LoraUart;

        static EquipmentBus()
        {
            BasePca = new Pca9685();
            LEDPca = new Pca9685();
            PushBackPca = new Pca9685();
            RemoteUart = new UART(4664);
            LoraUart = new UART(4665);
            SensorUart = new UART(4667);
        }
    }
}
