using System;
using System.Timers;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Models;

namespace RaspberryPiFCS.Interface
{
    public interface IFunction : IDisposable
    {
        int RetryTime { get; set; }
        abstract Timer Timer { get; set; }
        bool Lock { get; set; }
        FunctionStatus FunctionStatus { get; set; }
        RelyEquipment RelyEquipment { get; set; }
    }

}
