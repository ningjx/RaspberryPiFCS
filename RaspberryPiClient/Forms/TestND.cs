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
using RaspberryPiClient.Helper;

namespace RaspberryPiClient.Forms
{
    public partial class TestND : Form
    {
        Bitmap map;
        public TestND()
        {
            InitializeComponent();
            gMapControl1.MapProvider = AMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl1.Zoom = 10;
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;//指定最大最小zoom才可以缩放
            gMapControl1.DragButton = MouseButtons.Left;
        }

        private void Instance_MapSet(Bitmap map)
        {
            this.map = map;
        }

        private void TestND_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            gMapControl1.Position = new PointLatLng(trackBar1.Value, trackBar2.Value);
            //a350ND1.SetMap(map);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
            gMapControl1.Position = new PointLatLng(trackBar1.Value, trackBar2.Value);
            //gMapControl1

            //a350ND1.SetMap(map);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar3.Value.ToString();
            gMapControl1.Zoom = trackBar3.Value;
            //a350ND1.SetMap(map);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = trackBar4.Value.ToString();
            a350ND1.SetValues(trackBar4.Value);
            gMapControl1.Bearing = trackBar4.Value;// - 180;
            //a350ND1.SetMap(map);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            map = gMapControl1.backBuffer;
            a350ND1.SetMap(map);
        }
    }
}
