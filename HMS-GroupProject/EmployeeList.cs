using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class EmployeeList : UserControl
    {
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;"; // Replace with actual connection string
        private DataGridView dataGridViewEmployees;
        private TextBox textBoxSearch;
        private Button buttonSearch;

        public EmployeeList()
        {
            InitializeComponent();
            InitializeCustomControls();
            LoadEmployeeData();
        }

        private void InitializeCustomControls()
        {
            // Form Styling
            this.BackColor = Color.FromArgb(245, 245, 245); // Light gray background

            // Label Title
            Label labelTitle = new Label
            {
                Text = "Employee List",
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Search Box
            // Search Box
            textBoxSearch = new TextBox
            {
                Text = "Search by name...",
                ForeColor = Color.Gray,
                Location = new Point(20, 60),
                Size = new Size(300, 30),
                Font = new Font("Arial", 12)
            };

            // Add event handlers to mimic placeholder behavior
            textBoxSearch.GotFocus += (sender, e) =>
            {
                if (textBoxSearch.Text == "Search by name...")
                {
                    textBoxSearch.Text = "";
                    textBoxSearch.ForeColor = Color.Black;
                }
            };

            textBoxSearch.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    textBoxSearch.Text = "Search by name...";
                    textBoxSearch.ForeColor = Color.Gray;
                }
            };


            buttonSearch = new Button
            {
                Text = "Search",
                Location = new Point(330, 60),
                Size = new Size(100, 30),
                BackColor = Color.DarkSlateBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            buttonSearch.Click += ButtonSearch_Click;

            // DataGridView for Employees
            dataGridViewEmployees = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(600, 300),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.DarkSlateBlue,
                    ForeColor = Color.White,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Arial", 11),
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.LightSlateGray,
                    SelectionForeColor = Color.White
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightGray
                },
                AllowUserToAddRows = false,
                AllowUserToResizeRows = false
            };

            // Add Controls to Form
            this.Controls.Add(labelTitle);
            this.Controls.Add(textBoxSearch);
            this.Controls.Add(buttonSearch);
            this.Controls.Add(dataGridViewEmployees);
        }

        private void LoadEmployeeData(string searchQuery = "")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Employee_ID, Name, Contact, Salary, Hotel_ID, Role_ID FROM Employee";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE Name LIKE @Search";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridViewEmployees.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            LoadEmployeeData(textBoxSearch.Text.Trim());
        }

        private void EmployeeList_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }
    }
}
