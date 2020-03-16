using MavLink4Net.Messages;
using MavLink4Net.Messages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Helper;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

namespace RaspberryPiFCSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MicroTimer aa = new MicroTimer();
        }

        [TestMethod]
        public void MavlinkTest()
        {
            Dictionary<string, string> aaa = new Dictionary<string, string>() { { "111","222"},{ "333","444"} };
            var est = aaa.Keys;
            var ccc = est.GetEnumerator();

            IMessage aa = MessageFactory.CreateMessage(MavMessageType.Heartbeat);
            AttitudeMessage attitude = new AttitudeMessage();
            attitude.Pitch = 0.01F;
            attitude.Roll = 0.02F;
            attitude.Yaw = 0.03F;
            CrcExtraProvider.GetCrcExtra(MavMessageType.Attitude);
        }

        [TestMethod]
        public void StaticTest()
        {
            var a = TestStatic.test;
            TestStatic.test = 8;
            var b = TestStatic.test;
        }
    }

    public static class TestStatic
    {
        public static int test = 9;
    }
}
