﻿using RaspberryPiFCS.Handlers;
using RaspberryPiFCS.Interface;
using System;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    public class PushBackFunction : IFunction
    {
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(500);
        public bool Lock { get; set; } = false;

        public event WatcherHandler CallWatcher;

        public PushBackFunction()
        {
            Timer.AutoReset = true;
            Timer.Elapsed += Excute;
            Timer.Start();
        }

        private void Excute(object sender, ElapsedEventArgs e)
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
