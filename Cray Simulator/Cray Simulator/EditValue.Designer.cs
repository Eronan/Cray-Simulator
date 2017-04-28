namespace Cray_Simulator
{
    partial class EditValue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditValue));
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.checkBox_Add = new System.Windows.Forms.CheckBox();
            this.label_Value = new System.Windows.Forms.Label();
            this.numeric_Value = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Value)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Reset
            // 
            this.button_Reset.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.button_Reset.Location = new System.Drawing.Point(75, 34);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(58, 23);
            this.button_Reset.TabIndex = 4;
            this.button_Reset.Text = "Reset";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(139, 34);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(58, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(12, 34);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(58, 23);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // checkBox_Add
            // 
            this.checkBox_Add.AutoSize = true;
            this.checkBox_Add.Checked = true;
            this.checkBox_Add.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Add.Location = new System.Drawing.Point(126, 9);
            this.checkBox_Add.Name = "checkBox_Add";
            this.checkBox_Add.Size = new System.Drawing.Size(75, 17);
            this.checkBox_Add.TabIndex = 2;
            this.checkBox_Add.Text = "Add Value";
            this.checkBox_Add.UseVisualStyleBackColor = true;
            this.checkBox_Add.CheckedChanged += new System.EventHandler(this.checkBox_Add_CheckedChanged);
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Location = new System.Drawing.Point(9, 10);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(37, 13);
            this.label_Value.TabIndex = 0;
            this.label_Value.Text = "Value:";
            // 
            // numeric_Value
            // 
            this.numeric_Value.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numeric_Value.Location = new System.Drawing.Point(52, 8);
            this.numeric_Value.Maximum = new decimal(new int[] {
            9999000,
            0,
            0,
            0});
            this.numeric_Value.Minimum = new decimal(new int[] {
            99000,
            0,
            0,
            -2147483648});
            this.numeric_Value.Name = "numeric_Value";
            this.numeric_Value.Size = new System.Drawing.Size(68, 20);
            this.numeric_Value.TabIndex = 1;
            this.numeric_Value.ValueChanged += new System.EventHandler(this.numeric_Value_ValueChanged);
            this.numeric_Value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numeric_Value_KeyPress);
            // 
            // EditValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 65);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.checkBox_Add);
            this.Controls.Add(this.label_Value);
            this.Controls.Add(this.numeric_Value);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EditValue";
            this.Text = "EditValue";
            ((System.ComponentModel.ISupportInitialize)(this.numeric_Value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.CheckBox checkBox_Add;
        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.NumericUpDown numeric_Value;
    }
}