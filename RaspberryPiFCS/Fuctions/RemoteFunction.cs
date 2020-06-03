using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Main;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    /// <summary>
    /// 遥控功能
    /// </summary>
    public class RemoteFunction : IFunction
    {
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(20);
        public bool Lock { get; set; } = false;
        public FunctionStatus FunctionStatus { get; set; } = FunctionStatus.Online;
        public RelyEquipment RelyEquipment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RemoteFunction()
        {
            Timer.AutoReset = true;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (Lock)
                    return;
                Lock = true;
                EquipmentBus.RemoteController.Excute();
                Lock = false;
            }
            catch (Exception exception)
            {
                RetryTime++;
                if (RetryTime > 10)
                {
                    FunctionStatus = FunctionStatus.Failure;
                    Logger.Add(LogType.Error, "遥控功能启动失败", exception);
                }
            }
        }

        public void Dispose()
        {
            Timer.Dispose();
        }
    }
}
