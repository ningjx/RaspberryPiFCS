using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Interface;
using RaspberryPiFCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace RaspberryPiFCS.Fuctions
{
    /// <summary>
    /// 远程配置功能
    /// </summary>
    public class RemoteSettingFunction : IFunction
    {
        public int RetryTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Timer Timer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Lock { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FunctionStatus FunctionStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public RelyEquipment RelyEquipment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Excute(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
