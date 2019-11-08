using System;
using Newtonsoft.Json;
using System.Threading;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Controller;
using flyfire.IO.Ports;

namespace RaspberryPiFMS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("初始化");
                string[] portList = Extend.GetPorts();
                Console.WriteLine("串口列表：");
                foreach (var i in portList)
                {
                    Console.WriteLine(i);
                }
                RemoteController control = new RemoteController(Console.ReadLine()) ;
                while (true)
                {
                    Console.WriteLine(Config.RemoteSignal.Channel01.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
