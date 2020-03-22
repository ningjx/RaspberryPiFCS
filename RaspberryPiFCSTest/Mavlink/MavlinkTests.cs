using Microsoft.VisualStudio.TestTools.UnitTesting;
using MavLink;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using MavLink.Message;
using MavLink;
using RaspberryPiFCS.Controller;

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
            msg_Attitude.Aacceleration_X = 1;
            msg_Attitude.roll = 1;
            MavlinkPacket packet = new MavlinkPacket(msg_Attitude);
            var res = mavlink.Send(packet);
            mavlink.PacketReceived += Mavlink_PacketReceived;
            mavlink.ParseBytes(res);

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