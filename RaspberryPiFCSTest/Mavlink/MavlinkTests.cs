using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MavLink.Tests
{
    [TestClass()]
    public class MavlinkTests
    {
        [TestMethod()]
        public void SendTest()
        {
            Mavlink mavlink = new Mavlink();
            Msg_attitude msg_Attitude = new Msg_attitude();
            msg_Attitude.roll = 1;
            MavlinkPacket packet = new MavlinkPacket(msg_Attitude);
            var res = mavlink.Send(packet);
            mavlink.PacketReceived += Mavlink_PacketReceived;
            mavlink.ParseBytes(res);
            Assert.AreNotEqual(mavlink.PacketsReceived,0);
        }

        private void Mavlink_PacketReceived(object sender, MavlinkPacket e)
        {
            var type = e.Message.GetType();
            switch (type.Name)
            {
                case "Msg_attitude":
                    break;
            }
        }
    }
}