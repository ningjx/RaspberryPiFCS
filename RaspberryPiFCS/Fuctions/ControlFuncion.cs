using Iot.Device.Pwm;
using RaspberryPiFCS.Channels;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Handlers;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    public class ControlFuncion : IFunction
    {
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(20);
        public bool Lock { get; set; } = false;
        public FunctionStatus FunctionStatus { get; set; } = FunctionStatus.Online;

        public ControlFuncion()
        {
            Timer.AutoReset = true;
            Timer.Elapsed += Excute;
            Timer.Start();
        }

        private void Excute(object sender, ElapsedEventArgs e)
        {
            if (Lock)
                return;
            else
                Lock = true;

            try
            {
                //根据控制信号操作
                SetControl();




            }
            catch (Exception ex)
            {
                RetryTime++;
                if (RetryTime > 10)
                {
                    FunctionStatus = FunctionStatus.Failure;
                    //打日志throw ex;
                }
            }

            Lock = false;
        }


        public void Dispose()
        {
            Timer.Dispose();
        }

        private void SetControl()
        {
            #region 最基本的四个通道
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.PitchL, DataBus.CenterSignal.PitchL);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.PitchR, DataBus.CenterSignal.PitchR);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.RollL, DataBus.CenterSignal.RollL);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.RollR, DataBus.CenterSignal.RollR);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.Yaw, DataBus.CenterSignal.Yaw);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.ThrottelL, DataBus.CenterSignal.ThrottelL1);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.ThrottelR, DataBus.CenterSignal.ThrottelR1);
            #endregion
        }
    }
}
