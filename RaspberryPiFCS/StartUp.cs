using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS
{
    public static class StartUp
    {
        static StartUp()
        {
            //先启动日志
            //加载配置文件
            ConfigService.ReadConfig();
            //初始化系统
            ProcessService.Init();
            ProcessService.StartUp();


            //退出
        }

    }
}
