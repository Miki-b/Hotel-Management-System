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
