using flyfire.IO.Ports;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using System;
using System.Text;

namespace RaspberryPiFMS
{
    public static class Extends
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
        /// 字节转为二进制字符串,8位补全
        /// </summary>
        /// <param name="byteData"></param>
        /// <returns></returns>s
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
            return sb.ToString().PadLeft(8, '0');
        }

        public static string ByteToStr(this byte byteData)
        {
            int intTmp = byteData;
            return intTmp.ToString();
        }

        /// <summary>
        /// 获取系统的全部串口名
        /// </summary>
        /// <returns></returns>
        public static string[] GetPorts()
        {
            return CustomSerialPort.GetPortNames();
        }

        /// <summary>
        /// 获取开关状态
        /// </summary>
        /// <param name="data"></param>
        /// <param name="midValue">中间值</param>
        /// <returns></returns>
        public static Switch GetSwitch(this long data, long midValue = 1000)
        {
            if (data == midValue)
                return Switch.MId;
            else if (data > midValue)
                return Switch.On;
            else
                return Switch.Off;
        }

        /// <summary>
        /// 反转舵机角度
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="angleLimit"></param>
        /// <returns></returns>
        public static double Reverse(this double angle)
        {
            return 100 - angle;
        }

        /// <summary>
        /// 限制舵机最大角度
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="angleLimit"></param>
        /// <returns></returns>
        public static double AngleLimit(this double angle, double angleLimit)
        {
            return (angle - 50) * angleLimit / 50 + 50;
        }
    }
}
