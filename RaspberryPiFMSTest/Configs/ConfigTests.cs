using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFMS.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Configs.Tests
{
    [TestClass()]
    public class ConfigTests
    {
        [TestMethod()]
        public void ChangeConfigTest()
        {
            var config = Config.SysConfig;
        }
    }
}