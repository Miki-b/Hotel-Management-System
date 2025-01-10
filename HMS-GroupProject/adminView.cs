using HMS_GroupProject.user_control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class adminView : UserControl
    {
        public GuestUserControl ucguest = new GuestUserControl();
        public BookingListUserControl ucbooking = new BookingListUserControl();
        public HomeUserControl uchome = new HomeUserControl();
        public RoomUserControl ucroom = new RoomUserControl();
        public adminView()
        {
            InitializeComponent();
            panelMain.Controls.Clear();
            //panelMain.Controls.Add(uchome);
            //uchome.Dock = DockStyle.Fill;
             this.BackColor = Color.FromArgb(0x0A, 0x11, 0x28);

            StyleButton(homeButton);
            StyleButton(button1);
            StyleButton(button2);
            StyleButton(button3);

            StyleLabel(HMS);
            StyleLabel(UserName);
            StyleSearchBox(textBox1); // Style the search box

            StylePanel(panel1);
            StylePanel(panel2);
            StylePanel(panelMain);
            panelMain.BackColor = Color.FromArgb(0x1A, 0x21, 0x37);
        }

        private void StylePanel(Panel panel)
        {
            panel.BorderStyle = BorderStyle.FixedSingle; // Add a simple border
            panel.BackColor = Color.FromArgb(0x00, 0x1F, 0x54); // Set the background color to the second color (#001F54)
        }


        private void StyleSearchBox(TextBox textBox)
    {
        textBox.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // A suitable gray from your palette
        textBox.ForeColor = Color.LightGray; // Light gray text for placeholder
        textBox.Font = new Font("Segoe UI", 12); // Slightly larger font
        textBox.BorderStyle = BorderStyle.FixedSingle; // Keep a simple border

        // Placeholder text (using Enter and Leave events)
        textBox.Enter += (sender, e) =>
        {
            if (textBox.Text == "Search...")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.White; // Change text color when typing
            }
        };

        textBox.Leave += (sender, e) =>
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Search...";
                textBox.ForeColor = Color.LightGray; // Reset placeholder color
            }
        };

        textBox.Text = "Search..."; // Initial placeholder text
    }
    // ... (rest of your code)


        private void StyleButton(Button button)
        {
            button.BackColor = Color.FromArgb(0x03, 0x40, 0x78); // Lightest blue for background
            button.ForeColor = Color.White; // White text
            button.FlatStyle = FlatStyle.Flat; // Flat style
            button.FlatAppearance.BorderSize = 0; // Remove border
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0x00, 0x1F, 0x54); // Middle blue on hover
        }

        private void StyleLabel(System.Windows.Forms.Label label)
        {
            label.ForeColor = Color.White; // White text
            label.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Bigger font, bold (adjust size as needed)
        }


        private void adminView_Load(object sender, EventArgs e)
        {

        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(uchome);
            uchome.Dock = DockStyle.Fill;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(ucroom);
            ucroom.Dock = DockStyle.Fill;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(ucguest);
            ucguest.Dock = DockStyle.Fill;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(ucbooking);
            ucbooking.Dock = DockStyle.Fill;
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
