using flyfire.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS
{
    public static class Extend
    {
        public static string GetBitByPositon(this byte byteData, int begin, int end)
        {
            var buffer = byteData.ToString();
            return buffer.Substring(begin, end);
        }

        public static string[] GetPorts()
        {
            return CustomSerialPort.GetPortNames();
        }
    }
}
