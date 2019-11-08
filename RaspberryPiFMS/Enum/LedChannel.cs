using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Enum
{
    public enum LedChannel
    {
        /// <summary>
        /// 航行灯（翼尖闪烁）
        /// </summary>
        LogoLight = 1,
        /// <summary>
        /// 红色信标灯（闪烁）
        /// </summary>
        AntiCollisionLight = 2,
        /// <summary>
        /// 滑行灯
        /// </summary>
        TaxiLight = 3,
        /// <summary>
        /// 机头灯
        /// </summary>
        TakeoffLight = 4,
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        RunwayLight = 5,
        /// <summary>
        /// 着陆灯
        /// </summary>
        LandingLight = 6,
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        WingInspectionLight = 7,
        /// <summary>
        /// 白色防撞灯
        /// </summary>
        AntiCollisionLightWhite = 8,

        PushBack_L = 9,

        PushBack_R = 10
    }
}
