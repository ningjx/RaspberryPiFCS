using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Interface
{
    public interface IEquipment_IIC : IEquipment
    {
        /// <summary>
        /// 设备地址
        /// </summary>
        public int Addr { get;}

        /// <summary>
        /// 设备频率
        /// </summary>
        public double Freq { get;}
    }
}
