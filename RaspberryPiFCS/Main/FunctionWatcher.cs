using RaspberryPiFCS.Enum;
using RaspberryPiFCS.Fuctions;
using RaspberryPiFCS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaspberryPiFCS.Main
{
    /// <summary>
    /// 修改watcher的功能，在启动watcher时，通过watcher去收集所有function（但不启动）
    /// 然后将所有function信息发送到地面站
    /// 通过地面站去手动启动function
    /// </summary>
    public static class FunctionWatcher
    {
        public static Dictionary<string, FunctionStatus> Functions = new Dictionary<string, FunctionStatus>();

        private static List<IFunction> FunctionInstances = new List<IFunction>();
        public static void Lunch()
        {
            var funcTypes = typeof(IFunction).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass);
            var functionNames = funcTypes.Select(t => t.Name).ToList();
            functionNames.ForEach(t => Functions.Add(t, FunctionStatus.Offline));
            //发送mavlink message
            //Msg_sys_status message = new Msg_sys_status();
            //EquipmentBus.MavlinkEquipment.SendMessage(message);

            EquipmentBus.MavlinkEquipment.RecivePacket += SetFunction;

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        foreach (var func in FunctionInstances)
                        {
                            Functions.AddOrUpdate(func.GetType().Name, func.FunctionStatus);
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
        private static void SetFunction(MavLink.MavlinkPacket packet)
        {
            if(packet.ComponentId == 0)
            {

            }
        }

        public static void LunchFailure()
        {
            var failureNames = Functions.Where(t => t.Value == FunctionStatus.Failure).Select(t => t.Key);
            if (failureNames.Count() == 0)
                return;
            var needDalete = FunctionInstances.Where(t => failureNames.Contains(t.GetType().Name)).ToList();
            needDalete.ForEach(t => FunctionInstances.Remove(t));
            var funcTypes = typeof(IFunction).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass).Where(t => failureNames.Contains(t.Name));
            foreach (Type type in funcTypes)//所有function Online
            {
                FunctionInstances.Add(Activator.CreateInstance(type) as IFunction);
            }
        }
    }
}
