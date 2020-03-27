using System;
using System.Collections.Generic;
using System.Text;
using GMap.NET.MapProviders;
using GMap.NET.Projections;

namespace GMap.NET.GMap.NET.MapProviders.BMap
{
    public abstract class BaiduMapProviderBase : GMapProvider
    {
        private string ClientKey = "1308e84a0e8a1fc2115263a4b3cf87f1";
        public BaiduMapProviderBase()
        {
            MaxZoom = null;
            RefererUrl = "http://map.baidu.com";
            Copyright = string.Format("©{0} Baidu Corporation, ©{0} NAVTEQ, ©{0} Image courtesy of NASA", DateTime.Today.Year);
        }

        public override PureProjection Projection
        {
            get { return MercatorProjection.Instance; }
        }

        GMapProvider[] overlays;
        public override GMapProvider[] Overlays
        {
            get
            {
                if (overlays == null)
                {
                    overlays = new GMapProvider[] { this };
                }
                return overlays;
            }
        }
    }

    public class BaiduMapProvider : BaiduMapProviderBase
    {
        public static readonly BaiduMapProvider Instance;

        readonly Guid id = new Guid("608748FC-5FDD-4d3a-9027-356F24A755E5");
        public override Guid Id
        {
            get { return id; }
        }

        readonly string name = "BaiduMap";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        static BaiduMapProvider()
        {
            Instance = new BaiduMapProvider();
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);

            return GetTileImageUsingHttp(url);
        }

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            zoom = zoom - 1;
            var offsetX = Math.Pow(2, zoom);
            var offsetY = offsetX - 1;

            var numX = pos.X - offsetX;
            var numY = -pos.Y + offsetY;

            zoom = zoom + 1;
            var num = (pos.X + pos.Y) % 8 + 1;
            var x = numX.ToString().Replace("-", "M");
            var y = numY.ToString().Replace("-", "M");

            //http://q3.baidu.com/it/u=x=721;y=209;z=12;v=014;type=web&fm=44
            string url = string.Format(UrlFormat, num, x, y, zoom, "014", "web", "44");
            Console.WriteLine("url:" + url);
            return url;
        }

        static readonly string UrlFormat = "http://q{0}.baidu.com/it/u=x={1};y={2};z={3};v={4};type={5}&fm={6}";

    }
}
