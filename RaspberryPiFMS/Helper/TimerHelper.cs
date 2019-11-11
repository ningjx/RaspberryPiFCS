using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RaspberryPiFMS.Helper
{
    /// <summary>
    /// 计时器，不好用，报错，推荐System.Timers.Timer
    /// </summary>
    [Obsolete]
    public class TimerHelper
    {
        private ThreadStart _threadStart;
        private Thread _thread;
        private int _finalCount = 0;

        /// <summary>
        /// 初始化计时器
        /// </summary>
        /// <param name="seconds">计时几秒</param>
        public TimerHelper(int seconds)
        {
            _finalCount = seconds;
            _threadStart = () => StartTime();
            _thread = new Thread(_threadStart);
        }

        /// <summary>
        /// 启动计时
        /// </summary>
        public void StartTimming()
        {
            if (_thread.ThreadState != ThreadState.Running)
                _thread.Start();
        }

        /// <summary>
        /// 中途停止计时
        /// </summary>
        public void StopTimming()
        {
            if (_thread.ThreadState == ThreadState.Running)
            {
                _thread.Abort();
            }
        }

        private void StartTime()
        {
            int thisCount = 0;
            while (thisCount <= _finalCount)
            {
                Thread.Sleep(1000);
                thisCount++;
            }
            TimeFinishEvent?.Invoke();
        }

        public delegate void TimerEventHandle();

        /// <summary>
        /// 计时完成事件
        /// </summary>
        public event TimerEventHandle TimeFinishEvent;
    }
}
