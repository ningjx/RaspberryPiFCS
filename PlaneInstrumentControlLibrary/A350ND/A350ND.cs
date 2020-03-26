using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaneInstrumentControlLibrary.A350ND
{
    public partial class A350ND : InstrumentControl
    {
        public A350ND()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true);
        }

        Bitmap backGroung = new Bitmap(A350NDResource.WX_Mask_Rose_1);

        float scale;
        //public HeadingInstrument(IContainer container)
        //{
        //    container.Add(this);
        //
        //    InitializeComponent();
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            //获取控件缩放比
            scale = (float)Width / backGroung.Width;

            pe.Graphics.DrawImage(backGroung, 0, 0, backGroung.Width * scale, backGroung.Height * scale);

            if (MapImage != null)
                pe.Graphics.DrawImage(MapImage, 0, 0, backGroung.Width * scale, backGroung.Height * scale);

            
        }

        public void SetValues()
        {

            Refresh();
        }
        public Bitmap MapImage { get; set; }
    }
}
