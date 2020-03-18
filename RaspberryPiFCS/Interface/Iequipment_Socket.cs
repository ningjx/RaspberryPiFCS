using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RaspberryPiFCS.Interface
{
    public interface IEquipment_Socket : IEquipment
    {
        /// <summary>
        /// 绑定IP
        /// </summary>
        public IPEndPoint BindIP { get; }

        /// <summary>
        /// 目标IP
        /// </summary>
        public IPEndPoint TargetIP { get; }

        /// <summary>
        /// 来源IP
        /// </summary>
        public EndPoint OriginIP { get; }
    }
}
