using MavLink;
using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Fuctions;
using RaspberryPiFCS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using FunctionStatus = RaspberryPiFCS.Enum.FunctionStatus;
using Timer = System.Timers.Timer;

namespace RaspberryPiFCS.Main
{
    /// <summary>
    /// 将function的excute订阅到watcher
    /// 然后将所有function信息发送到地面站
    /// 通过地面站去手动启动function
    /// </summary>
    public static class FunctionWatcher
    {
        private static Dictionary<string, IFunction> Functions = new Dictionary<string, IFunction>();
        private static Timer Timer = new Timer(20);
        public static void Lunch()
        {
            Timer.AutoReset = true;
            Timer.Enabled = true;

            var funcTypes = typeof(IFunction).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass).ToList();
            //初始化功能
            funcTypes.ForEach(t =>
            {
                Functions.Add(t.Name, Activator.CreateInstance(t) as IFunction);
            });
            EquipmentBus.MavlinkEquipment.RecivePacket += SetFunction;

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        foreach (var func in Functions)
                        {
                            Msg_functionstatus msg_Functionstatus = new Msg_functionstatus();
                            msg_Functionstatus.functionname = Encoding.UTF8.GetBytes(func.Key);
                            msg_Functionstatus.status = (byte)func.Value.FunctionStatus;
                            msg_Functionstatus.time_usec = DateTime.Now.GetTimeStamp();
                            EquipmentBus.MavlinkEquipment.SendMessage(msg_Functionstatus);
                        }
                    }
                    catch
                    {

                    }
                }
            });
        }

        /// <summary>
        /// 通过message操作function的状态
        /// </summary>
        /// <param name="packet"></param>
        private static void SetFunction(MavlinkPacket packet)
        {
            if (packet.Message.GetType().Name == "Msg_setfunctionstatus")
            {
                var message = packet.Message as Msg_setfunctionstatus;
                switch ((int)message.status)
                {
                    case (int)FunctionStatus.Online:
                        Timer.Elapsed += Functions[Encoding.UTF8.GetString(message.functionname)].Excute;
                        break;
                    case (int)FunctionStatus.Offline:
                        Timer.Elapsed -= Functions[Encoding.UTF8.GetString(message.functionname)].Excute;
                        break;
                    case (int)FunctionStatus.Failure:
                        break;
                }
            }
        }
    }
}
