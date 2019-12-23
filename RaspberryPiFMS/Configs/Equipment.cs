using System;
using System.Collections.Generic;
using System.Linq;

namespace RaspberryPiFMS.Configs
{
    //[Obsolete("不要使用这个直接初始化配置文件，要初始化请调用Config.InitConfig()")]
    public class Equipment : Dictionary<string, int>
    {
        public new void Add(string name, int addr)
        {
            if (Keys.Contains(name) && !Values.Intersect(new int[] { this[name] }).Contains(addr))
            {
                this[name] = addr;
                return;
            }
            else if (Keys.Contains(name) && Values.Intersect(new int[] { this[name] }).Contains(addr))
                throw new Exception("不能在设备列表中保存相同的名字或设备地址");
            else if (Values.Contains(addr))
                throw new Exception("不能在设备列表中保存相同的名字或设备地址");
            base.Add(name, addr);
        }
    }
}
