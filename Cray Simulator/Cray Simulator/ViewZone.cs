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
    public partial class ViewZone : Form
    {
        private List<PictureBox> allPictures = new List<PictureBox>();
        private ContextMenuStrip control_MenuStrip;
        private MouseEventHandler control_MouseDown;
        private EventHandler control_MouseEnter;
        private Func<Card, bool> faceUpTest;
        private Bitmap cardBack;

        public ViewZone(List<Card> Zone, string title, Battlefield Parent, Bitmap cardSleeve, Func<Card, bool> faceUpTester, ContextMenuStrip menu = null)
        {
            InitializeComponent();

            this.Text = title;

            //Set Private Values
            control_MenuStrip = menu;
            faceUpTest = faceUpTester;
            cardBack = cardSleeve;
            //Event Handlers
            control_MouseDown = new MouseEventHandler(Parent.PictureBox_MouseDown);
            control_MouseEnter = new EventHandler(Parent.PictureBox_MouseEnter);
            this.FormClosed += new FormClosedEventHandler(Parent.CurrentZone_Closed);
            
            //Update Images
            PanelUpdate(Zone);
        }

        private void ViewZone_Load(object sender, EventArgs e)
        {
            //Move to Centre
            if (this.Owner == null) return;
            //Get Owner Location
            Point location = this.Owner.Location;
            int x = location.X;
            location = this.Owner.Location;
            int y = location.Y + this.Owner.Height / 2 - this.Height;
            this.Location = new Point(x, y);
        }

        public void PanelUpdate(List<Card> cardList)
        {
            int scrollValue = panel_Images.VerticalScroll.Value;
            int index = 0;
            //Iterate Through Cards
            foreach (Card card in cardList)
            {
                //Grab Associated PictureBOx
                PictureBox pictureBox;
                if (index < allPictures.Count) pictureBox = allPictures[index];
                else
                {
                    //Create New PictureBox
                    pictureBox = new PictureBox();
                    pictureBox.Size = new Size(52, 75);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    //Events
                    pictureBox.MouseEnter += control_MouseEnter;
                    pictureBox.MouseDown += control_MouseDown;

                    //MenuStrip
                    pictureBox.ContextMenuStrip = control_MenuStrip;

                    //Add to Form
                    allPictures.Add(pictureBox);
                    panel_Images.Controls.Add(pictureBox);
                }
                //Set Value
                pictureBox.Tag = card.Location;
                pictureBox.Location = new Point(index % 9 * 55 + (cardList.Count > 18 ? 0 : 10), index / 9 * 78 - scrollValue);

                //Image based on FaceUpTester
                pictureBox.Image = faceUpTest(card) ? card.Image : cardBack;

                //Increment
                index++;
            }
            //Clear Unused PictureBoxes from the End
            for (int removeIndex = allPictures.Count - 1; removeIndex > index - 1; removeIndex--)
            {
                //Remove Unused PictureBoxes
                allPictures[removeIndex].Dispose();
                panel_Images.Controls.Remove(allPictures[removeIndex]);
                allPictures.RemoveAt(removeIndex);
            }
        }
    }
}
