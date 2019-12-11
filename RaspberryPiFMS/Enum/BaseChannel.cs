namespace RaspberryPiFMS.Enum
{
    public enum BaseChannel
    {
        /// <summary>
        /// 滚转
        /// </summary>
        RollL = 1,
        RollR = 2,
        /// <summary>
        /// 偏航
        /// </summary>
        Yaw = 3,
        /// <summary>
        /// 俯仰
        /// </summary>
        PitchL = 4,
        PitchR = 5,
        /// <summary>
        /// 减速板
        /// </summary>
        AirBreakL = 6,
        AirBreakR = 7,
        /// <summary>
        /// 配平
        /// </summary>
        Trim = 8,
        /// <summary>
        /// 节流阀
        /// </summary>
        ThrottelL = 9,
        ThrottelR = 10,

        GearF = 11,
        GearBL = 12,
        GearBR = 13,

        FlapL = 14,
        FlapR = 15,

        //EnginePowerL = 15,
        //EnginePowerR = 16
    }
}
