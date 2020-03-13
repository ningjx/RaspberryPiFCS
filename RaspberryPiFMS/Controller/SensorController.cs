using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Interface;

namespace RaspberryPiFMS.Controller
{
    public class SensorController : IController
    {
        private readonly Timer _timer = new Timer(10);
        private bool _locker = false;

        public SensorController()
        {
            _timer.AutoReset = true;
            _timer.Elapsed += ReciveData;
            _timer.Start();
        }

        public void ReciveData(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_locker)
                return;
            _locker = true;
            if (EquipmentBus.SensorUart.RecBytes.Length != 0)
                GPSHelper.DecodeData(EquipmentBus.RemoteUart.RecBytes);
            _locker = false;
        }
    }
}
