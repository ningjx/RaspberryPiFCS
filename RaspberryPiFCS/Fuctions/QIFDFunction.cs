﻿using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    public class QIFDFunction : IFunction
    {
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(10);
        public bool Lock { get; set; } = false;
        public FunctionStatus FunctionStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public RelyEquipment RelyEquipment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event WatcherHandler CallWatcher;

        public QIFDFunction()
        {
            Timer.AutoReset = true;
            Timer.Elapsed += Excute;
            Timer.Start();
        }

        public void Excute(object sender, ElapsedEventArgs e)
        {
            if (Lock)
                return;
            else
                Lock = true;

            try
            {
                //根据控制信号操作





            }
            catch (Exception ex)
            {
                RetryTime++;
                if (RetryTime > 10)
                    throw ex;
            }

            CallWatcher?.Invoke();
            Lock = false;
        }


        public void Dispose()
        {
            Timer.Dispose();
            this.Dispose();
        }
    }
}
