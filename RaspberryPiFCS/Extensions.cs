using flyfire.IO.Ports;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS
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
        /// 在字节流中，从指定的字节值后面提取数据，默认长度10000
        /// </summary>
        /// <param name="bytesData"></param>
        /// <param name="aByte"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromByte(this byte[] bytesData, byte aByte, int length = 0)
        {
            bool isMatch = false;
            if (length == 0)
                length = bytesData.Length;
            int currLength = 0;
            byte[] bytes = new byte[length];

            for (int i = 0; i < bytesData.Length; i++)
            {
                if (currLength == length)
                    break;
                if (bytesData[i] == aByte)
                {
                    //bytes[0] = byteData[i];
                    //currLength ++;
                    isMatch = true;
                }
                if (isMatch)
                {
                    bytes[currLength] = bytesData[i];
                    currLength++;
                }
            }
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesData"></param>
        /// <param name="bytes">2个字节哦</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<byte[]> GetBytesFromByte(this byte[] bytesData, byte[] bytes, int length = 0)
        {
            bool isMatch = false;
            if (length == 0)
                length = bytesData.Length;
            int currLength = 1;
            List<byte[]> buffer = new List<byte[]>();
            int row = 0;
            for (int i = 0; i < bytesData.Length; i++)
            {
                if (isMatch == true && bytesData[i] == bytes[1] && bytesData[i - 1] == bytes[0])
                    break;
                if (currLength == length)
                {
                    row++;
                    buffer.Add(new byte[length]);
                    currLength = 0;
                }
                if (i > 0 && bytesData[i] == bytes[1] && bytesData[i - 1] == bytes[0])
                {
                    buffer.Add(new byte[length]);
                    buffer[row][0] = bytesData[i - 1];
                    isMatch = true;
                }
                if (isMatch)
                {
                    buffer[row][currLength] = bytesData[i];
                    currLength++;
                }
            }
            if (buffer.Count !=0 && buffer[buffer.Count - 1].Length != length)
                buffer.RemoveAt(buffer.Count - 1);
            return buffer;
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



        public static void AddOrUpdate<K, V>(this Dictionary<K, V> dic, K key, V value)
        {
            if (dic.TryGetValue(key, out V data))
            {
                dic[key] = value;
            }
            else
                dic.Add(key, value);
        }

        public static void ReadToEnd(this byte[] bytes, Action<int, byte> func)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                func.Invoke(i, bytes[i]);
            }
        }

        public static byte[] GetValue(this byte[] bytes, int start, int end)
        {
            int length = end - start + 1;
            byte[] newBytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                newBytes[i] = bytes[start + i];
            }
            return newBytes;
        }

        public static byte[] GetValueWithLength(this byte[] bytes, int length)
        {
            byte[] newBytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                newBytes[i] = bytes[i];
            }
            return newBytes;
        }

    }
}
