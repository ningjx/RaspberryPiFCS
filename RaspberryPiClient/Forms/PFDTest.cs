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
    public partial class PFDTest : Form
    {
        public PFDTest()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetXY(trackBar1.Value, trackBar2.Value, trackBar3.Value / 10F);
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetXY(trackBar1.Value, trackBar2.Value, trackBar3.Value / 10F); textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            b737PFD1.SetXY(trackBar1.Value, trackBar2.Value, trackBar3.Value / 10F); textBox3.Text = trackBar3.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            b737PFD1.Refresh();
        }
    }
}
