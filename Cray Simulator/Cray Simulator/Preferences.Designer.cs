namespace Cray_Simulator
{
    partial class Preferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
            this.label_Shuffle = new System.Windows.Forms.Label();
            this.checkBox_Shuffle = new System.Windows.Forms.CheckBox();
            this.checkBox_PrevDeck = new System.Windows.Forms.CheckBox();
            this.label_PrevDeck = new System.Windows.Forms.Label();
            this.button_Deck = new System.Windows.Forms.Button();
            this.textBox_Deck = new System.Windows.Forms.TextBox();
            this.label_Deck = new System.Windows.Forms.Label();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.label_Username = new System.Windows.Forms.Label();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.OK_Button = new System.Windows.Forms.Button();
            this.label_Sleeves = new System.Windows.Forms.Label();
            this.button_Sleeves = new System.Windows.Forms.Button();
            this.openFile_Deck = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label_Shuffle
            // 
            this.label_Shuffle.AutoSize = true;
            this.label_Shuffle.Location = new System.Drawing.Point(12, 97);
            this.label_Shuffle.Name = "label_Shuffle";
            this.label_Shuffle.Size = new System.Drawing.Size(117, 13);
            this.label_Shuffle.TabIndex = 19;
            this.label_Shuffle.Text = "Shuffle Deck On-Start: ";
            // 
            // checkBox_Shuffle
            // 
            this.checkBox_Shuffle.AutoSize = true;
            this.checkBox_Shuffle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBox_Shuffle.Location = new System.Drawing.Point(135, 97);
            this.checkBox_Shuffle.Name = "checkBox_Shuffle";
            this.checkBox_Shuffle.Size = new System.Drawing.Size(13, 12);
            this.checkBox_Shuffle.TabIndex = 18;
            this.checkBox_Shuffle.UseVisualStyleBackColor = true;
            // 
            // checkBox_PrevDeck
            // 
            this.checkBox_PrevDeck.AutoSize = true;
            this.checkBox_PrevDeck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBox_PrevDeck.Location = new System.Drawing.Point(135, 70);
            this.checkBox_PrevDeck.Name = "checkBox_PrevDeck";
            this.checkBox_PrevDeck.Size = new System.Drawing.Size(13, 12);
            this.checkBox_PrevDeck.TabIndex = 17;
            this.checkBox_PrevDeck.UseVisualStyleBackColor = true;
            this.checkBox_PrevDeck.CheckedChanged += new System.EventHandler(this.checkBox_PrevDeck_CheckedChanged);
            // 
            // label_PrevDeck
            // 
            this.label_PrevDeck.AutoSize = true;
            this.label_PrevDeck.Location = new System.Drawing.Point(22, 69);
            this.label_PrevDeck.Name = "label_PrevDeck";
            this.label_PrevDeck.Size = new System.Drawing.Size(107, 13);
            this.label_PrevDeck.TabIndex = 16;
            this.label_PrevDeck.Text = "Load Previous Deck:";
            // 
            // button_Deck
            // 
            this.button_Deck.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Deck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Deck.Location = new System.Drawing.Point(211, 37);
            this.button_Deck.Name = "button_Deck";
            this.button_Deck.Size = new System.Drawing.Size(19, 20);
            this.button_Deck.TabIndex = 15;
            this.button_Deck.Text = "...";
            this.button_Deck.UseVisualStyleBackColor = true;
            this.button_Deck.Click += new System.EventHandler(this.button_Deck_Click);
            // 
            // textBox_Deck
            // 
            this.textBox_Deck.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Deck.Enabled = false;
            this.textBox_Deck.Location = new System.Drawing.Point(91, 37);
            this.textBox_Deck.Name = "textBox_Deck";
            this.textBox_Deck.Size = new System.Drawing.Size(114, 20);
            this.textBox_Deck.TabIndex = 14;
            // 
            // label_Deck
            // 
            this.label_Deck.AutoSize = true;
            this.label_Deck.Location = new System.Drawing.Point(12, 40);
            this.label_Deck.Name = "label_Deck";
            this.label_Deck.Size = new System.Drawing.Size(73, 13);
            this.label_Deck.TabIndex = 13;
            this.label_Deck.Text = "Default Deck:";
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(91, 11);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(139, 20);
            this.textBox_Username.TabIndex = 12;
            // 
            // label_Username
            // 
            this.label_Username.AutoSize = true;
            this.label_Username.Location = new System.Drawing.Point(27, 14);
            this.label_Username.Name = "label_Username";
            this.label_Username.Size = new System.Drawing.Size(58, 13);
            this.label_Username.TabIndex = 11;
            this.label_Username.Text = "Username:";
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel_Button.Location = new System.Drawing.Point(130, 144);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 21;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            // 
            // OK_Button
            // 
            this.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OK_Button.Location = new System.Drawing.Point(49, 144);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(75, 23);
            this.OK_Button.TabIndex = 20;
            this.OK_Button.Text = "OK";
            this.OK_Button.UseVisualStyleBackColor = true;
            // 
            // label_Sleeves
            // 
            this.label_Sleeves.AutoSize = true;
            this.label_Sleeves.Location = new System.Drawing.Point(37, 120);
            this.label_Sleeves.Name = "label_Sleeves";
            this.label_Sleeves.Size = new System.Drawing.Size(48, 13);
            this.label_Sleeves.TabIndex = 22;
            this.label_Sleeves.Text = "Sleeves:";
            // 
            // button_Sleeves
            // 
            this.button_Sleeves.Location = new System.Drawing.Point(91, 115);
            this.button_Sleeves.Name = "button_Sleeves";
            this.button_Sleeves.Size = new System.Drawing.Size(75, 23);
            this.button_Sleeves.TabIndex = 23;
            this.button_Sleeves.Text = "Edit Sleeves";
            this.button_Sleeves.UseVisualStyleBackColor = true;
            this.button_Sleeves.Click += new System.EventHandler(this.button_Sleeves_Click);
            // 
            // openFile_Deck
            // 
            this.openFile_Deck.Filter = "Cray Deck Files (*.cra) |*.cra";
            this.openFile_Deck.FilterIndex = 2;
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 178);
            this.Controls.Add(this.button_Sleeves);
            this.Controls.Add(this.label_Sleeves);
            this.Controls.Add(this.label_Shuffle);
            this.Controls.Add(this.checkBox_Shuffle);
            this.Controls.Add(this.checkBox_PrevDeck);
            this.Controls.Add(this.label_PrevDeck);
            this.Controls.Add(this.button_Deck);
            this.Controls.Add(this.textBox_Deck);
            this.Controls.Add(this.label_Deck);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.label_Username);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.OK_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Preferences";
            this.Text = "Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Shuffle;
        private System.Windows.Forms.CheckBox checkBox_Shuffle;
        private System.Windows.Forms.CheckBox checkBox_PrevDeck;
        private System.Windows.Forms.Label label_PrevDeck;
        private System.Windows.Forms.Button button_Deck;
        private System.Windows.Forms.TextBox textBox_Deck;
        private System.Windows.Forms.Label label_Deck;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.Label label_Username;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Button OK_Button;
        private System.Windows.Forms.Label label_Sleeves;
        private System.Windows.Forms.Button button_Sleeves;
        private System.Windows.Forms.OpenFileDialog openFile_Deck;
    }
}