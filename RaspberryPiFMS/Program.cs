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
                    Console.WriteLine($"[1]{StateDatasBus.RemoteSignal.Yaw.ToString()}\n[2]{StateDatasBus.RemoteSignal.Pitch.ToString()}\n[3]{StateDatasBus.RemoteSignal.Throttel.ToString()}\n[4]{StateDatasBus.RemoteSignal.Roll.ToString()}\n" +
                        $"[5]{StateDatasBus.RemoteSignal.Channel05.ToString()}\n[6]{StateDatasBus.RemoteSignal.Channel06.ToString()}\n[7]{StateDatasBus.RemoteSignal.Channel07.ToString()}\n[8]{StateDatasBus.RemoteSignal.Channel08.ToString()}\n" +
                        $"[9]{StateDatasBus.RemoteSignal.Channel09.ToString()}\n[10]{StateDatasBus.RemoteSignal.Channel10.ToString()}\n[11]{StateDatasBus.RemoteSignal.Channel11.ToString()}\n[12]{StateDatasBus.RemoteSignal.Channel12.ToString()}\n" +
                        $"[13]{StateDatasBus.RemoteSignal.Channel13.ToString()}\n[14]{StateDatasBus.RemoteSignal.Channel14.ToString()}\n[15]{StateDatasBus.RemoteSignal.Channel15.ToString()}\n[16]{StateDatasBus.RemoteSignal.Channel16.ToString()}\n");
                    Console.WriteLine($"[rollL]{StateDatasBus.CenterSignal.RollL.ToString("f2")}\n[pitchL]{StateDatasBus.CenterSignal.Pitch.ToString("f2")}\n[yaw]{StateDatasBus.CenterSignal.Yaw.ToString("f2")}\n" +
                        $"[throttel]{StateDatasBus.CenterSignal.Throttel.ToString("f2")}\n");
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
