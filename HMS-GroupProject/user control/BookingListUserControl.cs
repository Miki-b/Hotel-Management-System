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
            // Style controls
            StyleGroupBox(groupBox1);
            StyleGroupBox(groupBox2);

            StyleTextBox(textBox1);
            StyleTextBox(textBox2);
            StyleTextBox(textBox3);
            StyleTextBox(textBox4);
            StyleTextBox(textBox5);
            StyleTextBox(textBox6);
            StyleTextBox(textBox7);
            StyleTextBox(textBox8);
            StyleTextBox(textBox9);
            StyleTextBox(textBox10);
            StyleTextBox(textBox11);
            StyleTextBox(textBox12);
            StyleTextBox(textBox13);
            StyleTextBox(textBox14);
            StyleTextBox(textBox15);
            StyleTextBox(textBox16);
            StyleTextBox(textBox17);
            StyleTextBox(textBox18);
            StyleTextBox(textBox19);

            StyleLabel(label1);
            StyleLabel(label2);
            StyleLabel(label3);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label6);
            StyleLabel(label7);
            StyleLabel(label8);
            StyleLabel(label9);
            StyleLabel(label10);
            StyleLabel(label11);
            StyleLabel(label12);
            StyleLabel(label13);
            StyleLabel(label14);
            StyleLabel(label15);
            StyleLabel(label16);
            StyleLabel(label17);
            StyleLabel(label18);
            StyleLabel(label19);
            StyleLabel(label20);
            StyleLabel(label21);
            StyleLabel(label22);
            StyleLabel(label23);
            StyleLabel(label24);
            StyleLabel(label25);

            StyleDateTimePicker(dateTimePicker1);
            StyleCheckedListBox(checkedListBox1);
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox2);
            StyleButton(button1);


        }

        private void StyleGroupBox(System.Windows.Forms.GroupBox groupBox)
        {
            groupBox.ForeColor = Color.White; // Text color
        }

        private void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // Gray background
            textBox.ForeColor = Color.LightGray; // Placeholder text color
            textBox.Font = new Font("Segoe UI", 12); // Font
            textBox.BorderStyle = BorderStyle.FixedSingle; // Border

            // Implement placeholder text logic (similar to StyleSearchBox in adminView)
        }

        private void StyleLabel(Label label)
        {
            label.ForeColor = Color.White; // Text color
            label.Font = new Font("Segoe UI", 12); // Font (adjust size if needed)
        }

        private void StyleDateTimePicker(DateTimePicker dateTimePicker)
        {
            dateTimePicker.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // Gray background
            dateTimePicker.ForeColor = Color.LightGray; // Text color
            dateTimePicker.Font = new Font("Segoe UI", 12); // Font
            dateTimePicker.CalendarForeColor = Color.White; // Calendar text color (optional)
        }

        private void StyleCheckedListBox(CheckedListBox checkedListBox)
        {
            checkedListBox.BackColor = Color.FromArgb(0x00, 0x1F, 54); // Background color
            checkedListBox.ForeColor = Color.White; // Text color
            //checkedListBox.ItemCheckForeColor = Color.White; // Checked item text color (optional)
        }

        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // Background color
            comboBox.ForeColor = Color.LightGray; // Text color
            comboBox.Font = new Font("Segoe UI", 12); // Font
            comboBox.FlatStyle = FlatStyle.Flat; // Flat style
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Remove border
        }

        private void StyleButton(Button button)
        {
            button.BackColor = Color.FromArgb(0x03, 0x40, 78); // Light blue background
            button.ForeColor = Color.White; // Text color
            button.FlatStyle = FlatStyle.Flat; // Flat style
            button.FlatAppearance.BorderSize = 0; // Remove border
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0x00, 0x1F, 54); // Hover effect
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

        private void BookingListUserControl_Load(object sender, EventArgs e)
        {

        }
    }
}
