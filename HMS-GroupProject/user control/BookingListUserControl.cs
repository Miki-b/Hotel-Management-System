using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace HMS_GroupProject
{
    public partial class BookingListUserControl : UserControl
    {
        public BookingListUserControl()
        {
            InitializeComponent();
            this.BackColor = AppColorss.BackgroundColor; // Set form background color

            // Set colors for other UI elements
            label2.ForeColor = AppColorss.TextColor;
            label22.ForeColor = AppColorss.TextColor;
            groupBox1.BackColor = AppColorss.BackgroundColor;
            button1.BackColor = AppColorss.HunterGreen;
            button1.ForeColor = Color.White; // Set button text color
            textBox6.ForeColor = Color.White;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
