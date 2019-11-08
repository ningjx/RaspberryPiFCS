using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Models
{
    public class SbusModel
    {
        private int _channelCount = 0;
        public long Channel01;
        public long Channel02;
        public long Channel03;
        public long Channel04;
        public long Channel05;
        public long Channel06;
        public long Channel07;
        public long Channel08;
        public long Channel09;
        public long Channel10;
        public long Channel11;
        public long Channel12;
        public long Channel13;
        public long Channel14;
        public long Channel15;
        public long Channel16;
        public bool IsConnected;

        public void SetSignal(long data)
        {
            _channelCount++;
            switch (_channelCount)
            {
                case 1:
                    Channel01 = data;
                    break;
                case 2:
                    Channel02 = data;
                    break;
                case 3:
                    Channel03 = data;
                    break;
                case 4:
                    Channel04 = data;
                    break;
                case 5:
                    Channel05 = data;
                    break;
                case 6:
                    Channel06 = data;
                    break;
                case 7:
                    Channel07 = data;
                    break;
                case 8:
                    Channel08 = data;
                    break;
                case 9:
                    Channel09 = data;
                    break;
                case 10:
                    Channel10 = data;
                    break;
                case 11:
                    Channel11 = data;
                    break;
                case 12:
                    Channel12 = data;
                    break;
                case 13:
                    Channel13 = data;
                    break;
                case 14:
                    Channel14 = data;
                    break;
                case 15:
                    Channel15 = data;
                    break;
                case 16:
                    Channel16 = data;
                    break;
                default:
                    break;
            }
            if (_channelCount == 16)
                _channelCount = 0;
        }
    }
}
