using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RaspberryPiFMS.Helper
{
    public class PIDHelper
    {
        private float SetSpeed; //定义设定值     
        private float ActualSpeed; //定义实际值    
        private float err; //定义偏差值   
        private float err_next; //定义上一个偏差值     
        private float err_last;  //定义最上前的偏差值      
        private float Kp, Ki, Kd; //定义比例、积分、微分系数 
        private int currentId = 0;//保持最后一个调用在输出
        private float currentTar = 0;

        public PIDHelper(float kp = 0.1f,float ki = 0.3f,float kd = 0.2f)
        {
            SetSpeed = 0;
            ActualSpeed = 0;
            err = 0;
            err_last = 0;
            err_next = 0;
            Kp = kp;
            Ki = ki;
            Kd = kd;
        }
 
        public void SetWithPID(float now,float target)
        {
            if (currentTar == target)
                return;
            currentTar = target;
            currentId++;
            int ownId = currentId;
            bool reverse = false;
            if(target< now)
            {
                var buf = target;
                target = now;
                now = buf;
                reverse = true;
            }
            //重置
            ActualSpeed = now;
            err = 0;
            err_last = 0;
            err_next = 0;

            int time = 0;
            double nowresult = 0;
            double lastresult = 0;
            while(Math.Abs(target - (nowresult = PIDCaculate(target))) > 0.1 && ownId == currentId)
            {
                if (lastresult > nowresult)
                    continue;
                if(time%2==0&&!reverse)
                    PIDOutEvent.Invoke(nowresult);
                else if(time % 2 == 0 && reverse)
                    PIDOutEvent.Invoke(target - nowresult);
                lastresult = nowresult;
                time++;
            }
            if(ownId == currentId)
                PIDOutEvent.Invoke(target);
        }

        private double PIDCaculate(float target)  
        {
            SetSpeed = target;
            err = SetSpeed - ActualSpeed;
            var incrementSpeed = Kp * (err - err_next + Ki * err + Kd * (err - 2 * err_next + err_last)); 
            ActualSpeed += incrementSpeed;
            err_last = err_next;
            err_next = err;
            Thread.Sleep(30);
            return ActualSpeed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">当前输出的值</param>
        public delegate void PIDHandller(double value);
        public event PIDHandller PIDOutEvent;
    }
}
