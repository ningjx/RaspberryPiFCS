using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFMS.Helper;

namespace RaspberryPiFMSTest.HelperTest
{
    [TestClass]
    public class SocketHelperTest
    {
        [TestMethod]
        public void SendDataTest()
        {
            SocketHelper socket = new SocketHelper();
            string data = "test";
            socket.SendData(data);
        }
    }
}
