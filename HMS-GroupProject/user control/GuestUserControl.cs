using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HMS_GroupProject
{
    public partial class GuestUserControl : UserControl
    {
        Form1 main;
        public GuestUserControl()
        {
            InitializeComponent();
            StyleGuestControl();
            
        }

        private void StyleGuestControl()
        {
            this.BackColor = Color.FromArgb(0x1A, 0x21, 0x37); // Darker gray background

            // Style Labels
          //  StyleLabel(label1);
          //  StyleLabel(label2);
          //  StyleLabel(label3);
          //  StyleLabel(label4);
            StyleLabel(label9);
            StyleLabel(label10);
            StyleLabel(label12);
            StyleLabel(label13);

            // Style TextBoxes
           // StyleTextBox(textBox1);
           // StyleTextBox(textBox2);
            StyleTextBox(textBox7);
            StyleTextBox(textBox8);
            StyleTextBox(textBox9);
            StyleTextBox(textBox10);
            //StyleTextBox(textBox7);

            // Style Buttons
            StyleButton(button1);
           // StyleButton(button2);

            // Style PictureBoxes
          //  StylePictureBox(pictureBox1);
           // StylePictureBox(pictureBox2);
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

 
private void button1_Click(object sender, EventArgs e)
    {
        // Capture input values from textboxes
        string name = textBox7.Text.Trim();
        string contact = textBox9.Text.Trim();
        string email = textBox8.Text.Trim();
        string address = textBox10.Text.Trim();

        // Input validation
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(email))
        {
            MessageBox.Show("Name, Contact, and Email fields are required.");
            return;
        }

        // SQL query to insert data into the Guest table
        string query = "INSERT INTO Guest (Name, Contact, Email, Address) VALUES (@Name, @Contact, @Email, @Address)";

        try
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;"))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Contact", contact);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? DBNull.Value : (object)address);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Guest registered successfully!");
                    ClearInputFields();
                        // Optional: Clear the input fields after successful registration
                        LoginPage loginPage = new LoginPage(this.main);
                        //adminView view1 = new adminView();
                        this.Controls.Add(loginPage);
                        loginPage.Visible = true;
                        loginPage.Dock = DockStyle.Fill;
                    }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }
    }

    // Method to clear input fields
    private void ClearInputFields()
    {
            textBox7.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox10.Text = string.Empty;
    }

        private void GuestUserControl_Load(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
