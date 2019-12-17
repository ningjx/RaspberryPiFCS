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
                CtrllerBus.SysLaunch();
                //Console.ReadLine();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"[1]{OriginConSingnal.Yaw.ToString()}\n[2]{OriginConSingnal.Pitch.ToString()}\n[3]{OriginConSingnal.Throttel.ToString()}\n[4]{OriginConSingnal.Roll.ToString()}\n" +
                        $"[5]{OriginConSingnal.Channel05.ToString()}\n[6]{OriginConSingnal.Channel06.ToString()}\n[7]{OriginConSingnal.Channel07.ToString()}\n[8]{OriginConSingnal.Channel08.ToString()}\n" +
                        $"[9]{OriginConSingnal.Channel09.ToString()}\n[10]{OriginConSingnal.Channel10.ToString()}\n[11]{OriginConSingnal.Channel11.ToString()}\n[12]{OriginConSingnal.Channel12.ToString()}\n" +
                        $"[13]{OriginConSingnal.Channel13.ToString()}\n[14]{OriginConSingnal.Channel14.ToString()}\n[15]{OriginConSingnal.Channel15.ToString()}\n[16]{OriginConSingnal.Channel16.ToString()}\n");
                    Console.WriteLine($"[rollL]{CenterData.RollL.ToString("f2")}\n[pitchL]{CenterData.PitchL.ToString("f2")}\n[yaw]{CenterData.Yaw.ToString("f2")}\n" +
                        $"[throttel]{CenterData.ThrottelL.ToString("f2")}\n");
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
