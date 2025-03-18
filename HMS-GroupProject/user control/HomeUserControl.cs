using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HMS_GroupProject.user_control
{
    public partial class HomeUserControl : UserControl
    {
        private string connectionString;
        int GuestId;
        string Role;
        private TableLayoutPanel tableLayout;

        public HomeUserControl(string conn, int GuestId, string Role)
        {
            InitializeComponent();
            connectionString = conn;
            this.GuestId = GuestId;
            this.Role = Role;
            this.AutoScroll = true;
            //this.AutoSize = true;   
            InitializeDashboardControls();
            UpdateDashboardData();
            LoadFilteredBookings();
            LoadGuestRequests();
        }

        private void InitializeDashboardControls()
        {
            tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 8,
                AutoSize = true
            };
            
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            labelRecentBookings = CreateLabel("Recent Bookings:");
            labelAvailableRooms = CreateLabel("Available Rooms:");
            labelTotalRevenue = CreateLabel("Total Revenue:");
            labelGuestCount = CreateLabel("Total Guests:");
            labelEmployeeCount = CreateLabel("Total Employees:");
            labelGuestRequests = CreateLabel("Pending Guest Requests:");

            dataGridViewBookings = CreateDataGridView();
            dataGridViewRequests = CreateDataGridView();

            tableLayout.Controls.Add(labelRecentBookings, 0, 0);
            tableLayout.Controls.Add(labelAvailableRooms, 0, 1);
            tableLayout.Controls.Add(labelTotalRevenue, 0, 2);
            tableLayout.Controls.Add(labelGuestCount, 0, 3);
            tableLayout.Controls.Add(labelEmployeeCount, 0, 4);
            tableLayout.Controls.Add(labelGuestRequests, 0, 5);
            tableLayout.Controls.Add(dataGridViewBookings, 0, 6);
            tableLayout.Controls.Add(dataGridViewRequests, 0, 7);

            Controls.Add(tableLayout);
            tableLayout.AutoScroll = true;
            tableLayout.AutoSize = true;
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Padding = new Padding(5)
            };
        }

        private DataGridView CreateDataGridView()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };
        }

        private void UpdateDashboardData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    labelRecentBookings.Text = $"Recent Bookings: {ExecuteScalarQuery("SELECT COUNT(*) FROM Reservations WHERE Check_in_Date >= DATEADD(DAY, -7, GETDATE());", connection)}";
                    labelAvailableRooms.Text = $"Available Rooms: {ExecuteScalarQuery("SELECT COUNT(*) FROM Room WHERE Status = 'Available';", connection)}";
                    labelTotalRevenue.Text = $"Total Revenue: ${ExecuteScalarQuery("SELECT SUM(Amount) FROM Invoice;", connection)}";
                    labelGuestCount.Text = $"Total Guests: {ExecuteScalarQuery("SELECT COUNT(*) FROM Guest;", connection)}";
                    labelEmployeeCount.Text = $"Total Employees: {ExecuteScalarQuery("SELECT COUNT(*) FROM Employee;", connection)}";
                    labelGuestRequests.Text = $"Pending Guest Requests: {ExecuteScalarQuery("SELECT COUNT(*) FROM GuestServiceRequests;", connection)}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating dashboard: " + ex.Message);
            }
        }

        private void LoadFilteredBookings()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Define SQL query
                    string query = @"
        SELECT 
            r.Reservation_ID,
            r.Guest_ID,
            g.Name AS Guest_Name,
            r.Room_ID,
            rm.Room_Type,
            r.Check_in_Date,
            r.Check_out_Date,
            i.Amount
            
            
        FROM Reservations r
        INNER JOIN Guest g ON r.Guest_ID = g.Guest_ID
        INNER JOIN Room rm ON r.Room_ID = rm.Room_ID
        LEFT JOIN Invoice i ON r.Reservation_ID = i.Reservation_ID
        ORDER BY r.Check_in_Date DESC;"; // Show recent bookings first

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Guest_ID", GuestId);
                        cmd.Parameters.AddWithValue("@Role", Role);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridViewBookings.DataSource = dataTable;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bookings: " + ex.Message);
            }
        }

        private void LoadGuestRequests()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Request_ID, Guest_ID, Service_Type, Category, Request_Details, Urgency FROM GuestServiceRequests";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewRequests.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading guest requests: " + ex.Message);
            }
        }

        private int ExecuteScalarQuery(string query, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }

        private Label labelTotalRevenue;
        private Label labelGuestCount;
        private Label labelEmployeeCount;
        private Label labelGuestRequests;
        private DataGridView dataGridViewRequests;
        private DataGridView dataGridViewBookings;
        private Label labelRecentBookings;
        private Label labelAvailableRooms;
    }
}
