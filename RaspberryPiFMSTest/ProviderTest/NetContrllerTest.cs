using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFMS.Providers;
using Newtonsoft.Json;
using RaspberryPiFMS.Models;

namespace RaspberryPiFMSTest.ProviderTest
{
    [TestClass]
    public class NetContrllerTest
    {
        [TestMethod]
        public void NetTest()
        {
            NetContrller a = new NetContrller();
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
                var a = JsonConvert.DeserializeObject<RemoteDataModel>(aa, settings);
            }
            catch(Exception e)
            {

            }
        }
    }
}
