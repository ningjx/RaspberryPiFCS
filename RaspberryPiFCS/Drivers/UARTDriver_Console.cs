using RaspberryPiFCS.Interface;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Drivers
{
    public class UARTDriver_Console : IUARTDriver
    {
        public event UARTRecHandler RecEvent;
        public int BufferSize;

        public UARTDriver_Console(string portName, int baudRate = 115200, Parity parity = Parity.None, int databits = 8, StopBits stopBits = StopBits.One)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "python";
            process.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.CreateNoWindow = true;//不显示程序窗口
            process.Start();//启动程序

        }

        public void WriteBytes(byte[] bytes)
        {
            throw new NotImplementedException();
        }


    }
}
