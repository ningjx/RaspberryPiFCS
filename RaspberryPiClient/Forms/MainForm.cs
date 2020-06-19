using FlightDataModel;
using GMap.NET;
using GMap.NET.MapProviders;
using RaspberryPiClient.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspberryPiClient.Forms
{
    public partial class MainForm : Form
    {
        FlightData data = new FlightData();
        public MainForm()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GoogleChinaTerrainMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;//指定最大最小zoom才可以缩放
            gMapControl1.DragButton = MouseButtons.Left;
            data = TestEq.FlightData;
            gMapControl1.Zoom = 15;
            //gMapControl1.SetPositionByKeywords("北京");
            gMapControl1.Position = new PointLatLng(40.034684, 116.309204);//纬度经度
            timer1.Start();
        }

        private void b737EICAS1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            b737PFD1.SetValues(data.Attitude.Angle_X - 180, 180 - data.Attitude.Angle_Y, data.Attitude.BarometricAltitude, 10, data.Attitude.Aacceleration_Z, data.Attitude.Angle_Z);
            a350ND1.SetValues(data.Attitude.Angle_Z, data.Attitude.Angle_Z);
            b737EICAS1.SetValues(20, 60, 60, 50, 50, data.Attitude.Angle_Z, 0, 4.2F, 4.2F, 0, 0, 0, 0);
            gMapControl1.Bearing = data.Attitude.Angle_Z;
        }
    }
}
