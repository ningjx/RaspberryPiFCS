using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RaspberryPiFCS.Helper.Tests
{
    [TestClass()]
    public class GPSHelperTests
    {
        [TestMethod()]
        public void DecodeDataTest()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void Uart_ReceivedEvent(object sender, byte[] bytes)
        {
            //GPSHelper.DecodeData(bytes);
        }
    }
}