using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiFCS.Models;

namespace RaspberryPiFCS.Interface
{
    public partial interface IEquipment
    {
        /// <summary>
        /// 设备信息
        /// </summary>
        public EquipmentData EquipmentData { get; }

        /// <summary>
        /// 启动设备
        /// </summary>
        /// <returns></returns>
        public bool Lunch();

        public RelyConyroller RelyConyroller { get; set; }
    }
}
