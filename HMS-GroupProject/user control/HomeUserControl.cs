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
            StyleHomeControl();
        }

        private void StyleHomeControl()
        {
            this.BackColor = Color.FromArgb(0x1A, 0x21, 0x37); // Darker gray background (like panelMain)

            // Style Labels
            StyleLabel(label2);
            StyleLabel(label14);
            StyleLabel(label11);
            StyleLabel(label13);
            StyleLabel(label3);
            StyleLabel(label12);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label4);

            // Style DataGridView
            StyleDataGridView(dataGridView1);

            // Style MonthCalendar
            StyleMonthCalendar(monthCalendar1);
        }

        private void StyleMonthCalendar(MonthCalendar monthCalendar)
        {
            monthCalendar.BackColor = Color.FromArgb(0x28, 0x33, 0x4A);
            monthCalendar.ForeColor = Color.White;
            monthCalendar.TitleBackColor = Color.FromArgb(0x00, 0x1F, 0x54);
            monthCalendar.TitleForeColor = Color.White;
            monthCalendar.TrailingForeColor = Color.Gray;
        }

        private void StyleDataGridView(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = Color.FromArgb(0x28, 0x33, 0x4A);
            dataGridView.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0x00, 0x1F, 0x54);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(0x28, 0x33, 0x4A);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.GridColor = Color.FromArgb(0x00, 0x1F, 0x54);
        }



        private void StyleLabel(Label label)
        {
            label.ForeColor = Color.White;
            label.Font = new Font("Segoe UI", 12);
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

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
