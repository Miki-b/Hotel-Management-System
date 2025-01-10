using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class GuestUserControl : UserControl
    {
        public GuestUserControl()
        {
            InitializeComponent();
            StyleGuestControl();
        }

        private void StyleGuestControl()
        {
            this.BackColor = Color.FromArgb(0x1A, 0x21, 0x37); // Darker gray background

            // Style Labels
            StyleLabel(label1);
            StyleLabel(label2);
            StyleLabel(label3);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label6);
            StyleLabel(label7);
            StyleLabel(label8);

            // Style TextBoxes
            StyleTextBox(textBox1);
            StyleTextBox(textBox2);
            StyleTextBox(textBox3);
            StyleTextBox(textBox4);
            StyleTextBox(textBox5);
            StyleTextBox(textBox6);
            StyleTextBox(textBox7);

            // Style Buttons
            StyleButton(button1);
            StyleButton(button2);

            // Style PictureBoxes
            StylePictureBox(pictureBox1);
            StylePictureBox(pictureBox2);
        }

        private void StyleLabel(Label label)
        {
            label.ForeColor = Color.White;
            label.Font = new Font("Segoe UI", 12);
        }

        private void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // Gray background
            textBox.ForeColor = Color.White;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StyleButton(Button button)
        {
            button.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // Gray background
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
        }

        private void StylePictureBox(PictureBox pictureBox)
        {
            pictureBox.BorderStyle = BorderStyle.None; // Remove any borders
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
