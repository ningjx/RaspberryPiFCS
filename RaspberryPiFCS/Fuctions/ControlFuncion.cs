using Iot.Device.Pwm;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Fuctions
{
    public class ControlFuncion: IFunction
    {
        private static ControlFuncion controlFuncion;
        public static ControlFuncion Instance
        {
            get 
            {
                if (controlFuncion == null)
                    controlFuncion = new ControlFuncion();
                return controlFuncion;
            }
        }

        public void Excute<Pca9685>(CenterSignal signal, Pca9685 equipment)
        {
            
        }
    }
}
