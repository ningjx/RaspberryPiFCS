using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Interface
{
    public interface IEquipment_UART:IEquipment
    {
        /// <summary>
        /// 串口名称
        /// </summary>
        string ComName { get; }

        List<byte[]> SendBytes { set; }

        event DataHandler ReciveEvent;
    }
}
