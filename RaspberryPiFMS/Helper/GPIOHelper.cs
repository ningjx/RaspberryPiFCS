using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Gpio.Drivers;
using System.Threading;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Swan.DependencyInjection;
using Unosquare.WiringPi.Native;
using System.Threading.Tasks;
using Unosquare.RaspberryIO.Abstractions.Native;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;
using Unosquare.WiringPi;

namespace RaspberryPiFMS.Helper
{
    public class GPIOHelper
    {
        public GPIOHelper()
        {
            Pi.Init<BootstrapWiringPi>();
            var aa = Pi.Gpio[BcmPin.Gpio23];
            aa.PinMode = GpioPinDriveMode.Input;
            //aa.RegisterInterruptCallback(EdgeDetection.FallingAndRisingEdge)
        }
    }

}
