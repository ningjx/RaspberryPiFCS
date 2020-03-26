namespace RaspberryPiClient
{
    partial class NDwithMap
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
            this.a350ND1 = new PlaneInstrumentControlLibrary.A350ND.A350ND();
            this.SuspendLayout();
            // 
            // a350ND1
            // 
            this.a350ND1.Location = new System.Drawing.Point(449, 259);
            this.a350ND1.Name = "a350ND1";
            this.a350ND1.Size = new System.Drawing.Size(279, 267);
            this.a350ND1.TabIndex = 0;
            this.a350ND1.Text = "a350ND1";
            this.a350ND1.Click += new System.EventHandler(this.a350ND1_Click);
            // 
            // NDwithMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 671);
            this.Controls.Add(this.a350ND1);
            this.Name = "NDwithMap";
            this.Text = "NDwithMap";
            this.ResumeLayout(false);

        }

        #endregion

        private PlaneInstrumentControlLibrary.A350ND.A350ND a350ND1;
    }
}