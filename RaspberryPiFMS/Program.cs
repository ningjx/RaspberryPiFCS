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
                Bus.SysLaunch();
                //Console.ReadLine();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"[1]{Bus.RemoteSignal.Channel01.ToString()}\n[2]{Bus.RemoteSignal.Channel02.ToString()}\n[3]{Bus.RemoteSignal.Channel03.ToString()}\n[4]{Bus.RemoteSignal.Channel04.ToString()}\n" +
                        $"[5]{Bus.RemoteSignal.Channel05.ToString()}\n[6]{Bus.RemoteSignal.Channel06.ToString()}\n[7]{Bus.RemoteSignal.Channel07.ToString()}\n[8]{Bus.RemoteSignal.Channel08.ToString()}\n" +
                        $"[9]{Bus.RemoteSignal.Channel09.ToString()}\n[10]{Bus.RemoteSignal.Channel10.ToString()}\n[11]{Bus.RemoteSignal.Channel11.ToString()}\n[12]{Bus.RemoteSignal.Channel12.ToString()}\n" +
                        $"[13]{Bus.RemoteSignal.Channel13.ToString()}\n[14]{Bus.RemoteSignal.Channel14.ToString()}\n[15]{Bus.RemoteSignal.Channel15.ToString()}\n[16]{Bus.RemoteSignal.Channel16.ToString()}\n");
                    Console.WriteLine($"[roll]{Bus.CenterData.RollL.ToString("f2")}\n[pitch]{Bus.CenterData.Pitch.ToString("f2")}\n[yaw]{Bus.CenterData.Yaw.ToString("f2")}\n" +
                        $"[throttel]{Bus.CenterData.ThrottelL.ToString("f2")}\n");
                    Thread.Sleep(20);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
