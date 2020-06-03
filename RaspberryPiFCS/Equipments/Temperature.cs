using RaspberryPiFCS.BaseController;
using RaspberryPiFCS.Drivers;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.RaspberryIO.Abstractions;

namespace RaspberryPiFCS.Equipments
{
    public class TempController : IEquipment_IIC
    {
        /*
        0x00  Temperature Register      
        0x01  Configuration register  器件模式 写00普通模式(100ms更新一次温度值) 写01为ShutDown模式
        0x02  Hysteresis register
        0x03  Over_temperature shutdown register
        Temp Register 
        MSByte                LSByte
        7   6  5  4  3  2  1  0  7  6  5  4 3 2 1 0
        D10 D9 D8 D7 D6 D5 D4 D3 D2 D1 D0 X X X X X
        D10=0    ℃=+(Temp Data×0.125) 	
        D10=1    ℃=-(Temp Data×0.125)
        Address Table
        MSB          LSB
        1 0 0 1 A2 A1 A0
        1 0 0 1 0  0  1 0/1       =0x92
        */
        public int Addr ;

        public double Freq;

        public EquipmentData EquipmentData => throw new NotImplementedException();

        public RelyConyroller RelyConyroller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        int IEquipment_IIC.Addr => throw new NotImplementedException();

        public II2CDevice I2CDevice { get; set; }

        public TempController(int addr)
        {
            Addr = addr;
            I2CDevice = I2CDriver.I2CBus.AddDevice(addr);
        }

        public float GetTemp()
        {
            float tempture;
            int temp;
            temp = I2CDevice.ReadAddressByte(0x00);
            tempture = temp >> 5;
            return tempture * 0.125F;
        }

        public bool Lunch()
        {
            return true;
        }
    }
}
