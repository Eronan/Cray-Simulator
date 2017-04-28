using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cray_Simulator
{
    public partial class Sleeves : Form
    {
        public Sleeves(Bitmap main, Bitmap G, Bitmap Mat)
        {
            InitializeComponent();

            pictureBox_MainSleeve.Image = main;
            pictureBox_GSleeve.Image = G;
            pictureBox_Mat.Image = Mat;
        }

        public Bitmap MainSleeve
        {
            get { return pictureBox_MainSleeve.Image as Bitmap; }
        }

        public Bitmap GSleeve
        {
            get { return pictureBox_GSleeve.Image as Bitmap; }
        }

        public Bitmap Mat
        {
            get { return pictureBox_Mat.Image as Bitmap; }
        }

        private void button_MainSleeve_Click(object sender, EventArgs e)
        {
            if (openFile_Image.ShowDialog(this) == DialogResult.OK) pictureBox_MainSleeve.Image = new Bitmap(openFile_Image.FileName);
        }

        private void button_GSleeve_Click(object sender, EventArgs e)
        {
            if (openFile_Image.ShowDialog(this) == DialogResult.OK) pictureBox_GSleeve.Image = new Bitmap(openFile_Image.FileName);
        }

        private void button_Playmat_Click(object sender, EventArgs e)
        {
            if (openFile_Image.ShowDialog(this) == DialogResult.OK) pictureBox_Mat.Image = new Bitmap(openFile_Image.FileName);
        }
    }
}
