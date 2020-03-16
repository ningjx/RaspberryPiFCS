using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RaspberryPiFCS.Enum;

namespace RaspberryPiFCS.SystemMessage
{
    public static class ErrorMessage
    {
        private static FileStream fileStream;
        static ErrorMessage()
        {
            string[] path = new string[] { "Log", "SystemLog.txt" };
            string pathStr = Path.Combine(path);
            string dicPath = Path.Combine(path.GetByCount(path.Length - 1));
            if (!Directory.Exists(dicPath))
                Directory.CreateDirectory(dicPath);
            fileStream = new FileStream(pathStr, FileMode.Append);
        }

        public static void Add(ErrorType errorType, string message, Exception ex)
        {
            ErrorData.Add(new Tuple<DateTime, ErrorType, string, Exception>(DateTime.Now, errorType, message, ex));
            //写入日志文件
            string text = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}    异常类型:{errorType.GetType().Name}    信息:{message}    异常信息:{ex.Message}    异常位置:{ex.StackTrace}";
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            fileStream.Write(bytes);
            //触发事件
            if (LoraEvent != null)
                LoraEvent.Invoke(errorType, message, ex);
        }

        public static List<Tuple<DateTime, ErrorType, string, Exception>> ErrorData { get; }

        public delegate void ErrorMessageHandler(ErrorType errorType, string message, Exception ex);

        public static event ErrorMessageHandler LoraEvent;
    }
}
