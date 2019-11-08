using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using flyfire.IO.Ports;
using RaspberryPiFMS.Helper;
using RJCP.IO.Ports;


namespace RaspberryPiFMS.Controller
{
    public class RemoteController
    {
        private SbusHelper _sbusHelper;
        private CustomSerialPort _sbus;
        public RemoteController(string portName)
        {
            _sbusHelper = new SbusHelper();
            _sbus = new CustomSerialPort(portName, 100000, Parity.Even, 8, StopBits.Two);
            _sbus.ReceiveTimeoutEnable = false ;
            _sbus.ReceivedEvent += Sbus_ReceivedEvent;
            _sbus.Open();
        }

        public void Dispose()
        {
            Dispose();
        }

        private void Sbus_ReceivedEvent(object sender, byte[] bytes)
        {
            _sbusHelper.DecodeSignal(bytes);
        }
    }
}
