using RaspberryPiFMS.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Models
{
    public class RemoteControlModel
    {
        private int _channelCount = 0;
        public double Channel01;
        public double Channel02;
        public double Channel03;
        public double Channel04;
        public Switch Channel05;
        public Switch Channel06;
        public Switch Channel07;
        public Switch Channel08;
        public Switch Channel09;
        public long Channel10;
        public Switch Channel11;
        public double Channel12;

        public long Channel13;
        public long Channel14;
        public long Channel15;
        public long Channel16;
        public bool IsConnected = true;

        public void SetSignal(long data)
        {
            _channelCount++;
            switch (_channelCount)
            {
                case 1:
                    Channel01 = Math.Abs(data / 20.0 - Channel01) < Cache.De_Shanking ? Math.Abs(data / 20.0) : Channel01;
                    break;
                case 2:
                    Channel02 = Math.Abs(data / 20.0 - Channel02) < Cache.De_Shanking ? Math.Abs(data / 20.0) : Channel02;
                    break;
                case 3:
                    Channel03 = Math.Abs((845 - data) / 7.0 - Channel03) < Cache.De_Shanking ? Math.Abs((845 - data) / 7.0) : Channel03;
                    break;
                case 4:
                    Channel04 = Math.Abs(data / 20.0 - Channel04) < Cache.De_Shanking ? Math.Abs(data / 20.0) : Channel04;
                    break;
                case 5:
                    Channel05 = data.GetSwitch();
                    break;
                case 6:
                    Channel06 = data.GetSwitch(500);
                    break;
                case 7:
                    Channel07 = data.GetSwitch();
                    break;
                case 8:
                    Channel08 = data.GetSwitch();
                    break;
                case 9:
                    Channel09 = data.GetSwitch();
                    break;
                case 10:
                    Channel10 = data;
                    break;
                case 11:
                    Channel11 = data.GetSwitch(500);
                    break;
                case 12:
                    Channel12 = Math.Abs((data - 306) / 14.0 - Channel12) < Cache.De_Shanking ? Math.Abs((data - 306) / 14.0) : Channel12;
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
