using System;
using RaspberryPiFMS.Providers;

namespace RaspberryPiFMS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("启动控制单元");
                OperationContrller contrller = new OperationContrller();
                Console.WriteLine("启动控制单元成功");
                Console.WriteLine();








            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
