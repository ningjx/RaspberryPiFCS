using flyfire.IO.Ports;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS
{
    /// <summary>
    /// 扩展方法
    /// </summary>
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

        public static string[] GetByCount(this string[] data, int count)
        {
            string[] result = new string[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = data[i];
            }
            return result;
        }

        /// <summary>
        /// 转换原始遥控数据
        /// </summary>
        /// <param name="originData"></param>
        public static void ConvertSignal()
        {
            OriginConSingnal.Roll = OriginSignal.Channel04 / 10;
            OriginConSingnal.Pitch = OriginSignal.Channel02 / 10;
            OriginConSingnal.Yaw = OriginSignal.Channel01 / 10;
            OriginConSingnal.Throttel = OriginSignal.Channel03 / 10;
            OriginConSingnal.Channel05 = OriginSignal.Channel05.GetSwitch();
            OriginConSingnal.Channel06 = OriginSignal.Channel06.GetSwitch(500);
            OriginConSingnal.Channel07 = OriginSignal.Channel07.GetSwitch();
            OriginConSingnal.Channel08 = OriginSignal.Channel08.GetSwitch();
            OriginConSingnal.Channel09 = OriginSignal.Channel09.GetSwitch();
            OriginConSingnal.Channel10 = OriginSignal.Channel10 / 10;
            OriginConSingnal.Channel11 = OriginSignal.Channel11.GetSwitch(500);
            OriginConSingnal.Channel12 = OriginSignal.Channel12 / 10;
            //OriginConSingnal.Channel13 = OriginSignal.Channel13 / 10;
            //OriginConSingnal.Channel14 = OriginSignal.Channel14 / 10;
            //OriginConSingnal.Channel15 = OriginSignal.Channel15 / 10;
            //OriginConSingnal.Channel16 = OriginSignal.Channel16 / 10;  
        }

        public static void AddOrUpdate<K,V>(this Dictionary<K, V> dic,K key,V value)
        {
            if (dic.TryGetValue(key, out V data))
            {
                dic[key] = value;
            }
            else
                dic.Add(key, value);
        }
    }
}
