using RaspberryPiFCS.Interface;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RaspberryPiFCS.Drivers
{
    public static class DriversFactory
    {
        public static SBusDriver GetSBusDriver(int sec)
        {
            return new SBusDriver(sec);
        }

        public static IUARTDriver GetUARTDriver(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new UARTDriver_Socket(portName, baudRate, parity, databits, stopBits);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new UART_Win_Driver(portName, baudRate, parity, databits, stopBits);
            }
            else
            {
                return null;
            }
        }

        public static PIDDriver GetPIDDriver(float kp = 0.1f, float ki = 0.2f, float kd = 0.4f)
        {
            return new PIDDriver(kp, ki, kd);
        }

        public static SocketDriver GetSocketDriver(int bindPort, int sendPort = 0)
        {
            return new SocketDriver(bindPort, sendPort);
        }
    }
}
