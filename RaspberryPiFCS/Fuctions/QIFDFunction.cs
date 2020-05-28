using RaspberryPiFCS.Handlers;
using RaspberryPiFCS.Interface;
using System;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    public class QIFDFunction : IFunction
    {
        public int RetryTime { get; set; } = 0;
        public Timer Timer { get; set; } = new Timer(10);
        public bool Lock { get; set; } = false;

        public event WatcherHandler CallWatcher;

        public QIFDFunction()
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
