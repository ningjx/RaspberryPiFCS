using flyfire.IO.Ports;
using RJCP.IO.Ports;

namespace RaspberryPiFCS.Drivers
{
    public class UARTDriver : CustomSerialPort
    {
        public UARTDriver(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One) : base(portName, baudRate, parity, databits, stopBits)
        {
            BufSize = 10000;
            ReceiveTimeoutEnable = false;
        }
    }
}
