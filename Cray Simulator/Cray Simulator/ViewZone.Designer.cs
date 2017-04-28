namespace Cray_Simulator
{
    partial class ViewZone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewZone));
            this.panel_Images = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_Images
            // 
            this.panel_Images.AutoScroll = true;
            this.panel_Images.Location = new System.Drawing.Point(1, 0);
            this.panel_Images.Name = "panel_Images";
            this.panel_Images.Size = new System.Drawing.Size(513, 223);
            this.panel_Images.TabIndex = 0;
            // 
            // ViewZone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 223);
            this.Controls.Add(this.panel_Images);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewZone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ViewZone";
            this.Load += new System.EventHandler(this.ViewZone_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Images;
    }
}