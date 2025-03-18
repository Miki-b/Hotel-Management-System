using Bunifu.UI.WinForms.Helpers.Transitions;
using HMS_GroupProject.user_control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HMS_GroupProject
{
    public partial class BookingListUserControl : UserControl
    {
        private string connectionString;
        public string RoomId;
        public string RoomType;
        decimal pricePerNight;
        decimal tax;
        decimal discount;
        int GuestID;
        string Role;

        public BookingListUserControl(string ConnectionString,string RoomId,int GuestID,string Role)
        {
            this.RoomId = RoomId;
            this.GuestID = GuestID;
            this.Role = Role;
            InitializeComponent();
            Console.WriteLine($"room id: {RoomId}");
            connectionString = ConnectionString;
            if (!string.IsNullOrEmpty(this.RoomId) && int.TryParse(this.RoomId, out int roomId)) {
                LoadRoomsIntoComboBox(roomId); 
            }
            else
            {
                LoadRoomsIntoComboBox(1);
            }
            label7.Text = "15%";
            label8.Text = "0";
        }
        public void setBooking(string id, string type)
        {
            this.RoomId = id;
            this.RoomType = type;

            if (int.TryParse(this.RoomId, out int roomId))
            {
                LoadRoomsIntoComboBox(roomId);
            }
            else
            {
                LoadRoomsIntoComboBox(1); // Default room
            }
        }


        public string AddBooking(
    DateTime checkInDate,
    DateTime checkOutDate,
    int roomId,
    string roomType,
    string guestName,
    string guestEmail,
    string guestAddress,
    string guestContact,
    decimal tax,
    decimal discount)
    {
            decimal totalAmount;
        try
        {
            // 🛑 Input Validation
            if (string.IsNullOrWhiteSpace(guestName))
                return "Error: Guest name is required.";

            if (string.IsNullOrWhiteSpace(guestEmail) || !IsValidEmail(guestEmail))
                return "Error: Invalid email format.";

            if (string.IsNullOrWhiteSpace(guestContact) || !IsValidPhoneNumber(guestContact))
                return "Error: Invalid phone number. It must be at least 10 digits.";

            if (string.IsNullOrWhiteSpace(guestAddress))
                return "Error: Guest address is required.";

            if (checkOutDate <= checkInDate)
                return "Error: Check-out date must be later than check-in date.";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // ✅ Update existing guest details
                    using (SqlCommand checkEmailCmd = new SqlCommand("SELECT Guest_ID FROM Guest WHERE Email = @Email", connection))
                    {
                        checkEmailCmd.Parameters.AddWithValue("@Email", guestEmail);
                        object existingGuestId = checkEmailCmd.ExecuteScalar(); // Check if email exists

                        if (existingGuestId != null) // If email exists, use the existing Guest_ID
                        {
                            this.GuestID = Convert.ToInt32(existingGuestId);
                        }
                        else
                        {
                            if (Role == "Guest")
                            {
                                // Update existing guest info if Role is "Guest"
                                using (SqlCommand updateGuestCmd = new SqlCommand(
                                    "UPDATE Guest SET Name = @Name, Contact = @Contact, Email = @Email, Address = @Address WHERE Guest_ID = @GuestID", connection))
                                {
                                    updateGuestCmd.Parameters.AddWithValue("@GuestID", this.GuestID);
                                    updateGuestCmd.Parameters.AddWithValue("@Name", guestName);
                                    updateGuestCmd.Parameters.AddWithValue("@Contact", guestContact);
                                    updateGuestCmd.Parameters.AddWithValue("@Email", guestEmail);
                                    updateGuestCmd.Parameters.AddWithValue("@Address", guestAddress);

                                    int rowsAffected = updateGuestCmd.ExecuteNonQuery();
                                    if (rowsAffected == 0)
                                    {
                                        Console.WriteLine("Error: Guest does not exist.");
                                        //return;
                                    }
                                }
                            }
                            else
                            {
                                // Insert new guest if Role is not "Guest"
                                using (SqlCommand insertGuestCmd = new SqlCommand(
                                    "INSERT INTO Guest (Name, Contact, Email, Address) OUTPUT INSERTED.Guest_ID VALUES (@Name, @Contact, @Email, @Address)", connection))
                                {
                                    insertGuestCmd.Parameters.AddWithValue("@Name", guestName);
                                    insertGuestCmd.Parameters.AddWithValue("@Contact", guestContact);
                                    insertGuestCmd.Parameters.AddWithValue("@Email", guestEmail);
                                    insertGuestCmd.Parameters.AddWithValue("@Address", guestAddress);

                                    GuestID = (int)insertGuestCmd.ExecuteScalar();
                                }
                            }
                        }
                    }

                    

                        SqlTransaction transaction = connection.BeginTransaction();

                        try
                        {
                            int reservationId;

                            // Insert into Reservations table
                            string insertReservationQuery = @"
            INSERT INTO Reservations (Check_in_Date, Check_out_Date, Total_Amount, Room_ID, Guest_ID)
            OUTPUT INSERTED.Reservation_ID
            VALUES (@CheckInDate, @CheckOutDate, @TotalAmount, @RoomID, @GuestID)";

                            using (SqlCommand cmd = new SqlCommand(insertReservationQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CheckInDate", checkInDate);
                                cmd.Parameters.AddWithValue("@CheckOutDate", checkOutDate);
                                cmd.Parameters.AddWithValue("@RoomID", roomId);
                                cmd.Parameters.AddWithValue("@GuestID", this.GuestID);

                                // Calculate total amount (assuming you have price per night)
                                 totalAmount = (checkOutDate - checkInDate).Days * pricePerNight;
                                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                                // Execute and retrieve Reservation ID
                                reservationId = (int)cmd.ExecuteScalar();
                            }

                            // Insert into Invoice table
                            string insertInvoiceQuery = @"
            INSERT INTO Invoice (Amount, Tax, Discount, Reservation_ID)
            VALUES (@Amount, @Tax, @Discount, @ReservationID)";

                            using (SqlCommand cmd = new SqlCommand(insertInvoiceQuery, connection, transaction))
                            {
                                decimal finalAmount = totalAmount + (totalAmount * tax / 100) - discount;

                                cmd.Parameters.AddWithValue("@Amount", finalAmount);
                                cmd.Parameters.AddWithValue("@Tax", tax);
                                cmd.Parameters.AddWithValue("@Discount", discount);
                                cmd.Parameters.AddWithValue("@ReservationID", reservationId);

                                cmd.ExecuteNonQuery();
                            }

                            // Commit transaction
                            transaction.Commit();
                            Console.WriteLine($"Booking successfully added! Reservation ID: {reservationId}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    




                }


                return "Booking successfully added!";
        }
        catch (SqlException ex)
        {
            return $"SQL Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // ✅ Function to validate email format
    private bool IsValidEmail(string email)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    // ✅ Function to validate phone number (Only digits, min 10 digits)
   private bool IsValidPhoneNumber(string phone)
{
    // Regex for a phone number that starts with a 0 followed by 10 or more digits
    return Regex.IsMatch(phone, @"^0\d{9,}$");
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
        public (int RoomId, decimal PricePerNight) roomAvailabilityChecker(string roomID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Room_ID, Price_Per_Night FROM Room WHERE Room_ID = @RoomID AND Status = 'Available'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (string.IsNullOrEmpty(roomID))
                        {
                            return (-1, 0);
                        }

                        command.Parameters.AddWithValue("@RoomID", roomID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int roomId = reader.GetInt32(0);
                                pricePerNight = reader.GetDecimal(1);
                                return (roomId, pricePerNight);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return (-1, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            KeyValuePair<int, string> selectedRoom = (KeyValuePair<int, string>)comboBox1.SelectedItem;
            string selectedRoomID = selectedRoom.Key.ToString();
            Console.WriteLine("Yes");
            Console.WriteLine(selectedRoomID);
            Console.WriteLine("Yes");
            //string selectedRoomID = (comboBox1.SelectedItem.GetType().GetProperty(comboBox1.ValueMember).GetValue(comboBox1.SelectedItem)).ToString();

            if (string.IsNullOrEmpty(selectedRoomID))
            {
                MessageBox.Show("Please select a room type.");
                return;
            }
           // Console.WriteLine((comboBox1.SelectedItem.GetType().GetProperty(comboBox1.ValueMember).GetValue(comboBox1.SelectedItem)).ToString());
            var roomDetails = roomAvailabilityChecker(selectedRoomID);
            if (roomDetails.RoomId == -1)
            {
                MessageBox.Show("No available room found for the specified room type.");
                return;
            }
//tax and Discount
            tax =15;
            Console.WriteLine(tax);
            discount = 0;

            string result = AddBooking(
                dateTimePicker1.Value,
                dateTimePicker2.Value,
                roomDetails.RoomId,
                selectedRoomID,
                textBox7.Text,
                textBox8.Text,
                textBox9.Text,
                textBox10.Text,
                tax,
                discount
            );

            MessageBox.Show(result);
        }




        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookingListView bookingListView=new BookingListView(connectionString,this.GuestID,this.Role);
            var parentContainer = this.Parent;
            if (parentContainer != null)
            {
                parentContainer.Controls.Clear(); // Remove RoomUserControl from the container
                bookingListView.Dock = DockStyle.Fill;   // Make ViewRoom fill the parent container
                parentContainer.Controls.Add(bookingListView); // Add ViewRoom to the container
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void BookingListUserControl_Load_1(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            string query = "SELECT Price_Per_Night FROM Room WHERE Room_Type = @RoomType AND Status = 'Available'";

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Get selected RoomID safely
                string roomType = comboBox1.Text;
                Console.WriteLine(roomType);
                if (string.IsNullOrEmpty(roomType))
                {
                    MessageBox.Show("Please select a room type.");
                    
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoomType", roomType);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pricePerNight = Decimal.Parse(reader["Price_Per_Night"].ToString()) ;
                        }
                    }
                }
            }

            // Calculate Total Amount correctly
            //decimal totalAmount = pricePerNight + (pricePerNight * tax) - discount;
            //label11.Text = totalAmount.ToString("0.00"); // Display as formatted string (2 decimal places)
            //label11.Text=(CalculateTotalAmount(pricePerNight, tax, discount, dateTimePicker1.Value, dateTimePicker1.Value)).ToString();

        }
        // Function to calculate total amount based on duration
        private decimal CalculateTotalAmount(decimal pricePerNight, decimal tax, decimal discount, DateTime checkIn, DateTime checkOut)
        {
            int duration = (checkOut - checkIn).Days;
            if (duration <= 0)
            {
                Console.WriteLine(checkOut);
                Console.WriteLine(checkIn);
                MessageBox.Show("Check-out date must be after check-in date.");
                return 0;
            }

            decimal total = (pricePerNight * duration) + ((pricePerNight * duration) * tax) - discount;
            return total;
        }
        private void LoadRoomsIntoComboBox(int defaultRoomId)
        {
            string query = "SELECT Room_ID, Room_Type FROM Room where Status = 'Available'"; // Modify as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Dictionary<int, string> rooms = new Dictionary<int, string>(); // Store Room_ID and Room_Type
                int defaultIndex = -1; // To track the default room's index
                int currentIndex = 0;

                while (reader.Read())
                {
                    int roomId = Convert.ToInt32(reader["Room_ID"]);
                    string roomType = reader["Room_Type"].ToString();

                    rooms.Add(roomId, roomType);

                    // ✅ Check if this is the default room
                    if (roomId == defaultRoomId)
                    {
                        defaultIndex = currentIndex;
                    }

                    currentIndex++;
                }

                reader.Close();

                // ✅ Bind to ComboBox
                comboBox1.DataSource = new BindingSource(rooms, null);
                comboBox1.DisplayMember = "Value"; // Shows Room_Type
                comboBox1.ValueMember = "Key"; // Holds Room_ID

                // ✅ Set default selection
                if (defaultIndex != -1)
                {
                    comboBox1.SelectedIndex = defaultIndex;
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            label11.Text = (CalculateTotalAmount(pricePerNight, tax, discount, dateTimePicker1.Value, dateTimePicker2.Value)).ToString();

        }
    }
}
