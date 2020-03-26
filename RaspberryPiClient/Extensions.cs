using flyfire.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiClient
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
            if (bytesData.Length < 2)
                return new List<byte[]>();
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
            if (buffer.Count != 0 && buffer[buffer.Count - 1].Length != length)
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
    public class ProjectionConvertUtil
    {
        /*
        * BD-09：百度坐标系(百度地图)
        * GCJ-02：火星坐标系（谷歌中国地图、高德地图）
        * WGS84：地球坐标系（国际通用坐标系，谷歌地图）
        */

        public double x_PI = 3.14159265358979324 * 3000.0 / 180.0;
        public double PI = 3.1415926535897932384626;
        public double a = 6378245.0;
        public double ee = 0.00669342162296594323;

        //百度坐标系转火星坐标系
        public double[] bd09togcj02(double bd_lon, double bd_lat)
        {
            double x = bd_lon - 0.0065;
            double y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_PI);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_PI);

            double gcj_lon = z * Math.Cos(theta);
            double gcj_lat = z * Math.Sin(theta);
            double[] gcj = { gcj_lon, gcj_lat };//火星坐标系值

            //火星坐标系转wgs84
            double[] wgs = gcj02towgs84(gcj[0], gcj[1]);
            return wgs;
        }

        //火星坐标系转wgs84
        public double[] gcj02towgs84(double gcj_lon, double gcj_lat)
        {
            if (out_of_china(gcj_lon, gcj_lat))
            {
                //不在国内，不进行纠偏
                double[] back = { gcj_lon, gcj_lat };
                return back;
            }
            else
            {
                var dlon = transformlon(gcj_lon - 105.0, gcj_lat - 35.0);
                var dlat = transformlat(gcj_lon - 105.0, gcj_lat - 35.0);
                var radlat = gcj_lat / 180.0 * PI;
                var magic = Math.Sin(radlat);
                magic = 1 - ee * magic * magic;
                var sqrtmagic = Math.Sqrt(magic);
                dlon = (dlon * 180.0) / (a / sqrtmagic * Math.Cos(radlat) * PI);
                dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
                double mglon = gcj_lon + dlon;
                double mglat = gcj_lat + dlat;
                double wgs_lon = gcj_lon * 2 - mglon;
                double wgs_lat = gcj_lat * 2 - mglat;
                double[] wgs = { wgs_lon, wgs_lat };//wgs84坐标系值
                return wgs;
            }
        }

        //火星坐标系转百度坐标系
        public double[] gcj02tobd09(double gcj_lon, double gcj_lat)
        {
            double z = Math.Sqrt(gcj_lon * gcj_lon + gcj_lat * gcj_lat) + 0.00002 * Math.Sin(gcj_lat * x_PI);
            double theta = Math.Atan2(gcj_lat, gcj_lon) + 0.000003 * Math.Cos(gcj_lon * x_PI);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            double[] bd = { bd_lon, bd_lat };
            return bd;
        }

        //wgs84转火星坐标系
        public double[] wgs84togcj02(double wgs_lon, double wgs_lat)
        {
            //if (out_of_china(wgs_lon, wgs_lat))
            //{
            //    //不在国内
            //    double[] back = { wgs_lon, wgs_lat };
            //    return back;
            //}
            //else
            //{
                double dwgs_lon = transformlon(wgs_lon - 105.0, wgs_lat - 35.0);
                double dwgs_lat = transformlat(wgs_lon - 105.0, wgs_lat - 35.0);
                double radwgs_lat = wgs_lat / 180.0 * PI;
                double magic = Math.Sin(radwgs_lat);
                magic = 1 - ee * magic * magic;
                double sqrtmagic = Math.Sqrt(magic);
                dwgs_lon = (dwgs_lon * 180.0) / (a / sqrtmagic * Math.Cos(radwgs_lat) * PI);
                dwgs_lat = (dwgs_lat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
                double gcj_lon = wgs_lon + dwgs_lon;
                double gcj_lat = wgs_lat + dwgs_lat;
                double[] gcj = { gcj_lon, gcj_lat };
                return gcj;
            //}
        }

        public double[] wgs2gcj(double wgs_lon, double wgs_lat)
        {
        
                double dLat = transformLat(wgs_lon - 105.0D, wgs_lat - 35.0D);
                double dLon = transformLon(wgs_lon - 105.0D, wgs_lat - 35.0D);
                double radLat = wgs_lat / 180.0D * 3.141592653589793D;
                double magic = Math.Sin(radLat);
                magic = 1.0D - 0.006693421622965943D * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dLat = dLat * 180.0D / (6335552.717000426D / (magic * sqrtMagic) * 3.141592653589793D);
                dLon = dLon * 180.0D / (6378245.0D / sqrtMagic * Math.Cos(radLat) * 3.141592653589793D);
                double lat = wgs_lat + dLat;
                double lng = wgs_lon + dLon;
                double[] gcj = { lng, lat };
                return gcj;
        }

        private double transformLat(double x, double y)
        {
            double ret = -100.0D + 2.0D * x + 3.0D * y + 0.2D * y * y + 0.1D * x * y + 0.2D * Math.Sqrt(Math.Abs(x));
            ret += (20.0D * Math.Sin(6.0D * x * 3.141592653589793D) + 20.0D * Math.Sin(2.0D * x * 3.141592653589793D)) * 2.0D / 3.0D;
            ret += (20.0D * Math.Sin(y * 3.141592653589793D) + 40.0D * Math.Sin(y / 3.0D * 3.141592653589793D)) * 2.0D / 3.0D;
            ret += (160.0D * Math.Sin(y / 12.0D * 3.141592653589793D) + 320.0D * Math.Sin(y * 3.141592653589793D / 30.0D)) * 2.0D / 3.0D;
            return ret;
        }

        private double transformLon(double x, double y)
        {
            double ret = 300.0D + x + 2.0D * y + 0.1D * x * x + 0.1D * x * y + 0.1D * Math.Sqrt(Math.Abs(x));
            ret += (20.0D * Math.Sin(6.0D * x * 3.141592653589793D) + 20.0D * Math.Sin(2.0D * x * 3.141592653589793D)) * 2.0D / 3.0D;
            ret += (20.0D * Math.Sin(x * 3.141592653589793D) + 40.0D * Math.Sin(x / 3.0D * 3.141592653589793D)) * 2.0D / 3.0D;
            ret += (150.0D * Math.Sin(x / 12.0D * 3.141592653589793D) + 300.0D * Math.Sin(x / 30.0D * 3.141592653589793D)) * 2.0D / 3.0D;
            return ret;
        }

        private double transformlon(double lon, double lat)
        {
            var ret = 300.0 + lon + 2.0 * lat + 0.1 * lon * lon + 0.1 * lon * lat + 0.1 * Math.Sqrt(Math.Abs(lon));
            ret += (20.0 * Math.Sin(6.0 * lon * PI) + 20.0 * Math.Sin(2.0 * lon * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lon * PI) + 40.0 * Math.Sin(lon / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lon / 12.0 * PI) + 300.0 * Math.Sin(lon / 30.0 * PI)) * 2.0 / 3.0;
            return ret;
        }

        private double transformlat(double lon, double lat)
        {
            var ret = -100.0 + 2.0 * lon + 3.0 * lat + 0.2 * lat * lat + 0.1 * lon * lat + 0.2 * Math.Sqrt(Math.Abs(lon));
            ret += (20.0 * Math.Sin(6.0 * lon * PI) + 20.0 * Math.Sin(2.0 * lon * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * PI) + 40.0 * Math.Sin(lat / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lat / 12.0 * PI) + 320 * Math.Sin(lat * PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        //判断是否在国内，不在国内则不做偏移
        private Boolean out_of_china(double lon, double lat)
        {
            return (lon < 72.004 || lon > 137.8347) || ((lat < 0.8293 || lat > 55.8271) || false);
        }
    }

    public static class MarsWGSTransform
    {
        /// <summary>
        /// 火星坐标转换为WGS坐标
        /// </summary>
        /// <param name="xMars">火星坐标经度</param>
        /// <param name="yMars">火星坐标纬度</param>
        /// <param name="xWgs">WGS经度</param>
        /// <param name="yWgs">WGS纬度</param>
        public static void ConvertMars2WGS(double xMars, double yMars, out double xWgs, out double yWgs)
        {
            xWgs = xMars;
            yWgs = yMars;
            double xtry = xMars;
            double ytry = yMars;
            ConvertWGS2Mars(xMars, yMars, out xtry, out ytry);
            double dx = xtry - xMars;
            double dy = ytry - yMars;

            xWgs = xMars - dx;
            yWgs = yMars - dy;
            return;
        }

        /// <summary>
        /// WGS坐标转换为火星坐标
        /// </summary>
        /// <param name="xWgs">WGS经度</param>
        /// <param name="yWgs">WGS纬度</param>
        /// <param name="xMars">火星坐标经度</param>
        /// <param name="yMars">火星坐标纬度</param>
        public static void ConvertWGS2Mars(double xWgs, double yWgs, out double xMars, out double yMars)
        {
            xMars = xWgs;
            yMars = yWgs;

            const double pi = 3.14159265358979324;

            //
            // Krasovsky 1940
            //
            // a = 6378245.0, 1/f = 298.3
            // b = a * (1 - f)
            // ee = (a^2 - b^2) / a^2;
            const double a = 6378245.0;
            const double ee = 0.00669342162296594323;

            if (xWgs < 72.004 || xWgs > 137.8347)
                return;
            if (yWgs < 0.8293 || yWgs > 55.8271)
                return;

            double x = 0, y = 0;
            x = xWgs - 105.0;
            y = yWgs - 35.0;

            double dLon = 300.0 + 1.0 * x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            dLon += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            dLon += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            dLon += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;

            double dLat = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            dLat += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            dLat += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            dLat += (160.0 * Math.Sin(y / 12.0 * pi) + 320.0 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;

            double radLat = yWgs / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            xMars = xWgs + dLon;
            yMars = yWgs + dLat;
        }
    }
}
