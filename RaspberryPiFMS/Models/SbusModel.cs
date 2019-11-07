using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Models
{
    public class SbusModel
    {
        public int Channel01;
        public int Channel02;
        public int Channel03;
        public int Channel04;
        public int Channel05;
        public int Channel06;
        public int Channel07;
        public int Channel08;
        public int Channel09;
        public int Channel10;
        public int Channel11;
        public int Channel12;
        public int Channel13;
        public int Channel14;
        public int Channel15;
        public int Channel16;
        public bool IsConnected;

        public void SetSignal(int channel,int data)
        {
            switch (channel)
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
        }
    }
}
