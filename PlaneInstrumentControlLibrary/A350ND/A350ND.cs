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

        Bitmap mapImage = new Bitmap(A350NDResource.WX_Mask_Rose_1);
        //public HeadingInstrument(IContainer container)
        //{
        //    container.Add(this);
        //
        //    InitializeComponent();
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            SetMap?.Invoke(mapImage);

        }

        public void SetValues()
        {

            Refresh();
        }

        public delegate void SetMapImageEventHandler(Bitmap mapImage);
        /// <summary>
        /// 设置地图图像
        /// </summary>
        public event SetMapImageEventHandler SetMap;
    }
}
