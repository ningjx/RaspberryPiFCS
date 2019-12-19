﻿using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Fuctions
{
    public static class PushBackFunction
    {

        public static void Excute(params bool[] datas)
        {
            bool pushBackL1 = datas[0];
            bool pushBackL2 = datas[1];
            bool pushBackR1 = datas[2];
            bool pushBackR2 = datas[3];
            bool pushBack = datas[4];

            switch (pushBackL1)
            {
                case true: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackL1, MotorAction.Forward);break;
                case false: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackL1, MotorAction.Backward);break;
            }

            switch (pushBackL2)
            {
                case true: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackL2, MotorAction.Forward); break;
                case false: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackL2, MotorAction.Backward); break;
            }

            switch (pushBackR1)
            {
                case true: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackR1, MotorAction.Forward); break;
                case false: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackR1, MotorAction.Backward); break;
            }

            switch (pushBackR2)
            {
                case true: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackR2, MotorAction.Forward); break;
                case false: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBackR2, MotorAction.Backward); break;
            }

            switch (pushBack)
            {
                case true: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBack, MotorAction.Forward); break;
                case false: EquipmentBus.PushBackPca.SetMotor((int)Channels.MotorChannel.PushBack, MotorAction.Backward); break;
            }
        }
    }
}
