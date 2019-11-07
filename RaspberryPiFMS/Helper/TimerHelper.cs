using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RaspberryPiFMS.Helper
{
    /// <summary>
    /// 计时器
    /// </summary>
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

        public void StartTimming()
        {
            _thread.Start();
        }

        public void StopTimming()
        {
            if (_thread.ThreadState == ThreadState.Running)
            {
                _thread.Abort();
                TimeStopEvent?.Invoke();
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
            TimeStartEvent?.Invoke();
        }

        public delegate void TimerEventHandle();
        /// <summary>
        /// 开始计时事件
        /// </summary>
        public event TimerEventHandle TimeStartEvent;
        /// <summary>
        /// 计时结束事件
        /// </summary>
        public event TimerEventHandle TimeStopEvent;
    }
}
