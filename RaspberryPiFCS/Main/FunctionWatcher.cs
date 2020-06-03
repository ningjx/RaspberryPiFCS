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
    public static class FunctionWatcher
    {
        public static Dictionary<string, FunctionStatus> Functions = new Dictionary<string, FunctionStatus>();

        private static List<IFunction> FunctionInstances = new List<IFunction>();
        public static void Lunch()
        {
            var funcTypes = typeof(IFunction).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass);
            var functionNames = funcTypes.Select(t => t.Name).ToList();
            functionNames.ForEach(t => Functions.Add(t, FunctionStatus.Offline));

            foreach (Type type in funcTypes)//所有function Online
            {
                FunctionInstances.Add(Activator.CreateInstance(type) as IFunction);
            }

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

        public static void LunchFailure()
        {
            var failureNames = Functions.Where(t => t.Value == FunctionStatus.Failure).Select(t => t.Key);
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
