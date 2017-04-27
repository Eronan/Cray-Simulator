using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cray_Simulator
{
    public partial class Preferences : Form
    {
        Bitmap mainDeck = null;
        Bitmap GZone = null;
        Bitmap Playmat = null;

        public Preferences()
        {
            InitializeComponent();

            //User Settings
            Properties.Settings userSettings = Properties.Settings.Default;

            textBox_Username.Text = userSettings.Username;
            textBox_Deck.Text = userSettings.Deck_Default;
            checkBox_PrevDeck.Checked = userSettings.LoadPrevDeck;
            checkBox_Shuffle.Checked = userSettings.AutoShuffle;
            mainDeck = new Bitmap(ImageConverter.ToBitmap(userSettings.MainSleeve), new Size(123, 176));
            GZone = new Bitmap(ImageConverter.ToBitmap(userSettings.GSleeve), new Size(123, 176));
            Playmat = new Bitmap(ImageConverter.ToBitmap(userSettings.Playmat), new Size(498, 590));
        }

        private void checkBox_PrevDeck_CheckedChanged(object sender, EventArgs e)
        {
            //Buton Enabled State is Contrary to Checked State
            button_Deck.Enabled = !checkBox_PrevDeck.Checked;
        }

        public void SetValues()
        {
            //Save Values
            Properties.Settings userSettings = Properties.Settings.Default;

            userSettings.Username = textBox_Username.Text;
            userSettings.Deck_Default = textBox_Deck.Text;
            userSettings.LoadPrevDeck = checkBox_PrevDeck.Checked;
            userSettings.AutoShuffle = checkBox_Shuffle.Checked;

            userSettings.MainSleeve = ImageConverter.ToString(mainDeck);
            userSettings.GSleeve = ImageConverter.ToString(GZone);
            userSettings.Playmat = ImageConverter.ToString(Playmat);

            userSettings.Save();
        }

        private void button_Deck_Click(object sender, EventArgs e)
        {
            //Open File
            if (openFile_Deck.ShowDialog(this) == DialogResult.OK) textBox_Deck.Text = openFile_Deck.FileName;
        }

        private void button_Sleeves_Click(object sender, EventArgs e)
        {
            Sleeves imageForm = new Sleeves(mainDeck, GZone, Playmat);
            if (imageForm.ShowDialog(this) == DialogResult.OK)
            {
                //Save Images
                mainDeck = imageForm.MainSleeve;
                GZone = imageForm.GSleeve;
                Playmat = imageForm.Mat;
            }
        }
    }
}
