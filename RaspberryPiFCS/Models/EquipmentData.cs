using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;

namespace RaspberryPiFCS.Models
{
    public class EquipmentData
    {
        /// <summary>
        /// 设备是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 设备异常信息
        /// </summary>
        public List<Tuple<DateTime, ErrorType, string, Exception>> ErrorData { get; set; } = new List<Tuple<DateTime, ErrorType, string, Exception>>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isEnable"></param>
        public EquipmentData(bool isEnable)
        {
            IsEnable = isEnable;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isEnable"></param>
        public EquipmentData()
        {
            IsEnable = false;
        }
    }
}
