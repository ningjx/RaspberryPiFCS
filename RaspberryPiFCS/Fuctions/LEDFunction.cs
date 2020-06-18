using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RaspberryPiFCS.Fuctions
{
    /// <summary>
    /// LED功能
    /// </summary>
    public class LEDFunction : IFunction
    {
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(500);
        public bool Lock { get; set; } = false;
        public FunctionStatus FunctionStatus { get; set; } = FunctionStatus.Online;
        public RelyEquipment RelyEquipment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event WatcherHandler CallWatcher;

        public LEDFunction()
        {
            Timer.AutoReset = true;
            Timer.Elapsed += Excute;
            Timer.Start();
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
                switch (SignalBus.CenterSignal.TaxiLight)
                {
                    case true:
                        EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.TaxiLight);
                        break;
                    case false:
                        EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.TaxiLight);
                        break;
                }
                switch (SignalBus.CenterSignal.RunwayLight)
                {
                    case true:
                        EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.RunwayLight);
                        break;
                    case false:
                        EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.RunwayLight);
                        break;
                }
                switch (SignalBus.CenterSignal.TakeOffLight)
                {
                    case true:
                        EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.TakeoffLight);
                        break;
                    case false:
                        EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.TakeoffLight);
                        break;
                }
                switch (SignalBus.CenterSignal.LandingLight)
                {
                    case true:
                        EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.LandingLight);
                        break;
                    case false:
                        EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.LandingLight);
                        break;
                }
                switch (SignalBus.CenterSignal.WingInspectionLight)
                {
                    case true:
                        EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.WingInspectionLight);
                        break;
                    case false:
                        EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.WingInspectionLight);
                        break;
                }
                switch (SignalBus.CenterSignal.PositionLight)
                {
                    case true:
                        EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.AntiCollisionLightWhite);
                        break;
                    case false:
                        EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.AntiCollisionLightWhite);
                        break;
                }


            }
            catch (Exception ex)
            {
                RetryTime++;
                if (RetryTime > 10)
                {
                    FunctionStatus = FunctionStatus.Failure;
                }
            }

            CallWatcher?.Invoke();
            Lock = false;
        }


        public void Dispose()
        {
            Timer.Dispose();
            this.Dispose();
        }
    }
}
