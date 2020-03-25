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
            data = TestEq.FlightData;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            b737PFD1.SetAttitudeIndicatorParameters(data.Attitude.Angle_X - 180, 180 - data.Attitude.Angle_Y, data.Attitude.BarometricAltitude, data.Attitude.Aacceleration_X, 0, data.Attitude.Angle_Z);
        }
    }
}
