﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlaneInstrumentControlLibrary.SoundHandle;

namespace PlaneInstrumentControlLibrary.B737PFD
{
    public partial class B737PFD : InstrumentControl
    {
        public double roll, pitch, alt, speed, vs, heading;

        Bitmap backGroung = new Bitmap(B737PFDResource.newBack);
        Bitmap horizon = new Bitmap(B737PFDResource.horizon_1);
        Bitmap speedTape = new Bitmap(B737PFDResource.newSpeed);
        Bitmap headingRose = new Bitmap(B737PFDResource.heading_rose_1);
        Bitmap altNum = new Bitmap(B737PFDResource.alt_ias_1);
        Bitmap headingBug1 = new Bitmap(B737PFDResource.heading_bug_1);
        Bitmap speedCover = new Bitmap(B737PFDResource.newSpeedCover);
        Bitmap headingPoint = new Bitmap(B737PFDResource.newPoint);
        Bitmap vsBottom = new Bitmap(B737PFDResource.buttomColor);
        Bitmap stick = new Bitmap(B737PFDResource.stic);
        Bitmap altCover = new Bitmap(B737PFDResource.altCover);
        Bitmap altTape = new Bitmap(B737PFDResource.altTape);
        Bitmap mag = new Bitmap(B737PFDResource.MGString);
        Bitmap horizontalDots = new Bitmap(B737PFDResource.horizontal_dots_1);
        Bitmap directorH = new Bitmap(B737PFDResource.flight_director_horizontal_1);
        Bitmap directorV = new Bitmap(B737PFDResource.flight_director_vertical_1);
        Bitmap pullup = new Bitmap(B737PFDResource.Pull_Up_1);
        Bitmap altScroll = new Bitmap(B737PFDResource.altScroll);
        Bitmap speedScroll = new Bitmap(B737PFDResource.speedScorll);

        System.Timers.Timer timer = new System.Timers.Timer(800);

        bool isShow = false;
        FlightStatus flightMode = FlightStatus.Park;
        Font drawFont;
        Font altFont;
        Font altScrFont;
        SolidBrush drawBrush = new SolidBrush(Color.White);
        SolidBrush altBrush = new SolidBrush(Color.FromArgb(202, 89, 198));
        SolidBrush modeBrush = new SolidBrush(Color.SpringGreen);
        B737PFDSound sound;
        float scale;

        Pen maskPen;
        Point horPosition = new Point(76, -411);
        Point horRotation = new Point(275, 241);
        Point stickPosition = new Point(573, 105);
        Point stickRotation = new Point(575, 240);
        Point speedTapePosition;
        Point altTapePosition;
        Point headingPosition = new Point(114, 439);
        Point headingRotation = new Point(276, 601);
        Point headingBudPosition = new Point(263, 429);
        public int x, y;
        public float z;
        
        public B737PFD()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            sound = new B737PFDSound();
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (isShow)
                isShow = false;
            else
                isShow = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //获取控件缩放比
            scale = (float)Width / backGroung.Width;
            //绘制安定面
            RotateAndTranslate(pe, horizon, roll, 0, horPosition, (int)(5.2 * pitch), horRotation, scale);

            maskPen = new Pen(Color.Black, 10 * scale);
            drawFont = new Font("Arial", 18 * scale);
            altScrFont = new Font("Arial", 14 * scale);
            altFont = new Font("Arial", 12 * scale);

            speedTapePosition = new Point(56, (int)(speed * 31.2) - 1075);
            altTapePosition = new Point(524, (int)(alt * 4.155) - 1577);
            //绘制飞行指示器


            //绘制垂直速度的底色
            pe.Graphics.DrawImage(vsBottom, (backGroung.Width - vsBottom.Width) * scale * 1.25F, (backGroung.Height - vsBottom.Height) * scale * 0.5F, vsBottom.Width * scale, vsBottom.Height * scale);

            //绘制垂直速度指示

            RotateImage(pe, stick, InterpolPhyToAngle((float)vs, -6, 6, 23, 157), stickPosition, stickRotation, scale);

            //绘制背景
            pe.Graphics.DrawImage(backGroung, 0, 0, backGroung.Width * scale, backGroung.Height * scale);

            string drawString = string.Empty;
            switch (flightMode)
            {
                case FlightStatus.Park:
                    break;
                case FlightStatus.APP:
                    drawString = "APP";
                    break;
                case FlightStatus.Takeoff:
                    drawString = "TKF";
                    break;
                case FlightStatus.Tax:
                    drawString = "Tax";
                    break;
                case FlightStatus.CRZ:
                    drawString = "CRZ";
                    break;
                case FlightStatus.Descend:
                    drawString = "DEC";
                    break;
                case FlightStatus.CLB:
                    drawString = "CLB";
                    break;
            }

            //绘制ILS指示器
            //pe.Graphics.DrawImage(horizontalDots, 0, 0, horizontalDots.Width * scale, horizontalDots.Height * scale);

