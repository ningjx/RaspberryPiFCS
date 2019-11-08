using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Helper;
using Swan.DependencyInjection;
using Xunit;

namespace RaspberryPiFMSTest.HelperTest
{
    [TestClass]
    class BaseControlTest
    {
        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(50)]
        [InlineData(60)]
        public void BaseControl(double angel)
        {
            BaseController bc = new BaseController();
            bc.contrlData.roll = angel;
        }

        [TestMethod]
        public void InstanceTese()
        {
            aaa test;
            DependencyContainer.Current.Register<aaa>(new aaa());
        }
    }

    public class aaa
    {
        public string aaaa = string.Empty;
        public aaa()
        {
            aaaa = "sss";
        }
    }
}

