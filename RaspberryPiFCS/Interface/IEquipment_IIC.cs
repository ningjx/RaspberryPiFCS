using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.RaspberryIO.Abstractions;

namespace RaspberryPiFCS.Interface
{
    public interface IEquipment_IIC : IEquipment
    {
        /// <summary>
        /// 设备地址
        /// </summary>
        public int Addr { get;}

        public II2CDevice I2CDevice { get; set; }
    }
}
