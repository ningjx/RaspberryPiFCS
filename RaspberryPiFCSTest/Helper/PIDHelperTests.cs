using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Helper.Tests
{
    [TestClass()]
    public class PIDHelperTests
    {
        [TestMethod()]
        public void PIDCaculateTest()
        {

        }

        private void PID_PIDOutEvent(double value)
        {
            Console.WriteLine(value.ToString("0.000"));
        }
    }
}