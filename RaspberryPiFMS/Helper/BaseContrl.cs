using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiFMS.Models;

namespace RaspberryPiFMS.Helper
{
    public class BaseContrl
    {
        public ContrlModel contrlData;
        private ContrlModel buffData;

        private Pca9685 pca;

        public BaseContrl()
        {
            pca = new Pca9685();
            contrlData = new ContrlModel();
            buffData = new ContrlModel();
            ThreadStart threadStart = () => Excute();
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void Excute()
        {
            while (true)
            {
                Thread.Sleep(100);
                if(contrlData.roll != buffData.roll)
                {
                    pca.SetPWMAngle(0, contrlData.roll);
                    //pca.SetPWMAngle(15,80 - contrlData.roll);
                }
                    pca.SetPWMAngle(0, contrlData.roll);
                if(contrlData.yaw != buffData.yaw)
                    pca.SetPWMAngle(1, contrlData.yaw);
                if(contrlData.pitch != buffData.pitch)
                    pca.SetPWMAngle(2, contrlData.pitch);
                if(contrlData.gear != buffData.gear)
                    pca.SetPWMAngle(3, contrlData.gear);
                if(contrlData.airBreak != buffData.airBreak)
                    pca.SetPWMAngle(4, contrlData.airBreak);
                if(contrlData.flap != buffData.flap)
                    pca.SetPWMAngle(5, contrlData.flap);
                if(contrlData.pushBack != buffData.pushBack)
                    pca.SetPWMAngle(6, contrlData.pushBack);
                if(contrlData.trim != buffData.trim)
                    pca.SetPWMAngle(7, contrlData.trim);
                if (contrlData.throttel != buffData.throttel)
                    pca.SetPWMAngle(15, contrlData.throttel);


                if (contrlData.taxiLight != buffData.taxiLight)
                    pca.SetPWMAngle(8, contrlData.taxiLight);
                if(contrlData.runwayLight != buffData.runwayLight)
                    pca.SetPWMAngle(9, contrlData.runwayLight);
                if(contrlData.logoLight != buffData.logoLight)
                    pca.SetPWMAngle(10, contrlData.logoLight);
                if(contrlData.landingLight != buffData.landingLight)
                    pca.SetPWMAngle(11, contrlData.landingLight);
                if(contrlData.wingInspectionLight != buffData.wingInspectionLight)
                    pca.SetPWMAngle(12, contrlData.wingInspectionLight);
                if(contrlData.positionLight != buffData.positionLight)
                    pca.SetPWMAngle(13, contrlData.positionLight);
                if (contrlData.antiCollisionLight != buffData.antiCollisionLight)
                    pca.SetPWMAngle(14, contrlData.antiCollisionLight);

                buffData = contrlData;
            }
        }
    }
}
