using RaspberryPiFCS.Helper;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Text;
using Timer = System.Timers.Timer;
using RaspberryPiFCS.Interface;

namespace RaspberryPiFCS.Controller
{
    public class QIFDController : IController
    {
        private GpioController _controller;
        private MicroTimer _timer;//执行计时
        private MicroTimer _pulseTimer;//脉冲发射
        private MicroTimer _overTimer;//超时计时
        private int _sendPin;
        private int _recPin;
        private bool _isOverTime = false;
        private Stopwatch _sw = new Stopwatch();
        private bool _isRecving = false;
        public QIFDController(int sendPin, int recPin)
        {
            _sendPin = sendPin;
            _recPin = recPin;
            _controller = new GpioController(PinNumberingScheme.Logical);
            _controller.OpenPin(sendPin, PinMode.Output);
            _controller.OpenPin(recPin, PinMode.Input);

            _timer = new MicroTimer(80, true);//测距轮询80ms
            _timer.Elapsed += Excute;

            _pulseTimer = new MicroTimer(0.01, false);//脉冲0.0.ms
            _pulseTimer.Elapsed += SetLow;

            _overTimer = new MicroTimer(50, false);//超时阈值50ms
            _overTimer.Elapsed += SetOvetTime;
        }
        public void Strat()
        {
            _timer.Start();
        }
        private void Excute()
        {
            _overTimer.Start();//开始超时计时
            _controller.Write(_sendPin, PinValue.High);
            _pulseTimer.Start();//发送10us脉冲
            while (_isOverTime)
            {
                if (_isRecving == false && _controller.Read(_recPin) == PinValue.High)
                {
                    _sw.Start();
                    _isRecving = true;
                }
                if (_isRecving == true && _controller.Read(_recPin) == PinValue.Low)
                {
                    _sw.Stop();
                    _isRecving = false;
                    break;
                }
            }
            StateDatasBus.FlightData.PositionData.MicroAltitude = _sw.Elapsed.Milliseconds * 343 / 20;
            _sw.Reset();
            _isOverTime = false;
        }
        private void SetLow()
        {
            _controller.Write(_sendPin, PinValue.Low);
        }
        public void Stop()
        {
            _timer.Stop();
            StateDatasBus.FlightData.PositionData.MicroAltitude = 0;
        }
        public void SetOvetTime()
        {
            _isOverTime = true;
        }
    }
}
