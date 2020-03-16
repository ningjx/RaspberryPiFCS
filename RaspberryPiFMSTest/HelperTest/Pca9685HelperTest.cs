using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Helper;

namespace RaspberryPiFCSTest.HelperTest
{
    [TestClass]
    public class Pca9685HelperTest
    {
        [TestMethod]
        public void Pca9685Test()
        {
            Pca9685 pca9685 = new Pca9685();
            pca9685.SetAngle(0,10);
        }
    }
}
