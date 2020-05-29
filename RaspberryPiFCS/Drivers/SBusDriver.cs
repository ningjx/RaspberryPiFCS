using System;
using System.Text;
using System.Timers;

namespace RaspberryPiFCS.Drivers
{
    public class SBusDriver
    {
        private readonly Timer Timer = new Timer();
        private bool _decodingLock = false;
        private bool _isRemoteConnected = true;

        /// <summary>
        /// SBus解码器
        /// </summary>
        /// <param name="sec">信号丢失延时（秒）</param>
        public SBusDriver(int sec)
        {
            Timer.AutoReset = false;
            Timer.Interval = 1000 * sec;
            Timer.Elapsed += SignalLoseDely;
        }

        private void SignalLoseDely(object sender, ElapsedEventArgs e)
        {
            SignalLost?.Invoke();
        }

        /// <summary>
        /// 解码遥控信号
        /// </summary>
        /// <param name="bytes"></param>
        public void DecodeSignal(byte[] bytes)
        {
            byte[] frameBytes = new byte[25];
            int allCount = 0;
            bool isBegin = false;
            for (int i = 0; i < 50; i++)
            {
                if (allCount == 25)
                {
                    break;
                }
                if (bytes[i] == 15)
                {
                    isBegin = true;
                }
                if (isBegin)
                {
                    frameBytes[allCount] = bytes[i];
                    allCount++;
                }
            }

            #region 判断信号是否连接
            if (frameBytes.Length != 25 || frameBytes[0] != 0x0f || frameBytes[24] != 0x00 || frameBytes[23] != 0x00)
            {
                if (_isRemoteConnected)
                {
                    Timer.Start();
                    _isRemoteConnected = false;
                }
                return;
            }
            else if (!_isRemoteConnected)
            {
                Timer.Stop();
                SignalConnected?.Invoke();
                _isRemoteConnected = true;
            }
            #endregion

            if (_decodingLock)
                return;
            else
                _decodingLock = true;

            #region decode
            long[] signals = new long[16];
            int needNext = 3;//需要下一字节的位数
            int thisRemainder = 8;//当前字节剩余
            int nextIndex = 1;//下一字节index
            for (int index = 1; index <= 22; index++)
            {
                if (thisRemainder == 0)
                {
                    thisRemainder = 8;
                    needNext = 3;
                    nextIndex = index + 1;
                    continue;
                }
                if (nextIndex != index)
                {
                    continue;
                }
                //获取当前字节
                string thisbyte = GetBitByPositon(frameBytes[index], 0, thisRemainder);
                string nextByte;
                if (needNext <= 8)
                {
                    nextByte = GetBitByPositon(frameBytes[index + 1], 8 - needNext, needNext);
                    nextIndex = index + 1;
                }
                else
                {
                    nextByte = GetBitByPositon(frameBytes[index + 1], 0, 7);
                    string nextnextByte = GetBitByPositon(frameBytes[index + 2], 8 - (needNext - 8), needNext - 8);
                    nextByte = nextnextByte + nextByte;
                    nextIndex = index + 2;
                }
                string thisChannel = nextByte + thisbyte;
                signals[index - 1] = Convert.ToInt64(thisChannel, 2);

                thisRemainder = needNext <= 8 ? 8 - needNext : 8 - (needNext - 8);
                needNext = 11 - thisRemainder;
            }
            SetSignal?.Invoke(signals);
            #endregion

            _decodingLock = false;
        }

        public delegate void SetSignalData(long[] signals);
        public event SetSignalData SetSignal;

        public delegate void SignalHandler();
        public event SignalHandler SignalLost;
        public event SignalHandler SignalConnected;

        private static string GetBitByPositon(byte byteData, int begin, int length)
        {
            return Convert.ToString(byteData, 2).PadLeft(8, '0').Substring(begin, length);
        }
    }
}
