using System;
using System.Diagnostics;

namespace RaspberryPiFCS.Helper
{
    /// <summary>
    /// 高精度定时器
    /// </summary>
    [Obsolete]
    public class MicroTimer
    {
        
        /// <summary>
        /// 毫秒
        /// </summary>
        public double Interval;
        public TimerEventHandler Elapsed;
        public bool AutoReset = true;
        private bool _enable = true;
        private Stopwatch _sw = new Stopwatch();

        /// <summary>
        /// 高精度定时器
        /// </summary>
        /// <param name="Interval">毫秒</param>
        /// <param name="AutoReset">是否重复执行</param>
        public MicroTimer(double Interval,bool AutoReset)
        {
            this.Interval = Interval;
            this.AutoReset = AutoReset;
        }
        public MicroTimer()
        {

        }
        public void Start()
        {
            _sw.Reset();
            _enable = true;
            if (AutoReset)
            {
                _sw.Start();
                while (_enable)
                {
                    if (_sw.Elapsed.TotalMilliseconds > Interval)
                    {
                        Elapsed.Invoke();
                        _sw.Restart();
                    }
                }
            }
            else
            {
                _sw.Start();
                while (_enable)
                {
                    if (_sw.Elapsed.TotalMilliseconds > Interval)
                    {
                        Elapsed.Invoke();
                        _sw.Stop();
                        break;
                    }
                }
            }
        }
        public void Stop()
        {
            _enable = false;
        }

        public delegate void TimerEventHandler();
        
    }
}
