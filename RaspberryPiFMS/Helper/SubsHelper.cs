using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMS.Helper
{
    public class SubsHelper
    {
        private TimerHelper _timer = new TimerHelper(Config.LosingSignalDelay);
        public void DecodeSignal(byte[] bytes)
        {
            if(bytes.Length != 25 || bytes[0] != 0x0f || bytes[24] != 0x00 || bytes[23] == 0x00)
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
            int needNext = 3;
            int thisRemainder = 8;
            for (int index = 1; index < 17; index++)
            {
                if (thisRemainder == 0)
                {
                    thisRemainder = 8;
                    needNext = 3;
                    continue;
                }
                //获取当前字节
                string thisbyte = bytes[index].GetBitByPositon(0, thisRemainder - 1);
                //获取下一字节
                string nextByte = bytes[index + 1].GetBitByPositon(7 - needNext, 7);
                
                string thisChannel = nextByte + thisbyte;
                Config.RemoteSignal.SetSignal(index, Convert.ToInt32(thisChannel));

                thisRemainder = 8 - needNext;
                needNext = 11 - thisRemainder;
            }

            Config.RemoteSignal.Channel01 = 0;
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
