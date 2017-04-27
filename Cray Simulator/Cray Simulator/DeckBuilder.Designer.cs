namespace Cray_Simulator
{
    partial class DeckBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeckBuilder));
            this.saveFile_Deck = new System.Windows.Forms.SaveFileDialog();
            this.label_Counts = new System.Windows.Forms.Label();
            this.DeckMenu_SVG = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox_Info = new System.Windows.Forms.RichTextBox();
            this.label_G = new System.Windows.Forms.Label();
            this.listBox_G = new System.Windows.Forms.ListBox();
            this.Context_DeckMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeckMenu_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.label_Trigger = new System.Windows.Forms.Label();
            this.listBox_Trigger = new System.Windows.Forms.ListBox();
            this.label_Normal = new System.Windows.Forms.Label();
            this.openFile_Deck = new System.Windows.Forms.OpenFileDialog();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenu_FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_New = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_Fight = new System.Windows.Forms.ToolStripMenuItem();
            this.FightMenu_Test = new System.Windows.Forms.ToolStripMenuItem();
            this.FightMenu_Listen = new System.Windows.Forms.ToolStripMenuItem();
            this.FightMenu_Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu_ImageClean = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_Search = new System.Windows.Forms.ListBox();
            this.textBox_SetSearch = new System.Windows.Forms.TextBox();
            this.textBox_NameSearch = new System.Windows.Forms.TextBox();
            this.listBox_Normal = new System.Windows.Forms.ListBox();
            this.pictureBox_Info = new System.Windows.Forms.PictureBox();
            this.Button_AdvSearch = new System.Windows.Forms.Button();
            this.Context_DeckMenu.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Info)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFile_Deck
            // 
            this.saveFile_Deck.FileName = "Default.vgd";
            this.saveFile_Deck.Filter = "Cray Deck Files (*.cra) |*.cra";
            this.saveFile_Deck.FilterIndex = 2;
            // 
            // label_Counts
            // 
            this.label_Counts.AutoSize = true;
            this.label_Counts.Location = new System.Drawing.Point(528, 28);
            this.label_Counts.Name = "label_Counts";
            this.label_Counts.Size = new System.Drawing.Size(45, 169);
            this.label_Counts.TabIndex = 12;
            this.label_Counts.Text = "Deck: 0\r\nSnt: 0\r\nG0: 0\r\nG1: 0\r\nG2: 0\r\nG3: 0\r\nG4: 0\r\nHT: 0\r\nCT: 0\r\nST: 0\r\nDT: 0\r\nS" +
    "tr: 0\r\nGGd: 0";
            // 
            // DeckMenu_SVG
            // 
            this.DeckMenu_SVG.Name = "DeckMenu_SVG";
            this.DeckMenu_SVG.Size = new System.Drawing.Size(153, 22);
            this.DeckMenu_SVG.Text = "Tag Starting Vg";
            this.DeckMenu_SVG.Click += new System.EventHandler(this.DeckMenu_SVG_Click);
            // 
            // richTextBox_Info
            // 
            this.richTextBox_Info.BackColor = System.Drawing.Color.White;
            this.richTextBox_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_Info.Location = new System.Drawing.Point(578, 199);
            this.richTextBox_Info.Name = "richTextBox_Info";
            this.richTextBox_Info.ReadOnly = true;
            this.richTextBox_Info.Size = new System.Drawing.Size(293, 215);
            this.richTextBox_Info.TabIndex = 11;
            this.richTextBox_Info.Text = "";
            // 
            // label_G
            // 
            this.label_G.AutoSize = true;
            this.label_G.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_G.Location = new System.Drawing.Point(357, 277);
            this.label_G.Name = "label_G";
            this.label_G.Size = new System.Drawing.Size(54, 13);
            this.label_G.TabIndex = 9;
            this.label_G.Text = "G Units: 0";
            // 
            // listBox_G
            // 
            this.listBox_G.DisplayMember = "Value";
            this.listBox_G.FormattingEnabled = true;
            this.listBox_G.Location = new System.Drawing.Point(357, 293);
            this.listBox_G.Name = "listBox_G";
            this.listBox_G.Size = new System.Drawing.Size(165, 121);
            this.listBox_G.TabIndex = 10;
            this.listBox_G.ValueMember = "Key";
            this.listBox_G.SelectedIndexChanged += new System.EventHandler(this.listBox_G_SelectedIndexChanged);
            this.listBox_G.DoubleClick += new System.EventHandler(this.listBox_Normal_DoubleClick);
            this.listBox_G.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            // 
            // Context_DeckMenu
            // 
            this.Context_DeckMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeckMenu_Remove,
            this.DeckMenu_SVG});
            this.Context_DeckMenu.Name = "DeckMenuStrip";
            this.Context_DeckMenu.Size = new System.Drawing.Size(154, 48);
            // 
            // DeckMenu_Remove
            // 
            this.DeckMenu_Remove.Name = "DeckMenu_Remove";
            this.DeckMenu_Remove.Size = new System.Drawing.Size(153, 22);
            this.DeckMenu_Remove.Text = "Remove";
            this.DeckMenu_Remove.Click += new System.EventHandler(this.listBox_Normal_DoubleClick);
            // 
            // label_Trigger
            // 
            this.label_Trigger.AutoSize = true;
            this.label_Trigger.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_Trigger.Location = new System.Drawing.Point(354, 28);
            this.label_Trigger.Name = "label_Trigger";
            this.label_Trigger.Size = new System.Drawing.Size(79, 13);
            this.label_Trigger.TabIndex = 7;
            this.label_Trigger.Text = "Trigger Units: 0";
            // 
            // listBox_Trigger
            // 
            this.listBox_Trigger.ContextMenuStrip = this.Context_DeckMenu;
            this.listBox_Trigger.DisplayMember = "Value";
            this.listBox_Trigger.FormattingEnabled = true;
            this.listBox_Trigger.Location = new System.Drawing.Point(357, 46);
            this.listBox_Trigger.Name = "listBox_Trigger";
            this.listBox_Trigger.Size = new System.Drawing.Size(165, 225);
            this.listBox_Trigger.TabIndex = 8;
            this.listBox_Trigger.ValueMember = "Key";
            this.listBox_Trigger.SelectedIndexChanged += new System.EventHandler(this.listBox_Trigger_SelectedIndexChanged);
            this.listBox_Trigger.DoubleClick += new System.EventHandler(this.listBox_Normal_DoubleClick);
            this.listBox_Trigger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            // 
            // label_Normal
            // 
            this.label_Normal.AutoSize = true;
            this.label_Normal.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label_Normal.Location = new System.Drawing.Point(183, 30);
            this.label_Normal.Name = "label_Normal";
            this.label_Normal.Size = new System.Drawing.Size(79, 13);
            this.label_Normal.TabIndex = 5;
            this.label_Normal.Text = "Normal Units: 0";
            // 
            // openFile_Deck
            // 
            this.openFile_Deck.Filter = "Cray Deck Files (*.cra) |*.cra";
            this.openFile_Deck.FilterIndex = 2;
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MainMenu.Font = new System.Drawing.Font("Segoe UI", 9.1F);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_FileMenu,
            this.MainMenu_Fight,
            this.helpToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.MainMenu.Size = new System.Drawing.Size(881, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "Main Menu";
            // 
            // MainMenu_FileMenu
            // 
            this.MainMenu_FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu_New,
            this.FileMenu_Open,
            this.FileMenu_Save,
            this.FileMenu_SaveAs,
            this.FileMenu_Export});
            this.MainMenu_FileMenu.Name = "MainMenu_FileMenu";
            this.MainMenu_FileMenu.Size = new System.Drawing.Size(39, 24);
            this.MainMenu_FileMenu.Text = "File";
            // 
            // FileMenu_New
            // 
            this.FileMenu_New.Name = "FileMenu_New";
            this.FileMenu_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileMenu_New.Size = new System.Drawing.Size(208, 22);
            this.FileMenu_New.Text = "New";
            this.FileMenu_New.Click += new System.EventHandler(this.FileMenu_New_Click);
            // 
            // FileMenu_Open
            // 
            this.FileMenu_Open.Name = "FileMenu_Open";
            this.FileMenu_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileMenu_Open.Size = new System.Drawing.Size(208, 22);
            this.FileMenu_Open.Text = "Open";
            this.FileMenu_Open.Click += new System.EventHandler(this.FileMenu_Open_Click);
            // 
            // FileMenu_Save
            // 
            this.FileMenu_Save.Name = "FileMenu_Save";
            this.FileMenu_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileMenu_Save.Size = new System.Drawing.Size(208, 22);
            this.FileMenu_Save.Text = "Save";
            this.FileMenu_Save.Click += new System.EventHandler(this.FileMenu_Save_Click);
            // 
            // FileMenu_SaveAs
            // 
            this.FileMenu_SaveAs.Name = "FileMenu_SaveAs";
            this.FileMenu_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.FileMenu_SaveAs.Size = new System.Drawing.Size(208, 22);
            this.FileMenu_SaveAs.Text = "Save As...";
            this.FileMenu_SaveAs.Click += new System.EventHandler(this.FileMenu_SaveAs_Click);
            // 
            // FileMenu_Export
            // 
            this.FileMenu_Export.Name = "FileMenu_Export";
            this.FileMenu_Export.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.FileMenu_Export.Size = new System.Drawing.Size(208, 22);
            this.FileMenu_Export.Text = "Export";
            this.FileMenu_Export.Click += new System.EventHandler(this.FileMenu_Export_Click);
            // 
            // MainMenu_Fight
            // 
            this.MainMenu_Fight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FightMenu_Test,
            this.FightMenu_Listen,
            this.FightMenu_Connect});
            this.MainMenu_Fight.Name = "MainMenu_Fight";
            this.MainMenu_Fight.Size = new System.Drawing.Size(48, 24);
            this.MainMenu_Fight.Text = "Fight";
            // 
            // FightMenu_Test
            // 
            this.FightMenu_Test.Name = "FightMenu_Test";
            this.FightMenu_Test.Size = new System.Drawing.Size(123, 22);
            this.FightMenu_Test.Text = "Test";
            this.FightMenu_Test.Click += new System.EventHandler(this.FightMenu_Test_Click);
            // 
            // FightMenu_Listen
            // 
            this.FightMenu_Listen.Name = "FightMenu_Listen";
            this.FightMenu_Listen.Size = new System.Drawing.Size(123, 22);
            this.FightMenu_Listen.Text = "Listen";
            this.FightMenu_Listen.Visible = false;
            this.FightMenu_Listen.Click += new System.EventHandler(this.FightMenu_Listen_Click);
            // 
            // FightMenu_Connect
            // 
            this.FightMenu_Connect.Name = "FightMenu_Connect";
            this.FightMenu_Connect.Size = new System.Drawing.Size(123, 22);
            this.FightMenu_Connect.Text = "Connect";
            this.FightMenu_Connect.Visible = false;
            this.FightMenu_Connect.Click += new System.EventHandler(this.FightMenu_Connect_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenu_About,
            this.HelpMenu_Settings,
            this.HelpMenu_ImageClean});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // HelpMenu_About
            // 
            this.HelpMenu_About.Name = "HelpMenu_About";
            this.HelpMenu_About.Size = new System.Drawing.Size(188, 22);
            this.HelpMenu_About.Text = "About";
            this.HelpMenu_About.Click += new System.EventHandler(this.HelpMenu_About_Click);
            // 
            // HelpMenu_Settings
            // 
            this.HelpMenu_Settings.Name = "HelpMenu_Settings";
            this.HelpMenu_Settings.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.HelpMenu_Settings.Size = new System.Drawing.Size(188, 22);
            this.HelpMenu_Settings.Text = "Preferences";
            this.HelpMenu_Settings.Click += new System.EventHandler(this.HelpMenu_Settings_Click);
            // 
            // HelpMenu_ImageClean
            // 
            this.HelpMenu_ImageClean.Name = "HelpMenu_ImageClean";
            this.HelpMenu_ImageClean.Size = new System.Drawing.Size(188, 22);
            this.HelpMenu_ImageClean.Text = "Clean Images";
            this.HelpMenu_ImageClean.Click += new System.EventHandler(this.HelpMenu_ImageClean_Click);
            // 
            // listBox_Search
            // 
            this.listBox_Search.DisplayMember = "Value";
            this.listBox_Search.FormattingEnabled = true;
            this.listBox_Search.Location = new System.Drawing.Point(12, 59);
            this.listBox_Search.Name = "listBox_Search";
            this.listBox_Search.Size = new System.Drawing.Size(168, 355);
            this.listBox_Search.TabIndex = 4;
            this.listBox_Search.ValueMember = "Key";
            this.listBox_Search.SelectedIndexChanged += new System.EventHandler(this.listBox_Search_SelectedIndexChanged);
            this.listBox_Search.DoubleClick += new System.EventHandler(this.listBox_Search_DoubleClick);
            this.listBox_Search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox_Search_KeyPress);
            // 
            // textBox_SetSearch
            // 
            this.textBox_SetSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SetSearch.ForeColor = System.Drawing.Color.Gray;
            this.textBox_SetSearch.Location = new System.Drawing.Point(111, 31);
            this.textBox_SetSearch.Name = "textBox_SetSearch";
            this.textBox_SetSearch.Size = new System.Drawing.Size(45, 20);
            this.textBox_SetSearch.TabIndex = 2;
            this.textBox_SetSearch.Text = "Set";
            this.textBox_SetSearch.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.textBox_SetSearch.GotFocus += new System.EventHandler(this.SearchBox_GotFocus);
            this.textBox_SetSearch.LostFocus += new System.EventHandler(this.textBox_SetSearch_LostFocus);
            // 
            // textBox_NameSearch
            // 
            this.textBox_NameSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_NameSearch.ForeColor = System.Drawing.Color.Gray;
            this.textBox_NameSearch.Location = new System.Drawing.Point(12, 31);
            this.textBox_NameSearch.Name = "textBox_NameSearch";
            this.textBox_NameSearch.Size = new System.Drawing.Size(97, 20);
            this.textBox_NameSearch.TabIndex = 1;
            this.textBox_NameSearch.Text = "Name";
            this.textBox_NameSearch.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.textBox_NameSearch.GotFocus += new System.EventHandler(this.SearchBox_GotFocus);
            this.textBox_NameSearch.LostFocus += new System.EventHandler(this.textBox_NameSearch_LostFocus);
            // 
            // listBox_Normal
            // 
            this.listBox_Normal.ContextMenuStrip = this.Context_DeckMenu;
            this.listBox_Normal.DisplayMember = "Value";
            this.listBox_Normal.FormattingEnabled = true;
            this.listBox_Normal.Location = new System.Drawing.Point(186, 46);
            this.listBox_Normal.Name = "listBox_Normal";
            this.listBox_Normal.Size = new System.Drawing.Size(165, 368);
            this.listBox_Normal.TabIndex = 6;
            this.listBox_Normal.ValueMember = "Key";
            this.listBox_Normal.SelectedIndexChanged += new System.EventHandler(this.listBox_Normal_SelectedIndexChanged);
            this.listBox_Normal.DoubleClick += new System.EventHandler(this.listBox_Normal_DoubleClick);
            this.listBox_Normal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            // 
            // pictureBox_Info
            // 
            this.pictureBox_Info.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Info.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Info.Location = new System.Drawing.Point(665, 31);
            this.pictureBox_Info.Name = "pictureBox_Info";
            this.pictureBox_Info.Size = new System.Drawing.Size(120, 162);
            this.pictureBox_Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Info.TabIndex = 36;
            this.pictureBox_Info.TabStop = false;
            // 
            // Button_AdvSearch
            // 
            this.Button_AdvSearch.BackColor = System.Drawing.Color.Transparent;
            this.Button_AdvSearch.FlatAppearance.BorderSize = 0;
            this.Button_AdvSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_AdvSearch.Image = global::Cray_Simulator.Properties.Resources.Icon_Search;
            this.Button_AdvSearch.Location = new System.Drawing.Point(157, 28);
            this.Button_AdvSearch.Margin = new System.Windows.Forms.Padding(0);
            this.Button_AdvSearch.Name = "Button_AdvSearch";
            this.Button_AdvSearch.Size = new System.Drawing.Size(23, 23);
            this.Button_AdvSearch.TabIndex = 3;
            this.Button_AdvSearch.UseVisualStyleBackColor = false;
            this.Button_AdvSearch.Click += new System.EventHandler(this.Button_AdvSearch_Click);
            // 
            // DeckBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 418);
            this.Controls.Add(this.label_Counts);
            this.Controls.Add(this.richTextBox_Info);
            this.Controls.Add(this.pictureBox_Info);
            this.Controls.Add(this.label_G);
            this.Controls.Add(this.listBox_G);
            this.Controls.Add(this.label_Trigger);
            this.Controls.Add(this.listBox_Trigger);
            this.Controls.Add(this.label_Normal);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.listBox_Search);
            this.Controls.Add(this.Button_AdvSearch);
            this.Controls.Add(this.textBox_SetSearch);
            this.Controls.Add(this.textBox_NameSearch);
            this.Controls.Add(this.listBox_Normal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(897, 457);
            this.Name = "DeckBuilder";
            this.Text = "Cray Simulator";
            this.Load += new System.EventHandler(this.DeckBuilder_Load);
            this.Context_DeckMenu.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Info)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFile_Deck;
        private System.Windows.Forms.Label label_Counts;
        private System.Windows.Forms.ToolStripMenuItem DeckMenu_SVG;
        private System.Windows.Forms.RichTextBox richTextBox_Info;
        private System.Windows.Forms.PictureBox pictureBox_Info;
        private System.Windows.Forms.Label label_G;
        private System.Windows.Forms.ListBox listBox_G;
        private System.Windows.Forms.ContextMenuStrip Context_DeckMenu;
        private System.Windows.Forms.ToolStripMenuItem DeckMenu_Remove;
        private System.Windows.Forms.Label label_Trigger;
        private System.Windows.Forms.ListBox listBox_Trigger;
        private System.Windows.Forms.Label label_Normal;
        private System.Windows.Forms.OpenFileDialog openFile_Deck;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_New;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Open;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Save;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Export;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_Fight;
        private System.Windows.Forms.ToolStripMenuItem FightMenu_Test;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu_About;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu_Settings;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu_ImageClean;
        private System.Windows.Forms.ListBox listBox_Search;
        private System.Windows.Forms.Button Button_AdvSearch;
        private System.Windows.Forms.TextBox textBox_SetSearch;
        private System.Windows.Forms.TextBox textBox_NameSearch;
        private System.Windows.Forms.ListBox listBox_Normal;
        private System.Windows.Forms.ToolStripMenuItem FightMenu_Listen;
        private System.Windows.Forms.ToolStripMenuItem FightMenu_Connect;
    }
}

