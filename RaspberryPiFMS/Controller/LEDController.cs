using RaspberryPiFMS.Enum;
using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RaspberryPiFMS.Controller
{
    public class LEDController
    {
        /// <summary>
        /// 航行灯（翼尖闪烁）
        /// </summary>
        public bool logoLight = false;
        /// <summary>
        /// 红色信标灯（闪烁）
        /// </summary>
        public bool antiCollisionLight = false;

        private Timer _timer = new Timer();
        public LEDController()
        {
            _timer.Enabled = true;
            _timer.Interval = 1500;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (logoLight)
            {
                Config.LedAndPushbackDriver.SetLedOn(1);
                Thread.Sleep(200);
                Config.LedAndPushbackDriver.SetLedOff(1);
                Thread.Sleep(200);
                Config.LedAndPushbackDriver.SetLedOn(1);
                Thread.Sleep(200);
                Config.LedAndPushbackDriver.SetLedOff(1);
            }
            if (antiCollisionLight)
            {
                Config.LedAndPushbackDriver.SetLedOn(2);
                Thread.Sleep(200);
                Config.LedAndPushbackDriver.SetLedOff(1);
            }
        }

        public void SetLedOnAndOff(LedChannel channel, Switch @switch)
        {
            if (channel == LedChannel.AntiCollisionLight && @switch == Switch.On)
            {
                antiCollisionLight = true;
                return;
            }
            if (channel == LedChannel.AntiCollisionLight && @switch == Switch.Off)
            {
                antiCollisionLight = false;
                return;
            }
            if (channel == LedChannel.LogoLight && @switch == Switch.On)
            {
                logoLight = true;
                return;
            }
            if (channel == LedChannel.LogoLight && @switch == Switch.Off)
            {
                logoLight = false;
                return;
            }
            if (@switch == Switch.Off)
                Config.LedAndPushbackDriver.SetLedOff(channel.GetHashCode());
            if (@switch == Switch.On)
                Config.LedAndPushbackDriver.SetLedOn(channel.GetHashCode());
        }
    }
}
