using System;
using System.Timers;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Models;

namespace RaspberryPiFCS.Interface
{
    public interface IFunction
    {
        bool Lock { get; set; }
        FunctionStatus FunctionStatus { get; set; }
        RelyEquipment RelyEquipment { get; set; }
        void Excute(object sender, ElapsedEventArgs e);
    }

}
