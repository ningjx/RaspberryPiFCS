using RaspberryPiFCS.Enum;
using System;
using System.Collections.Generic;
using HelperLib;

namespace RaspberryPiFCS.BaseController
{
    public static class Logger
    {
        private static readonly string[] path = new string[] { "Log", "SystemLog.txt" };

        public static void Add(ErrorType errorType, string message, Exception ex)
        {
            ErrorData.Add(new Tuple<DateTime, ErrorType, string, Exception>(DateTime.Now, errorType, message, ex));
            //写入日志文件
            string text = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}    异常类型:{errorType.ToString()}    信息:{message}    异常信息:{ex.Message}    异常位置:{ex.StackTrace}";
            path.Write_Append(text);
        }

        public static List<Tuple<DateTime, ErrorType, string, Exception>> ErrorData { get; }
    }
}
