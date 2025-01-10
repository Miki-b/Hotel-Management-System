using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject.user_control
{
    public partial class GuestRegUserCo : UserControl
    {
        public GuestRegUserCo()
        {
            InitializeComponent();
            StyleGuestRegControl();
        }

        private void StyleGuestRegControl()
        {
            this.BackColor = Color.FromArgb(0x1A, 0x21, 0x37); // Darker gray background

            // Style Labels
            StyleLabel(label1);
            StyleLabel(label3);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label11); // Title label

            // Style TextBoxes
            StyleTextBox(textBox1);
            StyleTextBox(textBox2);
            StyleTextBox(textBox4);
            StyleTextBox(textBox5);

            // Style Button
            StyleButton(button1);
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
            textBox.Font = new Font("Segoe UI", 10); // Consistent font
        }

        private void StyleButton(Button button)
        {
            button.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // Gray background
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Consistent font
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void GuestRegUserCo_Load(object sender, EventArgs e)
        {

        }
    }
}
