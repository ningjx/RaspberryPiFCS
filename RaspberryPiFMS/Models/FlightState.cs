using RaspberryPiFCS.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Models
{
    public class FlightState
    {
        public ContrlMode ContrlMode = ContrlMode.Manual;
        public LoraDataMode LoraDataMode = LoraDataMode.SysData;
    }
}
