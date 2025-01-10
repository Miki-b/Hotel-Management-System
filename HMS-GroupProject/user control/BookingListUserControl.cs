using HMS_GroupProject.user_control;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HMS_GroupProject
{
    public partial class BookingListUserControl : UserControl
    {
        private string connectionString;
        public BookingListUserControl(string ConnectionString)
        {
            InitializeComponent();
            connectionString = ConnectionString;
        }
        public string AddBooking(
    DateTime checkInDate,
    DateTime checkOutDate,
    int roomId,
    int guestId,
    decimal tax,
    decimal discount  )  // Add pricePerNight parameter
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AddBooking", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to match the stored procedure
                        command.Parameters.AddWithValue("@Check_in_Date", checkInDate);
                        command.Parameters.AddWithValue("@Check_out_Date", checkOutDate);
                        command.Parameters.AddWithValue("@Room_ID", roomId);
                        command.Parameters.AddWithValue("@Guest_ID", guestId);
                        command.Parameters.AddWithValue("@Tax", tax);
                        command.Parameters.AddWithValue("@Discount", discount);

                        // Execute the procedure
                        command.ExecuteNonQuery();
                    }

                }

                return "Booking successfully added!";
            }
            catch (SqlException ex)
            {
                // Log the error and return the message
                return $"SQL Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Log general errors
                return $"Error: {ex.Message}";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void BookingListUserControl_Load(object sender, EventArgs e)
        {

        }
        public (int RoomId, decimal PricePerNight) roomAvailabilityChecker(string roomType)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // SQL query to check room availability and retrieve the price
                    string query = @"
                SELECT Room_ID, Price_Per_Night, Status
                FROM Room
                WHERE Room_Type = @RoomType AND Status = 'Available'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameter for room type
                        command.Parameters.AddWithValue("@RoomType", roomType);

                        // Execute the query and get the result
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())  // If a room is found
                            {
                                // Retrieve the Room_ID and Price_Per_Night
                                int roomId = reader.GetInt32(0);
                                decimal pricePerNight = reader.GetDecimal(1);

                                // Return both Room_ID and Price_Per_Night
                                return (roomId, pricePerNight);
                            }
                            else
                            {
                                // Return -1 for Room_ID if no available room is found
                                return (-1, 0);  // You can adjust this based on your error handling strategy
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the SQL error and return an error message
                return (-1, 0);  // Adjust this based on your error handling strategy
            }
            catch (Exception ex)
            {
                // Log general errors
                return (-1, 0);  // Adjust this based on your error handling strategy
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the Room_ID from the roomAvailabilityChecker method
            var roomDetails = roomAvailabilityChecker(textBox3.Text);

            if (roomDetails.RoomId == -1)
            {
                MessageBox.Show("No available room found for the specified room type.");
                return;
            }

            // Convert tax and discount to decimal and handle any exceptions if the conversion fails
            decimal tax = 0;
            decimal discount = 0;

            // Try parsing the tax value from textBox15
            if (!decimal.TryParse(textBox15.Text, out tax))
            {
                MessageBox.Show("Invalid tax value.");
                return;
            }

            // Try parsing the discount value from textBox18
            if (!decimal.TryParse(textBox18.Text, out discount))
            {
                MessageBox.Show("Invalid discount value.");
                return;
            }

            // Call AddBooking without the pricePerNight parameter since it is handled in the procedure
            string result = AddBooking(
                dateTimePicker1.Value,  // Check-In Date
                dateTimePicker2.Value,  // Check-Out Date
                roomDetails.RoomId,     // Room ID
                1,                      // Guest ID (replace with actual guest ID if needed)
                tax,                    // Tax value
                discount                // Discount value
            );

            // Show the result of AddBooking
            MessageBox.Show(result);  // Display the result from AddBooking
        }



        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminView adminView = new adminView();
            adminView.BOOKINGLIST();
        }
    }
}
