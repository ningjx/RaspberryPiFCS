using RaspberryPiFMS.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Controller
{
    public class PushBackController
    {
        private Pca9685 _MotorDriver;
        public PushBackController(Pca9685 motorDriver)
        {
            _MotorDriver = motorDriver;
        }
    }
}
