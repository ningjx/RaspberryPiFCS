using System;
namespace RaspberryPiFMS.Interface
{
    public interface IFunction
    {
        void Excute(bool excute, params object[] datas);
    }
}
