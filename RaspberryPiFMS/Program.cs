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
                //Console.WriteLine("初始化");
                //string[] portList = Extend.GetPorts();
                //Console.WriteLine("串口列表：");
                //foreach (var i in portList)
                //{
                //    Console.WriteLine(i);
                //}
                RemoteController control = new RemoteController("COM3") ;
                
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"{Cache.RemoteSignal.Channel01.ToString()}\n{Cache.RemoteSignal.Channel02.ToString()}\n{Cache.RemoteSignal.Channel03.ToString()}\n{Cache.RemoteSignal.Channel04.ToString()}\n" +
                        $"{Cache.RemoteSignal.Channel05.ToString()}\n{Cache.RemoteSignal.Channel06.ToString()}\n{Cache.RemoteSignal.Channel07.ToString()}\n{Cache.RemoteSignal.Channel08.ToString()}\n" +
                        $"{Cache.RemoteSignal.Channel09.ToString()}\n{Cache.RemoteSignal.Channel10.ToString()}\n{Cache.RemoteSignal.Channel11.ToString()}\n{Cache.RemoteSignal.Channel12.ToString()}\n" +
                        $"{Cache.RemoteSignal.Channel13.ToString()}\n{Cache.RemoteSignal.Channel14.ToString()}\n{Cache.RemoteSignal.Channel15.ToString()}\n{Cache.RemoteSignal.Channel16.ToString()}");
                    //Console.ReadLine();
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
