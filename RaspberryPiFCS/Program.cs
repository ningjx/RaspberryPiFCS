using RaspberryPiFCS.Configs;
using RaspberryPiFCS.Models;
using RaspberryPiFCS.SystemMessage;
using System;
using System.Threading;

namespace RaspberryPiFCS
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //ErrorMessage
                Config.ReadConfig();

                while (SysSwitch.Switch)
                {

                }
            }
            catch (Exception ex)
            {
                Message.Add(Enum.ErrorType.Error, "系统异常", ex);
            }
            finally
            {
                Config.SaveConfig();
            }
        }
    }
    public static class SysSwitch
    {
        public static bool Switch = true;
    }
}
