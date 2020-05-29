using RaspberryPiFCS.Configs;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Models;
using RaspberryPiFCS.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightDataModel;

namespace RaspberryPiFCS
{
    public static class DataBus
    {
        

        /// <summary>
        /// 数据
        /// </summary>
        public static FlightData FlightData = new FlightData();

        public static FlightStatus FlightStatus = new FlightStatus();
    }
}
