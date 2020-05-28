namespace RaspberryPiClient.Forms
{
    partial class EFISTest
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
            this.b737EICAS1 = new PlaneInstrumentControlLibrary.B737EICAS.B737EICAS();
            this.SuspendLayout();
            // 
            // b737EICAS1
            // 
            this.b737EICAS1.Location = new System.Drawing.Point(85, 32);
            this.b737EICAS1.Name = "b737EICAS1";
            this.b737EICAS1.Size = new System.Drawing.Size(633, 633);
            this.b737EICAS1.TabIndex = 0;
            this.b737EICAS1.Text = "b737EICAS1";
            // 
            // EFISTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1103, 925);
            this.Controls.Add(this.b737EICAS1);
            this.Name = "EFISTest";
            this.Text = "EFISTest";
            this.ResumeLayout(false);

        }

        #endregion

        private PlaneInstrumentControlLibrary.B737EICAS.B737EICAS b737EICAS1;
    }
}