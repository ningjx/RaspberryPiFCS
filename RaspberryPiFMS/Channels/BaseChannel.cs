namespace RaspberryPiFMS.Channels
{
    public enum BaseChannel
    {
        /// <summary>
        /// 滚转
        /// </summary>
        RollL = 0,
        RollR = 1,
        /// <summary>
        /// 偏航
        /// </summary>
        Yaw = 2,
        /// <summary>
        /// 俯仰
        /// </summary>
        PitchL = 3,
        PitchR = 4,
        /// <summary>
        /// 减速板
        /// </summary>
        AirBreakL = 5,
        AirBreakR = 6,
        /// <summary>
        /// 配平
        /// </summary>
        Trim = 7,
        /// <summary>
        /// 节流阀
        /// </summary>
        ThrottelL = 8,
        ThrottelR = 9,

        GearF = 10,
        GearBL = 11,
        GearBR = 12,

        FlapL = 13,
        FlapR = 14,

        //EnginePowerL = 15,
        //EnginePowerR = 16
    }
}
