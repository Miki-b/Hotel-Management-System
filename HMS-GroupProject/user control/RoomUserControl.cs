using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace HMS_GroupProject.user_control
{
    public partial class RoomUserControl : UserControl
    {
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";

        public RoomUserControl()
        {
            InitializeComponent();
            StyleUI();
        }
        private void StyleUI()
        {
            panel1.BackColor = Color.FromArgb(240, 240, 240);

            // Style labels
            foreach (Control control in panel1.Controls)
            {
                if (control is Label label)
                {
                    label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    label.ForeColor = Color.FromArgb(50, 50, 50);
                }
            }

            // Style textboxes
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Font = new Font("Segoe UI", 10);
                    textBox.ForeColor = Color.Black;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                }
            }

            // Style buttons
            foreach (Control control in panel1.Controls)
            {
                if (control is Button button)
                {
                    button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    button.BackColor = Color.FromArgb(0, 120, 215);
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Cursor = Cursors.Hand;
                }
            }

            // Style CheckBoxes
            foreach (Control control in panel1.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Font = new Font("Segoe UI", 9);
                    checkBox.ForeColor = Color.FromArgb(50, 50, 50);
                }
            }

            // Style the PictureBox
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.BackColor = Color.FromArgb(200, 200, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            //panel1.BackColor = Color.FromArgb(240, 240, 240);

            // Style labels
            foreach (Control control in panel1.Controls)
            {
                if (control is Label label)
                {
                    label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    label.ForeColor = Color.FromArgb(50, 50, 50);
                }
            }

            // Style textboxes
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Font = new Font("Segoe UI", 10);
                    textBox.ForeColor = Color.Black;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                }
            }

            // Style buttons
            foreach (Control control in panel1.Controls)
            {
                if (control is Button button)
                {
                    button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    button.BackColor = Color.FromArgb(0, 120, 215);
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Cursor = Cursors.Hand;
                }
            }

            // Style CheckBoxes
            foreach (Control control in panel1.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Font = new Font("Segoe UI", 9);
                    checkBox.ForeColor = Color.FromArgb(50, 50, 50);
                }
            }

            // Style the PictureBox
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.BackColor = Color.FromArgb(200, 200, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }
            private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Collect Room Details from the Form
                string roomType = type.SelectedItem.ToString();
                //string roomStatus = RoomStatus.SelectedItem.ToString();
                decimal roomPrice = Convert.ToDecimal(price.Text);
                int roomCapacity = Convert.ToInt32(capacity.Text);
                int totalRooms = Convert.ToInt32(TotalRooms.Text);
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
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please select an image for the room.");
                    return;
                }

                // Save the image to disk
                string imagePath = SaveImageToDatabase();
                if (string.IsNullOrEmpty(imagePath))
                {
                    MessageBox.Show("Failed to save the image.");
                    return;
                }

                // Insert the room into the Room table
                int roomId = InsertRoomDetails(roomType, roomPrice, roomCapacity,totalRooms);
                if (roomId == -1)
                {
                    MessageBox.Show("Failed to insert room details into the database.");
                    return;
                }

                // Insert the image into the Room_Image table and link it with the room
                int imageId = InsertRoomImage(imagePath, roomId);
                if (imageId == -1)
                {
                    MessageBox.Show("Failed to insert image into the database.");
                    return;
                }

                // Update the Room table with the image ID
                //UpdateRoomWithImage(roomId, imageId);

                // Insert room amenities
                foreach (int amenityId in selectedAmenities)
                {
                    InsertRoomAmenities(roomId, amenityId);
                }
                // InsertRoomAmenities(roomId);

                MessageBox.Show("Room added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int InsertRoomDetails(string roomType,  decimal roomPrice, int roomCapacity, int totalRooms)
        {
            string query = "INSERT INTO Room (Room_Type, Status, Price_Per_Night,Hotel_ID, Room_Capacity,Total_Rooms,Available_Rooms) OUTPUT INSERTED.Room_ID VALUES (@Room_Type, 'Available', @Price_Per_Night,1, @Room_Capacity,@TotalRooms,@TotalRooms)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Room_Type", roomType);
                //cmd.Parameters.AddWithValue("@Status", roomStatus);
                cmd.Parameters.AddWithValue("@Price_Per_Night", roomPrice);
                cmd.Parameters.AddWithValue("@Room_Capacity", roomCapacity);
                cmd.Parameters.AddWithValue("@TotalRooms", totalRooms);
                connection.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        private int InsertRoomImage(string imagePath, int roomId)
        {
            string query = "INSERT INTO Room_Image (Image_Path, Room_ID) OUTPUT INSERTED.Image_ID VALUES (@Image_Path, @Room_ID)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Image_Path", imagePath);
                cmd.Parameters.AddWithValue("@Room_ID", roomId);
                connection.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        private void UpdateRoomWithImage(int roomId, int imageId)
        {
            string query = "UPDATE Room SET Room_Image_ID = @Image_ID WHERE Room_ID = @Room_ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Image_ID", imageId);
                cmd.Parameters.AddWithValue("@Room_ID", roomId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertRoomAmenities(int roomId,int amentityID)
        {
            string query = "INSERT INTO RoomAmenities (Room_ID, Amenity_ID) VALUES (@Room_ID, @Amenity_ID)";
            using (SqlConnection connection = new SqlConnection(connectionString)) // Define the SqlConnection
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Room_ID", roomId);
                    cmd.Parameters.AddWithValue("@Amenity_ID", amentityID);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string SaveImageToDatabase()
        {
            try
            {
                string savePath = @"C:\\HotelImages\\";
                if (!System.IO.Directory.Exists(savePath))
                    System.IO.Directory.CreateDirectory(savePath);
                string fileName = "Room_" + DateTime.Now.Ticks + ".jpg";
                string fullPath = System.IO.Path.Combine(savePath, fileName);
                pictureBox1.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return fullPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving image: " + ex.Message);
                return null;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void RoomUserControl_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

       
    }
}
