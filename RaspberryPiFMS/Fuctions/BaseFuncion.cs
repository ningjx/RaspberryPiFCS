using RaspberryPiFMS.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Fuctions
{
    public static class BaseFuncion// : IFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excute"></param>
        /// <param name="datas">yaw,pitchL,pitchR,throttelL,throttelR,rollL,rollR,trim</param>
        public static void Excute(params float[] datas)
        {
            float yaw = datas[0];
            float pitchL = datas[1];
            float pitchR = datas[2];
            float throttelL = datas[3];
            float throttelR = datas[4];
            float rollL = datas[5];
            float rollR = datas[6];
            float trim = datas[7];
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.Yaw, yaw);
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.PitchL, pitchL);
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.PitchR, pitchR);
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.ThrottelL, throttelL);
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.ThrottelR, throttelR);
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.RollL, rollL);
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.RollR, rollR);
            EquipmentBus.BasePca.SetAngle((int)Channels.BaseChannel.Trim, trim);
        }
    }
}
