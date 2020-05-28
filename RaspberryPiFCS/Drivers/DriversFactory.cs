using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Drivers
{
    public static class DriversFactory
    {
        public static SBusDriver GetSBusDriver(int sec)
        {
            return new SBusDriver(sec);
        }

        public static UARTDriver GetUARTDriver(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
        {
            return new UARTDriver(portName, baudRate, parity, databits, stopBits);
        }

        public static PIDDriver GetPIDDriver(float kp = 0.1f, float ki = 0.2f, float kd = 0.4f)
        {
            return new PIDDriver(kp, ki, kd);
        }
    }
}
