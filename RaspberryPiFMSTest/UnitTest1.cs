using MavLink4Net.Messages;
using MavLink4Net.Messages.Common;
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
        [TestMethod]
        public void TestMethod1()
        { 

        }

        [TestMethod]
        public void MavlinkTest()
        {
            IMessage aa = MessageFactory.CreateMessage(MavMessageType.Heartbeat);
            AttitudeMessage attitude = new AttitudeMessage();
            attitude.Pitch = 0.01F;
            attitude.Roll = 0.02F;
            attitude.Yaw = 0.03F;
            CrcExtraProvider.GetCrcExtra(MavMessageType.Attitude);
        }
    }
}
