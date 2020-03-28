﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaneInstrumentControlLibrary.B737EICAS
{
    public partial class B737EICAS : InstrumentControl
    {
        B737EICASSound sound;
        public B737EICAS()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint, true);
            sound = new B737EICASSound();
        }
        Bitmap backGroung = new Bitmap(B737EICASResource.background);
        Bitmap cover1 = new Bitmap(B737EICASResource.cover1);
        Bitmap cover2 = new Bitmap(B737EICASResource.cover2);
        Bitmap insBack = new Bitmap(B737EICASResource.insBack);
        Bitmap insBack2 = new Bitmap(B737EICASResource.insBack2);
        Bitmap top = new Bitmap(B737EICASResource.top);
        Bitmap eng_fail_1 = new Bitmap(B737EICASResource.ENG_FAIL_1);
        Bitmap eng_fail_2 = new Bitmap(B737EICASResource.ENG_FAIL_2);
        Bitmap low_vol_1 = new Bitmap(B737EICASResource.LOW_VOL_1);
        Bitmap low_vol_2 = new Bitmap(B737EICASResource.LOE_VOL_2);

        SolidBrush drawBrush = new SolidBrush(Color.White);

        bool cscWarning = false;
        bool scWarning = false;
        float tem, rpm1, rpm2, power1, power2, cos1, cos2, volte1, volte2;
        EngineStatus engineStatus1 = EngineStatus.Nor;
        EngineStatus engineStatus2 = EngineStatus.Nor;
        float scale;
        int x, y, rx, ry;
        Point insBackPosition1 = new Point(8, 26);
        Point insBackRotation1 = new Point(51, 69);
        Point insBackPosition2 = new Point(143, 26);
        Point insBackRotation2 = new Point(187, 69);
        Point insBack2Position1 = new Point(5, 113);
        Point insBack2Rotation1 = new Point(52, 159);
        Point insBack2Position2 = new Point(140, 113);
        Point insBack2Rotation2 = new Point(187, 159);

        protected override void OnPaint(PaintEventArgs pe)
        {
            //获取控件缩放比
            scale = (float)Width / backGroung.Width;

            //绘制背景
            pe.Graphics.DrawImage(backGroung, 0, 0, backGroung.Width * scale, backGroung.Height * scale);

            RotateImage(pe, insBack2, InterpolPhyToAngle(power1, 0, 100, 27, 209), insBack2Position1, insBack2Rotation1, scale);
            RotateImage(pe, insBack2, InterpolPhyToAngle(power2, 0, 100, 27, 209), insBack2Position2, insBack2Rotation2, scale);
            pe.Graphics.DrawImage(cover2, 0, 0, cover2.Width * scale, cover2.Height * scale);

            RotateImage(pe, insBack, InterpolPhyToAngle(rpm1, 0, 100, 27, 204), insBackPosition1, insBackRotation1, scale);
            RotateImage(pe, insBack, InterpolPhyToAngle(rpm2, 0, 100, 27, 204), insBackPosition2, insBackRotation2, scale);
            pe.Graphics.DrawImage(cover1, 0, 0, cover1.Width * scale, cover1.Height * scale);

            PainWarning(pe);

            pe.Graphics.DrawImage(top, 0, 0, top.Width * scale, top.Height * scale);


            Font altFont = new Font("Arial", 12 * scale);
            Font altFont1 = new Font("Arial", 14 * scale);
            Font altFont2 = new Font("Arial", 11 * scale);
            pe.Graphics.DrawString(tem.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 36 * scale, 0);
            pe.Graphics.DrawString(rpm1.ToString("f0").PadLeft(5, '0'), altFont1, drawBrush, 59 * scale, 41 * scale);
            pe.Graphics.DrawString(rpm2.ToString("f0").PadLeft(5, '0'), altFont1, drawBrush, 194 * scale, 41 * scale);
            pe.Graphics.DrawString(power1.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 58 * scale, 134 * scale);
            pe.Graphics.DrawString(power2.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 193 * scale, 134 * scale);
            pe.Graphics.DrawString(cos1.ToString("f0").PadLeft(3, '0'), altFont2, drawBrush, 51 * scale, 271 * scale);
            pe.Graphics.DrawString(cos2.ToString("f0").PadLeft(3, '0'), altFont2, drawBrush, 197 * scale, 271 * scale);
            pe.Graphics.DrawString(volte1.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 302 * scale, 430 * scale);
            pe.Graphics.DrawString(volte2.ToString("f0").PadLeft(3, '0'), altFont, drawBrush, 402 * scale, 430 * scale);

        }

        public void SetValues(float tem, float rpm1, float rpm2, float power1, float power2, float cos1, float cos2, float volte1, float volte2, EngineStatus en1 = EngineStatus.NoChange, EngineStatus en2 = EngineStatus.NoChange)
        {
            this.tem = tem;
            this.rpm1 = rpm1;
            this.rpm2 = rpm2;
            this.power1 = power1;
            this.power2 = power2;
            this.cos1 = cos1;
            this.cos2 = cos2;
            this.volte1 = volte1;
            this.volte2 = volte2;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            this.rpm1 = rpm1;
            if(en1!= EngineStatus.NoChange)
            {
                if (engineStatus1 != en1 && en1 == EngineStatus.LowVol)
                    sound.Play(SoundType.scSound);
                if (engineStatus1 != en1 && en1 == EngineStatus.Fail)
                    sound.PlayLoop(SoundType.cscSound);
                if (engineStatus1 != en1 && en1 == EngineStatus.Nor)
                    sound.Stop(SoundType.cscSound);
                engineStatus1 = en1;
            }
            if (en2 != EngineStatus.NoChange)
            {
                if (engineStatus2 != en2 && en2 == EngineStatus.LowVol)
                    sound.Play(SoundType.scSound);
                if (engineStatus2 != en1 && en2 == EngineStatus.Fail)
                    sound.PlayLoop(SoundType.cscSound);
                if (engineStatus2 != en1 && en2 == EngineStatus.Nor)
                    sound.Stop(SoundType.cscSound);
                engineStatus2 = en2;
            }
            Refresh();
        }

        public void SetXY(int x, int y, int rx, int ry)
        {
            this.x = x;
            this.y = y;
            this.rx = rx;
            this.ry = ry;
            Refresh();
        }


        public void CancelWarning()
        {
            cscWarning = false;
            scWarning = false;
        }

        private void PainWarning(PaintEventArgs pe)
        {
            switch (engineStatus1)
            {
                case EngineStatus.Fail:
                    pe.Graphics.DrawImage(eng_fail_1, 0, 0, eng_fail_1.Width * scale, eng_fail_1.Height * scale);
                    break;
                case EngineStatus.LowVol:
                    pe.Graphics.DrawImage(low_vol_1, 0, 0, low_vol_1.Width * scale, low_vol_1.Height * scale);
                    break;
            }
            switch (engineStatus2)
            {
                case EngineStatus.Fail:
                    pe.Graphics.DrawImage(eng_fail_2, 0, 0, eng_fail_2.Width * scale, eng_fail_2.Height * scale);
                    break;
                case EngineStatus.LowVol:
                    pe.Graphics.DrawImage(low_vol_2, 0, 0, low_vol_2.Width * scale, low_vol_2.Height * scale);
                    break;
            }
        }
    }

    public enum EngineStatus
    {
        Fail, LowVol, Nor,Unknown,NoChange
    }
    class B737EICASSound : Sound
    {
        SoundPlayer cscSound = new SoundPlayer(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737EICAS\Sounds\CSC_fix.wav");
        SoundPlayer scSound = new SoundPlayer(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737EICAS\Sounds\SC.wav");
        protected override SoundPlayer GetSoundPlayer(int hashCode)
        {
            switch (hashCode)
            {
                case 0: return cscSound;
                case 1: return scSound;
                default: return new SoundPlayer();
            }
        }
    }
    enum SoundType
    {
        cscSound,
        scSound
    }
}
