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
        public NDwithMap()
        {
            InitializeComponent();

            gMapControl1.MapProvider = AMapProvider.Instance;
            AMapProvider.Instance.MapSet += Instance_MapSet;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.Zoom = 10;
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;//指定最大最小zoom才可以缩放
            gMapControl1.DragButton = MouseButtons.Left;
        }
        
        private void Instance_MapSet(Bitmap map)
        {
            this.map = map;
            this.a350ND1.Refresh();
            //Action action = () => { a350ND1.Refresh(); };
            //this.a350ND1.BeginInvoke(action);
        }


        private void a350ND1_Click(object sender, EventArgs e)
        {

        }

        private void NDwithMap_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //a350ND1.SetValues(map);
            a350ND1.Refresh();
            //double lat, lon;
            //var data = textBox1.Text.Split(',');
            //lat = Convert.ToDouble(data[0]);
            //lon = Convert.ToDouble(data[1]);
            //gMapControl1.Position = new PointLatLng(lat, lon);
            ////gMapControl1.
            //gMapControl1.Zoom = Convert.ToInt32(textBox2.Text);
            //
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(trackBar1.Value, trackBar2.Value);
            //a350ND1.SetValues(map);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(trackBar1.Value, trackBar2.Value);
            //a350ND1.SetValues(map);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            gMapControl1.Zoom = trackBar3.Value;
            //a350ND1.SetValues(map);
        }
    }
}
