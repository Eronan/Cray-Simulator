namespace Cray_Simulator
{
    partial class AdvSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvSearch));
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.label_Abilities = new System.Windows.Forms.Label();
            this.richTextBox_Abilities = new System.Windows.Forms.RichTextBox();
            this.numeric_PowerMax = new System.Windows.Forms.NumericUpDown();
            this.numeric_PowerMin = new System.Windows.Forms.NumericUpDown();
            this.label_Power2 = new System.Windows.Forms.Label();
            this.label_Power = new System.Windows.Forms.Label();
            this.comboBox_UClass = new System.Windows.Forms.ComboBox();
            this.label_UClass = new System.Windows.Forms.Label();
            this.numeric_GradeMax = new System.Windows.Forms.NumericUpDown();
            this.numeric_GradeMin = new System.Windows.Forms.NumericUpDown();
            this.label_Grade2 = new System.Windows.Forms.Label();
            this.label_Grade = new System.Windows.Forms.Label();
            this.comboBox_Race = new System.Windows.Forms.ComboBox();
            this.label_Race = new System.Windows.Forms.Label();
            this.comboBox_Clan = new System.Windows.Forms.ComboBox();
            this.label_Clan = new System.Windows.Forms.Label();
            this.textBox_Set = new System.Windows.Forms.TextBox();
            this.label_Set = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_PowerMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_PowerMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_GradeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_GradeMin)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Cancel.Location = new System.Drawing.Point(165, 288);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 43;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(81, 288);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 42;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // label_Abilities
            // 
            this.label_Abilities.AutoSize = true;
            this.label_Abilities.Location = new System.Drawing.Point(12, 117);
            this.label_Abilities.Name = "label_Abilities";
            this.label_Abilities.Size = new System.Drawing.Size(45, 13);
            this.label_Abilities.TabIndex = 41;
            this.label_Abilities.Text = "Abilities:";
            // 
            // richTextBox_Abilities
            // 
            this.richTextBox_Abilities.Location = new System.Drawing.Point(15, 133);
            this.richTextBox_Abilities.Name = "richTextBox_Abilities";
            this.richTextBox_Abilities.Size = new System.Drawing.Size(277, 149);
            this.richTextBox_Abilities.TabIndex = 40;
            this.richTextBox_Abilities.Text = "";
            // 
            // numeric_PowerMax
            // 
            this.numeric_PowerMax.Location = new System.Drawing.Point(183, 94);
            this.numeric_PowerMax.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numeric_PowerMax.Name = "numeric_PowerMax";
            this.numeric_PowerMax.Size = new System.Drawing.Size(58, 20);
            this.numeric_PowerMax.TabIndex = 39;
            this.numeric_PowerMax.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // numeric_PowerMin
            // 
            this.numeric_PowerMin.Location = new System.Drawing.Point(73, 94);
            this.numeric_PowerMin.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numeric_PowerMin.Name = "numeric_PowerMin";
            this.numeric_PowerMin.Size = new System.Drawing.Size(58, 20);
            this.numeric_PowerMin.TabIndex = 38;
            // 
            // label_Power2
            // 
            this.label_Power2.AutoSize = true;
            this.label_Power2.Location = new System.Drawing.Point(137, 96);
            this.label_Power2.Name = "label_Power2";
            this.label_Power2.Size = new System.Drawing.Size(40, 13);
            this.label_Power2.TabIndex = 37;
            this.label_Power2.Text = "and <=";
            // 
            // label_Power
            // 
            this.label_Power.AutoSize = true;
            this.label_Power.Location = new System.Drawing.Point(12, 96);
            this.label_Power.Name = "label_Power";
            this.label_Power.Size = new System.Drawing.Size(55, 13);
            this.label_Power.TabIndex = 36;
            this.label_Power.Text = "Power: >=";
            // 
            // comboBox_UClass
            // 
            this.comboBox_UClass.BackColor = System.Drawing.Color.White;
            this.comboBox_UClass.DropDownHeight = 212;
            this.comboBox_UClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_UClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_UClass.ForeColor = System.Drawing.Color.Black;
            this.comboBox_UClass.IntegralHeight = false;
            this.comboBox_UClass.Items.AddRange(new object[] {
            "Any",
            "Normal Unit",
            "Trigger Unit",
            "G Unit"});
            this.comboBox_UClass.Location = new System.Drawing.Point(197, 36);
            this.comboBox_UClass.MaxDropDownItems = 16;
            this.comboBox_UClass.Name = "comboBox_UClass";
            this.comboBox_UClass.Size = new System.Drawing.Size(95, 21);
            this.comboBox_UClass.TabIndex = 29;
            // 
            // label_UClass
            // 
            this.label_UClass.AutoSize = true;
            this.label_UClass.Location = new System.Drawing.Point(162, 39);
            this.label_UClass.Name = "label_UClass";
            this.label_UClass.Size = new System.Drawing.Size(29, 13);
            this.label_UClass.TabIndex = 28;
            this.label_UClass.Text = "Unit:";
            // 
            // numeric_GradeMax
            // 
            this.numeric_GradeMax.Location = new System.Drawing.Point(266, 64);
            this.numeric_GradeMax.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numeric_GradeMax.Name = "numeric_GradeMax";
            this.numeric_GradeMax.Size = new System.Drawing.Size(26, 20);
            this.numeric_GradeMax.TabIndex = 35;
            this.numeric_GradeMax.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numeric_GradeMin
            // 
            this.numeric_GradeMin.Location = new System.Drawing.Point(188, 64);
            this.numeric_GradeMin.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numeric_GradeMin.Name = "numeric_GradeMin";
            this.numeric_GradeMin.Size = new System.Drawing.Size(26, 20);
            this.numeric_GradeMin.TabIndex = 33;
            // 
            // label_Grade2
            // 
            this.label_Grade2.AutoSize = true;
            this.label_Grade2.Location = new System.Drawing.Point(220, 66);
            this.label_Grade2.Name = "label_Grade2";
            this.label_Grade2.Size = new System.Drawing.Size(40, 13);
            this.label_Grade2.TabIndex = 34;
            this.label_Grade2.Text = "and <=";
            // 
            // label_Grade
            // 
            this.label_Grade.AutoSize = true;
            this.label_Grade.Location = new System.Drawing.Point(128, 66);
            this.label_Grade.Name = "label_Grade";
            this.label_Grade.Size = new System.Drawing.Size(54, 13);
            this.label_Grade.TabIndex = 32;
            this.label_Grade.Text = "Grade: >=";
            // 
            // comboBox_Race
            // 
            this.comboBox_Race.BackColor = System.Drawing.Color.White;
            this.comboBox_Race.DropDownHeight = 212;
            this.comboBox_Race.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Race.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_Race.ForeColor = System.Drawing.Color.Black;
            this.comboBox_Race.IntegralHeight = false;
            this.comboBox_Race.Items.AddRange(new object[] {
            "Any"});
            this.comboBox_Race.Location = new System.Drawing.Point(52, 63);
            this.comboBox_Race.MaxDropDownItems = 16;
            this.comboBox_Race.Name = "comboBox_Race";
            this.comboBox_Race.Size = new System.Drawing.Size(70, 21);
            this.comboBox_Race.TabIndex = 31;
            // 
            // label_Race
            // 
            this.label_Race.AutoSize = true;
            this.label_Race.Location = new System.Drawing.Point(12, 66);
            this.label_Race.Name = "label_Race";
            this.label_Race.Size = new System.Drawing.Size(39, 13);
            this.label_Race.TabIndex = 30;
            this.label_Race.Text = "Race: ";
            // 
            // comboBox_Clan
            // 
            this.comboBox_Clan.BackColor = System.Drawing.Color.White;
            this.comboBox_Clan.DropDownHeight = 212;
            this.comboBox_Clan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Clan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_Clan.ForeColor = System.Drawing.Color.Black;
            this.comboBox_Clan.IntegralHeight = false;
            this.comboBox_Clan.ItemHeight = 13;
            this.comboBox_Clan.Items.AddRange(new object[] {
            "Any"});
            this.comboBox_Clan.Location = new System.Drawing.Point(52, 36);
            this.comboBox_Clan.MaxDropDownItems = 30;
            this.comboBox_Clan.Name = "comboBox_Clan";
            this.comboBox_Clan.Size = new System.Drawing.Size(104, 21);
            this.comboBox_Clan.TabIndex = 27;
            // 
            // label_Clan
            // 
            this.label_Clan.AutoSize = true;
            this.label_Clan.Location = new System.Drawing.Point(12, 39);
            this.label_Clan.Name = "label_Clan";
            this.label_Clan.Size = new System.Drawing.Size(34, 13);
            this.label_Clan.TabIndex = 26;
            this.label_Clan.Text = "Clan: ";
            // 
            // textBox_Set
            // 
            this.textBox_Set.Location = new System.Drawing.Point(237, 10);
            this.textBox_Set.Name = "textBox_Set";
            this.textBox_Set.Size = new System.Drawing.Size(55, 20);
            this.textBox_Set.TabIndex = 25;
            // 
            // label_Set
            // 
            this.label_Set.AutoSize = true;
            this.label_Set.Location = new System.Drawing.Point(202, 13);
            this.label_Set.Name = "label_Set";
            this.label_Set.Size = new System.Drawing.Size(29, 13);
            this.label_Set.TabIndex = 24;
            this.label_Set.Text = "Set: ";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(12, 13);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(66, 13);
            this.label_Name.TabIndex = 22;
            this.label_Name.Text = "Card Name: ";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(84, 10);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(112, 20);
            this.textBox_Name.TabIndex = 23;
            // 
            // AdvSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 320);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label_Abilities);
            this.Controls.Add(this.richTextBox_Abilities);
            this.Controls.Add(this.numeric_PowerMax);
            this.Controls.Add(this.numeric_PowerMin);
            this.Controls.Add(this.label_Power2);
            this.Controls.Add(this.label_Power);
            this.Controls.Add(this.comboBox_UClass);
            this.Controls.Add(this.label_UClass);
            this.Controls.Add(this.numeric_GradeMax);
            this.Controls.Add(this.numeric_GradeMin);
            this.Controls.Add(this.label_Grade2);
            this.Controls.Add(this.label_Grade);
            this.Controls.Add(this.comboBox_Race);
            this.Controls.Add(this.label_Race);
            this.Controls.Add(this.comboBox_Clan);
            this.Controls.Add(this.label_Clan);
            this.Controls.Add(this.textBox_Set);
            this.Controls.Add(this.label_Set);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.textBox_Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(320, 359);
            this.Name = "AdvSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AdvSearch";
            this.Load += new System.EventHandler(this.AdvSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numeric_PowerMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_PowerMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_GradeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_GradeMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_Abilities;
        private System.Windows.Forms.RichTextBox richTextBox_Abilities;
        private System.Windows.Forms.NumericUpDown numeric_PowerMax;
        private System.Windows.Forms.NumericUpDown numeric_PowerMin;
        private System.Windows.Forms.Label label_Power2;
        private System.Windows.Forms.Label label_Power;
        private System.Windows.Forms.ComboBox comboBox_UClass;
        private System.Windows.Forms.Label label_UClass;
        private System.Windows.Forms.NumericUpDown numeric_GradeMax;
        private System.Windows.Forms.NumericUpDown numeric_GradeMin;
        private System.Windows.Forms.Label label_Grade2;
        private System.Windows.Forms.Label label_Grade;
        private System.Windows.Forms.ComboBox comboBox_Race;
        private System.Windows.Forms.Label label_Race;
        private System.Windows.Forms.ComboBox comboBox_Clan;
        private System.Windows.Forms.Label label_Clan;
        private System.Windows.Forms.TextBox textBox_Set;
        private System.Windows.Forms.Label label_Set;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Name;
    }
}