using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaspberryPiClient.CustomControls;

namespace RaspberryPiClient.MyPannel.B737PFD
{
    public partial class B737PFD : InstrumentControl
    {
        public double roll, pitch, alt, speed, vs, heading;
        Bitmap back = new Bitmap(B737PFDRes.newBack);
        Bitmap horizon = new Bitmap(B737PFDRes.horizon_1);
        Bitmap speedTape = new Bitmap(B737PFDRes.newSpeed);
        Bitmap headingRose = new Bitmap(B737PFDRes.heading_rose_1);
        Bitmap altNum = new Bitmap(B737PFDRes.alt_ias_1);
        Bitmap headingBug1 = new Bitmap(B737PFDRes.heading_bug_1);
        Bitmap speedCover = new Bitmap(B737PFDRes.newSpeedCover);
        Bitmap headingP = new Bitmap(B737PFDRes.newPoint);
        Bitmap buttomColor = new Bitmap(B737PFDRes.buttomColor);
        Bitmap stick = new Bitmap(B737PFDRes.stic);
        Bitmap altCover = new Bitmap(B737PFDRes.altCover);
        Bitmap altTape = new Bitmap(B737PFDRes.altTape);
        Bitmap mgstring = new Bitmap(B737PFDRes.MGString);

        

        SolidBrush drawBrush = new SolidBrush(Color.White);
        SolidBrush altBrush = new SolidBrush(Color.FromArgb(202,89,198));

        float scale;
        int x, y;
        public int count;
        public B737PFD()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
            customPoint = new Point(230, 250);
        }

        //public ResTest(IContainer container)
        //{
        //    container.Add(this);
        //
        //    InitializeComponent();
        //}
        Point customPoint;
        protected override void OnPaint(PaintEventArgs pe)
        {
            

            Point ptBoule = new Point(76, -411);
            Point ptRotation = new Point(275, 241);
            scale = (float)this.Width / back.Width;
            Font drawFont = new Font("Arial", 16 * scale);
            Font altFont = new Font("Arial", 12 * scale);
            RotateAndTranslate(pe, horizon, roll, 0, ptBoule, (int)(5.2 * pitch), ptRotation, scale);

            Pen maskPen = new Pen(this.BackColor, 30 * scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, back.Width * scale, back.Height * scale);

            //绘制底色
            pe.Graphics.DrawImage(buttomColor, (float)((0.5 * back.Width - 0.5 * buttomColor.Width) * scale * 2.5), (float)((0.5 * back.Height - 0.5 * buttomColor.Height) * scale), buttomColor.Width * scale, buttomColor.Height * scale);
            //绘制垂直速度指示
            Point stickPoint = new Point(573, 104);
            Point stickRotation = new Point(573 + stick.Width / 2, 104 + stick.Height / 2);
            RotateImage(pe, stick, InterpolPhyToAngle((float)vs,-6, 6, 23, 157), stickPoint, stickRotation, scale);
            //绘制背景
            pe.Graphics.DrawImage(back, 0, 0, back.Width * scale, back.Height * scale);
            //绘制垂直速度指示
            //pe.Graphics.DrawImage(overlay2_1, (float)((0.5 * back.Width - 0.5 * overlay2_1.Width) * scale*1.93), (float)((0.5 * back.Height - 0.5 * overlay2_1.Height) * scale * 1.55), overlay2_1.Width * scale, overlay2_1.Height * scale);
            //绘制wings
            //pe.Graphics.DrawImage(wings, (float)((0.5 * back.Width - 0.5 * wings.Width) * scale), (float)((0.5 * back.Height - 0.5 * wings.Height) * scale * 0.927), (float)(wings.Width * scale), (float)(wings.Height * scale));
            //绘制速度显示，高度显示
            Point speedTapePoint = new Point(56, (int)(speed * 31.2) - 1075);
            TranslateImage(pe, speedTape, 0, 0, speedTapePoint, scale);
            pe.Graphics.DrawImage(speedCover, 0, 0, speedCover.Width * scale, speedCover.Height * scale);

            Point altTapePoint = new Point(524, (int)(alt*4.155)-1577);
            TranslateImage(pe, altTape, 0, 0, altTapePoint, scale*0.8F);
            pe.Graphics.DrawImage(altCover, 0, 0, altCover.Width * scale, altCover.Height * scale);
            pe.Graphics.DrawImage(altNum, (float)((0.5 * back.Width - 0.5 * altNum.Width) * scale * 1.3), (float)((0.5 * back.Height - 0.5 * altNum.Height) * scale * 0.9), (float)(altNum.Width * scale), (float)(altNum.Height * scale));
            //绘制航向盘
            Point headingPoint = new Point(114, 439);
            Point headingRotation = new Point(114 + headingRose.Width / 2, 439 + headingRose.Height / 2);
            RotateImage(pe, headingRose, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), headingPoint, headingRotation, scale);
            //绘制紫色航向指示
            Point headingBudPoint = new Point(263, 429);
            RotateImage(pe, headingBug1, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), headingBudPoint, headingRotation, scale);
            //绘制航向指示
            pe.Graphics.DrawImage(headingP, (float)((0.5 * back.Width - 0.5 * headingP.Width) * scale * 1.637), (float)((0.5 * back.Height - 0.5 * headingP.Height) * scale * (-1.95)), headingP.Width * scale * 0.15F, headingP.Height * scale * 0.15F);
            pe.Graphics.DrawImage(mgstring, 0,0, mgstring.Width * scale, mgstring.Height * scale);


            pe.Graphics.DrawString(speed.ToString("f1").PadLeft(4), drawFont, drawBrush, (float)((0.5 * back.Width - 0.5 * altNum.Width) * scale * 1.3), (float)((0.5 * back.Height - 0.5 * altNum.Height) * scale * 0.98));
            pe.Graphics.DrawString(alt.ToString("f1").PadLeft(5), drawFont, drawBrush, (float)((0.5 * back.Width - 0.5 * altNum.Width) * scale * 10.1), (float)((0.5 * back.Height - 0.5 * altNum.Height) * scale * 0.98));
            pe.Graphics.DrawString(heading.ToString("f0").PadLeft(3,'0'), altFont, altBrush, (float)((0.5 * back.Width - 0.5 * altNum.Width) * scale *4.8), (float)((0.5 * back.Height - 0.5 * altNum.Height) * scale * 2.065));
        }

        public void SetAttitudeIndicatorParameters(double roll, double pitch, double alt, double speed, double vs, double heading)
        {
            this.roll = (roll * Math.PI / 180);
            this.pitch = pitch;
            this.alt = alt;
            this.speed = speed;
            this.vs = vs;
            this.heading = heading;// ;
            this.Refresh();
        }

        public void SetRoPoint(int x, int y)
        {
            customPoint = new Point((int)(x * scale), (int)(y * scale));
            this.x = x;
            this.y = y;
        }
    }
}
