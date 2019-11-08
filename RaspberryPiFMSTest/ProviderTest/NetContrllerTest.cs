using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RaspberryPiFMS.Controller;
using RaspberryPiFMS.Models;
using RaspberryPiFMS.Helper;
using Swan.DependencyInjection;
using Unosquare.RaspberryIO.Abstractions;

namespace RaspberryPiFMSTest.ProviderTest
{
    [TestClass]
    public class NetContrllerTest
    {
        [TestMethod]
        public void NetTest()
        {
            NetController a = new NetController();
            //while (true)
            //{
            //    Console.WriteLine($"{a.remoteData.roll}");
            //}
            
        }



        [TestMethod]
        public void JsonTest()
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    Error = (obj, args2) =>
                    {
                        args2.ErrorContext.Handled = true;
                    }
                };
                string aa = "{\"Yaw\":0.0,\"Roll\":0.0,\"Pitch\":0.0,\"Flap\":0.0,\"Throttle\":0.0,\"Gear\":false,\"AirBreak\":0.0,\"PushBack\":false,\"Trim\":0.0,\"TimeStamp\":0,\"VerticalNavigation\":false,\"LateralNavigation\":false,\"AutoTrim\":false,\"AutoThrottel\":false,\"TaxiLight\":false,\"RunwayLight\":false,\"LogoLight\":false,\"LandingLight\":false,\"WingInspectionLight\":false,\"PositionLight\":false,\"AntiCollisionLight\":false}";
                var a = JsonConvert.DeserializeObject<RemoteControlModel>(aa, settings);
            }
            catch(Exception e)
            {

            }
        }
        [TestMethod]
        public void InstanceTese()
        {

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
