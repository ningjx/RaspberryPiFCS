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
using RaspberryPiClient.Controllers;

namespace RaspberryPiClient
{
    public partial class MainForm : Form
    {
        FlightData data = new FlightData();
        public MainForm()
        {
            InitializeComponent();
            //data = TestEq.FlightData;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //b737PFD1.SetAttitudeIndicatorParameters(data.Attitude.Angle_X - 180, 180 - data.Attitude.Angle_Y, data.Attitude.BarometricAltitude, data.Attitude.Aacceleration_X, data.Attitude.Aacceleration_Y*10, data.Attitude.Angle_Z);
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
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            textBox5.Text = headingBar.Value.ToString();
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
            textBox6.Text = (speedBar.Value/10F).ToString();
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
            b737PFD1.SetXY(xBar.Value, yBar.Value);
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
        }

        private void yBar_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = yBar.Value.ToString();
            b737PFD1.SetXY(xBar.Value, yBar.Value);
            b737PFD1.SetValues(rollBar.Value, pitchBar.Value, altBar.Value / 10F, speedBar.Value / 10F, vsBar.Value / 10F, headingBar.Value);
        }
    }
}
