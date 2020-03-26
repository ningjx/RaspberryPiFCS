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
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using RaspberryPiClient.Helper;

namespace RaspberryPiClient
{
    public partial class NDwithMap : Form
    {
        Bitmap map;
        GMapControl MapControl = new GMapControl();
        public NDwithMap()
        {
            InitializeComponent();
            MapControl.MapProvider = AMapProvider.Instance;
            AMapProvider.Instance.MapSet += Instance_MapSet;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            MapControl.Zoom = 10;
            MapControl.MinZoom = 1;
            MapControl.MaxZoom = 24;//指定最大最小zoom才可以缩放
            MapControl.DragButton = MouseButtons.Left;
        }

        private void Instance_MapSet(Bitmap map)
        {
            a350ND1.MapImage = map;
        }


        private void a350ND1_Click(object sender, EventArgs e)
        {

        }
    }
}
