using RaspberryPiFMS.Models;
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
                ControllerBus.SysLaunch();
                //Console.ReadLine();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"[1]{RemoteSignal.Yaw.ToString()}\n[2]{RemoteSignal.Pitch.ToString()}\n[3]{RemoteSignal.Throttel.ToString()}\n[4]{RemoteSignal.Roll.ToString()}\n" +
                        $"[5]{RemoteSignal.Channel05.ToString()}\n[6]{RemoteSignal.Channel06.ToString()}\n[7]{RemoteSignal.Channel07.ToString()}\n[8]{RemoteSignal.Channel08.ToString()}\n" +
                        $"[9]{RemoteSignal.Channel09.ToString()}\n[10]{RemoteSignal.Channel10.ToString()}\n[11]{RemoteSignal.Channel11.ToString()}\n[12]{RemoteSignal.Channel12.ToString()}\n" +
                        $"[13]{RemoteSignal.Channel13.ToString()}\n[14]{RemoteSignal.Channel14.ToString()}\n[15]{RemoteSignal.Channel15.ToString()}\n[16]{RemoteSignal.Channel16.ToString()}\n");
                    Console.WriteLine($"[rollL]{CenterSignal.RollL.ToString("f2")}\n[pitchL]{CenterSignal.PitchL.ToString("f2")}\n[yaw]{CenterSignal.Yaw.ToString("f2")}\n" +
                        $"[throttel]{CenterSignal.ThrottelL.ToString("f2")}\n");
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
