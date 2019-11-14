using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFMS.Helper;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

namespace RaspberryPiFMSTest
{
    [TestClass]
    public class UnitTest1
    {
        Stopwatch sw = new Stopwatch();
        [TestMethod]
        public void TestMethod1()
        {
            MicroTimer timer = new MicroTimer();
            timer.Interval = 1000;
            timer.AutoReset = false;
            timer.Elapsed += Stop;
            sw.Start();
            timer.Start();
            Thread.Sleep(1200);
            var a = sw.Elapsed.Milliseconds;
        }

        private void Stop()
        {
            sw.Stop();
        }
    }
}
