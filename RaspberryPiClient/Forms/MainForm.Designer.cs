namespace RaspberryPiClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.xBar = new System.Windows.Forms.TrackBar();
            this.yBar = new System.Windows.Forms.TrackBar();
            this.rollBar = new System.Windows.Forms.TrackBar();
            this.pitchBar = new System.Windows.Forms.TrackBar();
            this.headingBar = new System.Windows.Forms.TrackBar();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.altBar = new System.Windows.Forms.TrackBar();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.vsBar = new System.Windows.Forms.TrackBar();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.a350ND1 = new PlaneInstrumentControlLibrary.A350ND.A350ND();
            this.b737PFD1 = new PlaneInstrumentControlLibrary.B737PFD.B737PFD();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsBar)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // xBar
            // 
            this.xBar.Location = new System.Drawing.Point(755, 668);
            this.xBar.Maximum = 1000;
            this.xBar.Minimum = -1000;
            this.xBar.Name = "xBar";
            this.xBar.Size = new System.Drawing.Size(495, 45);
            this.xBar.TabIndex = 1;
            this.xBar.Scroll += new System.EventHandler(this.xBar_Scroll);
            // 
            // yBar
            // 
            this.yBar.Location = new System.Drawing.Point(761, 742);
            this.yBar.Maximum = 1000;
            this.yBar.Minimum = -1000;
            this.yBar.Name = "yBar";
            this.yBar.Size = new System.Drawing.Size(495, 45);
            this.yBar.TabIndex = 2;
            this.yBar.Scroll += new System.EventHandler(this.yBar_Scroll);
            // 
            // rollBar
            // 
            this.rollBar.Location = new System.Drawing.Point(744, 72);
            this.rollBar.Maximum = 180;
            this.rollBar.Minimum = -180;
            this.rollBar.Name = "rollBar";
            this.rollBar.Size = new System.Drawing.Size(495, 45);
            this.rollBar.TabIndex = 3;
            this.rollBar.Scroll += new System.EventHandler(this.rollBar_Scroll);
            // 
            // pitchBar
            // 
            this.pitchBar.Location = new System.Drawing.Point(744, 155);
            this.pitchBar.Maximum = 90;
            this.pitchBar.Minimum = -180;
            this.pitchBar.Name = "pitchBar";
            this.pitchBar.Size = new System.Drawing.Size(495, 45);
            this.pitchBar.TabIndex = 4;
            this.pitchBar.Scroll += new System.EventHandler(this.pitchBar_Scroll);
            // 
            // headingBar
            // 
            this.headingBar.Location = new System.Drawing.Point(755, 269);
            this.headingBar.Maximum = 360;
            this.headingBar.Name = "headingBar";
            this.headingBar.Size = new System.Drawing.Size(495, 45);
            this.headingBar.TabIndex = 5;
            this.headingBar.Scroll += new System.EventHandler(this.headingBar_Scroll);
            // 
            // speedBar
            // 
            this.speedBar.Location = new System.Drawing.Point(755, 391);
            this.speedBar.Maximum = 400;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(495, 45);
            this.speedBar.TabIndex = 6;
            this.speedBar.Scroll += new System.EventHandler(this.speedBar_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1296, 692);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1286, 742);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 8;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(1289, 72);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(1289, 155);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 21);
            this.textBox4.TabIndex = 10;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(1289, 269);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 21);
            this.textBox5.TabIndex = 11;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(1289, 391);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 21);
            this.textBox6.TabIndex = 12;
            // 
            // altBar
            // 
            this.altBar.Location = new System.Drawing.Point(755, 493);
            this.altBar.Maximum = 4000;
            this.altBar.Minimum = -200;
            this.altBar.Name = "altBar";
            this.altBar.Size = new System.Drawing.Size(495, 45);
            this.altBar.TabIndex = 13;
            this.altBar.Scroll += new System.EventHandler(this.altBar_Scroll);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(1299, 493);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 21);
            this.textBox7.TabIndex = 14;
            // 
            // vsBar
            // 
            this.vsBar.Location = new System.Drawing.Point(755, 557);
            this.vsBar.Maximum = 60;
            this.vsBar.Minimum = -60;
            this.vsBar.Name = "vsBar";
            this.vsBar.Size = new System.Drawing.Size(495, 45);
            this.vsBar.TabIndex = 15;
            this.vsBar.Scroll += new System.EventHandler(this.vsBar_Scroll);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(1299, 557);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 21);
            this.textBox8.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(753, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "roll";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(753, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "pitch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(759, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "heading";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(776, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "speed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(782, 458);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "alt";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(776, 526);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "vs";
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(12, 364);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(726, 443);
            this.gMapControl1.TabIndex = 26;
            this.gMapControl1.Zoom = 0D;
            // 
            // a350ND1
            // 
            this.a350ND1.Location = new System.Drawing.Point(388, 12);
            this.a350ND1.MapImage = null;
            this.a350ND1.Name = "a350ND1";
            this.a350ND1.Size = new System.Drawing.Size(350, 346);
            this.a350ND1.TabIndex = 25;
            this.a350ND1.Text = "a350ND1";
            // 
            // b737PFD1
            // 
            this.b737PFD1.Location = new System.Drawing.Point(12, 12);
            this.b737PFD1.Name = "b737PFD1";
            this.b737PFD1.Size = new System.Drawing.Size(370, 346);
            this.b737PFD1.TabIndex = 24;
            this.b737PFD1.Text = "b737PFD1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 812);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "label6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(283, 812);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(449, 812);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(600, 812);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "label10";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 609);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 833);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.a350ND1);
            this.Controls.Add(this.b737PFD1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.vsBar);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.altBar);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.speedBar);
            this.Controls.Add(this.headingBar);
            this.Controls.Add(this.pitchBar);
            this.Controls.Add(this.rollBar);
            this.Controls.Add(this.yBar);
            this.Controls.Add(this.xBar);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar xBar;
        private System.Windows.Forms.TrackBar yBar;
        private System.Windows.Forms.TrackBar rollBar;
        private System.Windows.Forms.TrackBar pitchBar;
        private System.Windows.Forms.TrackBar headingBar;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TrackBar altBar;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TrackBar vsBar;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private PlaneInstrumentControlLibrary.B737PFD.B737PFD b737PFD1;
        private PlaneInstrumentControlLibrary.A350ND.A350ND a350ND1;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
    }
}