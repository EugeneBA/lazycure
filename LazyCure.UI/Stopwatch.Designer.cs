namespace LifeIdea.LazyCure.UI
{
    partial class Stopwatch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._labelDisplay = new System.Windows.Forms.Label();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _labelDisplay
            // 
            this._labelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this._labelDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._labelDisplay.Location = new System.Drawing.Point(0, 0);
            this._labelDisplay.Name = "_labelDisplay";
            this._labelDisplay.Size = new System.Drawing.Size(553, 282);
            this._labelDisplay.TabIndex = 0;
            this._labelDisplay.Text = "00 : 00 : 00 . 0";
            this._labelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _timer
            // 
            this._timer.Interval = 300;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // Stopwatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._labelDisplay);
            this.Name = "Stopwatch";
            this.Size = new System.Drawing.Size(553, 282);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _labelDisplay;
        private System.Windows.Forms.Timer _timer;
    }
}
