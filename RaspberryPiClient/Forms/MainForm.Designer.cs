namespace RaspberryPiClient.Forms
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
            this.b737PFD1 = new PlaneInstrumentControlLibrary.B737PFD.B737PFD();
            this.a350ND1 = new PlaneInstrumentControlLibrary.A350ND.A350ND();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.b737EICAS1 = new PlaneInstrumentControlLibrary.B737EICAS.B737EICAS();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // b737PFD1
            // 
            this.b737PFD1.Location = new System.Drawing.Point(551, 12);
            this.b737PFD1.Name = "b737PFD1";
            this.b737PFD1.Size = new System.Drawing.Size(845, 775);
            this.b737PFD1.TabIndex = 0;
            this.b737PFD1.Text = "b737PFD1";
            // 
            // a350ND1
            // 
            this.a350ND1.Location = new System.Drawing.Point(1, 547);
            this.a350ND1.MapImage = null;
            this.a350ND1.Name = "a350ND1";
            this.a350ND1.Size = new System.Drawing.Size(596, 566);
            this.a350ND1.TabIndex = 1;
            this.a350ND1.Text = "a350ND1";
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(1, 1);
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
            this.gMapControl1.Size = new System.Drawing.Size(596, 541);
            this.gMapControl1.TabIndex = 2;
            this.gMapControl1.Zoom = 0D;
            // 
            // b737EICAS1
            // 
            this.b737EICAS1.Location = new System.Drawing.Point(1476, 23);
            this.b737EICAS1.Name = "b737EICAS1";
            this.b737EICAS1.Size = new System.Drawing.Size(563, 866);
            this.b737EICAS1.TabIndex = 3;
            this.b737EICAS1.Text = "b737EICAS1";
            this.b737EICAS1.Click += new System.EventHandler(this.b737EICAS1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(2042, 1167);
            this.Controls.Add(this.b737EICAS1);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.a350ND1);
            this.Controls.Add(this.b737PFD1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private PlaneInstrumentControlLibrary.B737PFD.B737PFD b737PFD1;
        private PlaneInstrumentControlLibrary.A350ND.A350ND a350ND1;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private PlaneInstrumentControlLibrary.B737EICAS.B737EICAS b737EICAS1;
        private System.Windows.Forms.Timer timer1;
    }
}