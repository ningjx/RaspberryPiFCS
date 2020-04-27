namespace PlaneInstrumentControlLibrary
{
    partial class TemForm
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
            this.b737PFD1 = new PlaneInstrumentControlLibrary.B737PFD.B737PFD();
            this.attitudeIndicatorInstrumentControl1 = new PlaneInstrumentControlLibrary.OtherInstruments.AttitudeIndicatorInstrumentControl();
            this.turnCoordinatorInstrumentControl1 = new PlaneInstrumentControlLibrary.OtherInstruments.TurnCoordinatorInstrumentControl();
            this.airSpeedIndicatorInstrumentControl1 = new PlaneInstrumentControlLibrary.OtherInstruments.AirSpeedIndicatorInstrumentControl();
            this.SuspendLayout();
            // 
            // b737PFD1
            // 
            this.b737PFD1.Location = new System.Drawing.Point(88, 22);
            this.b737PFD1.Name = "b737PFD1";
            this.b737PFD1.Size = new System.Drawing.Size(520, 484);
            this.b737PFD1.TabIndex = 0;
            this.b737PFD1.Text = "b737PFD1";
            // 
            // attitudeIndicatorInstrumentControl1
            // 
            this.attitudeIndicatorInstrumentControl1.Location = new System.Drawing.Point(768, 91);
            this.attitudeIndicatorInstrumentControl1.Name = "attitudeIndicatorInstrumentControl1";
            this.attitudeIndicatorInstrumentControl1.Size = new System.Drawing.Size(285, 289);
            this.attitudeIndicatorInstrumentControl1.TabIndex = 1;
            this.attitudeIndicatorInstrumentControl1.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // turnCoordinatorInstrumentControl1
            // 
            this.turnCoordinatorInstrumentControl1.Location = new System.Drawing.Point(928, 507);
            this.turnCoordinatorInstrumentControl1.Name = "turnCoordinatorInstrumentControl1";
            this.turnCoordinatorInstrumentControl1.Size = new System.Drawing.Size(188, 173);
            this.turnCoordinatorInstrumentControl1.TabIndex = 2;
            this.turnCoordinatorInstrumentControl1.Text = "turnCoordinatorInstrumentControl1";
            // 
            // airSpeedIndicatorInstrumentControl1
            // 
            this.airSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(1249, 235);
            this.airSpeedIndicatorInstrumentControl1.Name = "airSpeedIndicatorInstrumentControl1";
            this.airSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(259, 281);
            this.airSpeedIndicatorInstrumentControl1.TabIndex = 3;
            this.airSpeedIndicatorInstrumentControl1.Text = "airSpeedIndicatorInstrumentControl1";
            // 
            // TemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1786, 811);
            this.Controls.Add(this.airSpeedIndicatorInstrumentControl1);
            this.Controls.Add(this.turnCoordinatorInstrumentControl1);
            this.Controls.Add(this.attitudeIndicatorInstrumentControl1);
            this.Controls.Add(this.b737PFD1);
            this.Name = "TemForm";
            this.Text = "TemForm";
            this.ResumeLayout(false);

        }

        #endregion

        private B737PFD.B737PFD b737PFD1;
        private OtherInstruments.AttitudeIndicatorInstrumentControl attitudeIndicatorInstrumentControl1;
        private OtherInstruments.TurnCoordinatorInstrumentControl turnCoordinatorInstrumentControl1;
        private OtherInstruments.AirSpeedIndicatorInstrumentControl airSpeedIndicatorInstrumentControl1;
    }
}