            //绘制速度显示，高度显示
            TranslateImage(pe, speedTape, 0, 0, speedTapePosition, scale);
            pe.Graphics.DrawImage(speedCover, 0, 0, speedCover.Width * scale, speedCover.Height * scale);

            TranslateImage(pe, altTape, 0, 0, altTapePosition, scale * 0.8F);
            pe.Graphics.DrawImage(altCover, -5, 0, altCover.Width * scale, altCover.Height * scale);
            pe.Graphics.DrawImage(altNum, (backGroung.Width - altNum.Width) * scale * 0.65F, (backGroung.Height - altNum.Height) * scale * 0.45F, altNum.Width * scale, altNum.Height * scale);

            //显示高度速度数字
            string altStr = Math.Truncate(alt).ToString().PadLeft(4, '0');
            if (altStr.Contains("-"))
            {
                string[] spl = altStr.Split('-');
                altStr = "-"+spl[0] + spl[1];
            }
            pe.Graphics.DrawString(Math.Truncate(speed).ToString().PadLeft(2,'0'), drawFont, drawBrush, 62 * scale, 230* scale );
            pe.Graphics.DrawString(altStr, altScrFont, drawBrush, 448 * scale, 233 * scale);

            //绘制航向盘
            RotateImage(pe, headingRose, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), headingPosition, headingRotation, scale);

