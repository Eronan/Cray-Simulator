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
    public partial class Battlefield : Form
    {
        //Field Information
        Field plyr_Field;
        //Field opponentField;

        //Form Information
        List<PictureBox> movingPictures = new List<PictureBox>();
        PictureBox[] RGPictures = new PictureBox[5];
        Label[] RGLabels = new Label[5];
        Card hoverCard = null;
        ViewZone currentZone = null;
        bool dragOn = false;
        string playerName = Environment.NewLine + "<" + Properties.Settings.Default.Username + "> ";

        //Sleeves
        public Bitmap mainSleeve;
        public Bitmap GSleeve;

        public Battlefield(List<Card> allCards, string startVGKey)
        {
            InitializeComponent();

            //Create Player Field
            plyr_Field = new Field(allCards, startVGKey);

            //Images
            mainSleeve = ImageConverter.ToBitmap(Properties.Settings.Default.MainSleeve);
            GSleeve = ImageConverter.ToBitmap(Properties.Settings.Default.GSleeve);
            pictureBox_Field.Image = ImageConverter.ToBitmap(Properties.Settings.Default.Playmat);

            //Set Arrays
            RGPictures = new PictureBox[5] { pictureBox_RG0, pictureBox_RG1, pictureBox_RG2, pictureBox_RG3, pictureBox_RG4 };
            RGLabels = new Label[5] { label_RG1, label_RG2, label_RG3, label_RG4, label_RG5 };

            //Automatic Shuffle
            if (Properties.Settings.Default.AutoShuffle)
            {
                plyr_Field.ShuffleDeck();
                shortcutMenu_Shuffle_Click(shortcutMenu_Shuffle, null);
            }

            //Update Images
            ImageUpdate();
        }

        public void ImageUpdate()
        {
            //Update Deck
            if (plyr_Field.Deck[0].FaceUp) pictureBox_PlyrDeck.Image = plyr_Field.Deck[0].Image;
            else pictureBox_PlyrDeck.Image = mainSleeve;
            //Set Values
            for (int i = 0; i < plyr_Field.Deck.Count; i++) plyr_Field.Deck[i].Location = "Deck-" + i;
            toolTip_Counts.SetToolTip(pictureBox_PlyrDeck, "Deck: " + plyr_Field.Deck.Count);

            //Update G Zone
            //Disable PictureBoxes
            pictureBox_GDown.Visible = false;
            pictureBox_GUp.Visible = false;
            //Enable PictureBoxes
            int gFaceUp = 0;
            for (int i = 0; i < plyr_Field.GZone.Count; i++)
            {
                Card crd = plyr_Field.GZone[i];
                crd.Location = "G Zone-" + i;
                if (crd.FaceUp)
                {
                    pictureBox_GUp.Visible = true;
                    pictureBox_GUp.Image = crd.Image;
                    pictureBox_GUp.Tag = "G Zone-" + i;
                    gFaceUp++;
                }
                else
                {
                    pictureBox_GDown.Visible = true;
                    pictureBox_GDown.Tag = "G Zone-" + i;
                }
            }
            //Set Tool Tip
            toolTip_Counts.SetToolTip(pictureBox_GUp, "G Zone: " + gFaceUp);
            toolTip_Counts.SetToolTip(pictureBox_GDown, "G Zone: " + gFaceUp);

            //Trigger Zone
            if (plyr_Field.TriggerZone != null)
            {
                plyr_Field.TriggerZone.Location = "Trigger-0";
                if (pictureBox_PlyrTrigger.Image != null) pictureBox_PlyrTrigger.Image.Dispose();
                //Image
                pictureBox_PlyrTrigger.Image = new Bitmap(plyr_Field.TriggerZone.Image);
                pictureBox_PlyrTrigger.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox_PlyrTrigger.Visible = true;
            }
            else pictureBox_PlyrTrigger.Visible = false;

            //Update Hand
            List<PictureBox> handPics = movingPictures.Where(pic => pic.Name.StartsWith("Hand")).ToList();
            for (int i = 0; i < plyr_Field.Hand.Count; i++)
            {
                PictureBox picBox;
                Card crd = plyr_Field.Hand[i];
                //Find PictureBox
                if (i < handPics.Count) picBox = handPics[i];
                else
                {
                    //Create New PictureBox
                    picBox = new PictureBox();
                    picBox.Size = new Size(52, 75);
                    picBox.Name = "Hand" + i;
                    picBox.ContextMenuStrip = HandMenu;
                    picBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
                    picBox.MouseEnter += new EventHandler(PictureBox_MouseEnter);
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    movingPictures.Add(picBox);
                    handPics.Add(picBox);
                    this.Controls.Add(picBox);
                }

                //Set Values
                if (crd.FaceUp) picBox.Image = crd.RedImage;
                else picBox.Image = crd.Image;
                picBox.Tag = "Hand-" + i;
                crd.Location = "Hand-" + i;
                if (plyr_Field.Hand.Count > 9) picBox.Location = new Point(446 / (plyr_Field.Hand.Count - 1) * i, 663);
                else picBox.Location = new Point(55 * i, 663);
                picBox.BringToFront();
            }
            //Clean up Remaining PictureBoxes
            for (int i = plyr_Field.Hand.Count; i < handPics.Count; i++)
            {
                handPics[i].Dispose();
                movingPictures.Remove(handPics[i]);
                this.Controls.Remove(handPics[i]);
            }

            //Drop Zone
            if (plyr_Field.DropZone.Count > 0)
            {
                //Update PictureBox
                pictureBox_PlyrDrop.Visible = true;
                pictureBox_PlyrDrop.Image = plyr_Field.DropZone.Last().Image;
                pictureBox_PlyrDrop.Tag = "Drop-" + (plyr_Field.DropZone.Count - 1);
                toolTip_Counts.SetToolTip(pictureBox_PlyrDeck, "Drop Zone: " + plyr_Field.DropZone.Count);

                //Drop Zone
                for (int i = 0; i < plyr_Field.DropZone.Count; i++) plyr_Field.DropZone[i].Location = "Drop-" + i;
            }
            else pictureBox_PlyrDrop.Visible = false;

            //Heart
            List<PictureBox> heartBoxes = movingPictures.Where(pic => pic.Name.StartsWith("Heart")).ToList();

            for (int i = 0; i < plyr_Field.Heart.Count; i++)
            {
                PictureBox picBox;
                if (i < heartBoxes.Count) picBox = heartBoxes[i];
                else
                {
                    //Create New PictureBox
                    picBox = new PictureBox();
                    picBox.Name = "Heart" + i;
                    picBox.Tag = "Heart-" + i;
                    picBox.Size = new Size(52, 75);
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    picBox.MouseEnter += new EventHandler(PictureBox_MouseEnter);

                    //Add to Form
                    this.Controls.Add(picBox);
                    movingPictures.Add(picBox);
                    heartBoxes.Add(picBox);
                }

                picBox.Image = plyr_Field.Heart[i].Image;
                picBox.Location = new Point(247 + 26 * (plyr_Field.Heart.Count - 1) - 52 * i - picBox.Width / 2, (int)(493.5 - ((double)picBox.Height / 2)));
                picBox.Tag = "Heart-" + i;
                picBox.BringToFront();
                plyr_Field.Heart[i].Location = "Heart-" + i;
            }
            for (int i = plyr_Field.Heart.Count; i < heartBoxes.Count; i++)
            {
                heartBoxes[i].Dispose();
                this.Controls.Remove(heartBoxes[i]);
                movingPictures.Remove(heartBoxes[i]);
            }

            //Vanguard
            IEnumerable<PictureBox> vgPics = movingPictures.Where(pic => pic.Name == "Vanguard");
            for (int i = 0; i < plyr_Field.VGCircle.Count; i++)
            {
                Card crd = plyr_Field.VGCircle[i];
                PictureBox picBox;
                if (crd.Location == "Vanguard-0")
                {
                    picBox = pictureBox_VG;
                    picBox.Visible = true;
                }
                else if (vgPics.Count() > 0) picBox = vgPics.First();
                else
                {
                    picBox = new PictureBox();
                    picBox.Name = "Vanguard";
                    picBox.Tag = "Vanguard-1";
                    picBox.ContextMenuStrip = VGMenu;
                    picBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
                    picBox.MouseEnter += new EventHandler(PictureBox_MouseEnter);
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    movingPictures.Add(picBox);
                    this.Controls.Add(picBox);
                    picBox.BringToFront();
                }

                if (picBox.Image != null) picBox.Image.Dispose();

                //Card Image
                if (crd.FaceUp) picBox.Image = new Bitmap(crd.Image);
                else if (crd.Unit == unitType.G) picBox.Image = new Bitmap(GSleeve);
                else picBox.Image = new Bitmap(mainSleeve);

                if (crd.Unit == unitType.G) picBox.ContextMenuStrip = VG_GMenu;
                else picBox.ContextMenuStrip = VGMenu;

                //Set Location + Size
                if (crd.Rested)
                {
                    picBox.Size = new Size(75, 52);
                    picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    if (plyr_Field.VGCircle.Count == 2) picBox.Location = new Point(210, 436 + 52 * i);
                    else picBox.Location = new Point(210, (crd.Unit == unitType.G ? 455 : 467));
                }
                else
                {
                    picBox.Size = new Size(52, 75);
                    picBox.Location = new Point(247 + 26 * (plyr_Field.VGCircle.Count - 1) - 52 * i - picBox.Width / 2, (int)(493.5 - (double) picBox.Height / 2 - (crd.Unit == unitType.G ? 12 : 0)));
                }

                //Tool Tip
                toolTip_Counts.SetToolTip(picBox, "Soul: " + plyr_Field.Soul.Count);
                //Bring to Front
                picBox.BringToFront();
            }
            if (vgPics.Count() >= plyr_Field.VGCircle.Count)
            {
                foreach (PictureBox vgPic in vgPics.ToList())
                {
                    vgPic.Dispose();
                    movingPictures.Remove(vgPic);
                    this.Controls.Remove(vgPic);
                }
            }

            //VG Labels
            if (plyr_Field.VGCircle.Count == 0) label_VGPower.Visible = false;
            else if (plyr_Field.VGCircle.Count == 1)
            {
                //Singular Power
                label_VGPower.Text = "Power: " + plyr_Field.VGCircle[0].Power;
                label_VGPower.Location = new Point(246 - label_VGPower.Size.Width / 2, 460);
                label_VGPower.Visible = true;
            }
            else
            {
                //Double Power
                label_VGPower.Text = plyr_Field.VGCircle[1].Power + " :Power: " + plyr_Field.VGCircle[0].Power;
                label_VGPower.Location = new Point(246 - label_VGPower.Size.Width / 2, 463);
                label_VGPower.Visible = true;
            }

            //Rearguard
            for (int i = 0; i < plyr_Field.RGCircles.Length; i++)
            {
                Card rgCircle = plyr_Field.RGCircles[i];
                PictureBox picBox = RGPictures[i];
                Label rgLabel = RGLabels[i];

                if (rgCircle == null)
                {
                    picBox.Visible = false;
                    rgLabel.Visible = false;
                }
                else
                {
                    //Delete Last Image
                    if (picBox.Image != null) picBox.Image.Dispose();

                    //Check if Locked
                    if (rgCircle.FaceUp)
                    {
                        //Not Locked
                        picBox.Image = new Bitmap(rgCircle.Image);

                        //ContextMenuStrip
                        if (rgCircle.Unit == unitType.G) picBox.ContextMenuStrip = RG_GMenu;
                        else picBox.ContextMenuStrip = RGMenu;

                        //Power
                        rgLabel.Text = "Power: " + rgCircle.Power;
                        rgLabel.Visible = true;
                    }
                    else
                    {
                        //Locked
                        picBox.Image = new Bitmap(mainSleeve);
                        picBox.ContextMenuStrip = LockMenu;
                        rgLabel.Visible = false;
                    }

                    //Rested Card
                    if (rgCircle.Rested)
                    {
                        picBox.Size = new Size(75, 52);
                        picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        picBox.Location = RGPictureLocation(i, true);
                    }
                    else
                    {
                        picBox.Size = new Size(52, 75);
                        picBox.Location = RGPictureLocation(i, false);
                    }

                    //Set Values
                    RGLabelLocation(i, rgLabel);
                    rgCircle.Location = "Rearguard-" + i;
                    picBox.Visible = true;
                }
            }

            //Damage Zone
            List<PictureBox> damagePics = movingPictures.Where(pic => pic.Name.StartsWith("Damage")).ToList();
            for (int i = 0; i < plyr_Field.DamageZone.Count; i++)
            {
                Card crd = plyr_Field.DamageZone[i];
                PictureBox picBox;

                if (i < damagePics.Count) picBox = damagePics[i];
                else
                {
                    picBox = new PictureBox();
                    picBox.Size = new Size(75, 52);
                    picBox.Name = "Damage" + i;
                    picBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
                    picBox.MouseEnter += new EventHandler(PictureBox_MouseEnter);
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    //Add to Form
                    movingPictures.Add(picBox);
                    this.Controls.Add(picBox);
                    damagePics.Add(picBox);
                }

                //Images
                if (picBox.Image != null) picBox.Image.Dispose();

                //Set Image
                picBox.Image = crd.FaceUp ? new Bitmap(crd.Image) : new Bitmap(mainSleeve);
                picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                picBox.Location = new Point(11, (int)(454.0 + 23.5 * i));

                //Set Values
                crd.Location = "Damage-" + i;
                picBox.Tag = "Damage-" + i;
                picBox.BringToFront();
            }
            //Clear Extra Damage Cards
            for (int i = plyr_Field.DamageZone.Count; i < damagePics.Count; i++)
            {
                damagePics[i].Dispose();
                this.Controls.Remove(damagePics[i]);
                movingPictures.Remove(damagePics[i]);
            }

            //Guardian Circle
            List<PictureBox> guardPics = movingPictures.Where(pic => pic.Name.StartsWith("Guardian")).ToList();
            int totalShield = 0;
            for (int i = 0; i < plyr_Field.GuardCircle.Count; i++)
            {
                Card crd = plyr_Field.GuardCircle[i];
                PictureBox picBox;

                if (i < guardPics.Count) picBox = guardPics[i];
                else
                {
                    picBox = new PictureBox();
                    picBox.Name = "Guardian" + i;
                    picBox.Size = new Size(75, 52);
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    picBox.MouseDown += new MouseEventHandler(PictureBox_MouseDown);
                    picBox.MouseEnter += new EventHandler(PictureBox_MouseEnter);

                    //Add to Form
                    this.Controls.Add(picBox);
                    movingPictures.Add(picBox);
                    guardPics.Add(picBox);
                }

                //Dispose Image
                if (picBox.Image != null) picBox.Image.Dispose();

                //Image
                picBox.Image = !crd.FaceUp ? (Image)new Bitmap(mainSleeve) : (Image)new Bitmap(crd.Image);
                picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);

                //Image Location
                picBox.Location = new Point((int)(210.0 - 18.75 * (double)(plyr_Field.GuardCircle.Count - 1) + 37.5 * i), 346);

                //ContextMenuStrip
                if (crd.Unit == unitType.G) picBox.ContextMenuStrip = G_GuardMenu;
                else picBox.ContextMenuStrip = GuardMenu;

                //Set Values
                picBox.Tag = "Guardian-" + i;
                crd.Location = "Guardian-" + i;
                picBox.BringToFront();

                //Total Shield
                totalShield += crd.Shield;
            }
            //Clear Extra PictureBoxes
            for (int i = plyr_Field.GuardCircle.Count; i < guardPics.Count; i++)
            {
                guardPics[i].Dispose();
                this.Controls.Remove(guardPics[i]);
                movingPictures.Remove(guardPics[i]);
            }

            //Shield Label
            if (plyr_Field.GuardCircle.Count > 0)
            {
                //Set Label
                label_ShieldTotal.Text = "Shield: " + totalShield;
                label_ShieldTotal.Visible = true;
                label_ShieldTotal.Location = new Point((int)(249.5 - (label_ShieldTotal.Width / 2.0)), 341);
            }
            else label_ShieldTotal.Visible = false;

            //Soul
            for (int i = 0; i < plyr_Field.Soul.Count; i++) plyr_Field.Soul[i].Location = "Soul-" + i;

            //Bind Zone
            for (int i = 0; i < plyr_Field.BindZone.Count; i++) plyr_Field.BindZone[i].Location = "Bind-" + i;
            //Set Tool Tip
            toolTip_Counts.SetToolTip(button_PlyrBind, "Bind Zone: " + plyr_Field.BindZone.Count);

            //Update Current ViewArea
            if (currentZone != null)
            {
                if (currentZone.Text == "Deck") currentZone.PanelUpdate(plyr_Field.Deck);
                else if (currentZone.Text == "Check Cards")
                {
                    //Update Check Cards
                    if (plyr_Field.CheckDeck == 0) currentZone.Close();
                    else currentZone.PanelUpdate(plyr_Field.CheckNDeck());
                }
                else if (currentZone.Text == "Soul") currentZone.PanelUpdate(plyr_Field.Soul);
                else if (currentZone.Text == "Drop Zone") currentZone.PanelUpdate(plyr_Field.DropZone);
                else if (currentZone.Text == "G Zone") currentZone.PanelUpdate(plyr_Field.GZone);
                else if (currentZone.Text == "Bind Zone") currentZone.PanelUpdate(plyr_Field.BindZone);
            }
        }

        public Point RGPictureLocation(int index, bool rested)
        {
            //Get Picture Location
            switch (index)
            {
                case 0:
                    return rested ? new Point(111, 467) : new Point(123, 456);
                case 1:
                    return rested ? new Point(305, 467) : new Point(317, 456);
                case 2:
                    return rested ? new Point(111, 571) : new Point(123, 560);
                case 3:
                    return rested ? new Point(208, 571) : new Point(220, 560);
                case 4:
                    return rested ? new Point(305, 571) : new Point(317, 560);
                default:
                    return Point.Empty;
            }
        }

        public Point RGLabelLocation(int index, Label labelInfo)
        {
            //Get Label Location
            switch (index)
            {
                case 0:
                    return (labelInfo.Location = new Point(148 - labelInfo.Width / 2, 460));
                case 1:
                    return (labelInfo.Location = new Point(344 - labelInfo.Width / 2, 460));
                case 2:
                    return (labelInfo.Location = new Point(148 - labelInfo.Width / 2, 570));
                case 3:
                    return (labelInfo.Location = new Point(246 - labelInfo.Width / 2, 570));
                case 4:
                    return (labelInfo.Location = new Point(344 - labelInfo.Width / 2, 570));
                default:
                    return Point.Empty;
            }
        }

        public void CurrentZone_Closed(object sender, FormClosedEventArgs e)
        {
            string name = currentZone.Text;
            //Set Null
            currentZone = null;

            //Get Values
            if (name == "Deck") richTextBox_Chat.AppendText(playerName + "stopped viewing the deck.");
            else if (name == "Check Cards") richTextBox_Chat.AppendText(playerName + "has stopped checking.");

            //Stop Checking
            plyr_Field.CheckDeck = 0;
        }

        private void button_Check_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (!int.TryParse(textBox_ChatInput.Text, out result)) return;

            //Close Previous Form
            if (currentZone != null && currentZone.Text != "Check Cards")
            {
                currentZone.Close();
                if (currentZone.Text != "Check Cards") plyr_Field.CheckDeck = 0;
            }

            //Show Form
            plyr_Field.CheckDeck += result;

            if (currentZone != null && currentZone.Text == "Check Cards")
            {
                currentZone.PanelUpdate(plyr_Field.CheckNDeck());
                richTextBox_Chat.AppendText(playerName + "checked " + result + " more cards from their deck.");
            }
            else
            {
                currentZone = new ViewZone(plyr_Field.CheckNDeck(), "Check Cards", this, mainSleeve, crd => true, DeckCardMenu);
                currentZone.Show(this);
                richTextBox_Chat.AppendText(playerName + "checked the top " + result + " of their deck.");
            }

            textBox_ChatInput.Text = "";
        }

        private void button_PlyrBind_Click(object sender, EventArgs e)
        {
            if (currentZone != null)
            {
                string text = currentZone.Text;
                currentZone.Close();
                if (text == "Bind Zone") return;
            }
            currentZone = new ViewZone(plyr_Field.BindZone, "Bind Zone", this, mainSleeve, (crd => crd.FaceUp), BindMenu);
            currentZone.Show(this);
        }

        private void shortcutMenu_Draw_Click(object sender, EventArgs e)
        {
            //Set Values
            plyr_Field.Deck[0].Location = "Hand-" + plyr_Field.Hand.Count;
            plyr_Field.Deck[0].Reset();

            //Add to Hand
            plyr_Field.Hand.Add(plyr_Field.Deck[0]);
            plyr_Field.Deck.RemoveAt(0);

            //Update Check Deck
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck--;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "drew a card.");
            //Update Images
            ImageUpdate();
        }

        private void shortcutMenu_Shuffle_Click(object sender, EventArgs e)
        {
            //Shuffle Deck
            plyr_Field.ShuffleDeck();

            //Get CheckNDeck
            if (currentZone != null && currentZone.Text == "Check Cards")
            {
                currentZone.Close();
                plyr_Field.CheckDeck = 0;
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "shuffled their deck.");
            //Update Images
            ImageUpdate();
        }

        private void shortcutMenu_Trigger_Click(object sender, EventArgs e)
        {
            if (plyr_Field.TriggerZone != null) return;

            //Set Values
            Card crd = plyr_Field.Deck[0];
            crd.Location = "Trigger-0";
            crd.Reset();

            //Move to Trigger Zone
            plyr_Field.TriggerZone = crd;
            plyr_Field.Deck.RemoveAt(0);

            //Update Check Deck
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck--;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "trigger checked " + crd.Name + ". (" + crd.TriggerCheck() + ")");
            //Update Images
            ImageUpdate();
        }

        private void shortcutMenu_SoulCharge_Click(object sender, EventArgs e)
        {
            //Set Values
            Card crd = plyr_Field.Deck[0];
            crd.Location = "Soul-" + plyr_Field.Soul.Count;
            crd.Reset();

            //Move to Soul
            plyr_Field.Soul.Add(crd);
            plyr_Field.Deck.RemoveAt(0);

            //Update Check Deck
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck--;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "soul charged " + crd.Name + ".");
            //Update Images
            ImageUpdate();
        }

        private void shortcutMenu_Coin_Click(object sender, EventArgs e)
        {
            Random RNGESUS = new Random();
            if (RNGESUS.Next(0, 2) == 0) richTextBox_Chat.AppendText(playerName + "flipped a coin that landed on heads.");
            else richTextBox_Chat.AppendText(playerName + "flipped a coin that landed on tails.");
        }

        private void shortcutMenu_End_Click(object sender, EventArgs e)
        {
            //Get Initial Rest State
            bool rested = plyr_Field.VGCircle[0].Rested;
            foreach (Card vgCard in plyr_Field.VGCircle)
            {
                //Return G Units to the G Zone
                if (vgCard.Unit == unitType.G && plyr_Field.Heart.Count > 0)
                {
                    foreach (Card crd in plyr_Field.VGCircle)
                    {
                        if (crd.Unit == unitType.G)
                        {
                            //All Legioned G Units to G Zone as well
                            crd.Location = "G Zone-" + plyr_Field.GZone.Count;
                            crd.FaceUp = true;
                            plyr_Field.GZone.Add(crd);
                        }
                        else
                        {
                            //Put Legioned Units into Drop Zone
                            crd.Location = "Drop Zone-" + plyr_Field.DropZone.Count;
                            plyr_Field.DropZone.Add(crd);
                        }
                    }
                    //Get New Vanguards
                    plyr_Field.VGCircle.Clear();
                    plyr_Field.VGCircle.AddRange(plyr_Field.Heart);
                    //Set Values
                    for (int index = 0; index < plyr_Field.VGCircle.Count; ++index)
                    {
                        plyr_Field.VGCircle[index].Location = "Vanguard-" + index;
                        plyr_Field.VGCircle[index].Reset();
                        plyr_Field.VGCircle[index].Rested = rested;
                    }
                    plyr_Field.Heart.Clear();
                    break;
                }

                //Reset Power and Undelete
                vgCard.FaceUp = true;
                vgCard.ResetPower();
            }

            //Rearguards
            for (int index = 0; index < plyr_Field.RGCircles.Length; ++index)
            {
                //Get Card
                Card rgCircle = plyr_Field.RGCircles[index];
                if (rgCircle != null)
                {
                    //Determine Action
                    if (rgCircle.Unit == unitType.G)
                    {
                        //Return G Unit to G Zone
                        string location = rgCircle.Location;
                        rgCircle.Location = "G Zone-" + plyr_Field.GZone.Count;
                        rgCircle.Reset();
                        rgCircle.FaceUp = true;
                        plyr_Field.GZone.Add(rgCircle);
                        plyr_Field.RemoveCard(location);
                    }
                    else if (rgCircle.Rested && !rgCircle.FaceUp) rgCircle.RestStand(); //Un-Omega Lock Card
                    else if (!rgCircle.FaceUp) rgCircle.Reset(); //Reset
                    else rgCircle.ResetPower(); //Change Power
                }
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "ended their turn.");
            //Update Images
            ImageUpdate();
        }

        private void shortcutMenu_ResetAll_Click(object sender, EventArgs e)
        {
            List<Card> allUnits = new List<Card>();
            allUnits.AddRange(plyr_Field.VGCircle);
            allUnits.AddRange(plyr_Field.RGCircles);
            allUnits.AddRange(plyr_Field.GuardCircle);
            foreach (Card crd in allUnits)
            {
                if (crd == null || !crd.FaceUp) continue;
                crd.ResetPower();
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "reset the Power of all of their units.");
            //Update Images
            ImageUpdate();
        }

        private void ShortcutMenu_XRide_Click(object sender, EventArgs e)
        {
            if (plyr_Field.VGCircle.Count == 0) return;

            PictureBox picBox = pictureBox_VG;

            Card crd = plyr_Field.VGCircle[0];
            //Set Crossride Power
            crd.ResetPower();
            crd.Power += 2000;

            //Update Images
            ImageUpdate();
        }

        private void FieldMenu_StandAll_Click(object sender, EventArgs e)
        {
            //Reset Vanguard To Stand
            foreach (Card crd in plyr_Field.VGCircle) crd.Rested = false;
            //Rearguards
            foreach (Card crd in plyr_Field.RGCircles)
            {
                if (crd == null || !crd.FaceUp) continue;
                crd.Rested = false;
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "stood all of their vanguards and rearguards.");
            //Update Images
            ImageUpdate();
        }

        private void FieldMenu_Unlock_Click(object sender, EventArgs e)
        {
            //Unlock Rearguards Including Omega Locked Cards
            foreach (Card crd in plyr_Field.RGCircles)
            {
                if (crd == null) continue;
                crd.FaceUp = true;
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "unlocked all of their rearguards.");
            //Update Images
            ImageUpdate();
        }

        private void FieldPowerMenu_Add5000_Click(object sender, EventArgs e)
        {
            foreach (Card crd in plyr_Field.VGCircle) crd.Power += 5000;
            foreach (Card crd in plyr_Field.RGCircles)
            {
                //Ignore Null and Locked Units
                if (crd == null || !crd.FaceUp) continue;
                crd.Power += 5000;
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave +5000 Power to all Units.");
            //Update Images
            ImageUpdate();
        }

        private void FieldPowerMenu_Change_Click(object sender, EventArgs e)
        {
            //Get Value
            EditValue edit = new EditValue("Power", hoverCard.Power);
            //Return if not OK
            if (edit.ShowDialog(this) != DialogResult.OK)
            {
                //Close Form Completely and Return
                if (!edit.IsDisposed) edit.Close();
                return;
            }

            foreach (Card crd in plyr_Field.VGCircle) crd.Power += edit.returnValue;
            foreach (Card crd in plyr_Field.RGCircles)
            {
                //Ignore Null and Locked Units
                if (crd == null || !crd.FaceUp) continue;
                crd.Power += edit.returnValue;
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave " + (edit.returnValue > 0 ? "+" + edit.returnValue : edit.returnValue.ToString()) + " Power to all Units.");
            //Update Images
            ImageUpdate();

            if (!edit.IsDisposed) edit.Close();
        }

        private void FieldPowerMenu_Minus5000_Click(object sender, EventArgs e)
        {
            foreach (Card crd in plyr_Field.VGCircle) crd.Power -= 5000;
            foreach (Card crd in plyr_Field.RGCircles)
            {
                //Ignore Null and Locked Units
                if (crd == null || !crd.FaceUp) continue;
                crd.Power -= 5000;
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave -5000 Power to all Units.");
            //Update Images
            ImageUpdate();
        }

        private void PlaceVCMenu_Opening(object sender, CancelEventArgs e)
        {
            //Disable Legion
            if (plyr_Field.VGCircle.Count > 1 || plyr_Field.VGCircle[0].Unit == unitType.G) PlaceVCMenu_Legion.Enabled = false;
            else PlaceVCMenu_Legion.Enabled = true;
        }

        private void PlaceVCMenu_Ride_Click(object sender, EventArgs e)
        {
            //Prevent G Unit Riding
            if (hoverCard.Unit == unitType.G)
            {
                dragOn = false;
                return;
            }

            //Remove all Heart and Vanguard Cards
            List<Card> heartVanguard = new List<Card>();
            heartVanguard.AddRange(plyr_Field.VGCircle);
            heartVanguard.AddRange(plyr_Field.Heart);

            int highestGrade = 0;
            foreach (Card crd in heartVanguard)
            {
                if (crd.Unit == unitType.G)
                {
                    //Check Grade
                    if (crd.Location.StartsWith("Vanguard") && crd.Grade > highestGrade) highestGrade = crd.Grade;

                    //Return G Unit to G Zone
                    crd.Location = "G Zone-" + plyr_Field.GZone.Count;
                    crd.Reset();
                    plyr_Field.GZone.Add(crd);
                }
                else
                {
                    if (crd.Location.StartsWith("Vanguard") && crd.Grade > highestGrade) highestGrade = crd.Grade;

                    //To Soul
                    crd.Location = "Soul-" + plyr_Field.Soul.Count;
                    crd.Reset();
                    plyr_Field.Soul.Add(crd);
                }
            }

            plyr_Field.VGCircle.Clear();
            plyr_Field.Heart.Clear();

            //Set Values
            string initLocation = hoverCard.Location;
            hoverCard.Location = "Vanguard-0";

            //Move to Vanguard
            plyr_Field.VGCircle.Add(hoverCard);
            plyr_Field.RemoveCard(initLocation);

            //Update Chat
            if ((!initLocation.StartsWith("Rearguard") && !initLocation.StartsWith("Guardian")) || !hoverCard.FaceUp)
            {
                hoverCard.Reset();
                richTextBox_Chat.AppendText(playerName + "rode " + hoverCard.Name + ". Grade " + highestGrade + " -> " + hoverCard.Grade);
            }
            else richTextBox_Chat.AppendText(playerName + "moved " + hoverCard.Name + " to (VC).");
            //Update Images
            ImageUpdate();

            dragOn = false;
        }

        private void PlaceVCMenu_Legion_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Vanguard-1";
            hoverCard.Reset();
            hoverCard.Rested = plyr_Field.VGCircle[0].Rested;

            //Add to (VC)
            plyr_Field.VGCircle.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "performed legioned with " + hoverCard.Name + ".");
            //Update Images
            ImageUpdate();

            dragOn = false;
        }

        private void PlaceVCMenu_Soul_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Soul-" + plyr_Field.Soul.Count;
            hoverCard.Reset();

            //Add to Soul
            plyr_Field.Soul.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " into the soul.");
            //Update Images
            ImageUpdate();

            dragOn = false;
        }

        private void DeckMenu_Search_Click(object sender, EventArgs e)
        {
            //Search Deck
            if (this.currentZone != null)
            {
                //Close Previous Check
                string text = this.currentZone.Text;
                this.currentZone.Close();
                if (text == "Deck")
                    return;
            }

            //Open New CurrentZone
            currentZone = new ViewZone(plyr_Field.Deck, "Deck", this, mainSleeve, (crd => true), DeckCardMenu);
            currentZone.Show(this);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "is searching their deck.");
        }

        private void DeckMenu_BindUp_Click(object sender, EventArgs e)
        {
            //Set Values
            Card crd = plyr_Field.Deck[0];
            crd.Location = "Bind-" + plyr_Field.BindZone.Count;
            crd.Reset();
            crd.FaceUp = true;

            //Move to Bind Zone
            plyr_Field.BindZone.Add(crd);
            plyr_Field.Deck.RemoveAt(0);

            //Update Check Deck
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck--;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "bound " + crd.Name + " face up from top deck.");
            //Update Images
            ImageUpdate();
        }

        private void DeckMenu_BindDown_Click(object sender, EventArgs e)
        {
            //Set Values
            Card crd = plyr_Field.Deck[0];
            bool initialPosition = crd.FaceUp;
            crd.Location = "Bind-" + plyr_Field.BindZone.Count;
            crd.Reset();
            crd.FaceUp = false;

            //Move to Bind Zone
            plyr_Field.BindZone.Add(crd);
            plyr_Field.Deck.RemoveAt(0);

            //Update Check Deck
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck--;

            //Update Chat
            if (initialPosition) richTextBox_Chat.AppendText(playerName + "bound " + crd.Name + " face down from top deck.");
            else richTextBox_Chat.AppendText(playerName + "bound a card face down from top deck.");
            //Update Images
            ImageUpdate();
        }

        private void DeckMenu_Mill_Click(object sender, EventArgs e)
        {
            //Set Values
            Card crd = plyr_Field.Deck[0];
            crd.Location = "Drop-" + plyr_Field.DropZone.Count;
            crd.Reset();

            //Move to Drop Zone
            plyr_Field.DropZone.Add(crd);
            plyr_Field.Deck.RemoveAt(0);

            //Update Check Deck
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck--;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "milled " + crd.Name + " from top deck.");
            //Update Images
            ImageUpdate();
        }

        private void DeckMenu_Check_Click(object sender, EventArgs e)
        {
            //Face Up
            plyr_Field.Deck[0].TurnOver();

            //Update Chat
            if (plyr_Field.Deck[0].FaceUp) richTextBox_Chat.AppendText(playerName + "look at the top card of their deck.");
            else richTextBox_Chat.AppendText(playerName + "stopped looking at the top card of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void DeckMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            //Move to End
            Card crd = plyr_Field.Deck[0];
            plyr_Field.Deck.Add(plyr_Field.Deck[0]);
            plyr_Field.Deck.RemoveAt(0);

            //Update Check Deck
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck--;

            //Update Chat
            if (crd.FaceUp)
            {
                crd.FaceUp = false;
                richTextBox_Chat.AppendText(playerName + "put " + crd.Name + " on the bottom of their deck.");
            }
            else richTextBox_Chat.AppendText(playerName + "put the top card on the bottom of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void HandMenu_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip origSender = sender as ContextMenuStrip;
            if (origSender == null) return;
            //Set Reveal Text
            PictureBox_MouseEnter(origSender.SourceControl, e);
            if (hoverCard.FaceUp) HandMenu_Reveal.Text = "Unreveal";
            else HandMenu_Reveal.Text = "Reveal";
        }

        private void HandMenu_Reveal_Click(object sender, EventArgs e)
        {
            //Reveal
            hoverCard.TurnOver();

            //Update Chat
            if (hoverCard.FaceUp) richTextBox_Chat.AppendText(playerName + " revealed " + hoverCard.Name + " from their hand.");
            else richTextBox_Chat.AppendText(playerName + " stopped revealing " + hoverCard.Name + " from their hand.");
            //update Images
            ImageUpdate();
        }

        private void HandMenu_BindUp_Click(object sender, EventArgs e)
        {
            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Move to Bind Zone
            plyr_Field.BindZone.Add(hoverCard);
            plyr_Field.Hand.Remove(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + " bound " + hoverCard.Name + " from their hand.");
            //Update Images
            ImageUpdate();
        }

        private void HandMenu_BindDown_Click(object sender, EventArgs e)
        {
            bool initFaceUp = hoverCard.FaceUp;

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = false;

            //Move to Bind Zone
            plyr_Field.BindZone.Add(hoverCard);
            plyr_Field.Hand.Remove(hoverCard);

            //Update Chat
            if (initFaceUp) richTextBox_Chat.AppendText(playerName + " bound " + hoverCard.Name + " from their hand.");
            else richTextBox_Chat.AppendText(playerName + " bound a card from their hand.");
            //Update Images
            ImageUpdate();
        }

        private void HandMenu_TopDeck_Click(object sender, EventArgs e)
        {
            bool initFaceUp = hoverCard.FaceUp;

            //Set Values
            hoverCard.Location = "Deck-0";
            hoverCard.Reset();

            //Move to Deck
            plyr_Field.Deck.Insert(0, hoverCard);
            plyr_Field.Hand.Remove(hoverCard);

            //Update Chat
            if (initFaceUp) richTextBox_Chat.AppendText(playerName + " put " + hoverCard.Name + " on top deck from their hand.");
            else richTextBox_Chat.AppendText(playerName + " put a card on top deck from their hand.");
            //Update Images
            ImageUpdate();
        }

        private void HandMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            bool initFaceUp = hoverCard.FaceUp;

            //Set Values
            hoverCard.Location = "Deck-" + plyr_Field.Deck.Count;
            hoverCard.Reset();

            //Move to Deck
            plyr_Field.Deck.Add(hoverCard);
            plyr_Field.Hand.Remove(hoverCard);

            //Update Chat
            if (initFaceUp) richTextBox_Chat.AppendText(playerName + " put " + hoverCard.Name + " on bottom deck from their hand.");
            else richTextBox_Chat.AppendText(playerName + " put a card on bottom deck from their hand.");
            //Update Images
            ImageUpdate();
        }

        private void HandMenu_Shuffle_Click(object sender, EventArgs e)
        {
            //Shuffle Hand
            plyr_Field.ShuffleHand();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "shuffled their hand.");
            //Update Images
            ImageUpdate();
        }

        private void HandMenu_Target_Click(object sender, EventArgs e)
        {
            //Target
            hoverCard.Target();

            //Image Update
            ImageUpdate();
        }

        private void PowerMenu_Add5000_Click(object sender, EventArgs e)
        {
            //Give 5000 Power
            hoverCard.Power += 5000;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave " + hoverCard.Name + " +5000 Power.");
            //Update Images
            ImageUpdate();
        }

        private void PowerMenu_Change_Click(object sender, EventArgs e)
        {
            EditValue edit = new EditValue("Power", hoverCard.Power);
            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                //Add Power
                hoverCard.Power += edit.returnValue;

                //Update Chat
                richTextBox_Chat.AppendText(playerName + "gave " + hoverCard.Name + " " + (edit.returnValue > 0 ? "+" + edit.returnValue : edit.returnValue.ToString()) + " Power.");
                //Update Images
                ImageUpdate();
            }

            if (!edit.IsDisposed) edit.Close();
        }

        private void PowerMenu_Minus5000_Click(object sender, EventArgs e)
        {
            //Give -5000 Power
            hoverCard.Power -= 5000;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave " + hoverCard.Name + " -5000 Power.");
            //Update Images
            ImageUpdate();
        }

        private void PowerMenu_Reset_Click(object sender, EventArgs e)
        {
            //Give 5000 Power
            if (!hoverCard.FaceUp) hoverCard.Power = 0;
            else hoverCard.ResetPower();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "reset " + hoverCard.Name + "'s Power.");
            //Update Images
            ImageUpdate();
        }

        private void VGMenu_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip origSender = sender as ContextMenuStrip;
            if (origSender == null) return;
            //Set Reveal Text
            PictureBox_MouseEnter(origSender.SourceControl, e);
            //Rested State
            if (hoverCard.Rested)
            {
                VGMenu_Attack.Enabled = false;
                VG_GMenu_Attack.Enabled = false;
                VGMenu_Rest.Text = "[Stand]";
                VG_GMenu_Rest.Text = "[Stand]";
            }
            else
            {
                VGMenu_Attack.Enabled = true;
                VG_GMenu_Attack.Enabled = true;
                VGMenu_Rest.Text = "[Rest]";
                VG_GMenu_Rest.Text = "[Rest]";
            }

            //Delete
            if (hoverCard.FaceUp)
            {
                VGMenu_Delete.Text = "Delete";
                VG_GMenu_Delete.Text = "Delete";
            }
            else
            {
                VGMenu_Delete.Text = "Undelete";
                VG_GMenu_Delete.Text = "Undelete";
            }

            //Top Deck and Bottom Deck Disable
            if (hoverCard.Location == "Vanguard-0")
            {
                VGMenu_TopDeck.Enabled = false;
                VGMenu_BottomDeck.Enabled = false;
            }
            else
            {
                VGMenu_TopDeck.Enabled = true;
                VGMenu_BottomDeck.Enabled = true;
            }
        }

        private void VGMenu_Attack_Click(object sender, EventArgs e)
        {
            List<string> allVanguards = new List<string>();
            int totalPower = 0;
            foreach (Card crd in plyr_Field.VGCircle)
            {
                allVanguards.Add(crd.Name);
                crd.Rested = true;
                totalPower += crd.Power;
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "attacked with " + string.Join(" and ", allVanguards) + ". " + totalPower + " Power.");
            //Update Images
            ImageUpdate();
        }

        private void VGMenu_Delete_Click(object sender, EventArgs e)
        {
            List<string> allVanguards = new List<string>();
            foreach (Card crd in plyr_Field.VGCircle)
            {
                allVanguards.Add(crd.Name);
                crd.TurnOver();
                if (crd.FaceUp) crd.Power += crd.OriginalPower;
                else crd.Power -= crd.OriginalPower;
            }

            //Update Chat
            if (plyr_Field.VGCircle[0].FaceUp) richTextBox_Chat.AppendText(playerName + "undeleted " + string.Join(" and ", allVanguards) + ".");
            else richTextBox_Chat.AppendText(playerName + "deleted " + string.Join(" and ", allVanguards) + ".");
            //Update Images
            ImageUpdate();
        }

        private void VGMenu_Soul_Click(object sender, EventArgs e)
        {
            //Close Previous Form
            if (currentZone != null)
            {
                //Get Information
                string text = currentZone.Text;
                currentZone.Close();
                if (text == "Soul") return;
            }

            //Create New Form
            currentZone = new ViewZone(plyr_Field.Soul, "Soul", this, mainSleeve, crd => true, SoulMenu);
            currentZone.Show(this);

        }

        private void VGMenu_Rest_Click(object sender, EventArgs e)
        {
            List<string> allVanguards = new List<string>();
            foreach (Card crd in plyr_Field.VGCircle)
            {
                allVanguards.Add(crd.Name);
                crd.RestStand();
            }

            //Update Chat
            if (plyr_Field.VGCircle[0].Rested) richTextBox_Chat.AppendText(playerName + "rested " + string.Join(" and ", allVanguards) + ".");
            else richTextBox_Chat.AppendText(playerName + "stood " + string.Join(" and ", allVanguards) + ".");
            //Update Images
            ImageUpdate();
        }

        private void VGMenu_TopDeck_Click(object sender, EventArgs e)
        {
            //Set Values
            Card crd = plyr_Field.VGCircle[1];
            crd.Location = "Deck-0";
            crd.Reset();

            //Move to Deck
            plyr_Field.Deck.Insert(0, crd);
            plyr_Field.VGCircle.RemoveAt(1);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + crd.Name + " on the top of his deck.");
            //Update Images
            ImageUpdate();
        }

        private void VGMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            //Set Values
            Card crd = plyr_Field.VGCircle[1];
            crd.Location = "Deck-" + plyr_Field.Deck.Count;
            crd.Reset();

            //Move to Deck
            plyr_Field.Deck.Add(crd);
            plyr_Field.VGCircle.RemoveAt(1);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + crd.Name + " on the bottom of his deck.");
            //Update Images
            ImageUpdate();
        }

        private void VG_GMenu_GZone_Click(object sender, EventArgs e)
        {
            if (hoverCard.Location == "Vanguard-0")
            {
                bool rested = hoverCard.Rested;
                //Return Cards
                foreach (Card card in plyr_Field.VGCircle)
                {
                    if (card.Unit == unitType.G)
                    {
                        //Return G Unit to G Zone
                        card.Location = "G Zone-" + plyr_Field.GZone.Count;
                        card.Reset();
                        card.FaceUp = true;
                        plyr_Field.GZone.Add(card);
                    }
                    else
                    {
                        //Send Legion Mate to Drop Zone
                        card.Location = "Drop-" + plyr_Field.DropZone.Count;
                        card.Reset();
                        plyr_Field.DropZone.Add(card);
                    }
                }

                plyr_Field.VGCircle.Clear();
                plyr_Field.VGCircle.AddRange(plyr_Field.Heart);
                for (int i = 0; i < plyr_Field.VGCircle.Count; i++)
                {
                    plyr_Field.VGCircle[i].Location = "Vanguard-" + i;
                    plyr_Field.VGCircle[i].Rested = rested;
                }
                plyr_Field.Heart.Clear();
            }
            else
            {
                //Set Values
                string initLocation = hoverCard.Location;
                hoverCard.Location = "G Zone-" + plyr_Field.GZone.Count;
                hoverCard.Reset();
                hoverCard.FaceUp = true;

                //Move to G Zone
                plyr_Field.GZone.Add(hoverCard);
                plyr_Field.VGCircle.Remove(hoverCard);
            }

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "returned " + hoverCard.Name + " to the G Zone.");
            //Update Images
            ImageUpdate();
        }

        private void VGMenu_Target_Click(object sender, EventArgs e)
        {
            //Target
            hoverCard.Target();

            //Image Update
            ImageUpdate();
        }

        private void RGMenu_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip origSender = sender as ContextMenuStrip;
            if (origSender == null) return;
            //Set Reveal Text
            PictureBox_MouseEnter(origSender.SourceControl, e);
            //Rested State
            if (hoverCard.Rested)
            {
                RGMenu_Attack.Enabled = false;
                RG_GMenu_Attack.Enabled = false;
                RGMenu_Rest.Text = "[Stand]";
                RG_GMenu_Rest.Text = "[Stand]";
            }
            else
            {
                RGMenu_Attack.Enabled = true;
                RG_GMenu_Attack.Enabled = true;
                RGMenu_Rest.Text = "[Rest]";
                RG_GMenu_Rest.Text = "[Rest]";
            }
        }

        private void RGMenu_Attack_Click(object sender, EventArgs e)
        {
            //Rest Rearguard
            hoverCard.Rested = true;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "attacked with " + hoverCard.Name + ". (" + hoverCard.Power + " Power)");
            //Update Images
            ImageUpdate();
        }

        private void RGMenu_Lock_Click(object sender, EventArgs e)
        {
            hoverCard.Reset();
            hoverCard.FaceUp = false;

            //update Chat
            richTextBox_Chat.AppendText(playerName + "locked " + hoverCard.Name + ".");
            //Update Images
            ImageUpdate();
        }

        private void RGMenu_Bind_Click(object sender, EventArgs e)
        {
            string initLocation = hoverCard.Location;

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Move to Bind Zone
            plyr_Field.BindZone.Add(hoverCard);
            plyr_Field.RemoveCard(initLocation);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + " bound " + hoverCard.Name + " from rear-guard.");
            //Update Images
            ImageUpdate();
        }

        private void RGMenu_Rest_Click(object sender, EventArgs e)
        {
            hoverCard.RestStand();

            //Update Chat
            if (hoverCard.Rested) richTextBox_Chat.AppendText(playerName + "rested " + hoverCard.Name + ".");
            else richTextBox_Chat.AppendText(playerName + "stood " + hoverCard.Name + ".");
            //Update Images
            ImageUpdate();
        }

        private void RGMenu_TopDeck_Click(object sender, EventArgs e)
        {
            //Move to Deck
            plyr_Field.Deck.Insert(0, hoverCard);
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-0";
            hoverCard.Reset();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on the top of his deck.");
            //Update Images
            ImageUpdate();
        }

        private void RGMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            //Move to Deck
            plyr_Field.Deck.Add(hoverCard);
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-" + plyr_Field.Deck.Count;
            hoverCard.Reset();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on the bottom of his deck.");
            //Update Images
            ImageUpdate();
        }

        private void RG_GMenu_GZone_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "G Zone-" + plyr_Field.GZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Add to G Zone
            plyr_Field.GZone.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "returned " + hoverCard.Name + " to the G Zone.");
            //Update Images
            ImageUpdate();
        }

        private void RGMenu_Target_Click(object sender, EventArgs e)
        {
            //Target Card
            hoverCard.Target();

            //Update Images
            ImageUpdate();
        }

        private void LockMenu_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip origSender = sender as ContextMenuStrip;
            if (origSender == null) return;
            //Set Reveal Text
            PictureBox_MouseEnter(origSender.SourceControl, e);
            //Omega Locked
            if (hoverCard.Rested) LockMenu_Omega.Text = "Stop Omega Lock";
            else LockMenu_Omega.Text = "Omega Lock";
        }

        private void LockMenu_Unlock_Click(object sender, EventArgs e)
        {
            //Unlock
            hoverCard.Reset();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "unlocked " + hoverCard.Name + ".");
            //Update Images
            ImageUpdate();
        }

        private void LockMenu_Omega_Click(object sender, EventArgs e)
        {
            //Unlock
            hoverCard.RestStand();

            //Update Chat
            if (hoverCard.Rested) richTextBox_Chat.AppendText(playerName + "omega locked " + hoverCard.Name + ".");
            else richTextBox_Chat.AppendText(playerName + "stopped omega locked " + hoverCard.Name + ".");
            //Update Images
            ImageUpdate();
        }

        private void GuardMenu_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip origSender = sender as ContextMenuStrip;
            if (origSender == null) return;
            //Set Reveal Text
            PictureBox_MouseEnter(origSender.SourceControl, e);
        }

        private void GuardMenu_RetireAll_Click(object sender, EventArgs e)
        {
            foreach (Card crd in plyr_Field.GuardCircle)
            {
                //Move cards to appropriate Zone
                if (crd.Unit == unitType.G)
                {
                    crd.Location = "G Zone-" + plyr_Field.GZone.Count;
                    crd.Reset();
                    plyr_Field.GZone.Add(crd);
                }
                else
                {
                    crd.Location = "Drop-" + plyr_Field.DropZone.Count;
                    crd.Reset();
                    plyr_Field.DropZone.Add(crd);
                }
            }
            //Clear Guardian Circle
            plyr_Field.GuardCircle.Clear();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "retired all of their guardians.");
            //Update Images
            ImageUpdate();
        }

        private void GuardMenu_Bind_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();

            //Add to Bind ZOne
            plyr_Field.BindZone.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "bound " + hoverCard.Name + " from their guardians.");
            //Update Images
            ImageUpdate();
        }

        private void ShieldMenu_Add5000_Click(object sender, EventArgs e)
        {
            //Give 5000 Shield
            hoverCard.Shield += 5000;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave " + hoverCard.Name + " +5000 Shield.");
            //Update Images
            ImageUpdate();
        }

        private void ShieldMenu_Change_Click(object sender, EventArgs e)
        {
            EditValue edit = new EditValue("Shield", hoverCard.Shield);
            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                //Add Power
                hoverCard.Shield += edit.returnValue;

                //Update Chat
                richTextBox_Chat.AppendText(playerName + "gave " + hoverCard.Name + " " + (edit.returnValue > 0 ? "+" + edit.returnValue : edit.returnValue.ToString()) + " Shield.");
                //Update Images
                ImageUpdate();
            }

            if (!edit.IsDisposed) edit.Close();
        }

        private void ShieldMenu_Minus5000_Click(object sender, EventArgs e)
        {
            //Give 5000 Shield
            hoverCard.Shield -= 5000;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave " + hoverCard.Name + " -5000 Shield.");
            //Update Images
            ImageUpdate();
        }

        private void ShieldMenu_Reset_Click(object sender, EventArgs e)
        {
            //Reset Shield
            hoverCard.ResetShield();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "reset " + hoverCard.Name + "'s Shield.");
            //Update Images
            ImageUpdate();
        }

        private void ShieldAllMenu_Add5000_Click(object sender, EventArgs e)
        {
            //Power to All Guardians
            foreach (Card crd in plyr_Field.GuardCircle) crd.Shield += 5000;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave all guardians +5000 Shield.");
            //Update Chat
            ImageUpdate();
        }

        private void ShieldAllMenu_Change_Click(object sender, EventArgs e)
        {
            EditValue edit = new EditValue("Shield", hoverCard.Shield);
            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                //Add Power
                foreach (Card crd in plyr_Field.GuardCircle) crd.Shield += edit.returnValue;

                //Update Chat
                richTextBox_Chat.AppendText(playerName + "gave all guardians " + (edit.returnValue > 0 ? "+" + edit.returnValue : edit.returnValue.ToString()) + " Shield.");
                //Update Images
                ImageUpdate();
            }

            if (!edit.IsDisposed) edit.Close();
        }

        private void ShieldAllMenu_Minus5000_Click(object sender, EventArgs e)
        {
            //Power to All Guardians
            foreach (Card crd in plyr_Field.GuardCircle) crd.Shield -= 5000;

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "gave all guardians -5000 Shield.");
            //Update Chat
            ImageUpdate();
        }

        private void ShieldAllMenu_Reset_Click(object sender, EventArgs e)
        {
            //Power to All Guardians
            foreach (Card crd in plyr_Field.GuardCircle) crd.ResetShield();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "reset the Shield of all guardians.");
            //Update Chat
            ImageUpdate();
        }

        private void GuardMenu_TopDeck_Click(object sender, EventArgs e)
        {
            //Move to Deck
            plyr_Field.Deck.Insert(0, hoverCard);
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-0";
            hoverCard.Reset();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on the top of his deck.");
            //Update Images
            ImageUpdate();
        }

        private void GuardMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            //Move to Deck
            plyr_Field.Deck.Add(hoverCard);
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-" + plyr_Field.Deck.Count;
            hoverCard.Reset();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on the bottom of his deck.");
            //Update Images
            ImageUpdate();
        }

        private void G_GuardMenu_GZone_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "G Zone-" + plyr_Field.GZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Add to G Zone
            plyr_Field.GZone.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "returned " + hoverCard.Name + " to the G Zone.");
            //Update Images
            ImageUpdate();
        }

        private void GuardMenu_Target_Click(object sender, EventArgs e)
        {
            //Target Card
            hoverCard.Target();

            //Update Images
            ImageUpdate();
        }

        private void DeckCardMenu_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip origSender = sender as ContextMenuStrip;
            if (origSender == null) return;
            //Set Reveal Text
            PictureBox_MouseEnter(origSender.SourceControl, e);

            if (currentZone.Text == "Deck") DeckCardMenu_Shuffle.Enabled = false;
            else DeckCardMenu_Shuffle.Enabled = true;
        }

        private void DeckCardMenu_Reveal_Click(object sender, EventArgs e)
        {
            hoverCard.TurnOver();

            if (hoverCard.FaceUp) richTextBox_Chat.AppendText(playerName + "revealed " + hoverCard.Name + ". Grade " + hoverCard.Grade + ".");
            else richTextBox_Chat.AppendText(playerName + "stopped revealing " + hoverCard.Name + ".");
        }

        private void DeckCardMenu_BindUp_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Add to Bind ZOne
            plyr_Field.BindZone.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "bound " + hoverCard.Name + " face up from deck.");
            //Update Images
            ImageUpdate();
        }

        private void DeckCardMenu_BindDown_Click(object sender, EventArgs e)
        {
            bool initState = hoverCard.FaceUp;

            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = false;

            //Add to Bind ZOne
            plyr_Field.BindZone.Add(hoverCard);

            //Update Chat
            if (initState) richTextBox_Chat.AppendText(playerName + "bound " + hoverCard.Name + " face down from their deck.");
            else richTextBox_Chat.AppendText(playerName + "bound a card face down from their deck.");
            //Update Images
            ImageUpdate();
        }

        private void DeckCardMenu_TopDeck_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Location
            hoverCard.Location = "Deck-0";

            //Add to Deck
            plyr_Field.Deck.Insert(0, hoverCard);

            //Keep Same Number
            if (currentZone != null && currentZone.Text == "Check Cards") plyr_Field.CheckDeck++;

            //Update Chat
            if (hoverCard.FaceUp) richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on the top of their deck.");
            else richTextBox_Chat.AppendText(playerName + "put a card on the top of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void DeckCardMenu_Bottom_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Location
            hoverCard.Location = "Deck-" + plyr_Field.Deck.Count;

            //Add to Deck
            plyr_Field.Deck.Add(hoverCard);

            //Update Chat
            if (hoverCard.FaceUp) richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on the bottom of their deck.");
            else richTextBox_Chat.AppendText(playerName + "put a card on the bottom of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void SoulMenu_SoulBlast_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Drop-" + plyr_Field.DropZone.Count;
            hoverCard.Reset();

            //Add to Bind ZOne
            plyr_Field.DropZone.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "soul blasted " + hoverCard.Name + ".");
            //Update Images
            ImageUpdate();
        }

        private void SoulMenu_Bind_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Add to Bind ZOne
            plyr_Field.BindZone.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "bound " + hoverCard.Name + " face up from their soul.");
            //Update Images
            ImageUpdate();
        }

        private void SoulMenu_TopDeck_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-0";
            hoverCard.Reset();

            //Add to Bind ZOne
            plyr_Field.Deck.Insert(0, hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on top of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void SoulMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-" + plyr_Field.Deck.Count;
            hoverCard.Reset();

            //Add to Bind ZOne
            plyr_Field.Deck.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on bottom of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void SoulMenu_Target_Click(object sender, EventArgs e)
        {
            hoverCard.Target();

            //Update Chat
            if (hoverCard.Targetted) richTextBox_Chat.AppendText(playerName + "targetted " + hoverCard.Name + " in the soul.");
            //Update Images
            currentZone.PanelUpdate(plyr_Field.Soul);
        }

        private void DropMenu_Bind_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Add to Bind ZOne
            plyr_Field.BindZone.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "bound " + hoverCard.Name + " face up from their drop zone.");
            //Update Images
            ImageUpdate();
        }

        private void DropMenu_Soul_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Soul-" + plyr_Field.Soul.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Add to Bind ZOne
            plyr_Field.Soul.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " into their soul.");
            //Update Images
            ImageUpdate();
        }

        private void DropMenu_TopDeck_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-0";
            hoverCard.Reset();

            //Add to Bind ZOne
            plyr_Field.Deck.Insert(0, hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on top of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void DropMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-" + plyr_Field.Deck.Count;
            hoverCard.Reset();

            //Add to Bind ZOne
            plyr_Field.Deck.Add(hoverCard);

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on bottom of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void DropMenu_Target_Click(object sender, EventArgs e)
        {
            hoverCard.Target();

            //Update Chat
            if (hoverCard.Targetted) richTextBox_Chat.AppendText(playerName + "targetted " + hoverCard.Name + " in the drop zone.");
            //Update Images
            currentZone.PanelUpdate(plyr_Field.DropZone);
        }

        private void DropMenu_AllDeck_Click(object sender, EventArgs e)
        {
            //Set Values and Add
            foreach (Card crd in plyr_Field.DropZone)
            {
                crd.Location = "Deck-" + plyr_Field.Deck.Count;
                crd.Reset();
                plyr_Field.Deck.Add(crd);
            }

            //Remove All Cards
            plyr_Field.DropZone.Clear();
            plyr_Field.ShuffleDeck();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "put all cards in the drop zone into the deck, and shuffled.");
            //Update Images
            ImageUpdate();
            currentZone.Close();
        }

        private void BindMenu_TopDeck_Click(object sender, EventArgs e)
        {
            bool initFaceUp = hoverCard.FaceUp;

            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Deck-0";
            hoverCard.Reset();

            //Add to Bind ZOne
            plyr_Field.Deck.Insert(0, hoverCard);

            //Update Chat
            if (initFaceUp) richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on top of their deck.");
            else richTextBox_Chat.AppendText(playerName + "put a card on top of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void BindMenu_BottomDeck_Click(object sender, EventArgs e)
        {
            bool initFaceUp = hoverCard.FaceUp;

            //Remove Card
            plyr_Field.RemoveCard(hoverCard.Location);

            //Set Values
            hoverCard.Location = "Bind-" + plyr_Field.BindZone.Count;
            hoverCard.Reset();
            hoverCard.FaceUp = true;

            //Add to Bind ZOne
            plyr_Field.BindZone.Add(hoverCard);

            //Update Chat
            if (initFaceUp) richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " on bottom of their deck.");
            else richTextBox_Chat.AppendText(playerName + "put a card on bottom of their deck.");
            //Update Images
            ImageUpdate();
        }

        private void BindMenu_TurnOver_Click(object sender, EventArgs e)
        {
            hoverCard.TurnOver();

            //Update Chat
            richTextBox_Chat.AppendText(playerName + "turned " + hoverCard.Name + (hoverCard.FaceUp ? " face up." : " face down."));
            //Update Images
            currentZone.PanelUpdate(plyr_Field.BindZone);
        }

        private void BindMenu_Target_Click(object sender, EventArgs e)
        {
            hoverCard.Target();

            //Update Chat
            if (hoverCard.FaceUp) richTextBox_Chat.AppendText(playerName + "targetted " + hoverCard.Name + " in the bind zone.");
            else richTextBox_Chat.AppendText(playerName + "targetted a card in the bind zone.");
            //Update Images
            currentZone.PanelUpdate(plyr_Field.BindZone);
        }

        private void pictureBox_PlyrDrop_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentZone != null)
            {
                string text = currentZone.Text;
                currentZone.Close();
                if (text == "Drop Zone") return;
            }
            currentZone = new ViewZone(this.plyr_Field.DropZone, "Drop Zone", this, mainSleeve, (crd => crd.FaceUp), DropMenu);
            currentZone.Show(this);
        }

        private void pictureBox_GDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentZone != null)
            {
                string text = currentZone.Text;
                currentZone.Close();
                if (text == "G Zone") return;
            }
            currentZone = new ViewZone(plyr_Field.GZone, "G Zone", this, GSleeve, (crd => crd.FaceUp));
            currentZone.Show(this);
        }

        public void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox_MouseEnter(sender, e);

            if (e.Button == MouseButtons.Left)
            {
                if (ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Shift))
                {
                    if ((hoverCard.Location.StartsWith("G Zone") || hoverCard.Location.StartsWith("Hand")) &&
                        MessageBox.Show("Are you sure you want to remove " + hoverCard.Name + " from play?", "Remove from Play",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        plyr_Field.RemoveCard(hoverCard.Location);

                        //Update Chat
                        richTextBox_Chat.AppendText(playerName + "removed " + hoverCard.Name + " from play.");
                        //Update Images
                        ImageUpdate();

                        hoverCard.Location = "Removed";
                        hoverCard = null;
                    }
                    else return;
                }
                else if (ModifierKeys.HasFlag(Keys.Control))
                {
                    if (hoverCard.Location.StartsWith("Vanguard")) VGMenu_Soul_Click(sender, e);
                    else return;
                }
                else if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    if (hoverCard.Location.StartsWith("Vanguard")) VGMenu_Rest.PerformClick();
                    else if (hoverCard.Location.StartsWith("Rearguard")) RGMenu_Rest.PerformClick();
                    else return;
                }
                else if (hoverCard.Location != "Vanguard-0")
                {
                    //Set Drag On
                    dragOn = true;
                    Cursor.Current = Cursors.Cross;
                    this.Capture = true;
                }
            }
            else if (hoverCard.Location.StartsWith("Damage") || hoverCard.Location.StartsWith("G Zone"))
            {
                hoverCard.TurnOver();

                //Update Chat
                if (hoverCard.FaceUp) richTextBox_Chat.AppendText(playerName + "flipped over " + hoverCard.Name + ".");
                //Update Images
                ImageUpdate();
            }
        }

        public void Field_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragOn)
            {
                string[] strArray = hoverCard.Location.Split('-');
                if (PointRange(new Point(123, 456), new Point(175, 531)).Contains(e.Location)) RGMethod(0); //Rear-guard 0
                else if (PointRange(new Point(317, 456), new Point(369, 531)).Contains(e.Location)) RGMethod(1);//Rear-guard 1
                else if (PointRange(new Point(123, 560), new Point(175, 635)).Contains(e.Location)) RGMethod(2);//Rear-guard 2
                else if (PointRange(new Point(220, 560), new Point(272, 635)).Contains(e.Location)) RGMethod(3);//Rear-guard 3
                else if (PointRange(new Point(317, 560), new Point(369, 635)).Contains(e.Location)) RGMethod(4);//Rear-guard 4
                else if (PointRange(new Point(220, 456), new Point(272, 531)).Contains(e.Location) && !hoverCard.Location.StartsWith("Vanguard"))
                {
                    //Vanguard
                    bool rested = plyr_Field.VGCircle[0].Rested;

                    if (hoverCard.Unit == unitType.G)
                    {
                        //Stride
                        int maxPower = 0;

                        //If G Unit is current Vanguard
                        if (plyr_Field.VGCircle[0].Unit == unitType.G)
                        {
                            //Return Vanguards to G Zone
                            foreach (Card crd in plyr_Field.VGCircle)
                            {
                                if (crd.Unit == unitType.G)
                                {
                                    //G Unit back to G Zone
                                    crd.Location = "G Zone-" + plyr_Field.GZone.Count;
                                    plyr_Field.GZone.Add(crd);
                                }
                                else
                                {
                                    //Legion-Mates to Drop Zone
                                    crd.Location = "Drop-" + plyr_Field.DropZone.Count;
                                    plyr_Field.DropZone.Add(crd);
                                }
                            }

                            //Get Strongest Heart Card
                            plyr_Field.VGCircle.Clear();
                            foreach (Card crd in plyr_Field.Heart)
                            {
                                if (crd.OriginalPower > maxPower) maxPower = crd.OriginalPower;
                            }
                        }
                        else
                        {
                            //Normal Stride
                            foreach (Card crd in plyr_Field.VGCircle)
                            {
                                if (crd.Unit == unitType.G)
                                {
                                    //Returned Legion G Unit
                                    crd.Location = "G Zone-" + plyr_Field.GZone.Count;
                                    plyr_Field.GZone.Add(crd);
                                }
                                else
                                {
                                    //Move to Heart
                                    crd.Location = "Heart-" + plyr_Field.Heart.Count;
                                    plyr_Field.Heart.Add(crd);
                                    //Count Maximum Power
                                    if (crd.OriginalPower > maxPower) maxPower = crd.OriginalPower;
                                }
                            }
                            //Remove all Vanguard Cards
                            plyr_Field.VGCircle.Clear();
                        }

                        //Stride Unit
                        plyr_Field.VGCircle.Add(hoverCard);
                        plyr_Field.RemoveCard(hoverCard.Location);
                        //Set Values
                        hoverCard.Location = "Vanguard-0";
                        hoverCard.Reset();
                        //Stride Power and State
                        hoverCard.Power += maxPower;
                        hoverCard.Rested = rested;

                        //Update Chat
                        richTextBox_Chat.AppendText(playerName + "strode " + hoverCard.Name + ". (" + hoverCard.Power + " Power)");
                    }
                    else
                    {
                        //Show Menu
                        PlaceVCMenu.Show(Cursor.Position);
                        return;
                    }
                }
                else if (PointRange(new Point(421, 555), new Point(473, 630)).Contains(e.Location))
                {
                    //Drop Zone

                    //Get Index
                    int result = 0;
                    if (!int.TryParse(strArray[1], out result)) return;

                    if (hoverCard.Location.StartsWith("Rearguard")) RetireRG(result); //Retire Rear-guard
                    else if (hoverCard.Unit == unitType.G)
                    {
                        //To G Zone
                        if (hoverCard.Location.StartsWith("G Zone")) return;

                        //Update Chat
                        if (hoverCard.Location == "Vanguard-1") richTextBox_Chat.AppendText(playerName + "retired  " + hoverCard.Name + ".");
                        else this.richTextBox_Chat.AppendText(playerName + "returned " + hoverCard.Name + " to G Zone.");

                        //Move to G Zone
                        plyr_Field.GZone.Add(hoverCard);
                        plyr_Field.RemoveCard(hoverCard.Location);

                        //Set Values
                        hoverCard.Location = "G Zone-" + plyr_Field.GZone.Count;
                        hoverCard.Reset();
                    }
                    else
                    {
                        //Add to Drop Zone
                        if (hoverCard.Location.StartsWith("Drop")) return;

                        //Update Chat
                        if (hoverCard.Location == "Vanguard-1") richTextBox_Chat.AppendText(playerName + "retired  " + hoverCard.Name + ".");
                        else richTextBox_Chat.AppendText(playerName + "sent " + hoverCard.Name + " to Drop Zone.");

                        //Remove Card
                        plyr_Field.RemoveCard(hoverCard.Location);

                        //Set Values
                        hoverCard.Location = "Drop-" + plyr_Field.DropZone.Count;
                        hoverCard.Reset();

                        //Add to Drop Zone
                        plyr_Field.DropZone.Add(hoverCard);
                    }
                }
                else if (PointRange(new Point(9, 447), new Point(84, 640)).Contains(e.Location) && !hoverCard.Location.StartsWith("Damage"))
                {
                    //Damage Zone
                    if (hoverCard.Unit == unitType.G) return;

                    //Remove Card
                    plyr_Field.RemoveCard(hoverCard.Location);

                    //Set Values
                    hoverCard.Location = "Damage-" + plyr_Field.DamageZone.Count;
                    hoverCard.Reset();

                    //Add to Damage Zone
                    plyr_Field.DamageZone.Add(hoverCard);

                    //Update Chat
                    richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " into the damage zone.");
                }
                else if (PointRange(new Point(0, 663), new Point(446, 738)).Contains(e.Location) && !hoverCard.Location.StartsWith("Hand"))
                {
                    //Hand
                    if (hoverCard.Unit == unitType.G) return;

                    bool initFaceUp = hoverCard.FaceUp;
                    string initLocation = hoverCard.Location;

                    //Remove Card
                    plyr_Field.RemoveCard(hoverCard.Location);

                    //Set Values
                    hoverCard.Location = "Hand-" + plyr_Field.Hand.Count;
                    hoverCard.Reset();

                    //Add to Damage Zone
                    plyr_Field.Hand.Add(hoverCard);

                    //Update Chat
                    if (initFaceUp || initLocation.StartsWith("Rearguard") || initLocation.StartsWith("Damage")) richTextBox_Chat.AppendText(playerName + "put " + hoverCard.Name + " into their hand.");
                    else richTextBox_Chat.AppendText(playerName + "put a card into their hand.");
                }
                else if (PointRange(new Point(151, 342), new Point(346, 394)).Contains(e.Location) && !hoverCard.Location.StartsWith("Guardian"))
                {
                    //Guardian Circle
                    if (hoverCard.Location.StartsWith("Rearguard") && !hoverCard.FaceUp) return;

                    //Move to Guardian Circle
                    plyr_Field.GuardCircle.Add(hoverCard);
                    plyr_Field.RemoveCard(hoverCard.Location);

                    //Set Values
                    hoverCard.Location = "Guardian-" + plyr_Field.GuardCircle.Count;
                    hoverCard.Reset();

                    //Update Chat
                    richTextBox_Chat.AppendText(playerName + "guarded with " + hoverCard.Name + ". (" + hoverCard.Shield + " Shield)");
                }

                //Update Images
                ImageUpdate();
            }

            dragOn = false;
            Cursor.Current = Cursors.Default;
        }

        public void RGMethod(int RGIndex)
        {
            //Only allow RGIndex between 0 and 4
            if (RGIndex < 0 || RGIndex > 4) return;
            //Move if card is Rear-guard
            if (hoverCard.Location.StartsWith("Rearguard"))
            {
                //Get Index
                int result = -1;
                if (!int.TryParse(hoverCard.Location.Split('-')[1], out result) || !(hoverCard.Location != "Rearguard-" + RGIndex)) return;

                //Swap Cards
                Card rgCircle = plyr_Field.RGCircles[RGIndex];
                plyr_Field.RGCircles[RGIndex] = plyr_Field.RGCircles[result];
                plyr_Field.RGCircles[result] = rgCircle;

                //Update Chat and Set Values
                if (rgCircle == null)
                {
                    plyr_Field.RGCircles[RGIndex].Location = "Rearguard-" + RGIndex;
                    richTextBox_Chat.AppendText(Environment.NewLine + "<" + Properties.Settings.Default.Username + "> moved " + plyr_Field.RGCircles[RGIndex].Name + ".");
                }
                else
                {
                    plyr_Field.RGCircles[result].Location = "Rearguard-" + result;
                    plyr_Field.RGCircles[RGIndex].Location = "Rearguard-" + RGIndex;
                    richTextBox_Chat.AppendText(Environment.NewLine + "<" + Properties.Settings.Default.Username + "> swapped " + plyr_Field.RGCircles[result].Name + " and " + plyr_Field.RGCircles[RGIndex].Name + ".");
                }
            }
            else
            {
                //Retire Rear-guard if not locked
                if (plyr_Field.RGCircles[RGIndex] != null && plyr_Field.RGCircles[RGIndex].FaceUp) RetireRG(RGIndex);
                else if (plyr_Field.RGCircles[RGIndex] != null && !plyr_Field.RGCircles[RGIndex].FaceUp) return;

                //Get Location
                string location = hoverCard.Location;
                plyr_Field.RGCircles[RGIndex] = hoverCard;
                RGPictures[RGIndex].Tag = ("Rearguard-" + RGIndex);
                plyr_Field.RGCircles[RGIndex].Location = "Rearguard-" + RGIndex;

                //Reset Information if Card was not from Vanguard
                if (location != "Vanguard-1" && !location.StartsWith("Guardian"))
                {
                    hoverCard.Reset();
                    richTextBox_Chat.AppendText(Environment.NewLine + "<" + Properties.Settings.Default.Username + "> called " + plyr_Field.RGCircles[RGIndex].Name + " to rear-guard.");
                }
                else richTextBox_Chat.AppendText(Environment.NewLine + "<" + Properties.Settings.Default.Username + "> mvoed " + plyr_Field.RGCircles[RGIndex].Name + " to rear-guard.");

                //Remove Card
                plyr_Field.RemoveCard(location);
            }
        }

        public void RetireRG(int index)
        {
            //Return to G Zone if G Unit
            if (plyr_Field.RGCircles[index].Unit == unitType.G)
            {
                //Set Values
                plyr_Field.RGCircles[index].Location = "G Zone-" + plyr_Field.GZone.Count;
                plyr_Field.RGCircles[index].Reset();
                //Add to G Zone
                plyr_Field.GZone.Add(plyr_Field.RGCircles[index]);

                //Update Chat
                richTextBox_Chat.AppendText(Environment.NewLine + "<" + Properties.Settings.Default.Username + "> retired " + plyr_Field.RGCircles[index].Name + " from rear-guard.");
            }
            else
            {
                //Set Values
                plyr_Field.RGCircles[index].Location = "Drop-" + plyr_Field.DropZone.Count;
                plyr_Field.RGCircles[index].Reset();
                //Add to Drop Zone
                plyr_Field.DropZone.Add(plyr_Field.RGCircles[index]);

                //Update Chat
                richTextBox_Chat.AppendText(Environment.NewLine + "<" + Properties.Settings.Default.Username + "> retired " + plyr_Field.RGCircles[index].Name + " from rear-guard.");
            }
            //Remove Rearguard
            plyr_Field.RGCircles[index] = null;
        }

        public IEnumerable<Point> PointRange(Point start, Point end)
        {
            List<Point> pointList = new List<Point>();

            for (int x = start.X; x < end.X; x++)
            {
                for (int y = start.Y; y < end.Y; y++) pointList.Add(new Point(x, y));
            }

            return pointList;
        }

        public void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox origSender = sender as PictureBox;
            if (!dragOn && origSender.Name != "pictureBox_PlyrDeck")
            {
                hoverCard = plyr_Field.returnCard(origSender.Tag.ToString());
                //Load Information
                pictureBox_Info.Image = hoverCard.Image;
                richTextBox_Info.Text = hoverCard.InformationText;
            }
        }
    }
}
