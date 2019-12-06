using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Helper.Tests
{
    [TestClass()]
    public class PIDHelperTests
    {
        [TestMethod()]
        public void PIDCaculateTest()
        {
            PIDHelper PID = new PIDHelper();
            while (true)
            {
                //var res = PID.PIDCaculate(100);
            }
            //PID.PIDOutEvent += PID_PIDOutEvent;
            //PID.SetWithPID(10, 100);
        }

        private void PID_PIDOutEvent(double value)
        {
            Console.WriteLine(value.ToString("0.000"));
        }
    }
}