using RaspberryPiFMS.Configs;
using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using System.Timers;

namespace RaspberryPiFMS.Controller
{
    /// <summary>
    /// 遥控数据解码&转换
    /// </summary>
    public class RemoteController
    {
        private readonly Timer _timer = new Timer(10);
        private bool _locker = false;

        public RemoteController()
        {
            _timer.AutoReset = true;
            _timer.Elapsed += ReciveData;
            _timer.Start();
        }

        private void ReciveData(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_locker)
                return;
            _locker = true;
            if (EquipmentBus.RemoteUart.Bytes.Length != 0)
                SbusHelper.DecodeSignal(EquipmentBus.RemoteUart.Bytes);
            //转换遥控数据为系统数据
            foreach (var channel in StateDatasBus.ControlConfig?.Channels)
            {
                switch (channel.RemoteMapping)
                {
                    case RemoteMapping.偏航:
                        StateDatasBus.RemoteSignal.Yaw = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.俯仰:
                        StateDatasBus.RemoteSignal.Pitch = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.油门:
                        StateDatasBus.RemoteSignal.Throttel = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.滚转:
                        StateDatasBus.RemoteSignal.Roll = (float)ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道5:
                        StateDatasBus.RemoteSignal.Channel05 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道6:
                        StateDatasBus.RemoteSignal.Channel06 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道7:
                        StateDatasBus.RemoteSignal.Channel07 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道8:
                        StateDatasBus.RemoteSignal.Channel08 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道9:
                        StateDatasBus.RemoteSignal.Channel09 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道10:
                        StateDatasBus.RemoteSignal.Channel10 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道11:
                        StateDatasBus.RemoteSignal.Channel11 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道12:
                        StateDatasBus.RemoteSignal.Channel12 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道13:
                        StateDatasBus.RemoteSignal.Channel13 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道14:
                        StateDatasBus.RemoteSignal.Channel14 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道15:
                        StateDatasBus.RemoteSignal.Channel15 = ConvertOriginData(channel);
                        break;
                    case RemoteMapping.通道16:
                        StateDatasBus.RemoteSignal.Channel16 = ConvertOriginData(channel);
                        break;
                }
            }
            _locker = false;
        }

        private static object ConvertOriginData(Channel channel)
        {
            int num = channel.ChannelNum;
            float data = StateDatasBus.OriginSignal[num];
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
