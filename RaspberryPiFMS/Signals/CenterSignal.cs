using RaspberryPiFMS.Enum;
using System.Collections.Generic;

namespace RaspberryPiFMS.Models
{
    /// <summary>
    /// 中心控制信号
    /// </summary>
    public class CenterSignal
    {
        /// <summary>
        /// 滚转角度
        /// </summary>
        public double RollL;
        public double RollR;
        /// <summary>
        /// 偏航角度
        /// </summary>
        public double Yaw;
        /// <summary>
        /// 俯仰角度
        /// </summary>
        public double PitchL;
        public double PitchR;
        /// <summary>
        /// 减速板角度
        /// </summary>
        public double AirBreakL;
        public double AirBreakR;
        /// <summary>
        /// 配平角度
        /// </summary>
        public double Trim;
        /// <summary>
        /// 节流阀
        /// </summary>
        public double ThrottelL;
        public double ThrottelR;
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
