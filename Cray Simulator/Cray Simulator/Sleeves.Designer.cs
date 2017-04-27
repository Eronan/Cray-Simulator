namespace Cray_Simulator
{
    partial class Sleeves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sleeves));
            this.label_MainDeck = new System.Windows.Forms.Label();
            this.label_GZone = new System.Windows.Forms.Label();
            this.pictureBox_Mat = new System.Windows.Forms.PictureBox();
            this.pictureBox_GSleeve = new System.Windows.Forms.PictureBox();
            this.pictureBox_MainSleeve = new System.Windows.Forms.PictureBox();
            this.label_Mat = new System.Windows.Forms.Label();
            this.button_MainSleeve = new System.Windows.Forms.Button();
            this.button_GSleeve = new System.Windows.Forms.Button();
            this.button_Mat = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.openFile_Image = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Mat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GSleeve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MainSleeve)).BeginInit();
            this.SuspendLayout();
            // 
            // label_MainDeck
            // 
            this.label_MainDeck.AutoSize = true;
            this.label_MainDeck.Location = new System.Drawing.Point(63, 11);
            this.label_MainDeck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_MainDeck.Name = "label_MainDeck";
            this.label_MainDeck.Size = new System.Drawing.Size(75, 16);
            this.label_MainDeck.TabIndex = 0;
            this.label_MainDeck.Text = "Main Deck:";
            // 
            // label_GZone
            // 
            this.label_GZone.AutoSize = true;
            this.label_GZone.Location = new System.Drawing.Point(248, 11);
            this.label_GZone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_GZone.Name = "label_GZone";
            this.label_GZone.Size = new System.Drawing.Size(55, 16);
            this.label_GZone.TabIndex = 1;
            this.label_GZone.Text = "G Zone:";
            // 
            // pictureBox_Mat
            // 
            this.pictureBox_Mat.Image = global::Cray_Simulator.Properties.Resources.Cray_Simulator_Loaded_Playmat;
            this.pictureBox_Mat.Location = new System.Drawing.Point(371, 31);
            this.pictureBox_Mat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox_Mat.Name = "pictureBox_Mat";
            this.pictureBox_Mat.Size = new System.Drawing.Size(467, 548);
            this.pictureBox_Mat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Mat.TabIndex = 4;
            this.pictureBox_Mat.TabStop = false;
            // 
            // pictureBox_GSleeve
            // 
            this.pictureBox_GSleeve.Image = global::Cray_Simulator.Properties.Resources.G_Unit_Sleeve_Grey;
            this.pictureBox_GSleeve.Location = new System.Drawing.Point(199, 31);
            this.pictureBox_GSleeve.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox_GSleeve.Name = "pictureBox_GSleeve";
            this.pictureBox_GSleeve.Size = new System.Drawing.Size(164, 223);
            this.pictureBox_GSleeve.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_GSleeve.TabIndex = 3;
            this.pictureBox_GSleeve.TabStop = false;
            // 
            // pictureBox_MainSleeve
            // 
            this.pictureBox_MainSleeve.Image = global::Cray_Simulator.Properties.Resources.Sleeve_Blue;
            this.pictureBox_MainSleeve.Location = new System.Drawing.Point(20, 31);
            this.pictureBox_MainSleeve.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox_MainSleeve.Name = "pictureBox_MainSleeve";
            this.pictureBox_MainSleeve.Size = new System.Drawing.Size(164, 223);
            this.pictureBox_MainSleeve.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_MainSleeve.TabIndex = 2;
            this.pictureBox_MainSleeve.TabStop = false;
            // 
            // label_Mat
            // 
            this.label_Mat.AutoSize = true;
            this.label_Mat.Location = new System.Drawing.Point(587, 11);
            this.label_Mat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Mat.Name = "label_Mat";
            this.label_Mat.Size = new System.Drawing.Size(60, 16);
            this.label_Mat.TabIndex = 5;
            this.label_Mat.Text = "Playmat:";
            // 
            // button_MainSleeve
            // 
            this.button_MainSleeve.Location = new System.Drawing.Point(52, 261);
            this.button_MainSleeve.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_MainSleeve.Name = "button_MainSleeve";
            this.button_MainSleeve.Size = new System.Drawing.Size(100, 28);
            this.button_MainSleeve.TabIndex = 6;
            this.button_MainSleeve.Text = "Open Image";
            this.button_MainSleeve.UseVisualStyleBackColor = true;
            this.button_MainSleeve.Click += new System.EventHandler(this.button_MainSleeve_Click);
            // 
            // button_GSleeve
            // 
            this.button_GSleeve.Location = new System.Drawing.Point(231, 261);
            this.button_GSleeve.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_GSleeve.Name = "button_GSleeve";
            this.button_GSleeve.Size = new System.Drawing.Size(100, 28);
            this.button_GSleeve.TabIndex = 7;
            this.button_GSleeve.Text = "Open Image";
            this.button_GSleeve.UseVisualStyleBackColor = true;
            this.button_GSleeve.Click += new System.EventHandler(this.button_GSleeve_Click);
            // 
            // button_Mat
            // 
            this.button_Mat.Location = new System.Drawing.Point(554, 587);
            this.button_Mat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Mat.Name = "button_Mat";
            this.button_Mat.Size = new System.Drawing.Size(100, 28);
            this.button_Mat.TabIndex = 8;
            this.button_Mat.Text = "Open Image";
            this.button_Mat.UseVisualStyleBackColor = true;
            this.button_Mat.Click += new System.EventHandler(this.button_Playmat_Click);
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(846, 31);
            this.button_OK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(100, 28);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // openFile_Image
            // 
            this.openFile_Image.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif;" +
    " *.png";
            // 
            // Sleeves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 628);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Mat);
            this.Controls.Add(this.button_GSleeve);
            this.Controls.Add(this.button_MainSleeve);
            this.Controls.Add(this.label_Mat);
            this.Controls.Add(this.pictureBox_Mat);
            this.Controls.Add(this.pictureBox_GSleeve);
            this.Controls.Add(this.pictureBox_MainSleeve);
            this.Controls.Add(this.label_GZone);
            this.Controls.Add(this.label_MainDeck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Sleeves";
            this.Text = "Sleeves";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Mat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GSleeve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MainSleeve)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_MainDeck;
        private System.Windows.Forms.Label label_GZone;
        private System.Windows.Forms.PictureBox pictureBox_MainSleeve;
        private System.Windows.Forms.PictureBox pictureBox_GSleeve;
        private System.Windows.Forms.PictureBox pictureBox_Mat;
        private System.Windows.Forms.Label label_Mat;
        private System.Windows.Forms.Button button_MainSleeve;
        private System.Windows.Forms.Button button_GSleeve;
        private System.Windows.Forms.Button button_Mat;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.OpenFileDialog openFile_Image;
    }
}