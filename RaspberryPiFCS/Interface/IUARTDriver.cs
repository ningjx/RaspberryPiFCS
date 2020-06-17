using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Interface
{
    public interface IUARTDriver
    {
        public void WriteBytes(byte[] bytes);
        public event UARTRecHandler RecEvent;
    }
}
