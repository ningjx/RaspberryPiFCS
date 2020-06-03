using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swan.DependencyInjection;
using Xunit;

namespace RaspberryPiFCSTest.HelperTest
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

