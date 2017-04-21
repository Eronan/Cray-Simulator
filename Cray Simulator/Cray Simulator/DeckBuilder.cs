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
    public partial class DeckBuilder : Form
    {
        //Data
        DataTable allCards;
        Dictionary<string, string> cardDictionary = new Dictionary<string, string>();
        Dictionary<string, int> restrictList = new Dictionary<string, int>();
        Dictionary<string, int> effectRestrict = new Dictionary<string, int>();

        //List Information
        List<KeyValuePair<string, string>> list_Searched = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_NormalUnits = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_TriggerUnits = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_GUnits = new List<KeyValuePair<string, string>>();

        //Form Information
        Card selectedCard;
        bool SearchFocusChange = false;
        List<string> Clans = new List<string>();
        List<string> Races = new List<string>();
        AdvSearch searchForm;

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

            //Set Tabbed Control
            ActiveControl = listBox_Search;

            //restrictList.Add("100% Orange", 2);
            restrictList.Add("3 Apple Sisters", 1);

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
                    if (CheckValid(crd, list_NormalUnits))
                    {
                        list_NormalUnits.Add(crd.KeyValuePair);
                        Increment(crd);
                        list_NormalUnits = list_NormalUnits.OrderBy(keyValue => keyValue.Value).ToList();
                    }
                    break;
                case unitType.Trigger:
                    if (CheckValid(crd, list_TriggerUnits))
                    {
                        list_TriggerUnits.Add(crd.KeyValuePair);
                        Increment(crd);
                        list_TriggerUnits = list_TriggerUnits.OrderBy(keyValue => keyValue.Value).ToList();
                    }
                    break;
                case unitType.G:
                    if (CheckValid(crd, list_GUnits))
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

            if (restrictList.TryGetValue(crd.Name, out restrictValue)) return count < restrictValue && listedRestrict < 2 && sntCount < 4 && hlCount < 4;
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
            if (restrictList.TryGetValue(crd.Name, out restrictValue)) listedRestrict++;

            //Count Text
            label_Counts.Text = "Deck: " + (list_NormalUnits.Count + list_TriggerUnits.Count + list_GUnits.Count)
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
            if (restrictList.TryGetValue(crd.Name, out restrictValue)) listedRestrict--;

            //Count Text
            label_Counts.Text = "Deck: " + (list_NormalUnits.Count + list_TriggerUnits.Count + list_GUnits.Count)
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
    }
}
