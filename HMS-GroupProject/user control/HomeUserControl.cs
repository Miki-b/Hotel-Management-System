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
using System.Data.SqlClient;

namespace HMS_GroupProject.user_control
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
        }
        

private string connectionString = "Data Source=DESKTOP-2RI98PE\\SQLEXPRESS;Initial Catalog=Hotel_Managment;Integrated Security=True;Encrypt=False;";

    // Method to Update Dashboard Data
    private void UpdateDashboardData()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // New Booking
                //string queryNewBooking = "SELECT COUNT(*) FROM Booking WHERE Status = 'New'";
                //SqlCommand cmdNewBooking = new SqlCommand(queryNewBooking, connection);
                //int newBookingCount = (int)cmdNewBooking.ExecuteScalar();
                //lblNewBooking.Text = newBookingCount.ToString();

                // Available Rooms
                string queryAvailableRooms = "SELECT COUNT(*) FROM[Hotel_Managment].[dbo].[Room] where FO_Status='Vacant'";
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

    private void HomeUserControl_Load(object sender, EventArgs e)
        {
            UpdateDashboardData();
        }
    }
}
