using flyfire.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS
{
    public static class Extend
    {
        public static string GetBitByPositon(this byte byteData, int begin, int length)
        {
            var buffer = byteData.ByteArrToHexStr();
            return buffer.Substring(begin, length);
        }

        public static string ByteArrToHexStr(this byte byteData)
        {
            StringBuilder sb = new StringBuilder(2);
            int intTmp = byteData;
            while (intTmp < 0)
            {
                intTmp = intTmp + 256;
            }
            // 小于0F的数需要在前面补0
            if (intTmp < 16)
            {
                sb.Append("0");
            }
            sb.Append(Convert.ToString(intTmp, 2));
            return sb.ToString().PadLeft(8,'0');
        }

        public static string[] GetPorts()
        {
            return CustomSerialPort.GetPortNames();
        }
    }
}
