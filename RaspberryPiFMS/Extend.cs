using flyfire.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS
{
    public static class Extend
    {
        /// <summary>
        /// 提取指定长度字节为二进制字符串
        /// </summary>
        /// <param name="byteData"></param>
        /// <param name="begin">起始位置</param>
        /// <param name="length">截取的长度</param>
        /// <returns></returns>
        public static string GetBitByPositon(this byte byteData, int begin, int length)
        {
            var buffer = byteData.ByteArrToHexStr();
            return buffer.Substring(begin, length);
        }

        /// <summary>
        /// 字节转为二进制字符串
        /// </summary>
        /// <param name="byteData"></param>
        /// <returns></returns>
        public static string ByteArrToHexStr(this byte byteData)
        {
            StringBuilder sb = new StringBuilder(2);
            int intTmp = byteData;
            while (intTmp < 0)
            {
                intTmp = intTmp + 256;
            }
            if (intTmp < 16)
            {
                sb.Append("0");
            }
            sb.Append(Convert.ToString(intTmp, 2));
            return sb.ToString().PadLeft(8,'0');
        }

        /// <summary>
        /// 获取系统的全部串口名
        /// </summary>
        /// <returns></returns>
        public static string[] GetPorts()
        {
            return CustomSerialPort.GetPortNames();
        }
    }
}
