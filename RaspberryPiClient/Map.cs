using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using RaspberryPiClient.Helper;

namespace RaspberryPiClient
{
    public partial class Map : Form
    {
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
            gMapControl1.SetPositionByKeywords("北京");
        }

        private void Map_Load(object sender, EventArgs e)
        {

        }
    }
}
