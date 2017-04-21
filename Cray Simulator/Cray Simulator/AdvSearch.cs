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
    public partial class AdvSearch : Form
    {
        public AdvSearch(List<string> Clans, List<string> Races)
        {
            InitializeComponent();

            //Initialize Clans and Races
            comboBox_Clan.DataSource = Clans;
            comboBox_Race.DataSource = Races;
        }

        private void AdvSearch_Load(object sender, EventArgs e)
        {
            comboBox_UClass.SelectedIndex = 0;
        }

        public string CardName
        {
            get { return textBox_Name.Text.ToUpper(); }
        }

        public string CardID
        {
            get { return textBox_Set.Text.ToUpper(); }
        }

        public bool IsClan(string s)
        {
            //Case Sensitive
            if (comboBox_Clan.Text == "Any") return true;
            else return s == comboBox_Clan.Text;
        }

        public bool IsUClass(string s)
        {
            if (comboBox_UClass.Text == "Any") return true;
            else return comboBox_UClass.Text == s;
        }

        public bool IsUClass(unitType uclass)
        {
            if (comboBox_UClass.Text == "Any") return true;
            else return UnitType.Parse(comboBox_UClass.Text) == uclass;
        }

        public bool IsRace(string s)
        {
            //Case Sensitiive
            if (comboBox_Race.Text == "Any") return true;
            string[] races = s.Split('/');

            foreach (string str in races)
            {
                if (str == comboBox_Race.Text) return true;
            }

            return false;
        }

        public string Race
        {
            get { return comboBox_Race.Text; }
        }

        public bool Grade(int grade)
        {
            return grade >= numeric_GradeMin.Value && grade <= numeric_GradeMax.Value;
        }

        public bool Power(int power)
        {
            return power >= numeric_PowerMin.Value && power <= numeric_PowerMax.Value;
        }

        public bool HasAbility(string s)
        {
            return s.ToUpper().Contains(richTextBox_Abilities.Text.ToUpper());
        }
    }
}
