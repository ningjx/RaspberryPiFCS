using RaspberryPiFCS.Interface;
using RJCP.IO.Ports;
using System.Runtime.InteropServices;
using System.Timers;

namespace RaspberryPiFCS.Drivers
{
    public class UARTDriver : IUARTDriver
    {
        [DllImport(@"SerialPortLib.so")]
        public static extern void InitPort(string portName, int baudRate, int parity, int databits, int stopBits);
        [DllImport(@"SerialPortLib.so")]
        public static extern byte[] Read();
        [DllImport(@"SerialPortLib.so")]
        public static extern void Write(byte[] bytes);
        [DllImport(@"SerialPortLib.so")]
        public static extern void Close();

        private Timer Timer = new Timer(20);
        public UARTDriver(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
        {
            InitPort(portName, baudRate, parity.GetHashCode(), databits, stopBits.GetHashCode());
            Timer.AutoReset = true;
            Timer.Elapsed += ReadBytes;
            Timer.Start();
        }

        private void ReadBytes(object sender, ElapsedEventArgs e)
        {
            byte[] bytes = Read();
            if (bytes.Length != 0)
                RecEvent?.Invoke(bytes);
        }

        public void WriteBytes(byte[] bytes)
        {
            Write(bytes);
        }

        public event UARTRecHandler RecEvent;
    }
}
