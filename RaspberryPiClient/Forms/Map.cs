using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightDataModel;
using GMap.NET;
using RaspberryPiClient.Controllers;
using RaspberryPiClient.Helper;

namespace RaspberryPiClient
{
    public partial class Map : Form
    {
        FlightData data;
        public Map()
        {
            InitializeComponent();

            gMapControl1.MapProvider = AMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl1.Zoom = 10;
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;//指定最大最小zoom才可以缩放
            gMapControl1.DragButton = MouseButtons.Left;
            //gMapControl1.Position = new PointLatLng(100, 100);
            //gMapControl1.SetPositionByKeywords("北京");
            data = TestEq.FlightData;
        }

        private void Map_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(data.GPSData.Latitude, data.GPSData.Longitude);
        }
    }
}
