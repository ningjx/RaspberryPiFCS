using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaspberryPiFCS.Enum;

namespace RaspberryPiFCS.Models
{
    /// <summary>
    /// 控制器注册
    /// </summary>
    public class SysRegister
    {
        
        private List<RegistedEquipment> RelyControllers = new List<RegistedEquipment>();
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="controllerType"></param>
        /// <param name="isOnly"></param>
        public void Register(RegisterType controllerType,bool isOnly)
        {
            if (RelyControllers.Where(t => t.ControllerType == controllerType && t.IsOnly).Count() != 0)
                throw new Exception("该设备不允许重复注册");
            RelyControllers.Add(new RegistedEquipment(controllerType, isOnly));
        }
        public bool CheckRely(RelyConyroller relyConyrollers)
        {
            bool result = true;
            List<int> current = RelyControllers.Select(t => t.ControllerType.GetHashCode()).ToList();
            foreach (var item in relyConyrollers.RelyConyrollers)
            {
                if (!current.Contains(item.GetHashCode()))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }

    public class RelyConyroller
    {
        public List<RegisterType> RelyConyrollers = new List<RegisterType>();
        public void Add(RegisterType controllerType)
        {
            RelyConyrollers.Add(controllerType);
        }
    }

    class RegistedEquipment
    {
        public RegisterType ControllerType;
        public bool IsOnly = false;
        public RegistedEquipment(RegisterType controllerType, bool isOnly)
        {
            ControllerType = controllerType;
            IsOnly = isOnly;
        }
    }
}
