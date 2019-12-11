using RaspberryPiFMS.Enum;

namespace RaspberryPiFMS.Models
{
    public static class CenterData
    {
        /// <summary>
        /// 滚转角度
        /// </summary>
        public static double RollL;
        public static double RollR;
        /// <summary>
        /// 偏航角度
        /// </summary>
        public static double Yaw;
        /// <summary>
        /// 俯仰角度
        /// </summary>
        public static double PitchL;
        public static double PitchR;
        /// <summary>
        /// 减速板角度
        /// </summary>
        public static double AirBreakL;
        public static double AirBreakR;
        /// <summary>
        /// 配平角度
        /// </summary>
        public static double Trim;
        /// <summary>
        /// 节流阀
        /// </summary>
        public static double ThrottelL;
        public static double ThrottelR;
        /// <summary>
        /// 起落架
        /// </summary>
        public static bool Gear = true;
        /// <summary>
        /// 襟翼
        /// </summary>
        public static FlapMode Flap = FlapMode.FlapUp;
        /// <summary>
        /// 反推
        /// </summary>
        public static bool PushBack = false;
        /// <summary>
        /// 发动机电源左
        /// </summary>
        public static bool EnginePowerL = false;
        /// <summary>
        /// 发动机电源右
        /// </summary>
        public static bool EnginePowerR = false;
        /// <summary>
        /// 近进高度
        /// </summary>
        public static float MicroAltitude;
        /// <summary>
        /// 气压高度
        /// </summary>
        public static float Altitude;
        /// <summary>
        /// 空速
        /// </summary>
        public static float AirSpeed;
        /// <summary>
        /// 地速
        /// </summary>
        public static float GroundSpeed;

        #region 灯光组
        //1
        /// <summary>
        /// 航行灯
        /// </summary>
        public static bool FlightLight;
        /// <summary>
        /// 防撞灯
        /// </summary>
        public static bool AntiCollisionLight;
        /// <summary>
        /// Logo灯
        /// </summary>
        public static bool LogoLight;
        //2
        /// <summary>
        /// 滑行灯
        /// </summary>
        public static bool TaxiLight;
        //3
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        public static bool RunwayLight;
        //4
        /// <summary>
        /// 起飞灯
        /// </summary>
        public static bool TakeOffLight;
        //5
        /// <summary>
        /// 着陆灯
        /// </summary>
        public static bool LandingLight;
        //6
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        public static bool WingInspectionLight;
        /// <summary>
        /// 位置灯
        /// </summary>
        public static bool PositionLight;
        #endregion
    }
}
