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
        }

        Bitmap backGroung = new Bitmap(A350NDResource.backGround);
        Bitmap rose = new Bitmap(A350NDResource.ARCRose);
        Bitmap mapCover = new Bitmap(A350NDResource.mapCover);
        Bitmap top = new Bitmap(A350NDResource.top);

        double heading;

        float scale;
        Point rosePosition;
        Point roseRotation;
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
            //if (rosePosition == null)
            //{
              rosePosition = new Point(0, 0);
              roseRotation = new Point(rose.Width-400, rose.Height-387);
            //}


            if (MapImage != null)
            {
                scale = (float)Width / MapImage.Width;
                pe.Graphics.DrawImage(MapImage, 0, 0, MapImage.Width * scale, MapImage.Height * scale);
            }
            pe.Graphics.DrawImage(mapCover, 0, 0, mapCover.Width * scale, mapCover.Height * scale);
            RotateImage(pe, rose, InterpolPhyToAngle((float)heading, 0, 360, 360, 0), rosePosition, roseRotation, scale);
            pe.Graphics.DrawImage(top, 0, 0, top.Width * scale, top.Height * scale);

        }

        public void SetValues(Bitmap MapImage,double heading)
        {
            this.heading = heading;
            this.MapImage = MapImage;
            Task.Run(()=> { Thread.Sleep(500); this.MapImage = MapImage; });
            Refresh();
        }

        public void SetValues( double heading)
        {
            this.heading = heading;
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
