using System;
using Newtonsoft.Json;
using System.Threading;
using RaspberryPiFMS.Helper;
using RaspberryPiFMS.Controller;

namespace RaspberryPiFMS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RemoteController control = new RemoteController();
                while (true)
                {
                    Console.WriteLine(control.data.ToString());
                }
                //Console.WriteLine("启动控制单元\r\n");
                //OperationContrller contrller = new OperationContrller();
                //Console.WriteLine("启动控制单元成功\r\n");
                //Console.WriteLine();

                //while (true)
                //{
                //    //Console.Clear();
                //    //Console.Write(contrller.remoteData.roll + "\r\n");
                //    //Console.Write(contrller.testData + "\r\n");
                //    //Console.Write(contrller.baseValue + "\r\n");
                //    //Thread.Sleep(100);
                //}
                //Pca9685 pca = new Pca9685();
                //while (true)
                //{
                //    pca.SetPWMAngle(0, 0);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 10);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 20);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 30);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 40);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0,50);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 60);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 70);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 80);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 90);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 100);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 110);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 120);
                //    Thread.Sleep(1000);
                //    pca.SetPWMAngle(0, 130);
                //    Thread.Sleep(1000);


            //}




            }
            catch (Exception e)
            {
                Console.WriteLine($"系统出现异常\r\n异常消息[{e.Message}]\r\n堆栈追踪\r\n--------------------------------------------------------------------------\r\n{e.StackTrace}\r\n--------------------------------------------------------------------------");
            }
        }
    }
}
