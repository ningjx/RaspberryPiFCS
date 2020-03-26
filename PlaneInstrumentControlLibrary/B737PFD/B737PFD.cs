using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlaneInstrumentControlLibrary.B737PFD
{
    public partial class B737PFD : InstrumentControl
    {
        public double roll, pitch, alt, speed, vs, heading;
        Bitmap backgroung = new Bitmap(B737PFDResource.newBack);
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

        SolidBrush drawBrush = new SolidBrush(Color.White);
        SolidBrush altBrush = new SolidBrush(Color.FromArgb(202, 89, 198));

        float scale;





        int x, y;
        public B737PFD()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //获取控件缩放比
            scale = (float)Width / backgroung.Width;

            //绘制安定面
            Point horPosition = new Point(76, -411);
            Point horRotation = new Point(275, 241);
            RotateAndTranslate(pe, horizon, roll, 0, horPosition, (int)(5.2 * pitch), horRotation, scale);

            Font drawFont = new Font("Arial", 16 * scale);
            Font altFont = new Font("Arial", 12 * scale);

            

            //绘制垂直速度的底色
            pe.Graphics.DrawImage(vsBottom, (backgroung.Width - vsBottom.Width) * scale * 1.25F, (backgroung.Height - vsBottom.Height) * scale * 0.5F, vsBottom.Width * scale, vsBottom.Height * scale);

            //绘制垂直速度指示
            Point stickPosition = new Point(573, 104);
            Point stickRotation = new Point(573 + stick.Width / 2, 104 + stick.Height / 2);
            RotateImage(pe, stick, InterpolPhyToAngle((float)vs, -6, 6, 23, 157), stickPosition, stickRotation, scale);

            //绘制背景
            pe.Graphics.DrawImage(backgroung, 0, 0, backgroung.Width * scale, backgroung.Height * scale);

            //绘制速度显示，高度显示
            Point speedTapePosition = new Point(56, (int)(speed * 31.2) - 1075);
            TranslateImage(pe, speedTape, 0, 0, speedTapePosition, scale);
            pe.Graphics.DrawImage(speedCover, 0, 0, speedCover.Width * scale, speedCover.Height * scale);

            Point altTapePosition = new Point(524, (int)(alt * 4.155) - 1577);
            TranslateImage(pe, altTape, 0, 0, altTapePosition, scale * 0.8F);
            pe.Graphics.DrawImage(altCover, 0, 0, altCover.Width * scale, altCover.Height * scale);
            pe.Graphics.DrawImage(altNum, (backgroung.Width - altNum.Width) * scale * 0.65F, (backgroung.Height - altNum.Height) * scale * 0.45F, altNum.Width * scale, altNum.Height * scale);

            //绘制航向盘
            Point headingPosition = new Point(114, 439);
            Point headingRotation = new Point(114 + headingRose.Width / 2, 439 + headingRose.Height / 2);
            RotateImage(pe, headingRose, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), headingPosition, headingRotation, scale);

            //绘制紫色航向指示
            Point headingBudPosition = new Point(263, 429);
            RotateImage(pe, headingBug1, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), headingBudPosition, headingRotation, scale);
            pe.Graphics.DrawString(heading.ToString("f0").PadLeft(3, '0'), altFont, altBrush, (backgroung.Width - altNum.Width) * scale * 2.4F, (backgroung.Height - altNum.Height) * scale * 1.03F);

            //绘制航向指示
            pe.Graphics.DrawImage(this.headingPoint, (float)((0.5 * backgroung.Width - 0.5 * this.headingPoint.Width) * scale * 1.637), (float)((0.5 * backgroung.Height - 0.5 * this.headingPoint.Height) * scale * (-1.95)), this.headingPoint.Width * scale * 0.15F, this.headingPoint.Height * scale * 0.15F);
            pe.Graphics.DrawImage(mag, 0, 0, mag.Width * scale, mag.Height * scale);

            //绘制边框
            Pen maskPen = new Pen(Color.Black, 5 * scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, Width, Height);

            pe.Graphics.DrawString(speed.ToString("f1").PadLeft(4), drawFont, drawBrush, (backgroung.Width - altNum.Width) * scale * 0.65F, (backgroung.Height - altNum.Height) * scale * 0.49F);
            pe.Graphics.DrawString(alt.ToString("f1").PadLeft(5), drawFont, drawBrush, (backgroung.Width - altNum.Width) * scale * 5.05F, (backgroung.Height - altNum.Height) * scale * 0.49F);
        }

        public void SetAttitudeIndicatorParameters(double roll, double pitch, double alt, double speed, double vs, double heading)
        {
            this.roll = roll * Math.PI / 180;
            this.pitch = pitch;
            this.alt = alt;
            this.speed = speed;
            this.vs = vs;
            this.heading = heading;
            this.Refresh();
        }

        public void SetXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
