namespace RaspberryPiFMS.Enum
{
    public enum LedChannel
    {
        /// <summary>
        /// 航行灯（翼尖闪烁）
        /// </summary>
        FilghtLightL = 1,
        FilghtLightR = 2,
        /// <summary>
        /// Logo灯
        /// </summary>
        LogoLight = 3,
        /// <summary>
        /// 红色信标灯（闪烁）
        /// </summary>
        AntiCollisionLight = 4,
        /// <summary>
        /// 滑行灯
        /// </summary>
        TaxiLight = 5,
        /// <summary>
        /// 起飞
        /// </summary>
        TakeoffLight = 6,
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        RunwayLight = 7,
        /// <summary>
        /// 着陆灯
        /// </summary>
        LandingLight = 8,
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        WingInspectionLight = 9,
        /// <summary>
        /// 白色防撞灯
        /// </summary>
        AntiCollisionLightWhite = 10,

        PushBack_L = 11,

        PushBack_R = 12
    }
}
