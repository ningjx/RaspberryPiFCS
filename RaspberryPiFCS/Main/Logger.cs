using System;
using System.Collections.Generic;
using HelperLib;
using MavLink;
using LogType = RaspberryPiFCS.Enum.LogType;
using System.Text;

namespace RaspberryPiFCS.Main
{
    public static class Logger
    {
        public static bool ReadyToSend = false;
        private static readonly string[] path = new string[] { "Log", "SystemLog.txt" };
        private static List<string> msgBuffer = new List<string>();

        public static void Add(LogType errorType, string message, Exception ex)
        {
            string text = $"{DateTime.Now:yyyy-MM-dd hh:mm:ss fff}    异常类型:{errorType}    信息:{message}    异常信息:{ex?.Message}    异常位置:{ex?.StackTrace}\r";
            path.Write_Append(text);
        }

        public static void Add(LogType errorType, string message)
        {
            string text = $"异常类型:{errorType}    信息:{message}\r";
            msgBuffer.Add(text);
            string textLocal = $"{DateTime.Now:yyyy-MM-dd hh:mm:ss fff}    异常类型:{errorType}    信息:{message}\r";
            path.Write_Append(textLocal);
            if (ReadyToSend)
            {
                lock (msgBuffer)
                {
                    foreach (string msg in msgBuffer)
                    {
                        //发送mavlink消息
                        Msg_log msg_log = new Msg_log();
                        msg_log.time_usec = DateTime.Now.GetTimeStamp();
                        msg_log.logtype = (byte)errorType;
                        msg_log.logtext = Encoding.UTF8.GetBytes(msg);
                        EquipmentBus.MavlinkEquipment.SendMessage(msg_log);
                    }
                    msgBuffer.Clear();
                }

            }
            else
                msgBuffer.Add(text);
        }
    }
}
