﻿namespace RaspberryPiClient
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
            this.rollBar = new System.Windows.Forms.TrackBar();
            this.pitchBar = new System.Windows.Forms.TrackBar();
            this.headingBar = new System.Windows.Forms.TrackBar();
            this.speedBar = new System.Windows.Forms.TrackBar();
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
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.a350ND1 = new PlaneInstrumentControlLibrary.A350ND.A350ND();
            this.b737PFD1 = new PlaneInstrumentControlLibrary.B737PFD.B737PFD();
            this.b737EICAS1 = new PlaneInstrumentControlLibrary.B737EICAS.B737EICAS();
            ((System.ComponentModel.ISupportInitialize)(this.rollBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rollBar
            // 
            this.rollBar.Location = new System.Drawing.Point(1218, 36);
            this.rollBar.Margin = new System.Windows.Forms.Padding(4);
            this.rollBar.Maximum = 180;
            this.rollBar.Minimum = -180;
            this.rollBar.Name = "rollBar";
            this.rollBar.Size = new System.Drawing.Size(690, 69);
            this.rollBar.TabIndex = 3;
            this.rollBar.Scroll += new System.EventHandler(this.rollBar_Scroll);
            // 
            // pitchBar
            // 
            this.pitchBar.Location = new System.Drawing.Point(1218, 140);
            this.pitchBar.Margin = new System.Windows.Forms.Padding(4);
            this.pitchBar.Maximum = 90;
            this.pitchBar.Minimum = -180;
            this.pitchBar.Name = "pitchBar";
            this.pitchBar.Size = new System.Drawing.Size(690, 69);
            this.pitchBar.TabIndex = 4;
            this.pitchBar.Scroll += new System.EventHandler(this.pitchBar_Scroll);
            // 
            // headingBar
            // 
            this.headingBar.Location = new System.Drawing.Point(1218, 232);
            this.headingBar.Margin = new System.Windows.Forms.Padding(4);
            this.headingBar.Maximum = 360;
            this.headingBar.Name = "headingBar";
            this.headingBar.Size = new System.Drawing.Size(690, 69);
            this.headingBar.TabIndex = 5;
            this.headingBar.Scroll += new System.EventHandler(this.headingBar_Scroll);
            // 
            // speedBar
            // 
            this.speedBar.Location = new System.Drawing.Point(1218, 351);
            this.speedBar.Margin = new System.Windows.Forms.Padding(4);
            this.speedBar.Maximum = 4000;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(690, 69);
            this.speedBar.TabIndex = 6;
            this.speedBar.Scroll += new System.EventHandler(this.speedBar_Scroll);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(1929, 45);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(148, 28);
            this.textBox3.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(1929, 148);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(148, 28);
            this.textBox4.TabIndex = 10;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(1929, 246);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(148, 28);
            this.textBox5.TabIndex = 11;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(1929, 352);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(148, 28);
            this.textBox6.TabIndex = 12;
            // 
            // altBar
            // 
            this.altBar.Location = new System.Drawing.Point(1218, 470);
            this.altBar.Margin = new System.Windows.Forms.Padding(4);
            this.altBar.Maximum = 4000;
            this.altBar.Minimum = -200;
            this.altBar.Name = "altBar";
            this.altBar.Size = new System.Drawing.Size(690, 69);
            this.altBar.TabIndex = 13;
            this.altBar.Scroll += new System.EventHandler(this.altBar_Scroll);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(1929, 470);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(148, 28);
            this.textBox7.TabIndex = 14;
            // 
            // vsBar
            // 
            this.vsBar.Location = new System.Drawing.Point(1218, 586);
            this.vsBar.Margin = new System.Windows.Forms.Padding(4);
            this.vsBar.Maximum = 70;
            this.vsBar.Minimum = -70;
            this.vsBar.Name = "vsBar";
            this.vsBar.Size = new System.Drawing.Size(690, 69);
            this.vsBar.TabIndex = 15;
            this.vsBar.Scroll += new System.EventHandler(this.vsBar_Scroll);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(1929, 586);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(148, 28);
            this.textBox8.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1130, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 17;
            this.label1.Text = "roll";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1120, 153);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "pitch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1120, 246);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "heading";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1130, 366);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "speed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1138, 482);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 18);
            this.label5.TabIndex = 21;
            this.label5.Text = "alt";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1148, 591);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 18);
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
            this.gMapControl1.Location = new System.Drawing.Point(537, 529);
            this.gMapControl1.Margin = new System.Windows.Forms.Padding(4);
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
            this.gMapControl1.Size = new System.Drawing.Size(564, 455);
            this.gMapControl1.TabIndex = 26;
            this.gMapControl1.Zoom = 0D;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1418, 690);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 31;
            this.button1.Text = "设置飞行状态";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(582, 1006);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 27;
            this.label6.Text = "label6";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(726, 1006);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 28;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(855, 1006);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 29;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1005, 1006);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 18);
            this.label10.TabIndex = 30;
            this.label10.Text = "label10";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(248, 1009);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(165, 34);
            this.button4.TabIndex = 35;
            this.button4.Text = "设置引擎一状态";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "失效",
            "警告",
            "正常"});
            this.comboBox1.Location = new System.Drawing.Point(32, 1009);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(180, 26);
            this.comboBox1.TabIndex = 36;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "失效",
            "警告",
            "正常"});
            this.comboBox2.Location = new System.Drawing.Point(32, 1075);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(180, 26);
            this.comboBox2.TabIndex = 37;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(248, 1071);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(165, 34);
            this.button5.TabIndex = 38;
            this.button5.Text = "设置引擎二状态";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Park",
            "Tax",
            "Takeoff",
            "Climb",
            "CRZ",
            "Dec",
            "APP"});
            this.comboBox3.Location = new System.Drawing.Point(1179, 693);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(180, 26);
            this.comboBox3.TabIndex = 39;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(434, 1009);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 40;
            this.button2.Text = "取消警告";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1179, 764);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 28);
            this.textBox1.TabIndex = 41;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1418, 760);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 34);
            this.button3.TabIndex = 42;
            this.button3.Text = "设置信息";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(1280, 915);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(628, 69);
            this.trackBar1.TabIndex = 43;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(1280, 1012);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar2.Maximum = 500;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(628, 69);
            this.trackBar2.TabIndex = 44;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1929, 930);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(148, 28);
            this.textBox2.TabIndex = 45;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(1929, 1026);
            this.textBox9.Margin = new System.Windows.Forms.Padding(4);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(148, 28);
            this.textBox9.TabIndex = 46;
            // 
            // a350ND1
            // 
            this.a350ND1.Location = new System.Drawing.Point(537, 18);
            this.a350ND1.MapImage = null;
            this.a350ND1.Margin = new System.Windows.Forms.Padding(4);
            this.a350ND1.Name = "a350ND1";
            this.a350ND1.Size = new System.Drawing.Size(564, 499);
            this.a350ND1.TabIndex = 25;
            this.a350ND1.Text = "a350ND1";
            this.a350ND1.Click += new System.EventHandler(this.a350ND1_Click);
            // 
            // b737PFD1
            // 
            this.b737PFD1.Location = new System.Drawing.Point(18, 18);
            this.b737PFD1.Margin = new System.Windows.Forms.Padding(4);
            this.b737PFD1.Name = "b737PFD1";
            this.b737PFD1.Size = new System.Drawing.Size(511, 482);
            this.b737PFD1.TabIndex = 24;
            this.b737PFD1.Text = "b737PFD1";
            // 
            // b737EICAS1
            // 
            this.b737EICAS1.Location = new System.Drawing.Point(18, 576);
            this.b737EICAS1.Margin = new System.Windows.Forms.Padding(4);
            this.b737EICAS1.Name = "b737EICAS1";
            this.b737EICAS1.Size = new System.Drawing.Size(395, 422);
            this.b737EICAS1.TabIndex = 34;
            this.b737EICAS1.Text = "b737EICAS1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(2116, 1125);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
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
            this.Controls.Add(this.speedBar);
            this.Controls.Add(this.headingBar);
            this.Controls.Add(this.pitchBar);
            this.Controls.Add(this.rollBar);
            this.Controls.Add(this.b737EICAS1);
            this.Controls.Add(this.gMapControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rollBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headingBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vsBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar rollBar;
        private System.Windows.Forms.TrackBar pitchBar;
        private System.Windows.Forms.TrackBar headingBar;
        private System.Windows.Forms.TrackBar speedBar;
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
        private System.Windows.Forms.Button button1;
        private PlaneInstrumentControlLibrary.B737EICAS.B737EICAS b737EICAS1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox9;
    }
}