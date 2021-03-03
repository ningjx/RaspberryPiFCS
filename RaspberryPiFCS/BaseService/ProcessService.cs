using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspberryPiFCS
{
    public class ProcessService
    {
        public static bool Lock = false;
        public static event ProcessHandler ProcessEvent;
        public static void Init()
        {
            //初始化系统

        }

        public static void StartUp()
        {
            Lock = true;
            Task.Run(() =>
            {
                while (Lock)
                {
                    try
                    {
                        ProcessEvent.Invoke();
                        Thread.Sleep(0);
                    }
                    catch { }
                }
            });
        }
    }
}
