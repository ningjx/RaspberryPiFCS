using RaspberryPiFCS.Fuctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using RaspberryPiFCS.Interface;

namespace RaspberryPiFCS.Controller
{
    public class Controller : IController
    {
        private  readonly Timer _timer = new Timer(20);
        private  bool _isRunning = false;

        public Controller()
        {
            _timer.AutoReset = true;
            _timer.Elapsed += Elapsed;
        }

        public bool Lunch()
        {
            throw new NotImplementedException();
        }

        public  void Start()
        {
            _timer.Start();
        }
        private  void Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_isRunning) return;
            _isRunning = true;

            //ControlFuncion.Excute(StatusDatasBus.CenterSignal.Yaw, StatusDatasBus.CenterSignal.PitchL, StatusDatasBus.CenterSignal.PitchR, StatusDatasBus.CenterSignal.ThrottelL1, StatusDatasBus.CenterSignal.ThrottelR1, StatusDatasBus.CenterSignal.RollL, StatusDatasBus.CenterSignal.RollR, StatusDatasBus.CenterSignal.Trim);
            //LEDFunction.Excute(StatusDatasBus.CenterSignal.FlightLight, StatusDatasBus.CenterSignal.AntiCollisionLight, StatusDatasBus.CenterSignal.LogoLight, StatusDatasBus.CenterSignal.TaxiLight, StatusDatasBus.CenterSignal.RunwayLight, StatusDatasBus.CenterSignal.TakeOffLight, StatusDatasBus.CenterSignal.LandingLight, StatusDatasBus.CenterSignal.WingInspectionLight, StatusDatasBus.CenterSignal.PositionLight);
            //PushBackFunction.Excute(StatusDatasBus.CenterSignal.PushBackL1, StatusDatasBus.CenterSignal.PushBackL2, StatusDatasBus.CenterSignal.PushBackR1, StatusDatasBus.CenterSignal.PushBackR2, StatusDatasBus.CenterSignal.PushBack);

            _isRunning = false;
        }
    }
}
