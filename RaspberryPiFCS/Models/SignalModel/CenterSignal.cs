using RaspberryPiFCS.Enum;

namespace RaspberryPiFCS.Models
{
    /// <summary>
    /// 中心控制信号
    /// </summary>
    public class CenterSignal
    {
        /// <summary>
        /// 滚转角度
        /// </summary>
        public float RollL;
        public float RollR;
        public float Roll;
        /// <summary>
        /// 偏航角度
        /// </summary>
        public float YawL;
        public float YawR;
        public float Yaw;
        /// <summary>
        /// 俯仰角度
        /// </summary>
        public float PitchL;
        public float PitchR;
        public float Pitch;
        /// <summary>
        /// 减速板角度
        /// </summary>
        public float AirBreakL;
        public float AirBreakR;
        public float AirBreak;
        /// <summary>
        /// 配平角度
        /// </summary>
        public float TrimL;
        public float TrimR;
        public float Trim;
        /// <summary>
        /// 节流阀
        /// </summary>
        public float ThrottelL1;
        public float ThrottelL2;
        public float ThrottelR1;
        public float ThrottelR2;
        public float Throttel;
        /// <summary>
        /// 起落架
        /// </summary>
        public bool Gear = true;
        /// <summary>
        /// 襟翼
        /// </summary>
        public FlapMode Flap = FlapMode.FlapUp;
        /// <summary>
        /// 反推
        /// </summary>
        public bool PushBackL1 = false;
        public bool PushBackL2 = false;
        public bool PushBackR1 = false;
        public bool PushBackR2 = false;
        public bool PushBack = false;
        /// <summary>
        /// 发动机电源左
        /// </summary>
        public bool EnginePowerL = false;
        /// <summary>
        /// 发动机电源右
        /// </summary>
        public bool EnginePowerR = false;


        #region 灯光信号
        //1
        /// <summary>
        /// 航行灯
        /// </summary>
        public bool FlightLight;
        /// <summary>
        /// 防撞灯
        /// </summary>
        public bool AntiCollisionLight;
        /// <summary>
        /// Logo灯
        /// </summary>
        public bool LogoLight;
        //2
        /// <summary>
        /// 滑行灯
        /// </summary>
        public bool TaxiLight;
        //3
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        public bool RunwayLight;
        //4
        /// <summary>
        /// 起飞灯
        /// </summary>
        public bool TakeOffLight;
        //5
        /// <summary>
        /// 着陆灯
        /// </summary>
        public bool LandingLight;
        //6
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        public bool WingInspectionLight;
        /// <summary>
        /// 位置灯
        /// </summary>
        public bool PositionLight;
        #endregion
    }
}
