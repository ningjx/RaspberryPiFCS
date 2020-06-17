using RaspberryPiFCS.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Models
{
    public class FlightStatus
    {
        public ContrlMode ContrlMode = ContrlMode.Manual;
        public LoraDataMode LoraDataMode = LoraDataMode.SysData;
        public SpeedMode SpeedMode = SpeedMode.ManualSpeed;
    }
}
