using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cray_Simulator
{
    public partial class DeckBuilder : Form
    {
        //Data
        DataTable allCards;
        Dictionary<string, string> cardDictionary = new Dictionary<string, string>();
        Dictionary<string, int> restrictionList = new Dictionary<string, int>();
        Dictionary<string, int> effectRestrict = new Dictionary<string, int>();
        List<string> BannedSVG = new List<string>();
        List<KeyValuePair<string, string>> list_Searched = new List<KeyValuePair<string, string>>();

        //Deck Information
        List<KeyValuePair<string, string>> list_NormalUnits = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_TriggerUnits = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_GUnits = new List<KeyValuePair<string, string>>();
        string startVG = "";

        //Form Information
        Card selectedCard;
        bool SearchFocusChange = false;
        List<string> Clans = new List<string>();
        List<string> Races = new List<string>();
        AdvSearch searchForm;
        string saveLocation = null;
        Properties.Settings currentSettings = Properties.Settings.Default;

        //Counting
        int sentinel = 0;
        int g0 = 0;
        int g1 = 0;
        int g2 = 0;
        int g3 = 0;
        int g4 = 0;
        int heal = 0;
        int critical = 0;
        int stand = 0;
        int draw = 0;
        int stride = 0;
        int ggrd = 0;
        int listedRestrict = 0;

        public DeckBuilder(DataTable dt)
        {
            InitializeComponent();

            allCards = dt;

            foreach (DataRow dr in dt.Rows)
            {
                cardDictionary.Add((string) dr["cardID"], (string)dr["name"]);
                //Compile Clan and Race Information
                if (!Clans.Contains((string) dr["clan"])) Clans.Add((string) dr["clan"]);
                foreach (string race in dr["race"].ToString().Split('/'))
                {
                    //Check if Race Exists
                    if (!Races.Contains(race)) Races.Add(race);
                }
            }

            Clans.Sort();
            Races.Sort();

            Clans.Insert(0, "Any");
            Races.Insert(0, "Any");

            searchForm = new AdvSearch(Clans, Races);

            //Set Data Source
            list_Searched = cardDictionary.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_Search.DataSource = list_Searched;

            //Compile Restriction List
            using (StreamReader reader = new StreamReader(@"Restrictions.txt"))
            {
                string allText = "";

                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith("--")) allText += line;
                }

                string[] allLines = allText.Split(';');
                foreach(string str in allLines)
                {
                    if (str.Length < 2) continue;

                    //Remove first two characters
                    string[] parameters = str.Substring(2, str.Length - 2).Split('|');

                    switch (str[0])
                    {
                        case 'S':
                            BannedSVG.Add(parameters[0]);
                            break;
                        case 'L':
                            try { restrictionList.Add(parameters[0], int.Parse(parameters[1])); }
                            catch (FormatException ex) { MessageBox.Show("Invalid Restriction."); Console.WriteLine(ex.Message); }
                            break;
                        case 'E':
                            try { effectRestrict.Add(parameters[0], int.Parse(parameters[1])); }
                            catch (FormatException ex) { MessageBox.Show("Invalid Restriction."); Console.WriteLine(ex.Message); }
                            break;
                    }
                }
            }
        }


        private void DeckBuilder_Load(object sender, EventArgs e)
        {
            //Set Tabbed Control
            ActiveControl = listBox_Search;

            //Load Previous Deck
            if (currentSettings.LoadPrevDeck && currentSettings.PrevDeck.Length > 0)
            {
                OpenFromFile(currentSettings.PrevDeck);
                saveLocation = currentSettings.PrevDeck;
            }
            else if (currentSettings.Deck_Default.Length > 0)
            {
                OpenFromFile(currentSettings.Deck_Default);
                saveLocation = currentSettings.Deck_Default;
            }
        }

        private void FileMenu_New_Click(object sender, EventArgs e)
        {
            //Clear All Information
            saveLocation = null;

            list_NormalUnits.Clear();
            list_TriggerUnits.Clear();
            list_GUnits.Clear();

            listBox_Normal.DataSource = list_NormalUnits.ToList();
            listBox_Trigger.DataSource = list_TriggerUnits.ToList();
            listBox_G.DataSource = list_GUnits.ToList();

            //Counting Variables
            sentinel = 0;
            g0 = 0;
            g1 = 0;
            g2 = 0;
            g3 = 0;
            g4 = 0;
            heal = 0;
            critical = 0;
            stand = 0;
            draw = 0;
            stride = 0;
            ggrd = 0;
            listedRestrict = 0;
        }

        private void FileMenu_Open_Click(object sender, EventArgs e)
        {
            if (openFile_Deck.ShowDialog(this) == DialogResult.OK)
            {
                FileMenu_New_Click(sender, e);

                //Load Deck
                saveLocation = openFile_Deck.FileName;
                OpenFromFile(saveLocation);
            }
        }

        private void OpenFromFile(string fileLocation)
        {
            currentSettings.PrevDeck = fileLocation;
            currentSettings.Save();

            using (StreamReader reader = new StreamReader(fileLocation))
            {
                string allText = "";
                string line = "";

                if (fileLocation.EndsWith(".cra"))
                {
                    //Remove Commented Lines
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!line.StartsWith("--")) allText += line;
                    }
                }
                else
                {
                    MessageBox.Show("Cray Simulator cannot open this file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int countLines = 0;
                //Split among Lines
                foreach (string readLine in allText.Split(';'))
                {
                    //Set Starting Vanguard
                    if (countLines == 3)
                    {
                        startVG = readLine;
                        break;
                    }

                    //Add Cards
                    foreach (string card in readLine.Split(','))
                    {
                        DataRow[] drArray = allCards.Select("cardID='" + card + "'");
                        if (drArray.Length > 0)
                        {
                            AddCard(new Card(drArray[0], false));
                        }
                        else if (card.Length > 0) MessageBox.Show("Cray Simulator could not find card: " + card + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    countLines++;
                }

                list_NormalUnits = list_NormalUnits.OrderBy(keyValue => keyValue.Value).ToList();
                list_TriggerUnits = list_TriggerUnits.OrderBy(keyValue => keyValue.Value).ToList();
                list_GUnits = list_GUnits.OrderBy(keyValue => keyValue.Value).ToList();

                listBox_Normal.DataSource = list_NormalUnits;
                listBox_Trigger.DataSource = list_TriggerUnits;
                listBox_G.DataSource = list_GUnits;

                reader.Close();
            }
        }

        private void FileMenu_Save_Click(object sender, EventArgs e)
        {
            if (saveLocation == null && saveFile_Deck.ShowDialog(this) == DialogResult.OK)
            {
                SaveToFile(saveFile_Deck.FileName);
                saveLocation = saveFile_Deck.FileName;
            }
            else if (saveLocation != null)
            {
                SaveToFile(saveLocation);
            }
        }

        private void FileMenu_SaveAs_Click(object sender, EventArgs e)
        {
            if (saveFile_Deck.ShowDialog(this) == DialogResult.OK)
            {
                SaveToFile(saveFile_Deck.FileName);
                saveLocation = saveFile_Deck.FileName;
            }
        }

        private void SaveToFile(string fileLocation)
        {
            //Save Files
            currentSettings.PrevDeck = fileLocation;
            currentSettings.Save();

            using (StreamWriter writer = new StreamWriter(fileLocation, false))
            {
                string outputFile = "--Normal Units" + Environment.NewLine;

                foreach (KeyValuePair<string, string> keyValue in list_NormalUnits)
                {
                    outputFile += keyValue.Key + ',';
                }

                outputFile = outputFile.TrimEnd(',');
                outputFile += ';' + Environment.NewLine + "--Trigger Units" + Environment.NewLine;

                foreach (KeyValuePair<string, string> keyValue in list_TriggerUnits)
                {
                    outputFile += keyValue.Key + ',';
                }

                outputFile = outputFile.TrimEnd(',');
                outputFile += ';' + Environment.NewLine + "--G Units" + Environment.NewLine;

                foreach (KeyValuePair<string, string> keyValue in list_GUnits)
                {
                    outputFile += keyValue.Key + ',';
                }

                outputFile = outputFile.TrimEnd(',');
                outputFile += ';' + (startVG != null && startVG.Length > 0 ? Environment.NewLine + "--Starting Vanguard" + Environment.NewLine + startVG + ';' : "");

                writer.Write(outputFile);

                writer.Close();
            }
        }

        private void FileMenu_Export_Click(object sender, EventArgs e)
        {
            string grade0 = "";
            string grade1 = "";
            string grade2 = "";
            string grade3 = "";
            string grade4 = "";
            string gZone = "";
            int g0Count = 0;
            int g1Count = 0;
            int g2Count = 0;
            int g3Count = 0;
            int g4Count = 0;

            //Normal Units
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, string> normalCard in list_NormalUnits)
            {
                if (!stringList.Contains(normalCard.Key))
                {
                    Card tempCard = new Card(allCards.Select("cardID='" + normalCard.Key + "'")[0], false);
                    int count = this.list_NormalUnits.Count(value => value.Key == tempCard.CardID);
                    switch (tempCard.Grade)
                    {
                        case 0:
                            grade0 = grade0 + tempCard.Name + " x" + count + Environment.NewLine;
                            g0Count += count;
                            break;
                        case 1:
                            grade1 = grade1 + tempCard.Name + " x" + count + Environment.NewLine;
                            g1Count += count;
                            break;
                        case 2:
                            grade2 = grade2 + tempCard.Name + " x" + count + Environment.NewLine;
                            g2Count += count;
                            break;
                        case 3:
                            grade3 = grade3 + tempCard.Name + " x" + count + Environment.NewLine;
                            g3Count += count;
                            break;
                        case 4:
                            grade4 = grade4 + tempCard.Name + " x" + count + Environment.NewLine;
                            g4Count += count;
                            break;
                    }
                    stringList.Add(normalCard.Key);
                }
            }
            stringList.Clear();

            //Trigger Units
            foreach (KeyValuePair<string, string> triggerCard in list_TriggerUnits)
            {
                if (!stringList.Contains(triggerCard.Key))
                {
                    Card tempCard = new Card(allCards.Select("cardID='" + triggerCard.Key + "'")[0], false);
                    int count = list_TriggerUnits.Count(value => value.Key == tempCard.CardID);
                    switch (tempCard.Grade)
                    {
                        case 0:
                            grade0 = grade0 + tempCard.Name + " x" + count + " (" + tempCard.Trigger + ")" + Environment.NewLine;
                            g0Count += count;
                            break;
                        case 1:
                            grade1 = grade1 + tempCard.Name + " x" + count + " (" + tempCard.Trigger + ")" + Environment.NewLine;
                            g1Count += count;
                            break;
                        case 2:
                            grade2 = grade2 + tempCard.Name + " x" + count + " (" + tempCard.Trigger + ")" + Environment.NewLine;
                            g2Count += count;
                            break;
                        case 3:
                            grade3 = grade3 + tempCard.Name + " x" + count + " (" + tempCard.Trigger + ")" + Environment.NewLine;
                            g3Count += count;
                            break;
                        case 4:
                            grade4 = grade4 + tempCard.Name + " x" + count + " (" + tempCard.Trigger + ")" + Environment.NewLine;
                            g4Count += count;
                            break;
                    }
                    stringList.Add(triggerCard.Key);
                }
            }
            stringList.Clear();

            //G Units
            foreach (KeyValuePair<string, string> gCard in list_GUnits)
            {
                if (!stringList.Contains(gCard.Key))
                {
                    Card tempCard = new Card(allCards.Select("cardID='" + gCard.Key + "'")[0], false);
                    gZone = gZone + tempCard.Name + " x" + list_GUnits.Count(value => value.Key == tempCard.CardID) + Environment.NewLine;
                    stringList.Add(gCard.Key);
                }
            }

            //Initialize Variables
            string[] strArray = new string[6];

            //Create Strings
            //Grade 0
            string g0String;
            if (grade0.Length <= 0) g0String = "";
            else g0String = "Grade 0 (" + g0Count + ")" + Environment.NewLine + grade0 + Environment.NewLine;
            strArray[0] = g0String;

            //Grade 1
            string g1String;
            if (grade1.Length <= 0) g1String = "";
            else g1String = "Grade 1 (" + g1Count + ")" + Environment.NewLine + grade1 + Environment.NewLine;
            strArray[1] = g1String;

            //Grade 2
            string g2String;
            if (grade2.Length <= 0) g2String = "";
            else g2String = "Grade 2 (" + g2Count + ")" + Environment.NewLine + grade2 + Environment.NewLine;
            strArray[2] = g2String;

            //Grade 3
            string g3String;
            if (grade3.Length <= 0) g3String = "";
            else g3String = "Grade 3 (" + g3Count + ")" + Environment.NewLine + grade3 + Environment.NewLine;
            strArray[3] = g3String;

            //Grade 4
            string g4String;
            if (grade4.Length <= 0) g4String = "";
            else g4String = "Grade 4 (" + g4Count + ")" + Environment.NewLine + grade4 + Environment.NewLine;
            strArray[4] = g4String;

            //G Units
            string gUnitString;
            if (gZone.Length <= 0) gUnitString = "";
            else gUnitString = "G Zone (" + this.list_GUnits.Count + ")" + Environment.NewLine + gZone;
            strArray[5] = gUnitString;

            LargeTextBoxDialog(string.Concat(strArray), "View Deck");
        }

        private void FightMenu_Test_Click(object sender, EventArgs e)
        {
            List<Card> deckCards = new List<Card>();
            
            foreach (KeyValuePair<string, string> keyValue in list_NormalUnits)
            {
                DataRow[] drArray = allCards.Select("cardID='" + keyValue.Key + "'");
                if (drArray.Length > 0) deckCards.Add(new Card(drArray[0]));
            }

            foreach (KeyValuePair<string, string> keyValue in list_TriggerUnits)
            {
                DataRow[] drArray = allCards.Select("cardID='" + keyValue.Key + "'");
                if (drArray.Length > 0) deckCards.Add(new Card(drArray[0]));
            }

            foreach (KeyValuePair<string, string> keyValue in list_GUnits)
            {
                DataRow[] drArray = allCards.Select("cardID='" + keyValue.Key + "'");
                if (drArray.Length > 0) deckCards.Add(new Card(drArray[0]));
            }

            Battlefield battleForm = new Battlefield(deckCards, startVG);
            this.Visible = false;
            battleForm.ShowDialog(this);
            this.Visible = true;

            //Dispose BattleForm
            if (!battleForm.IsDisposed) battleForm.Dispose();
        }

        private void FightMenu_Listen_Click(object sender, EventArgs e)
        {

        }

        private void FightMenu_Connect_Click(object sender, EventArgs e)
        {

        }

        private void HelpMenu_About_Click(object sender, EventArgs e)
        {

        }

        private void HelpMenu_Settings_Click(object sender, EventArgs e)
        {
            Preferences prefForm = new Preferences();
            //Save Values
            if (prefForm.ShowDialog(this) == DialogResult.OK) prefForm.SetValues();
        }

        private void HelpMenu_ImageClean_Click(object sender, EventArgs e)
        {
            List<string> DeletedCards = new List<string>();

            foreach (string fileLocation in Directory.GetFiles("Images/"))
            {
                //Remove Extension
                string checkString = fileLocation.Substring(0, fileLocation.LastIndexOf('.'));

                //Get Only File Name
                int subStart = checkString.LastIndexOf('/') + 1;
                int subLength = checkString.Length - subStart;
                checkString = checkString.Substring(subStart, subLength);

                bool found = false;

                foreach (KeyValuePair<string, string> keyValue in cardDictionary)
                {
                    if (keyValue.Key.Replace('/', '-') == checkString)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    DeletedCards.Add(checkString);
                    File.Delete(fileLocation);
                }
            }

            MessageBox.Show(string.Join(", ", DeletedCards) + " were deleted.", "Deleted", MessageBoxButtons.OK);

            List<string> MissingCards = new List<string>();
            foreach (KeyValuePair<string, string> keyValue in cardDictionary)
            {
                if (!File.Exists("Images/" + keyValue.Key.Replace('/', '-') + ".jpg")) MissingCards.Add(keyValue.Value);
            }

            MessageBox.Show(string.Join(", ", MissingCards) + " need Images.", "Need Image", MessageBoxButtons.OK);
        }

        private void listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox origSender = sender as ListBox;
            if (!origSender.Focused || origSender.SelectedValue == null) return;
            
            selectedCard = new Card((string)origSender.SelectedValue, allCards);

            //Show Information
            pictureBox_Info.Image = selectedCard.Image;
            richTextBox_Info.Text = selectedCard.InformationText;
        }

        private void SearchBox_GotFocus(object sender, EventArgs e)
        {
            TextBox origSender = sender as TextBox;
            if (origSender.ForeColor == Color.Gray)
            {
                SearchFocusChange = true;
                origSender.Text = "";
                origSender.ForeColor = Color.Black;
            }
        }

        private void textBox_NameSearch_LostFocus(object sender, EventArgs e)
        {
            if (textBox_NameSearch.Text == "")
            {
                SearchFocusChange = true;
                textBox_NameSearch.Text = "Name";
                textBox_NameSearch.ForeColor = Color.Gray;
            }
        }

        private void textBox_SetSearch_LostFocus(object sender, EventArgs e)
        {
            if (textBox_SetSearch.Text == "")
            {
                SearchFocusChange = true;
                textBox_SetSearch.Text = "Set";
                textBox_SetSearch.ForeColor = Color.Gray;
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (SearchFocusChange)
            {
                SearchFocusChange = false;
                return;
            }

            if (textBox_SetSearch.ForeColor == Color.Gray && 
                textBox_NameSearch.ForeColor == Color.Gray)
            {
                if (list_Searched.Count == cardDictionary.Count) return;
                //Return all Cards
                list_Searched = cardDictionary.OrderBy(keyValue => keyValue.Value).ToList();
                listBox_Search.DataSource = list_Searched;
                return;
            }

            list_Searched.Clear();
            foreach (KeyValuePair<string, string> keyValue in cardDictionary.OrderBy(keyValue => keyValue.Value).ToList())
            {
                //Add Card to List
                if ((keyValue.Key.ToUpper().Contains(textBox_SetSearch.Text.ToUpper()) || textBox_SetSearch.ForeColor == Color.Gray)
                    && (keyValue.Value.ToUpper().Contains(textBox_NameSearch.Text.ToUpper()) || textBox_NameSearch.ForeColor == Color.Gray))
                    list_Searched.Add(keyValue);
            }

            listBox_Search.DataSource = list_Searched.ToList();
        }

        private void Button_AdvSearch_Click(object sender, EventArgs e)
        {
            if (searchForm.ShowDialog(this) != DialogResult.OK) return;

            list_Searched.Clear();

            foreach (DataRow dr in allCards.Rows)
            {
                //Create Card Class
                //Card checkCard = new Card(dr, false);

                //Analyse Advance Search
                if (dr["name"].ToString().ToUpper().Contains(searchForm.CardName)
                    && dr["cardID"].ToString().ToUpper().Contains(searchForm.CardID)
                    && searchForm.IsClan(dr["clan"].ToString())
                    && searchForm.IsUClass(dr["uclass"].ToString())
                    && searchForm.Grade(Convert.ToInt32(dr["grade"]))
                    && searchForm.IsRace(dr["race"].ToString())
                    && searchForm.Power(Convert.ToInt32(dr["power"]))
                    && searchForm.HasAbility(dr["effect"].ToString()))
                {
                    list_Searched.Add(new KeyValuePair<string, string>(dr["cardID"].ToString(), dr["name"].ToString()));
                }
            }

            list_Searched = list_Searched.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_Search.DataSource = list_Searched;
        }

        private void listBox_Search_DoubleClick(object sender, EventArgs e)
        {
            AddCard(selectedCard);
        }

        private void listBox_Search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) AddCard(selectedCard);
        }

        private void AddCard(Card crd)
        {
            //Return if Null
            if (crd == null) return;
            //Check Cards
            switch (crd.Unit)
            {
                case unitType.Normal:
                    if (list_NormalUnits.Count < 34 && CheckValid(crd, list_NormalUnits))
                    {
                        list_NormalUnits.Add(crd.KeyValuePair);
                        Increment(crd);
                        list_NormalUnits = list_NormalUnits.OrderBy(keyValue => keyValue.Value).ToList();
                    }
                    break;
                case unitType.Trigger:
                    if (list_TriggerUnits.Count < 16 && CheckValid(crd, list_TriggerUnits))
                    {
                        list_TriggerUnits.Add(crd.KeyValuePair);
                        Increment(crd);
                        list_TriggerUnits = list_TriggerUnits.OrderBy(keyValue => keyValue.Value).ToList();
                    }
                    break;
                case unitType.G:
                    if (list_GUnits.Count < 16 && CheckValid(crd, list_GUnits))
                    {
                        list_GUnits.Add(crd.KeyValuePair);
                        Increment(crd);
                        list_GUnits = list_GUnits.OrderBy(keyValue => keyValue.Value).ToList();
                    }
                    break;
            }
            
            //Update DataSource
            listBox_Normal.DataSource = list_NormalUnits;
            listBox_Trigger.DataSource = list_TriggerUnits;
            listBox_G.DataSource = list_GUnits;
        }

        private bool CheckValid(Card crd, List<KeyValuePair<string, string>> list)
        {
            int restrictValue = 0;
            int count = 0;
            int sntCount = crd.IsSentinel ? sentinel : 0;
            int hlCount = crd.Trigger == triggerType.Heal ? heal : 0;

            foreach (KeyValuePair<string, string> keyValue in list) if (keyValue.Value == crd.Name) count++;

            if (restrictionList.TryGetValue(crd.Name, out restrictValue)) return count < restrictValue && listedRestrict < 2 && sntCount < 4 && hlCount < 4;
            else if (effectRestrict.TryGetValue(crd.Name, out restrictValue)) Console.WriteLine("Effect Restricted");
            else restrictValue = 4;

            //Return Valid
            return count < restrictValue && sntCount < 4 && hlCount < 4;
        }

        private void Increment(Card crd)
        {
            //Increment Data Values
            if (crd.IsSentinel) sentinel++;
            //Grades
            switch (crd.Grade)
            {
                case 0:
                    g0++;
                    break;
                case 1:
                    g1++;
                    break;
                case 2:
                    g2++;
                    break;
                case 3:
                    g3++;
                    break;
                case 4:
                    break;
            }

            //Trigger Counts
            switch (crd.Trigger)
            {
                case triggerType.Critical:
                    critical++;
                    break;
                case triggerType.Draw:
                    draw++;
                    break;
                case triggerType.Heal:
                    heal++;
                    break;
                case triggerType.Stand:
                    stand++;
                    break;
            }

            //Stride/G-Guard
            if (crd.IsStride) stride++;
            if (crd.IsGGuardian) ggrd++;

            //Restricted
            int restrictValue = 0;
            if (restrictionList.TryGetValue(crd.Name, out restrictValue)) listedRestrict++;

            //Count Text
            label_Counts.Text = "Deck: " + (list_NormalUnits.Count + list_TriggerUnits.Count)
                + Environment.NewLine + "Snt: " + sentinel
                + Environment.NewLine + "G0: " + g0
                + Environment.NewLine + "G1: " + g1
                + Environment.NewLine + "G2: " + g2
                + Environment.NewLine + "G3: " + g3
                + Environment.NewLine + "G4: " + g4
                + Environment.NewLine + "HT: " + heal
                + Environment.NewLine + "CT: " + critical
                + Environment.NewLine + "ST: " + stand
                + Environment.NewLine + "DT: " + draw
                + Environment.NewLine + "Str: " + stride
                + Environment.NewLine + "GGd: " + ggrd;

            label_Normal.Text = "Normal Units: " + list_NormalUnits.Count;
            label_Trigger.Text = "Trigger Units: " + list_TriggerUnits.Count;
            label_G.Text = "G Units: " + list_GUnits.Count;
        }

        private void RemoveCard(Card crd)
        {
            //Return if Null
            if (crd == null) return;
            //Remove a Card
            switch (crd.Unit)
            {
                case unitType.Normal:
                    list_NormalUnits.Remove(crd.KeyValuePair);
                    Decrement(crd);
                    break;
                case unitType.Trigger:
                    list_TriggerUnits.Remove(crd.KeyValuePair);
                    Decrement(crd);
                    break;
                case unitType.G:
                    list_GUnits.Remove(crd.KeyValuePair);
                    Decrement(crd);
                    break;
            }

            //Update DataSource
            listBox_Normal.DataSource = list_NormalUnits.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_Trigger.DataSource = list_TriggerUnits.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_G.DataSource = list_GUnits.OrderBy(keyValue => keyValue.Value).ToList();
        }

        private void Decrement(Card crd)
        {
            //Increment Data Values
            if (crd.IsSentinel) sentinel--;
            //Grades
            switch (crd.Grade)
            {
                case 0:
                    g0--;
                    break;
                case 1:
                    g1--;
                    break;
                case 2:
                    g2--;
                    break;
                case 3:
                    g3--;
                    break;
                case 4:
                    break;
            }

            //Trigger Counts
            switch (crd.Trigger)
            {
                case triggerType.Critical:
                    critical--;
                    break;
                case triggerType.Draw:
                    draw--;
                    break;
                case triggerType.Heal:
                    heal--;
                    break;
                case triggerType.Stand:
                    stand--;
                    break;
            }

            //Stride/G-Guard
            if (crd.IsStride) stride--;
            if (crd.IsGGuardian) ggrd--;

            //Restricted
            int restrictValue = 0;
            if (restrictionList.TryGetValue(crd.Name, out restrictValue)) listedRestrict--;

            //Count Text
            label_Counts.Text = "Deck: " + (list_NormalUnits.Count + list_TriggerUnits.Count)
                + Environment.NewLine + "Snt: " + sentinel
                + Environment.NewLine + "G0: " + g0
                + Environment.NewLine + "G1: " + g1
                + Environment.NewLine + "G2: " + g2
                + Environment.NewLine + "G3: " + g3
                + Environment.NewLine + "G4: " + g4
                + Environment.NewLine + "HT: " + heal
                + Environment.NewLine + "CT: " + critical
                + Environment.NewLine + "ST: " + stand
                + Environment.NewLine + "DT: " + draw
                + Environment.NewLine + "Str: " + stride
                + Environment.NewLine + "GGd: " + ggrd;

            label_Normal.Text = "Normal Units: " + list_NormalUnits.Count;
            label_Trigger.Text = "Trigger Units: " + list_TriggerUnits.Count;
            label_G.Text = "G Units: " + list_GUnits.Count;
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && (sender as ListBox).Focused)
            {
                //Remove a Card
                ListBox origSender = sender as ListBox;
                if (origSender.Items.Count == 0) return;
                int index = origSender.SelectedIndex;
                RemoveCard(selectedCard);
                //Reset SelectedIndex
                if (origSender.Items.Count > 0) origSender.SelectedIndex = (index > 1 ? index - 1 : 0);
            }
        }

        private void listBox_Normal_DoubleClick(object sender, EventArgs e)
        {
            //Remove a Card
            ListBox origSender = sender as ListBox;
            if (origSender.Items.Count == 0) return;
            int index = origSender.SelectedIndex;
            RemoveCard(selectedCard);
            //Reset SelectedIndex
            if (origSender.Items.Count > 0) origSender.SelectedIndex = (index > 1 ? index - 1 : 0);
        }

        private void listBox_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!listBox_Search.Focused) return;
            selectedCard = new Card(list_Searched[listBox_Search.SelectedIndex].Key, allCards);

            //Update Information
            pictureBox_Info.Image = selectedCard.Image;
            richTextBox_Info.Text = selectedCard.InformationText;
        }

        private void listBox_Normal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!listBox_Normal.Focused) return;
            selectedCard = new Card(list_NormalUnits[listBox_Normal.SelectedIndex].Key, allCards);

            //Update Information
            pictureBox_Info.Image = selectedCard.Image;
            richTextBox_Info.Text = selectedCard.InformationText;
        }

        private void listBox_Trigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!listBox_Trigger.Focused) return;
            selectedCard = new Card(list_TriggerUnits[listBox_Trigger.SelectedIndex].Key, allCards);

            //Update Information
            pictureBox_Info.Image = selectedCard.Image;
            richTextBox_Info.Text = selectedCard.InformationText;
        }

        private void listBox_G_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!listBox_G.Focused) return;
            selectedCard = new Card(list_GUnits[listBox_G.SelectedIndex].Key, allCards);

            //Update Information
            pictureBox_Info.Image = selectedCard.Image;
            richTextBox_Info.Text = selectedCard.InformationText;
        }

        private void DeckMenu_SVG_Click(object sender, EventArgs e)
        {
            if (selectedCard.Grade == 0 && !BannedSVG.Contains(selectedCard.Name))
            {
                startVG = selectedCard.CardID;
                MessageBox.Show(selectedCard.Name + " was selected as the Starting Vanguard!", "Success", MessageBoxButtons.OK);
            }
            else MessageBox.Show(selectedCard.Name + " cannot be a starting vanguard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LargeTextBoxDialog(string information, string title)
        {
            Size size = new System.Drawing.Size(344, 433);
            Form inputBox = new Form();
            inputBox.Text = title;
            inputBox.ClientSize = size;
            inputBox.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            //Rich Text Box
            RichTextBox richtxtbox = new RichTextBox();
            richtxtbox.Location = new System.Drawing.Point(5, 9);
            richtxtbox.Name = "Text Box";
            richtxtbox.Size = new System.Drawing.Size(334, 390);
            richtxtbox.TabIndex = 0;
            richtxtbox.Text = information;
            richtxtbox.ReadOnly = true;
            richtxtbox.BackColor = Color.White;
            // OKButton
            Button OKButton = new Button();
            OKButton.Location = new System.Drawing.Point(134, 400);
            OKButton.Name = "OKButton";
            OKButton.Size = new System.Drawing.Size(75, 23);
            OKButton.TabIndex = 1;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.DialogResult = DialogResult.OK;

            //Add Controls
            inputBox.Controls.Add(OKButton);
            inputBox.Controls.Add(richtxtbox);

            inputBox.ShowDialog(this);
        }
    }
}
