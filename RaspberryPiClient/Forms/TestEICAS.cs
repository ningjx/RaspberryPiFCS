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
    public partial class TestEICAS : Form
    {
        public TestEICAS()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            b737EICAS1.SetXY(trackBar1.Value, trackBar2.Value, trackBar4.Value, trackBar5.Value);
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void TestEICAS_Load(object sender, EventArgs e)
        {
            
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            b737EICAS1.SetXY(trackBar1.Value, trackBar2.Value, trackBar4.Value, trackBar5.Value);
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(trackBar3.Value,2,2);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            b737EICAS1.SetXY(trackBar1.Value, trackBar2.Value, trackBar4.Value, trackBar5.Value);
            textBox3.Text = trackBar4.Value.ToString();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            b737EICAS1.SetXY(trackBar1.Value, trackBar2.Value, trackBar4.Value, trackBar5.Value);
            textBox4.Text = trackBar5.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(trackBar3.Value, 0, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(trackBar3.Value, 2, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(trackBar3.Value, 1, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(trackBar3.Value, 2, 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(trackBar3.Value, 2, 2);
        }

        private void b737EICAS1_Click(object sender, EventArgs e)
        {
            b737EICAS1.SetValues(trackBar3.Value, 2, 2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //textBox1.Text = Application.StartupPath;
            b737EICAS1.CancelWarning();
        }
    }
}
