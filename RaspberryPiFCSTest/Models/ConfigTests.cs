using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Models.Tests
{
    [TestClass()]
    public class ConfigTests
    {
        [TestMethod()]
        public void ChangeConfigTest()
        {
            int SequenceNumber = 100;
            var test = (byte)((SequenceNumber >> 24) & 0xFF);
            //Config.ChangeConfig("LosingSignalDelay", 879494);
        }
    }

    public static class AA
    {
        public static float t;
    }
}