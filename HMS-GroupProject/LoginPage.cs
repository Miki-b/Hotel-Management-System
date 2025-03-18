using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class LoginPage : UserControl
    {


        private Form1 mainForm;
        private String GuestID;
        public LoginPage(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
            
            this.BackColor = Color.FromArgb(0x8B, 0x9F, 0xCA); // Light blue color from your palette
        }
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";

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
            // Set a bright and modern background color
            this.BackColor = Color.FromArgb(224, 247, 250); // Light blue tint

            // Labels Styling
            label1.ForeColor = Color.FromArgb(21, 82, 135); // Dark blue for better readability
            label2.ForeColor = Color.FromArgb(21, 82, 135);
            Login.ForeColor = Color.FromArgb(10, 41, 75);
            Login.Font = new Font("Segoe UI", 18, FontStyle.Bold);

            // Textboxes Styling
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox1.Font = new Font("Segoe UI", 14);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Padding = new Padding(5);

            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox2.Font = new Font("Segoe UI", 14);
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Padding = new Padding(5);

            // Buttons Styling
            button1.BackColor = Color.FromArgb(45, 130, 195); // Vibrant blue
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            button1.Cursor = Cursors.Hand;
            button1.Text = "Login";
            //button1.MouseEnter += (s, e) => button1.BackColor = Color.FromArgb(26, 102, 168);
            //button1.MouseLeave += (s, e) => button1.BackColor = Color.FromArgb(45, 130, 195);

            // Links Styling
            linkLabel1.LinkColor = Color.FromArgb(21, 82, 135);
            linkLabel1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            linkLabel2.LinkColor = Color.FromArgb(21, 82, 135);
            linkLabel2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
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
                    try
                    {
                        conn.Open();

                        // Query to join Login table with Guest and Employee tables
                        string query = @"SELECT Password, Role, Guest_ID, Employee_ID FROM Login WHERE Username = @username";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@username", username);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string storedHash = reader["Password"].ToString();
                                    string role = reader["Role"].ToString();
                                    int? guestId = reader["Guest_ID"] != DBNull.Value ? Convert.ToInt32(reader["Guest_ID"]) : (int?)null;
                                    int? employeeId = reader["Employee_ID"] != DBNull.Value ? Convert.ToInt32(reader["Employee_ID"]) : (int?)null;

                                    // Verify the entered password using BCrypt
                                    bool isValid = BCrypt.Net.BCrypt.Verify(password, storedHash);

                                    if (isValid)
                                    {
                                        // Redirect user based on their role
                                        switch (role)
                                        {
                                            case "Guest":
                                                if (guestId.HasValue)
                                                {
                                                    MessageBox.Show($"Welcome Guest ! Redirecting to Guest Page...", "Login Successful");
                                                    mainForm.GuestView(role, guestId.Value); // Pass guest details
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Guest ID not found for this user.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                break;

                                            case "Employee":
                                                if (employeeId.HasValue)
                                                {
                                                    MessageBox.Show($"Welcome Employee Redirecting to Employee Page...", "Login Successful");
                                                    mainForm.adminView(role, employeeId.Value); // Pass employee details
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Employee ID not found for this user.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                break;

                                            case "Admin":
                                                MessageBox.Show("Welcome Admin! Redirecting to Admin Page...", "Login Successful");
                                                mainForm.adminView(role, employeeId.Value);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs eArgs)
        {
            mainForm.ShowGuestRegPage();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs eArgs)
        {
            mainForm.ShowAdminRegPage();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.PasswordChar = '\0'; // Removes password masking
            }
            else
            {
                textBox1.PasswordChar = '*'; // Masks password
            }
        }
    }
}
