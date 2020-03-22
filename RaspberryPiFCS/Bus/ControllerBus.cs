using RaspberryPiFCS.Models;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Controller;
using RaspberryPiFCS.Helper;
using System;
using RaspberryPiFCS.ComputeCenter;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using RaspberryPiFCS.SystemMessage;

namespace RaspberryPiFCS
{
    /// <summary>
    /// 逻辑控制器
    /// </summary>
    public static class ControllerBus
    {

        static ControllerBus()
        {

        }

        public static void SysLaunch()
        {
            
        }

        private static bool LunchLora()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorMessage.Add(Enum.ErrorType.Error, "启动远程数传时失败", ex);
                return false;
            }
            return true;
        }
    }
}
