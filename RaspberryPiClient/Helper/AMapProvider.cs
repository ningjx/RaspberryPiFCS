using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using GMap.NET.Internals;
using GMap.NET.MapProviders;
using GMap.NET.Projections;

namespace GMap.NET.MapProviders
{
    public abstract class AMapProviderBase : GMapProvider
    {
        public AMapProviderBase()
        {
            MaxZoom = null;
            RefererUrl = "http://www.amap.com/";
            Copyright = "";//string.Format("©{0} 高德 Corporation, ©{0} NAVTEQ, ©{0} Image courtesy of NASA", DateTime.Today.Year);
        }

        //public GeoCoderStatusCode GetPoints(string keywords, out List<PointLatLng> pointList)
        //{
        //http://where.yahooapis.com/geocode?q=lithuania,vilnius&appid=1234&flags=CG&gflags=QL&locale=LT-lt

        //    #region -- response --
        //    < ResultSet version = "1.0" >< Error > 0 </ Error >< ErrorMessage > No error </ ErrorMessage >< Locale > LT - lt </ Locale >< Quality > 40 </ Quality >< Found > 1 </ Found >< Result >< quality > 40 </ quality >< latitude > 54.689850 </ latitude >< longitude > 25.269260 </ longitude >< offsetlat > 54.689850 </ offsetlat >< offsetlon > 25.269260 </ offsetlon >< radius > 46100 </ radius ></ Result ></ ResultSet >
        //    #endregion

        //    return GetLatLngFromGeocoderUrl(MakeGeocoderUrl(keywords), out pointList);
        //}
        //GeoCoderStatusCode GetLatLngFromGeocoderUrl(string url, out List<PointLatLng> pointList)
        //{
        //    var status = GeoCoderStatusCode.Unknow;
        //    pointList = null;

        //    try
        //    {
        //        string geo = GMaps.Instance.UseGeocoderCache ? Cache.Instance.GetContent(url, CacheType.GeocoderCache) : string.Empty;

        //        bool cache = false;

        //        if (string.IsNullOrEmpty(geo))
        //        {
        //            geo = GetContentUsingHttp(url);

        //            if (!string.IsNullOrEmpty(geo))
        //            {
        //                cache = true;
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(geo))
        //        {
        //            if (geo.StartsWith("<?xml") && geo.Contains("<Result"))
        //            {
        //                if (cache && GMaps.Instance.UseGeocoderCache)
        //                {
        //                    Cache.Instance.SaveContent(url, CacheType.GeocoderCache, geo);
        //                }

        //                XmlDocument doc = new XmlDocument();
        //                doc.LoadXml(geo);
        //                {
        //                    XmlNodeList l = doc.SelectNodes("/ResultSet/Result");
        //                    if (l != null)
        //                    {
        //                        pointList = new List<PointLatLng>();

        //                        foreach (XmlNode n in l)
        //                        {
        //                            var nn = n.SelectSingleNode("quality");
        //                            if (nn != null)
        //                            {
        //                                var quality = int.Parse(nn.InnerText);
        //                                if (quality < MinExpectedQuality) continue;

        //                                nn = n.SelectSingleNode("latitude");
        //                                if (nn != null)
        //                                {
        //                                    double lat = double.Parse(nn.InnerText, CultureInfo.InvariantCulture);

        //                                    nn = n.SelectSingleNode("longitude");
        //                                    if (nn != null)
        //                                    {
        //                                        double lng = double.Parse(nn.InnerText, CultureInfo.InvariantCulture);
        //                                        pointList.Add(new PointLatLng(lat, lng));
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        status = GeoCoderStatusCode.G_GEO_SUCCESS;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status = GeoCoderStatusCode.ExceptionInCode;
        //        Debug.WriteLine("GetLatLngFromGeocoderUrl: " + ex);
        //    }

        //    return status;
        //}

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

    /// <summary>
    /// 高德地图
    /// </summary>
    public class AMapProvider : AMapProviderBase
    {
        public static readonly AMapProvider Instance;

        readonly Guid id = new Guid("EF3DD303-3F74-4938-BF40-232D0595EE88");
        public override Guid Id
        {
            get { return id; }
        }

        readonly string name = "AMap";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        static AMapProvider()
        {
            Instance = new AMapProvider();
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);

            var test = GetTileImageUsingHttp(url);
            //FileStream stream = new FileStream("D:/aaa.png", mode: FileMode.Create);
            //test.Data.CopyTo(stream);
            //stream.Close();
            return test;
        }

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {

            //http://webrd04.is.autonavi.com/appmaptile?x=5&y=2&z=3&lang=zh_cn&size=1&scale=1&style=7
            string url = string.Format(UrlFormat, pos.X, pos.Y, zoom);
            Console.WriteLine("url:" + url);
            return url;
        }

        static readonly string UrlFormat = "http://webrd04.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scale=1&style=7";
    }
}
