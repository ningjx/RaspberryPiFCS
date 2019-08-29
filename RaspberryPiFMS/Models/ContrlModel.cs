using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPiFMS.Models
{
    /// <summary>
    /// 控制数据模型，所有控件具体参数
    /// </summary>
    public class ContrlModel
    {
        /// <summary>
        /// 滚转角度
        /// </summary>
        public double roll;
        /// <summary>
        /// 偏航角度
        /// </summary>
        public double yaw;
        /// <summary>
        /// 俯仰角度
        /// </summary>
        public double pitch;

        /// <summary>
        /// 起落架角度
        /// </summary>
        public double gear;

        /// <summary>
        /// 减速板角度
        /// </summary>
        public double airBreak;

        /// <summary>
        /// 襟翼角度
        /// </summary>
        public double flap;

        /// <summary>
        /// 反推角度
        /// </summary>
        public double pushBack;

        /// <summary>
        /// 配平角度
        /// </summary>
        public double trim;

        #region 灯光组
        /// <summary>
        /// 滑行灯
        /// </summary>
        public double taxiLight;
        /// <summary>
        /// 跑道脱离灯
        /// </summary>
        public double runwayLight;
        /// <summary>
        /// 标志灯
        /// </summary>
        public double logoLight;
        /// <summary>
        /// 着陆灯
        /// </summary>
        public double landingLight;
        /// <summary>
        /// 机翼检查灯
        /// </summary>
        public double wingInspectionLight;
        /// <summary>
        /// 位置灯
        /// </summary>
        public double positionLight;
        /// <summary>
        /// 防撞灯
        /// </summary>
        public double antiCollisionLight;
        #endregion
    }
}
