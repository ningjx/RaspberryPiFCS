using Iot.Device.Pwm;
using RaspberryPiFCS.Channels;
using RaspberryPiFCS.Enum;
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
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.PitchL, SignalBus.CenterSignal.PitchL);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.PitchR, SignalBus.CenterSignal.PitchR);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.RollL, SignalBus.CenterSignal.RollL);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.RollR, SignalBus.CenterSignal.RollR);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.Yaw, SignalBus.CenterSignal.Yaw);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.ThrottelL, SignalBus.CenterSignal.ThrottelL1);
            EquipmentBus.BasePca.SetAngle((int)BaseChannel.ThrottelR, SignalBus.CenterSignal.ThrottelR1);
            #endregion
        }
    }
}
