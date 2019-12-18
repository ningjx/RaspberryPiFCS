using RaspberryPiFMS.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Fuctions
{
    public class BaseFuncion : IFunction
    {
        private static readonly IFunction _instance = new BaseFuncion();
        public static IFunction Instance => _instance;

        public void Excute(bool excute, params object[] datas)
        {
            if (excute == false)
                return;










            excute = false;
        }
    }
}
