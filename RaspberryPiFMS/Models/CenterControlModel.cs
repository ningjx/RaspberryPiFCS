using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Models
{
    public class CenterControlModel
    {
        /// <summary>
        /// 滚转角度
        /// </summary>
        public double Roll;
        /// <summary>
        /// 偏航角度
        /// </summary>
        public double Yaw;
        /// <summary>
        /// 俯仰角度
        /// </summary>
        public double Pitch;

        /// <summary>
        /// 起落架角度
        /// </summary>
        public double Gear;

        /// <summary>
        /// 减速板角度
        /// </summary>
        public double AirBreak;

        /// <summary>
        /// 襟翼角度
        /// </summary>
        public double Flap;

        /// <summary>
        /// 反推角度
        /// </summary>
        public double PushBack;

        /// <summary>
        /// 配平角度
        /// </summary>
        public double Trim;

        /// <summary>
        /// 节流阀
        /// </summary>
        public double Throttel;

        #region 灯光组
        /// <summary>
        /// 滑行灯
        /// </summary>
        public double TaxiLight;
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        public double RunwayLight;
        /// <summary>
        /// 标志灯
        /// </summary>
        public double LogoLight;
        /// <summary>
        /// 着陆灯
        /// </summary>
        public double LandingLight;
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        public double WingInspectionLight;
        /// <summary>
        /// 位置灯
        /// </summary>
        public double PositionLight;
        /// <summary>
        /// 防撞灯
        /// </summary>
        public double AntiCollisionLight;
        #endregion

    }
}
