using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class LoginPage : UserControl
    {


        private Form1 mainForm;
        public LoginPage(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
            this.BackColor = Color.FromArgb(0x8B, 0x9F, 0xCA); // Light blue color from your palette
        }
        private string connectionString = "Data Source=DESKTOP-2RI98PE\\SQLEXPRESS;Initial Catalog=Hotel_Managment;Integrated Security=True;Encrypt=False";

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {

        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            // Set the background color to the dark blue
            this.BackColor = Color.FromArgb(0x1F, 0x38, 0x5C); // Dark blue background for the form

            // Labels (assuming label1, label2, and Login are your labels)
            label1.ForeColor = Color.White; // White text for labels
            label2.ForeColor = Color.White; // White text for labels
            Login.ForeColor = Color.White; // White text for labels

            // Text boxes
            textBox2.BackColor = Color.White; // White background
            textBox2.BorderStyle = BorderStyle.FixedSingle; // Single border
            textBox2.ForeColor = Color.Black; // Text Color
            textBox1.BackColor = Color.White; // White background
            textBox1.BorderStyle = BorderStyle.FixedSingle; // Single border
            textBox1.ForeColor = Color.Black; // Text Color

            // Button
            button1.BackColor = Color.FromArgb(0x0A, 0x29, 0x4B); // Dark blue background (same as before)
            button1.ForeColor = Color.White; // White text (same as before)
            button1.FlatStyle = FlatStyle.Flat; // Flat style (same as before)
            button1.FlatAppearance.BorderSize = 0; // No border (same as before)
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0x07, 0x1A, 0x30); // Darker blue on hover (same as before)

            label1.Font = new Font("Segoe UI", 12); // Example font and size
            label2.Font = new Font("Segoe UI", 12);
            Login.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            textBox1.Font = new Font("Segoe UI", 12);
            textBox2.Font = new Font("Segoe UI", 12);
            button1.Font = new Font("Segoe UI", 12);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox2.Text.Trim();
            string password = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to validate login credentials
                    string query = "SELECT Role FROM Login WHERE Username = @username AND Password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        object result = cmd.ExecuteScalar();
                        Console.WriteLine(result);

                        if (result != null)
                        {
                            string role = result.ToString();
                            
                            // Redirect based on role
                            switch (role)
                            {
                                case "Guest":
                                    MessageBox.Show("Welcome Guest! Redirecting to Guest Page...", "Login Successful");
                                    
                                    break;
                                case "Employee":
                                    MessageBox.Show("Welcome Employee! Redirecting to Employee Page...", "Login Successful");
                                    //mainForm.ShowDashboard();
                                    // Open Employee page here
                                    break;
                                case "Admin":
                                    MessageBox.Show("Welcome Admin! Redirecting to Admin Page...", "Login Successful");
                                    //mainForm.ShowDashboard();
                                    mainForm.adminView();
                                    // Open Admin page here
                                    break;
                                default:
                                    MessageBox.Show("Invalid role assigned to the user.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging in: {ex.Message}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
