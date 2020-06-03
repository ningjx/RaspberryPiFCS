using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.BaseController;
using RaspberryPiFCS.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Configs.Tests
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