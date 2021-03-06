﻿using System;

namespace GMap.NET.MapProviders
{
    public class AMapSateliteProvider : AMapProviderBase
    {
        public static readonly AMapSateliteProvider Instance;

        readonly Guid id = new Guid("FCA94AF4-3467-47c6-BDA2-6F52E4A145BC");
        public override Guid Id
        {
            get { return id; }
        }

        readonly string name = "AMapSatelite";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        static AMapSateliteProvider()
        {
            Instance = new AMapSateliteProvider();
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);
            var image = GetTileImageUsingHttp(url);
            //var CurrentBitmap = new Bitmap(image.Data);
            //MapSet?.Invoke(CurrentBitmap);
            //FileStream stream = new FileStream("D:/aaa.png", mode: FileMode.Create);
            //test.Data.CopyTo(stream);
            //stream.Close();
            return image;
            //return GetTileImageUsingHttp(url);
        }

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {

            //http://webst04.is.autonavi.com/appmaptile?x=23&y=12&z=5&lang=zh_cn&size=1&scale=1&style=8
            string url = string.Format(UrlFormat, pos.X, pos.Y, zoom);
            Console.WriteLine("url:" + url);
            return url;
        }

        //public override PureImage GetTileImage(GPoint pos, int zoom)
        //{
        //    throw new NotImplementedException();
        //}

        static readonly string UrlFormat = "http://webst04.is.autonavi.com/appmaptile?x={0}&y={1}&z={2}&lang=zh_cn&size=1&scale=1&style=6";

        //public delegate void GetMapEventHandler(Bitmap map);
        //public event GetMapEventHandler MapSet;
    }
}
