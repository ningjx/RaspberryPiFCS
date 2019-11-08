using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMS.Helper
{
    public class SbusHelper
    {
        private TimerHelper _timer = new TimerHelper(Config.LosingSignalDelay);
        public void DecodeSignal(byte[] bytes)
        {
            if (bytes.Length != 25 || bytes[0] != 0x0f || bytes[24] != 0x00 || bytes[23] == 0x00)
            {

                _timer.TimeStopEvent += SetSignalLose;
                //_timer.TimeStopEvent += SetSignalConnected;
                _timer.StartTimming();
                return;
            }
            else
            {
                _timer.StopTimming();
                SetSignalConnected();
            }
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
                string thisbyte = bytes[index].GetBitByPositon(0, thisRemainder - 1);
                string nextByte = string.Empty;
                if (needNext <= 8)
                {
                    nextByte = bytes[index + 1].GetBitByPositon(7 - needNext, 7);
                    nextIndex = index + 1;
                }
                else
                {
                    nextByte = bytes[index + 1].GetBitByPositon(0, 7);
                    string nextnextByte = bytes[index + 2].GetBitByPositon(7 - (needNext - 8), 7);
                    nextByte = nextnextByte + nextByte;
                    nextIndex = index + 2;
                }
                string thisChannel = nextByte + thisbyte;
                Config.RemoteSignal.SetSignal(Convert.ToInt32(thisChannel));

                thisRemainder = needNext <= 8 ? 8 - needNext :8 - (needNext - 8);
                needNext = 11 - thisRemainder;
            }
        }
        private static void SetSignalLose()
        {
            Config.RemoteSignal.IsConnected = false;
        }
        private static void SetSignalConnected()
        {
            Config.RemoteSignal.IsConnected = true;
        }
    }
}
