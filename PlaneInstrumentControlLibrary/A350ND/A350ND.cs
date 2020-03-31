using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaneInstrumentControlLibrary.A350ND
{
    public partial class A350ND : InstrumentControl
    {
        public A350ND()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //BackColor = Color.FromArgb(0, 0, 0, 0);
            Region = Extendsion.ImageToRegionPx(mapCover, Color.Transparent);
        }

        Bitmap backGroung = new Bitmap(A350NDResource.backGround);
        Bitmap rose = new Bitmap(A350NDResource.ARCRose);
        Bitmap mapCover = new Bitmap(A350NDResource.mapCover);
        Bitmap mapCover1 = new Bitmap(A350NDResource.mapCover___复制);
        Bitmap top = new Bitmap(A350NDResource.top);
        Bitmap point = new Bitmap(A350NDResource.point);

        double heading,GPSHeading;

        float scale;
        Point rosePosition = new Point(0, 0);
        Point roseRotation = new Point(400, 413);
        Point topRotation = new Point(400, 412);
        //public HeadingInstrument(IContainer container)
        //{
        //    container.Add(this);
        //
        //    InitializeComponent();
        //}
        int x, y;
        protected override void OnPaint(PaintEventArgs pe)
        {
            //获取控件缩放比
            scale = (float)Width / backGroung.Width;

            pe.Graphics.DrawImage(backGroung, 0, 0, backGroung.Width * scale, backGroung.Height * scale);

            if (MapImage != null)
            {
                pe.Graphics.DrawImage(MapImage, 0, 0, 800, 800);
            }
            pe.Graphics.DrawImage(mapCover1, 0, 0, mapCover1.Width * scale, mapCover1.Height * scale);
            RotateImage(pe, rose, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), rosePosition, roseRotation, scale);
            double angel;
            if (GPSHeading>= heading)
            {
                angel = GPSHeading - heading;
            }
            else
            {
                angel = GPSHeading - heading + 360;
            }
            RotateImage(pe, top, InterpolPhyToAngle((float)angel, 0, 360, 0, 360), rosePosition, topRotation, scale);
            pe.Graphics.DrawImage(point, 0, 0, point.Width * scale, point.Height * scale);

        }

        public void SetValues(Bitmap MapImage,double heading)
        {
            this.heading = heading;
            this.MapImage = MapImage;
            Task.Run(()=> { Thread.Sleep(500); this.MapImage = MapImage; });
            Refresh();
        }

        public void SetValues( double heading,double GPSHeading)
        {
            this.heading = heading;
            this.GPSHeading = GPSHeading;
            Refresh();
        }
        public void SetXY(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetMap(Bitmap map)
        {
            if (map == null)
                return;
            MapImage = map;
            Refresh();
        }
        public Bitmap MapImage { get; set; }
    }
}
