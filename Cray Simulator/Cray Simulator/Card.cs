using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Cray_Simulator
{
    public enum unitType
    {
        Normal,
        Trigger,
        G,
    }

    public static class UnitType
    {
        public static unitType Parse(string s)
        {
            switch (s.ToUpper())
            {
                case "TRIGGER":
                case "TRIGGER UNIT":
                    return unitType.Trigger;
                case "G":
                case "G UNIT":
                    return unitType.G;
                default:
                    return unitType.Normal;
            }
        }
    }

    public enum triggerType
    {
        Critical,
        Draw,
        Stand,
        Heal,
        None,
    }

    public static class TriggerType
    {
        public static triggerType Parse(string s)
        {
            switch (s.ToUpper())
            {
                case "CRITICAL":
                    return triggerType.Critical;
                case "DRAW":
                    return triggerType.Draw;
                case "STAND":
                    return triggerType.Stand;
                case "HEAL":
                    return triggerType.Heal;
                default:
                    return triggerType.None;
            }
        }
    }

    public static class ImageConverter
    {
        public static Bitmap ToBitmap(string s)
        {
            try
            {
                Image img = null;
                byte[] bitmapBytes = Convert.FromBase64String(s);
                using (MemoryStream memoryStream = new MemoryStream(bitmapBytes))
                {
                    img = Image.FromStream(memoryStream);
                }

                return img as Bitmap;
            }
            catch { return null; }
        }

        public static string ToString(Bitmap image)
        {
            try
            {
                string bitmapString = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    byte[] bitmapBytes = memoryStream.GetBuffer();
                    bitmapString = Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);
                }

                return bitmapString;
            }
            catch { return null; }
        }
    }

    public class Card
    {
        //Editable Values
        int _power;
        int _shield;
        bool _faceup = false;
        bool _target = false;
        bool _rest = false;
        string _location;

        //Get Values
        string _cID;
        string _name;
        unitType _uclass;
        triggerType _trigger;
        int _grade;
        int _origPower;
        int _critical;
        int _origShield;
        Bitmap cardImage = null;
        Bitmap redImage = null;

        //Information Values
        string _clan;
        string[] _race;
        string _abilities;
        string _flavour;
        string _nation;
        string _illust;

        private void Initialize(DataRow dr, bool LoadImage)
        {
            _cID = dr["cardID"].ToString();
            _name = dr["name"].ToString();
            _uclass = UnitType.Parse(dr["uclass"].ToString());
            _trigger = TriggerType.Parse(dr["trigger"].ToString());
            _grade = Convert.ToInt32(dr["grade"]);
            _origPower = Convert.ToInt32(dr["power"]);
            _critical = Convert.ToInt32(dr["critical"]);
            try { _origShield = Convert.ToInt32(dr["shield"]); }
            catch { _origShield = 0; }

            //Set Power and Shield
            _power = _origPower;
            _shield = _origShield;

            //Text Values
            _clan = dr["clan"].ToString();
            _race = dr["race"].ToString().Split('/');
            _abilities = dr["effect"].ToString();
            _flavour = dr["flavour"].ToString();
            _nation = dr["nation"].ToString();
            _illust = dr["illustrator"].ToString();

            //Location Default
            if (_uclass == unitType.G) _location = "G Zone-0";
            else _location = "Deck-0";

            //Get Image
            if (LoadImage)
            {
                try { cardImage = new Bitmap(@"Images/" + _cID.Replace('/', '-') + ".jpg"); }
                catch (FileNotFoundException)
                {
                    //Create New Card
                    cardImage = new Bitmap(ImageConverter.ToBitmap(Properties.Settings.Default.GSleeve));
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                redImage = new Bitmap(cardImage);

                using (Graphics g = Graphics.FromImage(redImage))
                {
                    Rectangle rect = new Rectangle(Point.Empty, redImage.Size);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 0, 0)), rect);
                }
            }
        }

        public Card(DataRow dr, bool LoadImage = true)
        {
            Initialize(dr, LoadImage);
        }

        public Card(string cardID, DataTable dt, bool LoadImage = true)
        {
            _cID = cardID;
            DataRow[] drArray = dt.Select("cardID='" + _cID + "'");
            if (drArray.Length > 0) Initialize(drArray[0], LoadImage);
            else Console.WriteLine("Card does not exist.");
        }

        public string CardID
        {
            get { return _cID; }
        }

        public string Name
        {
            get { return _name; }
        }

        public Bitmap OrigImage
        {
            get { return cardImage; }
        }

        public Bitmap Image
        {
            get
            {
                if (_target) return redImage;
                else return cardImage;
            }
        }

        public Bitmap RedImage
        {
            get { return redImage; }
        }

        public unitType Unit
        {
            get { return _uclass; }
        }

        public triggerType Trigger
        {
            get { return _trigger; }
        }

        public string TriggerCheck()
        {
            switch (this._trigger)
            {
                case triggerType.Critical:
                    return "+5000 Power, +1 Critical";
                case triggerType.Draw:
                    return "+5000 Power, Draw 1";
                case triggerType.Heal:
                    return "+5000 Power, Heal";
                case triggerType.Stand:
                    return "+5000 Power, Stand";
                default:
                    return "No Trigger";
            }
        }

        public int Grade
        {
            get { return _grade; }
        }
        
        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        public int OriginalPower
        {
            get { return _origPower; }
        }

        public void ResetPower()
        {
            _power = _origPower;
        }

        public int Shield
        {
            get { return _shield; }
            set { _shield = value; }
        }

        public void ResetShield()
        {
            _shield = _origShield;
        }

        public bool IsClan(string s)
        {
            if (s == "Any") return true;
            else return s.ToUpper() == _clan.ToUpper();
        }

        public bool IsRace(string s)
        {
            if (s == "Any") return true;
            else
            {
                foreach (string str in _race)
                {
                    if (str.ToUpper() == s.ToUpper()) return true;
                }
                return false;
            }
        }

        public bool IsGGuardian
        {
            get { return _abilities.ToUpper().Contains("[G GUARDIAN]"); }
        }

        public bool IsStride
        {
            get { return _abilities.ToUpper().Contains("[STRIDE]"); }
        }
        
        public bool IsSentinel
        {
            get { return _abilities.ToUpper().Contains("[CONT]:SENTINEL"); }
        }

        public bool FaceUp
        {
            get { return _faceup; }
            set { _faceup = value; }
        }

        public bool TurnOver()
        {
            return _faceup = !_faceup;
        }

        public bool Rested
        {
            get { return _rest; }
            set { _rest = value; }
        }

        public bool RestStand()
        {
            return _rest = !_rest;
        }

        public bool Targetted
        {
            get { return _target; }
        }

        public bool Target()
        {
            return _target = !_target;
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public void Reset()
        {
            //Set Value based on Location (Rest + Face Up)
            string str = _location.Split('-')[0];
            switch (str)
            {
                case "Deck":
                case "Hand":
                    _faceup = false;
                    _rest = false;
                    break;
                case "Bind":
                case "G Zone":
                    _rest = false;
                    break;
                case "Guardian":
                    _faceup = true;
                    _rest = true;
                    break;
                default:
                    _faceup = true;
                    _rest = false;
                    break;
            }

            //Reset Other Values
            _power = _origPower;
            _shield = _origShield;
            _target = false;
        }

        public KeyValuePair<string, string> KeyValuePair
        {
            get { return new KeyValuePair<string, string>(_cID, _name); }
        }

        public bool HasEffect(string s)
        {
            return _abilities.Contains(s.ToUpper());
        }


        public string InformationText
        {
            get
            {
                string returnText = _name
                    + Environment.NewLine + "Grade " + _grade + "/" + _origPower + " Power/" + _origShield + " Shield"
                    + Environment.NewLine + _clan + "/" + string.Join("/", _race)+ "/" + _nation
                    + Environment.NewLine
                    + Environment.NewLine + _abilities
                    + Environment.NewLine
                    + Environment.NewLine + _flavour
                    + Environment.NewLine
                    + Environment.NewLine + _illust
                    + Environment.NewLine
                    + Environment.NewLine + _cID;

                return returnText;
            }
        }
    }
}
