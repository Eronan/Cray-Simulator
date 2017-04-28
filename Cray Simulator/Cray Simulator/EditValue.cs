using System;
using System.Windows.Forms;

namespace Cray_Simulator
{
    public partial class EditValue : Form
    {
        int initValue = 0;

        public EditValue(string value, int initialValue)
        {
            InitializeComponent();

            //Change Text
            this.Text = "Edit " + value;
            label_Value.Text = value + ":";
            checkBox_Add.Text = "Add " + value;

            //Get 'Original' Value
            initValue = initialValue;

            //Set Select
            numeric_Value.Select(1, 0);
        }

        private void numeric_Value_ValueChanged(object sender, EventArgs e)
        {
            if (numeric_Value.Value.ToString().EndsWith("000")) return;
            numeric_Value.Value *= 1000;
        }

        private void numeric_Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Press OK Button
            if (e.KeyChar == (char) Keys.Enter) button_OK.PerformClick();
        }

        private void checkBox_Add_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Add.Checked) numeric_Value.Value -= initValue;
            else numeric_Value.Value += initValue;
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            if (checkBox_Add.Checked) numeric_Value.Value = 0;
            else numeric_Value.Value = initValue;
        }

        public int returnValue
        {
            get { return (checkBox_Add.Checked ? (int) numeric_Value.Value : (int) numeric_Value.Value - initValue); }
        }
    }
}
