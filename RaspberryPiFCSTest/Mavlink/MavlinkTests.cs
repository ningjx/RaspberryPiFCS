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
            MavlinkController aa = new MavlinkController();
            aa.Lunch();
            aa.Text();
               //var asm = Assembly.Load("MavLink")
               var messageTypes = Assembly.GetExecutingAssembly().GetTypes();//asm.GetTypes();
            foreach (var type in messageTypes)
            {
                if (type.Name == "MavlinkMessage")
                    continue;
                //var intance = asm.CreateInstance(type.FullName);
                
            }
        }
    }
}