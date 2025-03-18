using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace HMS_GroupProject
{
    public partial class EmployeeReg : UserControl
    {
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";

        public EmployeeReg()
        {
            InitializeComponent();
            InitializeCustomControls();
        }

        private void InitializeCustomControls()
        {
            // Form Styling
            this.BackColor = Color.FromArgb(245, 245, 245); // Light Gray Background

            // GroupBox for form container
            GroupBox groupBox = new GroupBox
            {
                Text = "Employee Registration",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                Size = new Size(500, 400),
                Location = new Point(50, 20)
            };

            // Labels & TextBoxes
            Label labelName = CreateLabel("Name:", new Point(20, 40));
            TextBox textBoxName = CreateTextBox(new Point(150, 40));

            Label labelContact = CreateLabel("Contact:", new Point(20, 80));
            TextBox textBoxContact = CreateTextBox(new Point(150, 80));

            Label labelSalary = CreateLabel("Salary:", new Point(20, 120));
            TextBox textBoxSalary = CreateTextBox(new Point(150, 120));

            Label labelUsername = CreateLabel("Username:", new Point(20, 160));
            TextBox textBoxUsername = CreateTextBox(new Point(150, 160));

            Label labelPassword = CreateLabel("Password:", new Point(20, 200));
            TextBox textBoxPassword = CreateTextBox(new Point(150, 200));
            textBoxPassword.PasswordChar = '*'; // Mask Password

            Label labelRole = CreateLabel("Role:", new Point(20, 240));
            ComboBox comboBoxRole = new ComboBox
            {
                Location = new Point(150, 240),
                Size = new Size(200, 30),
                Font = new Font("Arial", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxRole.Items.AddRange(new string[] { "Manager", "Receptionist", "Housekeeping", "Chef", "Security" });

            // Register Button
            Button btnRegister = new Button
            {
                Text = "Register Employee",
                Location = new Point(150, 290),
                Size = new Size(200, 40),
                BackColor = Color.DarkSlateBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            btnRegister.Click += (sender, e) => RegisterEmployee(textBoxName.Text, textBoxContact.Text, textBoxSalary.Text, textBoxUsername.Text, textBoxPassword.Text, comboBoxRole.SelectedItem?.ToString());

            // Add Controls to GroupBox
            groupBox.Controls.Add(labelName);
            groupBox.Controls.Add(textBoxName);
            groupBox.Controls.Add(labelContact);
            groupBox.Controls.Add(textBoxContact);
            groupBox.Controls.Add(labelSalary);
            groupBox.Controls.Add(textBoxSalary);
            groupBox.Controls.Add(labelUsername);
            groupBox.Controls.Add(textBoxUsername);
            groupBox.Controls.Add(labelPassword);
            groupBox.Controls.Add(textBoxPassword);
            groupBox.Controls.Add(labelRole);
            groupBox.Controls.Add(comboBoxRole);
            groupBox.Controls.Add(btnRegister);

            // Add GroupBox to Form
            this.Controls.Add(groupBox);
        }

        private Label CreateLabel(string text, Point location)
        {
            return new Label
            {
                Text = text,
                Location = location,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true
            };
        }

        private TextBox CreateTextBox(Point location)
        {
            return new TextBox
            {
                Location = location,
                Size = new Size(200, 30),
                Font = new Font("Arial", 12)
            };
        }

        private void RegisterEmployee(string name, string contact, string salaryText, string username, string password, string role)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(salaryText) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (username.Length < 5)
            {
                MessageBox.Show("Username must be at least 5 characters long!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long, contain an uppercase letter, a digit, and a special character.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidPhoneNumber(contact))
                MessageBox.Show("Error: Invalid phone number. It must be at least 10 digits.");

            if (!decimal.TryParse(salaryText, out decimal salary))
            {
                MessageBox.Show("Invalid salary format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int roleId = GetRoleId(role);
            int hotelId = 1; // Change if needed

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string checkUserQuery = "SELECT COUNT(*) FROM Login WHERE Username = @Username";
                    using (SqlCommand checkCmd = new SqlCommand(checkUserQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", username);
                        int userExists = (int)checkCmd.ExecuteScalar();
                        if (userExists > 0)
                        {
                            MessageBox.Show("Username is already taken!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string insertEmployeeQuery = "INSERT INTO Employee (Name, Contact, Salary, Hotel_ID, Role_ID) OUTPUT INSERTED.Employee_ID VALUES (@Name, @Contact, @Salary, @HotelID, @RoleID)";
                    int employeeId;
                    using (SqlCommand empCmd = new SqlCommand(insertEmployeeQuery, con))
                    {
                        empCmd.Parameters.AddWithValue("@Name", name);
                        empCmd.Parameters.AddWithValue("@Contact", contact);
                        empCmd.Parameters.AddWithValue("@Salary", salary);
                        empCmd.Parameters.AddWithValue("@HotelID", hotelId);
                        empCmd.Parameters.AddWithValue("@RoleID", roleId);

                        employeeId = (int)empCmd.ExecuteScalar();
                    }

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                    string insertLoginQuery = "INSERT INTO Login (Username, Password, Role, Guest_ID, Employee_ID) VALUES (@Username, @Password, 'Employee', NULL, @EmployeeID)";
                    using (SqlCommand loginCmd = new SqlCommand(insertLoginQuery, con))
                    {
                        loginCmd.Parameters.AddWithValue("@Username", username);
                        loginCmd.Parameters.AddWithValue("@Password", hashedPassword);
                        loginCmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                        loginCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Employee registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool IsValidPhoneNumber(string phone)
        {
            // Regex for a phone number that starts with a 0 followed by 10 or more digits
            return Regex.IsMatch(phone, @"^0\d{9,}$");
        }
        private bool IsValidPassword(string password)
        {
            return password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsDigit) && password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private int GetRoleId(string role)
        {
            switch (role)
            {
                case "Manager": return 1;
                case "Receptionist": return 2;
                case "Housekeeping": return 3;
                case "Chef": return 4;
                case "Security": return 5;
                default: return -1;
            }
        }
    }
}
