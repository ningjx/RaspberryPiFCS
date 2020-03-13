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
                Lunch.StartUp();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
