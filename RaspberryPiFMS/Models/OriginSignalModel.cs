using RaspberryPiFMS.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryPiFMS.Models
{
    public static class OriginSignal
    { 
        public static long Channel01;
        public static long Channel02;
        public static long Channel03;
        public static long Channel04;
        public static long Channel05;
        public static long Channel06;
        public static long Channel07;
        public static long Channel08;
        public static long Channel09;
        public static long Channel10;
        public static long Channel11;
        public static long Channel12;
        public static long Channel13;
        public static long Channel14;
        public static long Channel15;
        public static long Channel16;
        public static bool IsConnected = true;

        private static int _channelCount = 0;
        public static void SetSignal(long data)
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
            {
                _channelCount = 0;
                Task.Run(() => ConvertSignal());
            }
        }

        public static void SetDefaultSignal()
        {
            Channel01 = 1000;
            Channel02 = 1000;
            Channel03 = 200;
            Channel04 = 1000;
        }

        public static void SetSignalLost()
        {
            Channel01 = 1000;
            Channel02 = 1000;
            Channel03 = 200;
            Channel04 = 1000;
            IsConnected = false;
        }

        /// <summary>
        /// 转换原始遥控数据
        /// </summary>
        /// <param name="originData"></param>
        private static void ConvertSignal()
        {
            OriginConSingnal.Roll = Channel04 / 10;
            OriginConSingnal.Pitch = Channel02 / 10;
            OriginConSingnal.Yaw = Channel01 / 10;
            OriginConSingnal.Throttel = Channel03 / 10 + 50;
            OriginConSingnal.Channel05 = Channel05.GetSwitch();
            OriginConSingnal.Channel06 = Channel06.GetSwitch(500);
            OriginConSingnal.Channel07 = Channel07.GetSwitch();
            OriginConSingnal.Channel08 = Channel08.GetSwitch();
            OriginConSingnal.Channel09 = Channel09.GetSwitch();
            OriginConSingnal.Channel10 = Channel10 / 10;
            OriginConSingnal.Channel11 = Channel11.GetSwitch(500);
            OriginConSingnal.Channel12 = Channel12 / 10;
            OriginConSingnal.Channel13 = Channel13 / 10;
            OriginConSingnal.Channel14 = Channel14 / 10;
            OriginConSingnal.Channel15 = Channel15 / 10;
            OriginConSingnal.Channel16 = Channel16 / 10;  
        }
    }
}
