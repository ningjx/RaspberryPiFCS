using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Enum
{
    public enum BaseChannel
    {
        /// <summary>
        /// 偏航
        /// </summary>
        Yaw = 1,
        /// <summary>
        /// 滚转
        /// </summary>
        Roll = 2,
        /// <summary>
        /// 俯仰
        /// </summary>
        Pitch = 3,
        /// <summary>
        /// 配平
        /// </summary>
        Trim = 4,
        /// <summary>
        /// 节流阀
        /// </summary>
        Throttel = 5,
        /// <summary>
        /// 襟翼
        /// </summary>
        Flaps = 6,
        /// <summary>
        /// 减速板
        /// </summary>
        AirBreak = 7,
    }
}
