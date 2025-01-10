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

namespace HMS_GroupProject.user_control
{
    public partial class HomeUserControl : UserControl
    {
        private string connectionString;
        public HomeUserControl(string conn)
        {
            InitializeComponent(); // Ensure controls are initialized first
            connectionString = conn; // Set the connection string
            UpdateDashboardData(); // Then update the dashboard
            LoadFilteredBookings(dataGridView1); // Finally load filtered bookings
        }

        public void LoadFilteredBookings(DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Define the command to execute the stored procedure
                    SqlCommand cmd = new SqlCommand("GetAllBookingsWithInvoices", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Open connection
                    conn.Open();

                    // Execute the stored procedure and fill a DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Create a new filtered DataTable for the desired columns
                    DataTable filteredTable = dataTable.DefaultView.ToTable(false,
                        "Room_Type", "Check_in_Date", "Check_out_Date", "Total_amount");

                    // Add a calculated column for duration
                    filteredTable.Columns.Add("Duration", typeof(int));
                    foreach (DataRow row in filteredTable.Rows)
                    {
                        DateTime checkIn = Convert.ToDateTime(row["Check_in_Date"]);
                        DateTime checkOut = Convert.ToDateTime(row["Check_out_Date"]);
                        row["Duration"] = (checkOut - checkIn).Days;
                    }

                    // Bind the filtered table to the DataGridView
                    dataGridView.DataSource = filteredTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bookings: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void UpdateDashboardData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string queryNewBooking = "SELECT COUNT(*) FROM Booking WHERE Room_Status = 'New'";
                    SqlCommand cmdNewBooking = new SqlCommand(queryNewBooking, connection);
                    int newBookingCount = (int)cmdNewBooking.ExecuteScalar();
                    label11.Text = newBookingCount.ToString();

                    // Available Rooms
                    string queryAvailableRooms = "SELECT COUNT(*) AS AvailableRoomCount\r\nFROM Room\r\nWHERE Status = 'Available';\r\n";
                    SqlCommand cmdAvailableRooms = new SqlCommand(queryAvailableRooms, connection);
                    int availableRoomsCount = (int)cmdAvailableRooms.ExecuteScalar();
                    label12.Text = availableRoomsCount.ToString();

                    // Check-In Today
                    //string queryCheckIn = "SELECT COUNT(*) FROM Booking WHERE CAST(Check_In_Date AS DATE) = CAST(GETDATE() AS DATE)";
                    //SqlCommand cmdCheckIn = new SqlCommand(queryCheckIn, connection);
                    //int checkInCount = (int)cmdCheckIn.ExecuteScalar();
                    //lblCheckIn.Text = checkInCount.ToString();

                    // Check-Out Today
                    //string queryCheckOut = "SELECT COUNT(*) FROM Booking WHERE CAST(Check_Out_Date AS DATE) = CAST(GETDATE() AS DATE)";
                    //SqlCommand cmdCheckOut = new SqlCommand(queryCheckOut, connection);
                    //int checkOutCount = (int)cmdCheckOut.ExecuteScalar();
                    //lblCheckOut.Text = checkOutCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating dashboard data: " + ex.Message);
            }
        }

    }
}
