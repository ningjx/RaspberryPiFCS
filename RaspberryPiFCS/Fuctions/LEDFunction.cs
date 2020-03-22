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
    public class LEDFunction : IFunction
    {
        private static readonly Timer _timer = new Timer(2000);

        /// <summary>
        /// 航行灯（翼尖闪烁）
        /// </summary>
        private static bool _flightLight = false;
        /// <summary>
        /// 红色信标灯（闪烁）
        /// </summary>
        private static bool _antiCollisionLight = false;

        static LEDFunction()
        {
            _timer.AutoReset = true;
            _timer.Elapsed += TwinkleLed;
            _timer.Start();
        }

        public void Excute<Pca9685>(CenterSignal signal, Pca9685 equipment)
        {
            //bool flightLight = datas[0];
            //bool antiCollisionLight = datas[1];
            //bool logoLight = datas[2];
            //bool taxiLight = datas[3];
            //bool runwayLight = datas[4];
            //bool takeOffLight = datas[5];
            //bool landingLight = datas[6];
            //bool wingInspectionLight = datas[7];
            //bool positionLight = datas[8];
            //
            //_flightLight = flightLight;
            //_antiCollisionLight = antiCollisionLight;
            //
            //switch (logoLight)
            //{
            //    case true: EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.LogoLight); break;
            //    case false: EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.LogoLight); break;
            //}
            //switch (taxiLight)
            //{
            //    case true: EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.TaxiLight); break;
            //    case false: EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.TaxiLight); break;
            //}
            //switch (runwayLight)
            //{
            //    case true: EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.RunwayLight); break;
            //    case false: EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.RunwayLight); break;
            //}
            //switch (takeOffLight)
            //{
            //    case true: EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.TakeoffLight); break;
            //    case false: EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.TakeoffLight); break;
            //}
            //switch (landingLight)
            //{
            //    case true: EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.LandingLight); break;
            //    case false: EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.LandingLight); break;
            //}
            //switch (wingInspectionLight)
            //{
            //    case true: EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.WingInspectionLight); break;
            //    case false: EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.WingInspectionLight); break;
            //}
            //switch (positionLight)
            //{
            //    case true: EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.AntiCollisionLightWhite); break;
            //    case false: EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.AntiCollisionLightWhite); break;
            //}
        }

        private static void TwinkleLed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_flightLight)
            {
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightL);
                Thread.Sleep(200);

                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightR);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightR);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightR);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightR);
                Thread.Sleep(200);

                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightB);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightB);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.FilghtLightB);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.FilghtLightB);
            }
            if (_antiCollisionLight)
            {
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.AntiCollisionLight);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.AntiCollisionLight);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOn((int)Channels.LedChannel.AntiCollisionLight);
                Thread.Sleep(100);
                EquipmentBus.LEDPca.SetOff((int)Channels.LedChannel.AntiCollisionLight);
            }
        }
    }
}
