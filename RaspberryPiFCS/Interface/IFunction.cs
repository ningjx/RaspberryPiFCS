using System;
using RaspberryPiFCS.Models;

namespace RaspberryPiFCS.Interface
{
    public interface IFunction
    {
        void Excute<T>(CenterSignal signal, T equipment);
    }
    
}
