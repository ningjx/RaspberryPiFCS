using RaspberryPiFCS.Enum;
using System;
using System.Collections.Generic;
using HelperLib;
using System.Text;
using MavLink.Message;

namespace RaspberryPiFCS.Main
{
    public static class Logger
    {
        public static bool ReadyToSend = false;
        private static readonly string[] path = new string[] { "Log", "SystemLog.txt" };
        private static List<Tuple<DateTime, LogType, string, Exception>> ErrorData { get; } = new List<Tuple<DateTime, LogType, string, Exception>>();

        public static void Add(LogType errorType, string message, Exception ex = null)
        {
            ErrorData.Add(new Tuple<DateTime, LogType, string, Exception>(DateTime.Now, errorType, message, ex));
            string text = $"{DateTime.Now:yyyy-MM-dd hh:mm:ss fff}    异常类型:{errorType}    信息:{message}    异常信息:{ex?.Message}    异常位置:{ex?.StackTrace}\r";
            path.Write_Append(text);
            if (ReadyToSend)
            {
                lock (ErrorData)
                {
                    ErrorData.ForEach(t =>
                    {
                        string text = $"{t.Item1:yyyy-MM-dd hh:mm:ss fff}    异常类型:{t.Item2}    信息:{t.Item3}    异常信息:{t.Item4?.Message}";
                        //发送mavlink消息
                        //Msg_sys_status message = new Msg_sys_status();
                        //EquipmentBus.MavlinkEquipment.SendMessage(message);
                    });
                    ErrorData.Clear();
                }
            }
        }
    }
}
