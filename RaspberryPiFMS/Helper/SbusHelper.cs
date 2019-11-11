using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Helper;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.Helper
{
    public class SbusHelper : IDisposable
    {
        /// <summary>
        /// 丢失信号后启动定时器，到点儿了就执行丢失信号动作
        /// </summary>
        private Timer _timer = new Timer(Cache.LosingSignalDelay * 1000);
        public SbusHelper()
        {
            _timer.Elapsed += SetSignalLose;
            _timer.AutoReset = false;
        }
        /// <summary>
        /// 解码遥控信号
        /// </summary>
        /// <param name="bytesDatas"></param>
        public void DecodeSignal(byte[] bytesDatas)
        {
            byte[] bytes = new byte[25];
            int allCount = 0;
            bool isBegin = false;
            for (int i = 0; i < 50; i++)
            {
                if (allCount == 25)
                {
                    break;
                }
                if (bytesDatas[i] == 15)
                {
                    isBegin = true;
                }
                if (isBegin)
                {
                    bytes[allCount] = bytesDatas[i];
                    allCount++;
                }
            }

            if (bytes.Length != 25 || bytes[0] != 0x0f || bytes[24] != 0x00 || bytes[23] != 0x00)
            {

                if (Cache.IsRemoteConnected)
                {
                    Cache.IsRemoteConnected = false;
                    _timer.Start();
                }
                return;
            }
            else
            {
                if (!Cache.IsRemoteConnected)
                {
                    Cache.IsRemoteConnected = true;
                    _timer.Stop();
                    SetSignalConnected();
                }
            }
            if (Cache.DecodingLock)
                return;
            else
                Cache.DecodingLock = true;
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
                string thisbyte = bytes[index].GetBitByPositon(0, thisRemainder);
                string nextByte;
                if (needNext <= 8)
                {
                    nextByte = bytes[index + 1].GetBitByPositon(8 - needNext, needNext);
                    nextIndex = index + 1;
                }
                else
                {
                    nextByte = bytes[index + 1].GetBitByPositon(0, 7);
                    string nextnextByte = bytes[index + 2].GetBitByPositon(8 - (needNext - 8), needNext - 8);
                    nextByte = nextnextByte + nextByte;
                    nextIndex = index + 2;
                }
                string thisChannel = nextByte + thisbyte;
                Cache.RemoteSignal.SetSignal(Convert.ToInt64(thisChannel, 2));

                thisRemainder = needNext <= 8 ? 8 - needNext : 8 - (needNext - 8);
                needNext = 11 - thisRemainder;
            }
            Cache.DecodingLock = false;
        }

        private void SetSignalLose(object sender, System.Timers.ElapsedEventArgs e)
        {
            Cache.RemoteSignal.IsConnected = false;
            Cache.RemoteSignal.Channel01 = Cache.RemoteSignal.Channel02 = Cache.RemoteSignal.Channel04 = 50;
        }

        /// <summary>
        /// 信号连接动作
        /// </summary>
        private static void SetSignalConnected()
        {
            Cache.RemoteSignal.IsConnected = true;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
