using RaspberryPiFMS.Configs;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMS.Models
{
    public static class OriginConSingnal
    {
        public static float Yaw;
        public static float Pitch;
        public static float Throttel;
        public static float Roll;
        public static object Channel05;
        public static object Channel06;
        public static object Channel07;
        public static object Channel08;
        public static object Channel09;
        public static object Channel10;
        public static object Channel11;
        public static object Channel12;
        public static object Channel13;
        public static object Channel14;
        public static object Channel15;
        public static object Channel16;

        /// <summary>
        /// 转换原始遥控数据(调用起始端在<see cref="SbusHelper">)
        /// </summary>
        public static void ConvertSignal()
        {
            foreach (var channel in StateBus.ControlConfig.Channels)
            {
                switch (channel.RemoteMapping)
                {
                    case RemoteMapping.偏航:
                        Yaw = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.俯仰:
                        Pitch = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.油门:
                        Throttel = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.滚转:
                        Roll = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道5:
                        Channel05 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道6:
                        Channel06 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道7:
                        Channel07 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道8:
                        Channel08 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道9:
                        Channel09 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道10:
                        Channel10 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道11:
                        Channel11 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道12:
                        Channel12 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道13:
                        Channel13 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道14:
                        Channel14 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道15:
                        Channel15 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道16:
                        Channel16 = ConvertOriginData(channel);
                        break;
                }
            }
        }

        private static object ConvertOriginData(Channel channel)
        {
            int num = channel.ChannelNum;
            float data = StateBus.OriginSignal[num];
            switch (channel.ChannelType)
            {
                case ChannelType.Switch:
                    if (data > channel.MidValue)
                        return Switch.On;
                    else if (data < channel.MidValue)
                        return Switch.Off;
                    else
                        return Switch.MId;
                case ChannelType.Rocker:
                    return (100 + channel.AngleLimit) * data / (channel.MaxValue - channel.MinValue);
                default:
                    return null;
            }
        }
    }
}
