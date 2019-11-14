using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO.Ports;
namespace RaspberryPiFMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                var aa = sw.Elapsed.TotalMilliseconds;
            }
        }
    }
}
