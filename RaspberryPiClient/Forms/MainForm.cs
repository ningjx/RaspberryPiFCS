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
using PlaneInstrumentControlLibrary.B737EICAS;
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
        }

        private void rollBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 100F, vsBar.Value / 10F, headingBar.Value);
            textBox3.Text = rollBar.Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pitchBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 100F, vsBar.Value / 10F, headingBar.Value);
            textBox4.Text = pitchBar.Value.ToString();
        }

        private void headingBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 100F, vsBar.Value / 10F, headingBar.Value);
            a350ND1.SetValues(headingBar.Value,0);
            textBox5.Text = headingBar.Value.ToString();
            a350ND1.SetValues(headingBar.Value,0);
            gMapControl1.Bearing = headingBar.Value;
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 100F, vsBar.Value / 10F, headingBar.Value);
            textBox6.Text = (speedBar.Value/10F).ToString();
        }

        private void altBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 100F, vsBar.Value / 10F, headingBar.Value);
            //b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            textBox7.Text = altBar.Value.ToString();
        }

        private void vsBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 100F, vsBar.Value / 10F, headingBar.Value);
            textBox8.Text = vsBar.Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //b737PFD1.SetValues(data.Attitude.Angle_X - 180, 180 - data.Attitude.Angle_Y, data.Attitude.BarometricAltitude, data.Attitude.Aacceleration_X, data.Attitude.Aacceleration_Y * 10, data.Attitude.Angle_Z);
            b737PFD1.SetValues(data.Attitude.Angle_X - 180, 180 - data.Attitude.Angle_Y, altBar.Value / 10F, speedBar.Value / 100F, vsBar.Value / 10F, data.Attitude.Angle_Z);
            a350ND1.SetValues(data.Attitude.Angle_Z,0);
            b737EICAS1.SetValues(20, 60, 60, 50, 50, data.Attitude.Angle_Z, 0, 4.2F, 4.2F);
            gMapControl1.Bearing = data.Attitude.Angle_Z;
            var a = Extends.GPSToGCJ(data.GPSData.Longitude / 1E7D, data.GPSData.Latitude / 1E7D);
            gMapControl1.Position = a;
            label9.Text = data.GPSData.Latitude.ToString();//40
            label10.Text = data.GPSData.Longitude.ToString();
            label6.Text = a.Lng.ToString();
            label8.Text = a.Lat.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Park
            //Tax
            //Takeoff
            //Climb
            //CRZ
            //Dec
            //APP
            switch (comboBox3.Text)
            {
                case "Park":
                    b737PFD1.SetFlightStatus(PlaneInstrumentControlLibrary.B737PFD.FlightStatus.Park);
                    break;
                case "Tax":
                    b737PFD1.SetFlightStatus(PlaneInstrumentControlLibrary.B737PFD.FlightStatus.Tax);
                    break;
                case "Takeoff":
                    b737PFD1.SetFlightStatus(PlaneInstrumentControlLibrary.B737PFD.FlightStatus.Takeoff);
                    break;
                case "Climb":
                    b737PFD1.SetFlightStatus(PlaneInstrumentControlLibrary.B737PFD.FlightStatus.CLB);
                    break;
                case "CRZ":
                    b737PFD1.SetFlightStatus(PlaneInstrumentControlLibrary.B737PFD.FlightStatus.CRZ);
                    break;
                case "Dec":
                    b737PFD1.SetFlightStatus(PlaneInstrumentControlLibrary.B737PFD.FlightStatus.Descend);
                    break;
                case "APP":
                    b737PFD1.SetFlightStatus(PlaneInstrumentControlLibrary.B737PFD.FlightStatus.APP);
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //失效
            //警告
            //正常
            switch (comboBox1.Text)
            {
                case "失效":
                    b737EICAS1.SetValues(20, 50, 75, 60, 80, 66, 99, 4.2F, 4.3F, EngineStatus.Fail);
                    break;
                case "正常":
                    b737EICAS1.SetValues(20, 50, 75, 60, 80, 66, 99, 4.2F, 4.3F, EngineStatus.Nor);
                    break;
                case "警告":
                    b737EICAS1.SetValues(20, 50, 75, 60, 80, 66, 99, 4.2F, 4.3F, EngineStatus.LowVol);
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //失效
            //警告
            //正常
            switch (comboBox2.Text)
            {
                case "失效":
                    b737EICAS1.SetValues(20, 50, 75, 60, 80, 66, 99, 4.2F, 4.3F, en2: EngineStatus.Fail);
                    break;
                case "正常":
                    b737EICAS1.SetValues(20, 50, 75, 60, 80, 66, 99, 4.2F, 4.3F, en2: EngineStatus.Nor);
                    break;
                case "警告":
                    b737EICAS1.SetValues(20, 50, 75, 60, 80, 66, 99, 4.2F, 4.3F, en2: EngineStatus.LowVol);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b737EICAS1.CancelWarning();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] aa = textBox1.Text.Split('.');
            List<EICASInfo> infos = new List<EICASInfo>();
            foreach(var item in aa)
            {
                infos.Add(new EICASInfo { WarningType = WarningType.Info, Text = item });
                infos.Add(new EICASInfo { WarningType = WarningType.Warning, Text = item });
                infos.Add(new EICASInfo { WarningType = WarningType.Error, Text = item });
            }
            b737EICAS1.SetTexts(infos);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar1.Value.ToString();
            b737EICAS1.SetXY(trackBar1.Value, trackBar2.Value,0,0);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox9.Text = trackBar2.Value.ToString();
            b737EICAS1.SetXY(trackBar1.Value, trackBar2.Value,0,0);

        }

        private void a350ND1_Click(object sender, EventArgs e)
        {

        }
    }
}
