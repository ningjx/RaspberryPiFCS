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
using GMap.NET.MapProviders;
using RaspberryPiClient.Controllers;

namespace RaspberryPiClient
{
    public partial class MainForm : Form
    {
        FlightData data = new FlightData();
        public MainForm()
        {
            InitializeComponent();
            gMapControl1.MapProvider = AMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;//指定最大最小zoom才可以缩放
            gMapControl1.DragButton = MouseButtons.Left;
            //data = TestEq.FlightData;
            gMapControl1.Zoom = 10;
            //timer1.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //
        }

        private void rollBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value,pitchBar.Value,altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            textBox3.Text = rollBar.Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pitchBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            textBox4.Text = pitchBar.Value.ToString();
        }

        private void headingBar_Scroll(object sender, EventArgs e)
        {
            //b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            //a350ND1.SetValues(headingBar.Value);
            //textBox5.Text = headingBar.Value.ToString();
            //a350ND1.SetValues(headingBar.Value);
            //gMapControl1.Bearing = headingBar.Value;
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            //b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            //textBox6.Text = (speedBar.Value/10F).ToString();
        }

        private void altBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            textBox7.Text = altBar.Value.ToString();
        }

        private void vsBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            textBox8.Text = vsBar.Value.ToString();
        }

        private void xBar_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = xBar.Value.ToString();
            //a350ND1.SetXY(xBar.Value, yBar.Value);
            b737PFD1.SetXY(xBar.Value, yBar.Value);
            //b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            //a350ND1.SetValues(headingBar.Value);
        }

        private void yBar_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = yBar.Value.ToString();
            //a350ND1.SetXY(xBar.Value, yBar.Value);
            b737PFD1.SetXY(xBar.Value, yBar.Value);
            //b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            //a350ND1.SetValues(headingBar.Value);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            b737PFD1.SetValues(data.Attitude.Angle_X - 180, 180 - data.Attitude.Angle_Y, data.Attitude.BarometricAltitude, data.Attitude.Aacceleration_X, data.Attitude.Aacceleration_Y * 10, data.Attitude.Angle_Z);
            a350ND1.SetValues(data.Attitude.Angle_Z);
            gMapControl1.Bearing = data.Attitude.Angle_Z;
            var a = Extends.GPSToGCJ(data.GPSData.Longitude / 1E7D, data.GPSData.Latitude / 1E7D);
            gMapControl1.Position = a;
            label9.Text = data.GPSData.Latitude.ToString();//40
            label10.Text = data.GPSData.Longitude.ToString();
            label6.Text = a.Lng.ToString();
            label8.Text = a.Lat.ToString();
            textBox1.Text = data.Attitude.Angle_X.ToString("f2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(50, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b737PFD1.SetMode(PlaneInstrumentControlLibrary.B737PFD.FlightMode.CRZ);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b737PFD1.SetMode(PlaneInstrumentControlLibrary.B737PFD.FlightMode.Park);
        }
    }
}
