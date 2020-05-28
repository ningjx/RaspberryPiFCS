using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    public class RemoteFunction : IFunction
    {
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(20);
        public bool Lock { get; set; } = false;
        public FunctionStatus FunctionStatus { get; set; } = FunctionStatus.Online;
        public RemoteFunction()
        {

        }

        public void Dispose()
        {
            Timer.Dispose();
        }
    }
}
