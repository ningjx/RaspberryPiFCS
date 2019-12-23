
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BrandonPotter.XBox;


//namespace FlightContrlClient
//{
//    class XboxHelper
//    {
//        IEnumerable<XBoxController> xBoxController;
//        public XboxHelper()
//        {
//            xBoxController = XBoxController.GetConnectedControllers();
//        }

//        /// <summary>
//        /// 获取手柄数据
//        /// </summary>
//        /// <returns></returns>
//        public string GetXboxData()
//        {
//            int count = xBoxController.Count();
//            if (count == 0)
//                return "null";
//            string rollData = Math.Ceiling(roll).ToString();//横滚
//            string pitchData = Math.Ceiling(pitch).ToString();//俯仰
//            string yawData = Math.Ceiling(yaw).ToString();//偏航
//            string flapData = flap.ToString();//襟翼
//            string gearData = gear.ToString();//起落架
//            string throttleData = Math.Ceiling(throttle).ToString();//节流阀
//            string breakData = speedBreak.ToString();//减速板
//            string pushBackData = pushBack.ToString();//反推
//            string vnavData = vnav.ToString();//垂直导航
//            string lnavData = lnav.ToString();//水平导航
//            string customBData = customB.ToString();
//            string customADtat = customA.ToString();
//            string backData = back.ToString();
//            string startData = start.ToString();
//            string result = $"{rollData} {pitchData} {yawData} {flapData} {gearData} {throttleData} {breakData} {pushBackData} {vnavData} {lnavData} {customBData} {customADtat} {backData} {startData}";
//            return result;
//        }

//        /// <summary>
//        /// 获取横滚动作 左摇杆左右
//        /// </summary>
//        /// <returns></returns>
//        private double roll
//        {
//            get
//            { return xBoxController.FirstOrDefault().ThumbLeftX; }

//        }

//        /// <summary>
//        /// 获取俯仰动作 左摇杆上下
//        /// </summary>
//        /// <returns></returns>
//        private double pitch
//        {
//            get { return xBoxController.FirstOrDefault().ThumbLeftY; }

//        }

//        /// <summary>
//        /// 获取偏航动作 左右扳机
//        /// </summary>
//        /// <returns></returns>
//        private double yaw
//        {
//            get
//            {
//                double leftYaw = xBoxController.FirstOrDefault().TriggerLeftPosition;
//                double rightYaw = xBoxController.FirstOrDefault().TriggerRightPosition;
//                //return System.Math.Abs(leftYaw - rightYaw);
//                return rightYaw - leftYaw + 50;
//            }

//        }

//        /// <summary>
//        /// 获取襟翼动作 十字键上下
//        /// </summary>
//        /// <returns></returns>
//        private int flap
//        {
//            get
//            {
//                bool flapUP = xBoxController.FirstOrDefault().ButtonUpPressed;
//                bool flapDown = xBoxController.FirstOrDefault().ButtonDownPressed;
//                if (flapUP && !flapDown)
//                {
//                    return 1;
//                }
//                else if (!flapUP && flapDown)
//                {
//                    return -1;
//                }
//                else
//                    return 0;

//            }

//        }

//        /// <summary>
//        /// 获取起落架动作 十字键左右
//        /// </summary>
//        /// <returns></returns>
//        private int gear
//        {
//            get
//            {
//                bool gearUp = xBoxController.FirstOrDefault().ButtonLeftPressed;
//                bool gearDown = xBoxController.FirstOrDefault().ButtonRightPressed;
//                if (gearUp && !gearDown)
//                    return 1;
//                else if (!gearUp && gearDown)
//                    return -1;
//                else
//                    return 0;
//            }

//        }

//        /// <summary>
//        /// 获取节流阀动作 右摇杆上下
//        /// </summary>
//        /// <returns></returns>
//        private double throttle
//        {
//            get
//            {
//                double throttle = xBoxController.FirstOrDefault().ThumbRightY;
//                return throttle;
//            }

//        }

//        /// <summary>
//        /// 获取减速板动作 LB键
//        /// </summary>
//        /// <returns></returns>
//        private int speedBreak
//        {

//            get
//            {

//                bool lb = xBoxController.FirstOrDefault().ButtonShoulderLeftPressed;
//                if (lb)
//                    return 1;
//                else
//                    return 0;
//            }

//        }

//        /// <summary>
//        /// 获取反推动作 RB键
//        /// </summary>
//        /// <returns></returns>
//        private int pushBack
//        {
//            get
//            {
//                bool rb = xBoxController.FirstOrDefault().ButtonShoulderRightPressed;
//                if (rb)
//                    return 1;
//                else
//                    return 0;
//            }

//        }

//        /// <summary>
//        /// 垂直导航 Y键
//        /// </summary>
//        private int vnav
//        {
//            get
//            {
//                bool Y = xBoxController.First().ButtonYPressed;
//                if (Y)
//                    return 1;
//                else
//                    return 0;
//            }
//        }

//        /// <summary>
//        /// 水平导航 X键
//        /// </summary>
//        private int lnav
//        {
//            get
//            {
//                bool X = xBoxController.First().ButtonXPressed;
//                if (X)
//                    return 1;
//                else
//                    return 0;
//            }
//        }

//        /// <summary>
//        /// 自定义 B键
//        /// </summary>
//        private int customB
//        {
//            get
//            {
//                bool B = xBoxController.First().ButtonBPressed;
//                if (B)
//                    return 1;
//                else
//                    return 0;
//            }
//        }

//        /// <summary>
//        /// 自定义 A键
//        /// </summary>
//        private int customA
//        {
//            get
//            {
//                bool A = xBoxController.First().ButtonAPressed;
//                if (A)
//                    return 1;
//                else
//                    return 0;
//            }
//        }

//        /// <summary>
//        /// 自定义 Start键
//        /// </summary>
//        private int start
//        {
//            get
//            {
//                bool start = xBoxController.First().ButtonStartPressed;
//                if (start)
//                    return 1;
//                else
//                    return 0;
//            }
//        }

//        /// <summary>
//        /// 自定义 Back键
//        /// </summary>
//        private int back
//        {
//            get
//            {
//                bool back = xBoxController.First().ButtonBackPressed;
//                if (back)
//                    return 1;
//                else
//                    return 0;
//            }
//        }
//    }
//}
