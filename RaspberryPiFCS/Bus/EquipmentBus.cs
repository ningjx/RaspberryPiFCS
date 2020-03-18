using RaspberryPiFCS.Controller;
using RaspberryPiFCS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.PiGpio;

namespace RaspberryPiFCS
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

        public static Socket RemoteUart;

        public static Socket SensorUart;

        

        static EquipmentBus()
        {
            BasePca = new Pca9685();
            LEDPca = new Pca9685();
            PushBackPca = new Pca9685();
            RemoteUart = new Socket(4664);
            
            SensorUart = new Socket(4667);
        }
    }
}
