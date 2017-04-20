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
        DataTable allCards;

        Dictionary<string, string> cardDictionary = new Dictionary<string, string>();

        List<KeyValuePair<string, string>> list_Searched = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_NormalUnits = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_TriggerUnits = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> list_GUnits = new List<KeyValuePair<string, string>>();

        public DeckBuilder(DataTable dt)
        {
            InitializeComponent();

            allCards = dt;

            foreach (DataRow dr in dt.Rows)
            {
                cardDictionary.Add((string)dr["cardID"],(string)dr["name"]);
            }

            //Set Data Source
            list_Searched = cardDictionary.OrderBy(keyValue => keyValue.Value).ToList();
            listBox_Search.DataSource = list_Searched;
        }
    }
}
