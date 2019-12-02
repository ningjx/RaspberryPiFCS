using System;
using System.Threading;

namespace RaspberryPiFMS
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bus.SysStart();
                while (true)
                {
                    Console.Clear();
                    //Console.WriteLine($"[1]{Cache.RemoteSignal.Channel01.ToString()}\n[2]{Cache.RemoteSignal.Channel02.ToString()}\n[3]{Cache.RemoteSignal.Channel03.ToString()}\n[4]{Cache.RemoteSignal.Channel04.ToString()}\n" +
                    //    $"[5]{Cache.RemoteSignal.Channel05.ToString()}\n[6]{Cache.RemoteSignal.Channel06.ToString()}\n[7]{Cache.RemoteSignal.Channel07.ToString()}\n[8]{Cache.RemoteSignal.Channel08.ToString()}\n" +
                    //    $"[9]{Cache.RemoteSignal.Channel09.ToString()}\n[10]{Cache.RemoteSignal.Channel10.ToString()}\n[11]{Cache.RemoteSignal.Channel11.ToString()}\n[12]{Cache.RemoteSignal.Channel12.ToString()}\n" +
                    //    $"[13]{Cache.RemoteSignal.Channel13.ToString()}\n[14]{Cache.RemoteSignal.Channel14.ToString()}\n[15]{Cache.RemoteSignal.Channel15.ToString()}\n[16]{Cache.RemoteSignal.Channel16.ToString()}");
                    Console.WriteLine($"[roll]{Bus.CenterControlData.Roll.ToString("f2")}\n[pitch]{Bus.CenterControlData.Pitch.ToString("f2")}\n[yaw]{Bus.CenterControlData.Yaw.ToString("f2")}\n" +
                        $"[throttel]{Bus.CenterControlData.ThrottelL.ToString("f2")}  {Bus.CenterControlData.ThrottelR.ToString("f2")}\n");
                    Thread.Sleep(50);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
