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

namespace HMS_GroupProject
{
    public partial class AdminReg : UserControl
    {
        private string connectionString;
        public AdminReg(string conn)
        {
            InitializeComponent();
            
            connectionString = conn;
        }

        private void AdminReg_Load(object sender, EventArgs e)
        {

        }
        

            // Password validation method
            private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasDigit && hasSpecialChar;
        }

        
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim(); // Username input
            string password = textBox2.Text.Trim(); // Password input

            // Validate input fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and Password are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (username.Length < 5)
            {
                MessageBox.Show("Username must be at least 5 characters long!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (username == password)
            {
                MessageBox.Show("Username and Password cannot be the same!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long, contain an uppercase letter, a digit, and a special character.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Check if username already exists
                    string checkUsernameQuery = "SELECT COUNT(*) FROM Login WHERE Username = @Username";
                    SqlCommand checkCmd = new SqlCommand(checkUsernameQuery, con);
                    checkCmd.Parameters.AddWithValue("@Username", username);
                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        MessageBox.Show("Username is already taken. Please choose a different one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Insert into Employee table (Name is required, others are optional)
                    string insertEmployeeQuery = @"
            INSERT INTO Employee (Name, Contact, Salary, Hotel_ID, Role_ID) 
            OUTPUT INSERTED.Employee_ID 
            VALUES (@Name, @Contact, @Salary, @HotelID, @RoleID)";

                    SqlCommand empCmd = new SqlCommand(insertEmployeeQuery, con);
                    empCmd.Parameters.AddWithValue("@Name", "New Manager");  // Required field
                    empCmd.Parameters.AddWithValue("@Contact", DBNull.Value);  // Optional
                    empCmd.Parameters.AddWithValue("@Salary", DBNull.Value);  // Optional
                    empCmd.Parameters.AddWithValue("@HotelID", DBNull.Value);  // Optional
                    empCmd.Parameters.AddWithValue("@RoleID", 2);  // 2 = Manager (from Role table)
                    int employeeId = (int)empCmd.ExecuteScalar();
                    Console.WriteLine(employeeId);

                    // Insert role for the new Employee
                    //string insertRoleQuery = "INSERT INTO Role (Role_ID, Role_Name) VALUES (2, 'Manager')";
                    //SqlCommand roleCmd = new SqlCommand(insertRoleQuery, con);
                    ////roleCmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    //roleCmd.ExecuteNonQuery();

                    // Hash the password before storing it
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    // Insert into Login table
                    string insertLoginQuery = "INSERT INTO Login (Username, Password, Role, Guest_ID, Employee_ID) VALUES (@Username, @Password, 'Admin', NULL, @EmployeeID)";
                    SqlCommand cmd = new SqlCommand(insertLoginQuery, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Manager account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear textboxes after registration
                    textBox1.Clear();
                    textBox2.Clear();
                    Form1 mainForm = (Form1)this.ParentForm;
                    mainForm.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            

        }
    }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.ParentForm;
            mainForm.GoBack();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0'; // Removes password masking
            }
            else
            {
                textBox2.PasswordChar = '*'; // Masks password
            }
        }
    }
}
