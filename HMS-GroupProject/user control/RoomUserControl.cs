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
    public partial class RoomUserControl : UserControl
    {
        public RoomUserControl()
        {
            InitializeComponent();
            StyleRoomControl();
        }

        private void StyleRoomControl()
        {
            this.BackColor = Color.FromArgb(0x1A, 0x21, 0x37); // Darker gray background

            // Style Labels
            StyleLabel(label1); // Room Picture label
            StyleLabel(label2); // Room Details label
            StyleLabel(label3); // Room Amenities label
            StyleLabel(label13); // Room Price
            StyleLabel(label12); // Reservation Status
            StyleLabel(label11); // Room Type (top)
            StyleLabel(label10); // Room Number
            StyleLabel(label9); // Room Status
            StyleLabel(label8); // Return Status
            StyleLabel(label7); // FO Status
            StyleLabel(label6); // Room Type (bottom)
            StyleLabel(label5); // Room Capacity
            StyleLabel(label4); // Bed Size

            // Style TextBoxes
            StyleTextBox(textBox1);
            StyleTextBox(textBox2);
            StyleTextBox(textBox3);

            // Style ComboBoxes
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox2);
            StyleComboBox(comboBox3);
            StyleComboBox(comboBox4);
            StyleComboBox(comboBox5);
            StyleComboBox(comboBox6);
            StyleComboBox(comboBox8);

            // Style CheckBoxes
            StyleCheckBox(checkBox1);
            StyleCheckBox(checkBox2);
            StyleCheckBox(checkBox3);
            StyleCheckBox(checkBox4);
            StyleCheckBox(checkBox5);
            StyleCheckBox(checkBox6);
            StyleCheckBox(checkBox7);
            StyleCheckBox(checkBox8);

            //Style Panel
            StylePanel(panel1);
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

        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // Gray background
            comboBox.ForeColor = Color.White;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        private void StyleCheckBox(CheckBox checkBox)
        {
            checkBox.ForeColor = Color.White;
        }

        private void StylePanel(Panel panel)
        {
            panel.BackColor = Color.FromArgb(0x28, 0x33, 0x4A);
        }

        private void RoomUserControl_Load(object sender, EventArgs e)
        {

        }
    }
}
