using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMS.Controller
{
    /// <summary>
    /// 遥控数据解码&转换
    /// </summary>
    public class LoraController
    {
        private Timer timer = new Timer(10);
        private bool _locker = false;

        public LoraController()
        {
            timer.AutoReset = true;
            timer.Elapsed += ReciveData;
            timer.Start();
        }

        private void ReciveData(object sender, ElapsedEventArgs e)
        {
            if (_locker)
                return;
            _locker = true;
            if (EquipmentBus.LoraUart.Bytes.Length != 0)
                LoraHelper.DecodeSignal(EquipmentBus.RemoteUart.Bytes);




            _locker = false;
        }
    }
}
