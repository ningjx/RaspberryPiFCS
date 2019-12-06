namespace RaspberryPiFMS.Enum
{
    public enum BaseChannel
    {
        /// <summary>
        /// 偏航
        /// </summary>
        Yaw = 1,
        //YawL = 2,
        //YawR = 3,
        /// <summary>
        /// 滚转
        /// </summary>
        //Roll = 5,
        RollL = 2,
        RollR = 3,
        /// <summary>
        /// 俯仰
        /// </summary>
        //Pitch = 9,
        PitchL = 4,
        PitchR = 5,
        /// <summary>
        /// 配平
        /// </summary>
        //Trim = 9,
        //TrimL = 9,
        //TrimR = 9,
        /// <summary>
        /// 节流阀
        /// </summary>
        Throttel = 6,
        //ThrottelL1 = 7,
        //ThrottelL2 = 8,
        //ThrottelR1 = 9,
        //ThrottelR2 = 10,

        /// <summary>
        /// 襟翼
        /// </summary>
        //Flaps = 7,
        /// <summary>
        /// 减速板
        /// </summary>
        //AirBreak = 8,
        //AirBreakL =9,
        //AirBreakR = 10
    }
}
