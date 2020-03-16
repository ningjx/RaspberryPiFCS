using System;
namespace RaspberryPiFCS.Interface
{
    public interface IFunction
    {
        void Excute( params object[] datas);
    }
    
}
