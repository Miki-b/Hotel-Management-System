using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using BCrypt.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace HMS_GroupProject.user_control
{
    public partial class GuestRegUserCo : UserControl
    {
        private string connectionString; // Update with your actual connection string
        public GuestRegUserCo(string conn) {
            InitializeComponent();
            StyleGuestReg();
            connectionString = conn;
           
            


        }
        private void StyleGuestReg()
        {
            this.BackColor = Color.FromArgb(0x1A, 0x21, 0x37); // Darker gray background

            // Style Labels
            StyleLabel(label1);
            StyleLabel(label3);
          

            // Style TextBoxes
            StyleTextBox(textBox1);
            StyleTextBox(textBox2);
           

            // Style Buttons
            StyleButton(button1);
            StyleBackButton(button2);

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
        private void StyleBackButton(Button button)
        {
            button.Text = "←";
            button.Font = new Font("Segoe UI", 15, FontStyle.Bold); // Adjust font for a better look
            button.TextAlign = ContentAlignment.MiddleCenter;


        }

        private void StylePictureBox(PictureBox pictureBox)
        {
            pictureBox.BorderStyle = BorderStyle.None; // Remove any borders
        }
        private void button1_Click(object sender, EventArgs e)
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

                    // Insert new Guest into Guest table
                    string insertGuestQuery = "INSERT INTO Guest (Name, Contact, Email, Address) OUTPUT INSERTED.Guest_ID VALUES (@Name, @Contact, @Email, @Address)";
                    SqlCommand guestCmd = new SqlCommand(insertGuestQuery, con);
                    guestCmd.Parameters.AddWithValue("@Name", "name");
                    guestCmd.Parameters.AddWithValue("@Contact", "Contact");
                    guestCmd.Parameters.AddWithValue("@Email", "Email");
                    guestCmd.Parameters.AddWithValue("@Address", "address");

                    int newGuestId = (int)guestCmd.ExecuteScalar(); // Retrieve the new Guest_ID

                    // Hash the password before storing it
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    // Insert into Login table with new Guest_ID
                    string insertLoginQuery = "INSERT INTO Login (Username, Password, Role, Guest_ID, Employee_ID) VALUES (@Username, @Password, 'Guest', @GuestID, NULL)";
                    SqlCommand cmd = new SqlCommand(insertLoginQuery, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@GuestID", newGuestId); // Link to the new Guest

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Guest account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.ParentForm;
            mainForm.GoBack();
        }

        private void GuestRegUserCo_Load(object sender, EventArgs e)
        {

        }
    }
}
