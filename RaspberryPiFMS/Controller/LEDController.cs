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
        private bool _flightLight = true;
        /// <summary>
        /// 红色信标灯（闪烁）
        /// </summary>
        private bool _antiCollisionLight = true;
        private Timer _timer = new Timer();
        private Timer _ledTimer = new Timer();
        private Pca9685 _ledDriver;
        public LEDController(Pca9685 ledDriver,int ms = 15)
        {
            ms = Math.Abs(ms);
            _ledDriver = ledDriver;
            _timer.Enabled = true;
            _timer.Interval = ms;
            _timer.Elapsed += Excute;

            _ledTimer.Enabled = true;
            _ledTimer.Interval = 1500;
            _ledTimer.Elapsed += TwinkleLed;
            _timer.Start();
            _ledTimer.Start();
        }

        //使用频道7(三挡)/8(两档)
        //8-off
        //      7-off:航行灯开 防撞灯开 LOGO灯开(根据起落架状态)
        //      7-mid:滑行灯开(根据起落架状态)
        //      7-on:跑道脱离灯
        //8-on
        //      7-on:起飞灯(根据起落架状态)
        //      7-mid:着陆灯(根据起落架状态)
        //      7-off:高亮度白色防撞灯 机翼检查灯开
        //7个灯光控制
        private void Excute(object sender, ElapsedEventArgs e)
        {





            
        }


        private void TwinkleLed(object sender, ElapsedEventArgs e)
        {
            //if (_flightLight)
            //{
                _ledDriver.SetLedOn((int)LedChannel.LogoLight);
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.LogoLight.GetHashCode());
                Thread.Sleep(200);
                _ledDriver.SetLedOn((int)LedChannel.LogoLight.GetHashCode());
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.LogoLight.GetHashCode());
            //}
            //if (_antiCollisionLight)
            //{
                _ledDriver.SetLedOn((int)LedChannel.AntiCollisionLight.GetHashCode());
                Thread.Sleep(200);
                _ledDriver.SetLedOff((int)LedChannel.AntiCollisionLight.GetHashCode());
            //}
        }
    }
}
