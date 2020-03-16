namespace RaspberryPiFCS.Enum
{
    /// <summary>
    /// 飞行控制模式
    /// </summary>
    public enum ContrlMode
    {
        /// <summary>
        /// 自动驾驶关闭
        /// </summary>
        Manual = 0,

        /// <summary>
        /// 水平导航
        /// </summary>
        LateralNavigation = 1,

        /// <summary>
        /// 垂直导航
        /// </summary>
        VerticalNavigation = 2,

        APOn = 3,

        AutoSpeed = 4
    }
}
