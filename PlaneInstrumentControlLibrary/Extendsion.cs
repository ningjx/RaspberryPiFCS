using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneInstrumentControlLibrary
{
    public static class Extendsion
    {
        public unsafe static Region ImageToRegionPx(Bitmap bitmap, Color TransparentColor)
        {
            Region rgn = new Region();
            rgn.MakeEmpty();

            int width = bitmap.Width;
            int height = bitmap.Height;
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* p = (byte*)bmData.Scan0;
            int offset = bmData.Stride - width * 3;

            int p0, p1, p2;         // 记录透明色
            p0 = TransparentColor.R;
            p1 = TransparentColor.G;
            p2 = TransparentColor.B;

            Rectangle curRect = new Rectangle();
            curRect.Height = 1;

            int start = -1;
            // 行座标 ( Y col ) 
            for (int Y = 0; Y < height; Y++)
            {
                // 列座标 ( X row ) 
                for (int X = 0; X < width; X++)
                {
                    if (start == -1 && (p[0] != p0 || p[1] != p1 || p[2] != p2))     //如果 之前的点没有不透明 且 不透明 
                    {
                        start = X;                            //记录这个点
                        curRect.X = X;
                        curRect.Y = Y;
                    }
                    else if (start > -1 && (p[0] == p0 && p[1] == p1 && p[2] == p2))      //如果 之前的点是不透明 且 透明
                    {
                        curRect.Width = X - curRect.X;
                        rgn.Union(curRect);
                        start = -1;
                    }

                    if (X == width - 1 && start > -1)        //如果 之前的点是不透明 且 是最后一个点
                    {
                        curRect.Width = X - curRect.X;
                        rgn.Union(curRect);
                        start = -1;
                    }
                    p += 3;//下一个内存地址
                }
                p += offset;
            }
            bitmap.UnlockBits(bmData);
            bitmap.Dispose();
            return rgn;
        }
    }
}
