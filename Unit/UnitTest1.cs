using System;
using System.IO;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.Projections;
using GMap.NET.WindowsForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //GMapProvider.TileImageProxy = WindowsFormsImageProxy.Instance;
            var p = LKS94Projection.Instance;
            //var p = PlateCarreeProjection.Instance;

            var pos = new PointLatLng(54.6961334816182, 25.2985095977783);
            var zoom = 4;
            var px = p.FromPixelToTileXY(p.FromLatLngToPixel(pos, zoom));
            Exception ex;
                var img = GMaps.Instance.GetImageFrom(GMapProviders.AMapSateliteProvider, px, zoom, out ex);
                File.WriteAllBytes(zoom + "z-" + px + ".png", img.Data.ToArray());

        }
    }
}
