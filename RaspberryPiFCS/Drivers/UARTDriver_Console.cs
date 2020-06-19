using RaspberryPiFCS.Interface;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspberryPiFCS.Drivers
{
    public class UARTDriver_Console : IUARTDriver
    {
        public event UARTRecHandler RecEvent;
        private char[] chars = new char[1000000];
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        public UARTDriver_Console(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
        {
            process.StartInfo.FileName = "python";
            process.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.CreateNoWindow = false;//不显示程序窗口
            process.Start();//启动程序
            process.StandardInput.AutoFlush = true;
            process.StandardInput.WriteLine("import SerialPortLib as ser");
            process.StandardInput.WriteLine($"ser.InitPort(\"{portName}\",{baudRate},{parity.GetHashCode()},{databits},{stopBits.GetHashCode()})");
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(20);
                    process.StandardInput.WriteLine("ser.Read()");
                    int count = process.StandardOutput.Read(chars, 0, 1000000);
                    byte[] byteData = Encoding.Default.GetBytes(chars, 0, count);
                    RecEvent?.Invoke(byteData);
                }
            });

        }

        public void WriteBytes(byte[] bytes)
        {
            process.StandardInput.Write($"ser.Write({Encoding.Default.GetString(bytes)})");
        }

    }
}
