using System;
using System.Collections.Generic;
using RaspberryPiFCS.Enum;
using HelperLib;

namespace RaspberryPiFCS.Models
{
    public class Logger
    {
        //private static FileStream fileStream;
        private string[] path = new string[] { "Log", "SystemLog.txt" };
        public Logger(string[] path = null)
        {
            if (path != null)
            {
                this.path = path;
            }
        }

        public void Add(ErrorType errorType, string message, Exception ex)
        {
            ErrorData.Add(new Tuple<DateTime, ErrorType, string, Exception>(DateTime.Now, errorType, message, ex));
            //写入日志文件
            string text = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}    异常类型:{errorType.GetType().Name}    信息:{message}    异常信息:{ex.Message}    异常位置:{ex.StackTrace}";
            path.Write_Append(text);
            //触发事件
            LoraEvent?.Invoke(errorType, message, ex);
        }

        public List<Tuple<DateTime, ErrorType, string, Exception>> ErrorData { get; }

        public event ErrorMessageHandler LoraEvent;
    }
}
