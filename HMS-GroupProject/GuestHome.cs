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
using System.IO;

namespace HMS_GroupProject
{
    public partial class GuestHome : UserControl
    {
        private string connectionString;
        string roomId;
        string Role;
        int GuestID;

        public GuestHome(string conn,string Role,int GuestID)
        {
            this.Role=Role;
            this.GuestID=GuestID;
            connectionString = conn;
            InitializeComponent();
        }

        private void GuestHome_Load(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            string query = @"
        SELECT r.Room_ID, r.Room_Type, r.Status, r.Price_Per_Night, ri.Image_Path 
        FROM Room r
        LEFT JOIN Room_Image ri ON r.Room_ID = ri.Room_ID"; // Join Room with Room_Image

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Fetch image path from database
                    string imagePath = reader["Image_Path"] != DBNull.Value ? reader["Image_Path"].ToString() : @"C:\Users\Dell\OneDrive\Documenti\Business\Efoy_rooms_logo.png";

                    // Create a PictureBox for the room image
                    PictureBox pictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Width = 220,
                        Height = 140,
                        Margin = new Padding(5)
                    };

                    try
                    {
                        if (File.Exists(imagePath))
                        {
                            pictureBox.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            pictureBox.Image = Image.FromFile(@"C:\Users\Dell\OneDrive\Documenti\Business\Efoy_rooms_logo.png"); // Fallback image
                        }
                    }
                    catch
                    {
                        pictureBox.Image = Image.FromFile(@"C:\Users\Dell\OneDrive\Documenti\Business\Efoy_rooms_logo.png"); // Handle errors gracefully
                    }

                    // Create a Label for the room type
                    Label roomLabel = new Label
                    {
                        Text = $"{reader["Room_Type"]}",
                        AutoSize = true,
                        Font = new Font("Arial", 12, FontStyle.Bold),
                        ForeColor = Color.Black
                    };

                    // Create a Label for the room status
                    Label statusLabel = new Label
                    {
                        Text = reader["Status"].ToString() == "Available" ? "Available" : "Booked",
                        AutoSize = true,
                        Font = new Font("Arial", 10, FontStyle.Regular),
                        ForeColor = reader["Status"].ToString() == "Available" ? Color.Green : Color.Red
                    };

                    // Create a Label for the price
                    Label priceLabel = new Label
                    {
                        Text = $"Price: ${reader["Price_Per_Night"]}",
                        AutoSize = true,
                        Font = new Font("Arial", 10, FontStyle.Italic),
                        ForeColor = Color.Gray
                    };

                    // Create a Button for booking the room
                    Button bookButton = new Button
                    {
                        Text = reader["Status"].ToString() == "Available" ? "Book Now" : "Booked",
                        Width = 200,
                        Height = 30,
                        Enabled = reader["Status"].ToString() == "Available",
                        BackColor = reader["Status"].ToString() == "Available" ? Color.SteelBlue : Color.Gray,
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand
                    };

                    string roomId = reader["Room_ID"].ToString();
                    string roomType = reader["Room_Type"].ToString();

                    // Add hover effects
                    bookButton.MouseEnter += (s, e) => bookButton.BackColor = Color.DarkBlue;
                    bookButton.MouseLeave += (s, e) => bookButton.BackColor = Color.SteelBlue;

                    bookButton.Tag = new Tuple<string, string>(roomId, roomType);
                    bookButton.Click += BookButton_Click;

                    // Create a Panel for the room
                    Panel roomPanel = new Panel
                    {
                        Width = 240,
                        Height = 320,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(10),
                        Margin = new Padding(10)
                    };

                    roomPanel.Controls.Add(pictureBox);
                    roomPanel.Controls.Add(roomLabel);
                    roomPanel.Controls.Add(statusLabel);
                    roomPanel.Controls.Add(priceLabel);
                    roomPanel.Controls.Add(bookButton);

                    // Adjust controls' positions
                    roomLabel.Top = pictureBox.Bottom + 5;
                    statusLabel.Top = roomLabel.Bottom + 5;
                    priceLabel.Top = statusLabel.Bottom + 5;
                    bookButton.Top = priceLabel.Bottom + 10;

                    // Add the room panel to the FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(roomPanel);
                }

                reader.Close();
            }
        }


        private void BookButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag is Tuple<string, string> roomData)
            {
                this.roomId = roomData.Item1;
                string roomType = roomData.Item2;

                // Call the booking function
                BookingListUserControl book = new BookingListUserControl(connectionString, roomId,this.GuestID,this.Role);
                book.RoomId = roomId;
                book.RoomType = roomType;
                ShowBookingPage();
            }
        }

        private void ShowBookingPage()
        {
            if (this.Parent is TabPage tabPage)
            {
                tabPage.Controls.Clear();

                BookingListUserControl bookingPage = new BookingListUserControl(connectionString, roomId, this.GuestID, this.Role)
                {
                    Dock = DockStyle.Fill
                };

                tabPage.Controls.Add(bookingPage);
            }
            else
            {
                MessageBox.Show("Error: GuestHome is not inside a TabPage!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
