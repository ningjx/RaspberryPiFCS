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
    public partial class NDTest : Form
    {
        public NDTest()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            a350ND1.SetValues(trackBar1.Value, trackBar2.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
            a350ND1.SetValues(trackBar1.Value, trackBar2.Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar3.Value.ToString();
            a350ND1.SetXY(trackBar3.Value, trackBar4.Value);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = trackBar4.Value.ToString();
            a350ND1.SetXY(trackBar3.Value, trackBar4.Value);
        }
    }
}
