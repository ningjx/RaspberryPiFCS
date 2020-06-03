using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryPiFCS.Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFCS.Main.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Logger.Add(Enum.LogType.Error, "测试日志系统1", new Exception("日志打印测试"));
            Logger.Add(Enum.LogType.Error, "测试日志系统2", new Exception("日志打印测试"));
            Logger.Add(Enum.LogType.Error, "测试日志系统3", new Exception("日志打印测试"));
            Logger.Add(Enum.LogType.Error, "测试日志系统4", new Exception("日志打印测试"));
            Logger.Add(Enum.LogType.Error, "测试日志系统5", new Exception("日志打印测试"));
        }
    }
}