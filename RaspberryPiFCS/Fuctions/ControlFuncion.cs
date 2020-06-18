using Iot.Device.Pwm;
using RaspberryPiFCS.Channels;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Main;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    public class ControlFuncion : IFunction
    {
        public bool Lock { get; set; } = false;
        public FunctionStatus FunctionStatus { get; set; } = FunctionStatus.Offline;
        public RelyEquipment RelyEquipment { get; set; } = new RelyEquipment
        {
            RegisterType.Sys,
            RegisterType.RemoteController
        };
        public ControlFuncion()
        {
            if (!EquipmentBus.ControllerRegister.CheckRely(RelyEquipment))
            {
                Logger.Add(LogType.Error, "无法启动控制功能，依赖设备不在线");
                FunctionStatus = FunctionStatus.Failure;
            }
        }

        public void Excute(object sender, ElapsedEventArgs e)
        {
            if (Lock)
                return;
            else
                Lock = true;
            try
            {
                //根据控制信号操作
                SetControl();
                FunctionStatus = FunctionStatus.Online;
            }
            catch (Exception ex)
            {
                FunctionStatus = FunctionStatus.Failure;
                Logger.Add(LogType.Error, "控制功能异常", ex);
            }
            Lock = false;
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