            //绘制紫色航向指示
            RotateImage(pe, headingBug1, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), headingBudPosition, headingRotation, scale);
            pe.Graphics.DrawString(heading.ToString("f0").PadLeft(3, '0'), altFont, altBrush, (backGroung.Width - altNum.Width) * scale * 2.4F, (backGroung.Height - altNum.Height) * scale * 1.03F);

            //绘制航向指示
            pe.Graphics.DrawImage(headingPoint, (float)((0.5 * backGroung.Width - 0.5 * headingPoint.Width) * scale * 1.637), (float)((0.5 * backGroung.Height - 0.5 * headingPoint.Height) * scale * (-1.95)), headingPoint.Width * scale * 0.15F, headingPoint.Height * scale * 0.15F);
            pe.Graphics.DrawImage(mag, 0, 0, mag.Width * scale, mag.Height * scale);

            //绘制边框
            pe.Graphics.DrawRectangle(maskPen, 0, 0, Width, Height);

            pe.Graphics.DrawString(drawString, drawFont, modeBrush, 253 * scale, 8 * scale);

            //pullup
            if (terrainWarning && isShow)
            {
                pe.Graphics.DrawImage(pullup, 0, 0, pullup.Width * scale, pullup.Height * scale);
            }

            Rectangle altrectangle = new Rectangle((int)(493* scale), (int)(224* scale), (int)(18* scale), (int)(43* scale));
            DrawScrollCounter(pe, altScroll, altrectangle, 0, 1, (float)(alt-Math.Truncate(alt)));
            Rectangle spdrectangle = new Rectangle((int)(93 * scale), (int)(222 * scale), (int)(12 * scale), (int)(44 * scale));
            DrawScrollCounter(pe, speedScroll, spdrectangle, 0, 1, (float)(speed - Math.Truncate(speed)));
        }

        public void SetValues(double roll, double pitch, double alt, double speed, double vs, double heading)
        {
            this.roll = roll * Math.PI / 180;
            this.pitch = pitch;
            this.alt = alt;
            this.speed = speed;
            this.vs = vs;
            this.heading = heading;

            AltSoundPlay(alt);
            SinkRate(vs);
            BankAngel(roll);
            TerrainWarning();
            this.Refresh();
        }

        public void SetXY(int x, int y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            Refresh();
        }


        public void SetFlightStatus(FlightStatus mode)
        {
            flightMode = mode;
            Refresh();
        }


        #region BankAngel
        private bool bankAngelWarning = false;
        private void BankAngel(double roll)
        {
            if (Math.Abs(roll) > 60 && !bankAngelWarning)
            {
                sound.PlayLoopInArray(SoundType.bankangle);
                bankAngelWarning = true;
            }
            if (Math.Abs(roll) <= 60 && bankAngelWarning)
            {
                sound.StopPlayInArray(SoundType.bankangle);
                bankAngelWarning = false;
            }
        }
        #endregion

        #region sinkrate
        private bool sinkRateWarning = false;
        private void SinkRate(double vs)
        {
            if (vs > -6 && sinkRateWarning)
            {
                sinkRateWarning = false;
                if (vs > 0)
                    sound.StopPlayInArray(SoundType.pullup);
            }
            if (sinkRateWarning)
                return;
            void action()
            {
                sound.PlayLoopInArray(SoundType.pullup);
            }
            if (vs < -6 && !sinkRateWarning)
            {
                sinkRateWarning = true;
                sound.PlaySync(SoundType.sinkrate, action,2);
            }
        }
        #endregion

        #region 高度SpeakOut
        private double altBuffer;
        /// <summary>
        /// 报高度
        /// </summary>
        /// <param name="alt"></param>
        private void AltSoundPlay(double alt)
        {
            //if (flightMode != FlightMode.APP)
            //    return;
            alt = Math.Round(alt, 1);
            if (alt <= 25 && altBuffer > 25)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S2500);
                return;
            }
            if (alt <= 20 && altBuffer > 20)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S2000);
                return;
            }
            if (alt <= 10 && altBuffer > 10)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S1000);
                return;
            }
            if (alt <= 5 && altBuffer > 5)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S500);
                return;
            }
            if (alt <= 4 && altBuffer > 4)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S400);
                return;
            }
            if (alt <= 3 && altBuffer > 3)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S300);
                return;
            }
            if (alt <= 2 && altBuffer > 2)
            {
                altBuffer = alt;
                void action()
                {
                    sound.Play(SoundType.minimums);
                }
                sound.PlaySync(SoundType.S200, action);
                return;
            }
            if (alt <= 1 && altBuffer > 1)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S100a);
                return;
            }

            if (alt <= 0.5 && altBuffer > 0.5)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S50);
                return;
            }
            if (alt <= 0.4 && altBuffer > 0.4)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S40);
                return;
            }
            if (alt <= 0.3 && altBuffer > 0.3)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S30);
                return;
            }
            if (alt <= 0.2 && altBuffer > 0.2)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S20);
                return;
            }
            if (alt <= 0.1 && altBuffer > 0.1)
            {
                altBuffer = alt;
                sound.PlaySync(SoundType.S10);
                return;
            }
            altBuffer = alt;
        }
        #endregion

        #region TerrainPullUp
        private bool terrainWarning = false;
        private void TerrainWarning()
        {
            if (vs >= 0 || alt >= 5)
            {
                if (!terrainWarning)
                    return;
                sound.StopPlayInArray(SoundType.terrain);
                sound.StopPlayInArray(SoundType.pullup);
                terrainWarning = false;
                return;
            }
            if (terrainWarning)
                return;
            if (flightMode == FlightStatus.APP || flightMode == FlightStatus.Park || flightMode == FlightStatus.Tax)
            {
                terrainWarning = false;
                return;
            }

            if (vs < 0 && alt < 5)
                terrainWarning = true;
            sound.PlayLoopInArray(SoundType.terrain);
            sound.PlayLoopInArray(SoundType.pullup);
        }
        #endregion
    }

    public enum FlightStatus
    {
        Park, Tax, Takeoff, CLB, CRZ, Descend, APP
    }

    class B737PFDSound : Sound
    {
        private SoundRes S10 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\10.wav");
        private SoundRes S20 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\20.wav");
        private SoundRes S30 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\30.wav");
        private SoundRes S40 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\40.wav");
        private SoundRes S50 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\50.wav");
        private SoundRes S100a = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\100_above.wav");
        private SoundRes S200 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\200.wav");
        private SoundRes S300 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\300.wav");
        private SoundRes S400 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\400.wav");
        private SoundRes S500 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\500.wav");
        private SoundRes S1000 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\1000.wav");
        private SoundRes S2000 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\2000.wav");
        private SoundRes S2500 = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\2500.wav", 1500);
        private SoundRes terrain = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\terrain.wav");
        private SoundRes pullup = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\pullup.wav");
        private SoundRes bankangle = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\bankangle.wav");
        private SoundRes dontsink = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\dontsink.wav");
        private SoundRes glideslope = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\glideslope.wav");
        private SoundRes minimums = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\minimums.wav");
        private SoundRes sinkrate = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\sinkrate.wav");
        private SoundRes toolowflaps = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\toolowflaps.wav");
        private SoundRes toolowgear = new SoundRes(@"D:\WorkSpace\RaspberryPiFCS\PlaneInstrumentControlLibrary\B737PFD\Sounds\toolowgear.wav");
        protected override SoundRes GetSoundRes(int hashCode)
        {
            switch (hashCode)
            {
                case 0:
                    return S10;
                case 1:
                    return S20;
                case 2:
                    return S30;
                case 3:
                    return S40;
                case 4:
                    return S50;
                case 5:
                    return S100a;
                case 6:
                    return S200;
                case 7:
                    return S300;
                case 8:
                    return S400;
                case 9:
                    return S500;
                case 10:
                    return S1000;
                case 11:
                    return S2000;
                case 12:
                    return S2500;
                case 13:
                    return terrain;
                case 14:
                    return pullup;
                case 15:
                    return bankangle;
                case 16:
                    return dontsink;
                case 17:
                    return glideslope;
                case 18:
                    return minimums;
                case 19:
                    return sinkrate;
                case 20:
                    return toolowflaps;
                case 21:
                    return toolowgear;
                default:
                    return new SoundRes();
            }
        }
    }

    enum SoundType
    {
        S10,
        S20,
        S30,
        S40,
        S50,
        S100a,
        S200,
        S300,
        S400,
        S500,
        S1000,
        S2000,
        S2500,
        terrain,
        pullup,
        bankangle,
        dontsink,
        glideslope,
        minimums,
        sinkrate,
        toolowflaps,
        toolowgear
    }
}
