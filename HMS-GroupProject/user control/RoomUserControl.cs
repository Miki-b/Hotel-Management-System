using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_GroupProject.user_control
{
    public partial class RoomUserControl : UserControl
    {
        private string connectionString = "Data Source=DESKTOP-2RI98PE\\SQLEXPRESS;Initial Catalog=Hotel_Managment;Integrated Security=True;Encrypt=False";

        public RoomUserControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Collect Room Details from the Form
                string roomType = type.SelectedItem.ToString();  // Room Type ComboBox
                string roomStatus = RoomStatus.SelectedItem.ToString();  // Room Status ComboBox
                string foStatus = FOStatus.SelectedItem.ToString();  // FO Status ComboBox
                string roomNumber = RoomNumber.Text;  // Room Number TextBox
                decimal roomPrice = Convert.ToDecimal(price.Text);  // Room Price TextBox
                int roomCapacity = Convert.ToInt32(capacity.Text);  // Room Capacity TextBox

                // Insert Room Details into Room Table
                int roomId = InsertRoomDetails(roomType, roomStatus, foStatus, roomNumber, roomPrice, roomCapacity);

                // Collect Selected Amenities from Checkboxes
                List<int> selectedAmenities = new List<int>();
                if (checkBox1.Checked) selectedAmenities.Add(GetAmenityId("Shower"));
                if (checkBox2.Checked) selectedAmenities.Add(GetAmenityId("Internet"));
                if (checkBox3.Checked) selectedAmenities.Add(GetAmenityId("Luggage"));
                if (checkBox4.Checked) selectedAmenities.Add(GetAmenityId("Air Conditioner"));
                if (checkBox5.Checked) selectedAmenities.Add(GetAmenityId("Sun View"));
                if (checkBox6.Checked) selectedAmenities.Add(GetAmenityId("TV Cable"));
                if (checkBox7.Checked) selectedAmenities.Add(GetAmenityId("Refrigerator"));
                if (checkBox8.Checked) selectedAmenities.Add(GetAmenityId("Concierge"));

                // Insert Amenities into RoomAmenities Table
                foreach (int amenityId in selectedAmenities)
                {
                    InsertRoomAmenity(roomId, amenityId);
                }

                MessageBox.Show("Room and amenities added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Method to Insert Room Details and Return Room_ID
        private int InsertRoomDetails(string roomType, string roomStatus, string foStatus, string roomNumber, decimal roomPrice, int roomCapacity)
        {
            string query = @"INSERT INTO Room (Room_Type, Status, FO_Status, Room_Number, Price_Per_Night, Room_Capacity)
                     OUTPUT INSERTED.Room_ID
                     VALUES (@Room_Type, @Status, @FO_Status, @Room_Number, @Price_Per_Night, @Room_Capacity)";
            using (SqlConnection connection = new SqlConnection(connectionString)) // Define the SqlConnection
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Room_Type", roomType);
                    cmd.Parameters.AddWithValue("@Status", roomStatus);
                    cmd.Parameters.AddWithValue("@FO_Status", foStatus);
                    cmd.Parameters.AddWithValue("@Room_Number", roomNumber);
                    cmd.Parameters.AddWithValue("@Price_Per_Night", roomPrice);
                    cmd.Parameters.AddWithValue("@Room_Capacity", roomCapacity);

                    connection.Open();
                    int roomId = (int)cmd.ExecuteScalar();  // Get the generated Room_ID
                    return roomId;
                }
            }
        }

        // Method to Get Amenity ID by Name
        private int GetAmenityId(string amenityName)
        {
            string query = "SELECT Amenity_ID FROM Amenities WHERE Amenity_Name = @Amenity_Name";
            using (SqlConnection connection = new SqlConnection(connectionString)) // Define the SqlConnection
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Amenity_Name", amenityName);
                    connection.Open();
                    int amenityId = (int)cmd.ExecuteScalar();
                    return amenityId;
                }
            }
        }

        // Method to Insert Room Amenity into RoomAmenities Table
        private void InsertRoomAmenity(int roomId, int amenityId)
        {
            string query = "INSERT INTO RoomAmenities (Room_ID, Amenity_ID) VALUES (@Room_ID, @Amenity_ID)";
            using (SqlConnection connection = new SqlConnection(connectionString)) // Define the SqlConnection
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Room_ID", roomId);
                    cmd.Parameters.AddWithValue("@Amenity_ID", amenityId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
