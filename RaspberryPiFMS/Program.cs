using System;
using RaspberryPiFMS.Providers;
using Newtonsoft.Json;
using System.Threading;

namespace RaspberryPiFMS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("启动控制单元\r\n");
                OperationContrller contrller = new OperationContrller();
                Console.WriteLine("启动控制单元成功\r\n");
                Console.WriteLine();

                //while (true)
                //{
                //    //Console.Clear();
                //    //Console.Write(contrller.remoteData.roll + "\r\n");
                //    //Console.Write(contrller.testData + "\r\n");
                //    //Console.Write(contrller.baseValue + "\r\n");
                //    //Thread.Sleep(100);
                //}






            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
