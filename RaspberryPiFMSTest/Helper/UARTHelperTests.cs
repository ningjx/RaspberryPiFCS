using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RaspberryPiFCS.Helper.Tests
{
    [TestClass()]
    public class UARTHelperTests
    {
        [TestMethod()]
        public void UARTHelperTest()
        {
            //string portname1 = Extends.GetPorts()[0];
            //string portname2 = Extends.GetPorts()[1];
            //UARTHelper UART1 = new UARTHelper(portname1);
            //UART1.Open();
            //UARTHelper UART2 = new UARTHelper(portname2);
            //UART2.ReceivedEvent += UART2_ReceivedEvent;
            //UART2.Open();

            byte[] sendbytes = new byte[1000];
            sendbytes[0] = 0;
            sendbytes[1] = 2;//两字节地址
            sendbytes[2] = 18;//一字节信道
            string data = "this is the datas";//后面是是数据
            var databytes = Encoding.ASCII.GetBytes(data);
            databytes.ReadToEnd((i, aByte) => { sendbytes[i + 3] = aByte; });
            sendbytes = sendbytes.GetValueWithLength(databytes.Length + 3);
            //string aa = sendbytes.GetBitByPositon(0, sendbytes.Length);

            //while (true)
            //{
            //    UART1.Write(sendbytes);
            //}

            //sendbytes[1] = 2;
            //UART1.Write(sendbytes);
            //Thread.Sleep(10000);
        }

        private void UART2_ReceivedEvent(object sender, byte[] bytes)
        {
            var test = bytes;
        }
    }
}