using System;
using System.Collections.Generic;

namespace Cray_Simulator
{
    internal class Field
    {
        private List<Card> AllCards = new List<Card>();
        public List<Card> Deck = new List<Card>();
        public List<Card> GZone = new List<Card>();
        public List<Card> VGCircle = new List<Card>();
        public List<Card> Heart = new List<Card>();
        public Card[] RGCircles = new Card[5];
        public List<Card> Hand = new List<Card>();
        public List<Card> DropZone = new List<Card>();
        public List<Card> BindZone = new List<Card>();
        public List<Card> DamageZone = new List<Card>();
        public List<Card> GuardCircle = new List<Card>();
        public List<Card> Soul = new List<Card>();
        private int checkDeck = 0;
        public Card TriggerZone;

        public int CheckDeck
        {
            get
            { return checkDeck; }
            set { checkDeck = value; }
        }

        public Field(List<Card> allCards, string startVGKey)
        {
            foreach (Card allCard in allCards)
            {
                if (allCard.Unit == unitType.G)
                {
                    allCard.Location = "G Zone-" + GZone.Count;
                    GZone.Add(allCard);
                }
                else if (allCard.CardID == startVGKey && VGCircle.Count < 2)
                {
                    allCard.Location = "Vanguard-0";
                    allCard.FaceUp = true;
                    VGCircle.Add(allCard);
                }
                else
                {
                    allCard.Location = "Deck-" + Deck.Count;
                    Deck.Add(allCard);
                }
            }
        }

        public Card returnCard(string location)
        {
            string[] strArray = location.ToString().Split('-');
            int result = -1;
            if (!int.TryParse(strArray[1], out result)) return null;
            
            switch (strArray[0])
            {
                case "Rearguard":
                    return RGCircles[result];
                case "DecK":
                    return Deck[result];
                case "Drop":
                    return DropZone[result];
                case "Bind":
                    return BindZone[result];
                case "Damage":
                    return DamageZone[result];
                case "G Zone":
                    return GZone[result];
                case "Soul":
                    return Soul[result];
                case "Vanguard":
                    return VGCircle[result];
                case "Hand":
                    return Hand[result];
                case "Heart":
                    return Heart[result];
                case "Guardian":
                    return GuardCircle[result];
                case "Trigger":
                    return TriggerZone;
                default:
                    return null;
            }
        }

        public void RemoveCard(string location)
        {
            string[] strArray = location.ToString().Split('-');
            int result = -1;
            if (!int.TryParse(strArray[1], out result)) return;

            switch (strArray[0])
            {
                case "Rearguard":
                    RGCircles[result] = null;
                    break;
                case "DecK":
                    Deck.RemoveAt(result);
                    break;
                case "Drop":
                    DropZone.RemoveAt(result);
                    break;
                case "Bind":
                    BindZone.RemoveAt(result);
                    break;
                case "Damage":
                    DamageZone.RemoveAt(result);
                    break;
                case "G Zone":
                    GZone.RemoveAt(result);
                    break;
                case "Soul":
                    Soul.RemoveAt(result);
                    break;
                case "Vanguard":
                    VGCircle.RemoveAt(result);
                    break;
                case "Hand":
                    Hand.RemoveAt(result);
                    break;
                case "Heart":
                    Heart.RemoveAt(result);
                    break;
                case "Guardian":
                    GuardCircle.RemoveAt(result);
                    break;
                case "Trigger":
                    TriggerZone = null;
                    break;
            }
        }

        public void ShuffleDeck()
        {
            Random random = new Random();
            for (int count = Deck.Count; count > 1; --count)
            {
                int index = random.Next(count);
                Card card = Deck[count - 1];
                Deck[count - 1] = Deck[index];
                Deck[index] = card;
            }
        }

        public void ShuffleHand()
        {
            Random random = new Random();
            for (int count = Hand.Count; count > 1; --count)
            {
                int index = random.Next(count);
                Card card = Hand[count - 1];
                Hand[count - 1] = Hand[index];
                Hand[index] = card;
            }
        }

        public List<Card> CheckNDeck()
        {
            List<Card> cardList = new List<Card>();
            if (checkDeck > Deck.Count)
            {
                checkDeck = 0;
                return Deck;
            }
            if (checkDeck <= 0) return null;
            for (int index = 0; index < checkDeck; ++index) cardList.Add(Deck[index]);
            return cardList;
        }
    }
}
