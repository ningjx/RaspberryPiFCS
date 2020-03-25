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
using GMap.NET.WindowsForms;
using RaspberryPiClient.Controllers;
using RaspberryPiClient.Helper;
using SharpGL.WinForms;
namespace RaspberryPiClient
{
    public partial class MainForm : Form 
    {
        FlightData data;
        int a = 0;
        public MainForm()
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
            data = TestEq.FlightData;
            timer1.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text =Convert.ToString(180-data.Attitude.Angle_Y);
            textBox2.Text =Convert.ToString( data.Attitude.Angle_X - 180);
            textBox3.Text =Convert.ToString((int)data.Attitude.BarometricAltitude);
            attitudeIndicatorInstrumentControl1.SetAttitudeIndicatorParameters(180-data.Attitude.Angle_Y, data.Attitude.Angle_X-180);
            altimeterInstrumentControl1.SetAlimeterParameters(a++);
            verticalSpeedIndicatorInstrumentControl1.SetVerticalSpeedIndicatorParameters((int)data.Attitude.Aacceleration_Y*1000);
            headingIndicatorInstrumentControl1.SetHeadingIndicatorParameters((int)data.Attitude.Angle_Z);
        }

        private void verticalSpeedIndicatorInstrumentControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
