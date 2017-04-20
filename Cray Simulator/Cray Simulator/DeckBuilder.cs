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

        public DeckBuilder(DataTable dt)
        {
            InitializeComponent();

            allCards = dt;

            foreach (DataRow dr in dt.Rows)
            {
                cardDictionary.Add((string)dr["cardID"], (string)dr["name"]);
            }

            //Set Data Source
            list_Searched = cardDictionary.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_Search.DataSource = list_Searched;

            ActiveControl = listBox_Search;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox origSender = sender as ListBox;
            selectedCard = new Card((string)origSender.SelectedValue, allCards);

            pictureBox_Info.Image = selectedCard.Image;
            richTextBox_Info.Text = selectedCard.InformationText;
        }

        private void SearchBox_GotFocus(object sender, EventArgs e)
        {
            TextBox origSender = sender as TextBox;
            if (origSender.ForeColor == Color.Gray)
            {
                origSender.Text = "";
                origSender.ForeColor = Color.Black;
            }
        }

        private void textBox_NameSearch_LostFocus(object sender, EventArgs e)
        {
            if (textBox_NameSearch.Text == "")
            {
                textBox_NameSearch.Text = "Name";
                textBox_NameSearch.ForeColor = Color.Gray;
            }
        }

        private void textBox_SetSearch_LostFocus(object sender, EventArgs e)
        {
            if (textBox_SetSearch.Text == "")
            {
                textBox_SetSearch.Text = "Set";
                textBox_SetSearch.ForeColor = Color.Gray;
            }
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
            switch (crd.Unit)
            {
                case unitType.Normal:
                    if (CheckValid(crd)) list_NormalUnits.Add(crd.KeyValuePair);
                    break;
                case unitType.Trigger:
                    if (CheckValid(crd)) list_TriggerUnits.Add(crd.KeyValuePair);
                    break;
                case unitType.G:
                    if (CheckValid(crd)) list_GUnits.Add(crd.KeyValuePair);
                    break;
            }

            listBox_Normal.DataSource = list_NormalUnits.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_Trigger.DataSource = list_TriggerUnits.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_G.DataSource = list_GUnits.OrderBy(keyValue => keyValue.Value).ToList();
        }

        private bool CheckValid(Card crd)
        {
            return true;
        }

        private void Increment(Card crd)
        {
            //Increment Data Values
        }
    }
}
