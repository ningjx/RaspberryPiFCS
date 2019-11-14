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
        FilghtLight = 1,
        /// <summary>
        /// Logo灯
        /// </summary>
        LogoLight = 2,
        /// <summary>
        /// 红色信标灯（闪烁）
        /// </summary>
        AntiCollisionLight = 3,
        /// <summary>
        /// 滑行灯
        /// </summary>
        TaxiLight = 4,
        /// <summary>
        /// 起飞
        /// </summary>
        TakeoffLight = 5,
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        RunwayLight = 6,
        /// <summary>
        /// 着陆灯
        /// </summary>
        LandingLight =7,
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        WingInspectionLight = 8,
        /// <summary>
        /// 白色防撞灯
        /// </summary>
        AntiCollisionLightWhite = 9,

        PushBack_L = 10,

        PushBack_R = 11
    }
}
