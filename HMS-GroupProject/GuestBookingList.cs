using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class GuestBookingList : UserControl
    {
        string connectionString;
        string Role;
        int GuestID;
        public GuestBookingList(string conn,string Role,int GuestID)
        {
            connectionString = conn;
            this.GuestID = GuestID; 
            this.Role = Role;
            InitializeComponent();
            //CustomizeDataGridView();
            LoadBookingHistory();
            Console.WriteLine("Role");
        }
        private void LoadBookingHistory()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define the base query
                string query = @"
    SELECT res.Reservation_ID, r.Room_Type, res.Total_Amount, res.Check_in_Date, res.Check_out_Date 
    FROM Reservations res 
    JOIN Room r ON res.Room_ID = r.Room_ID 
    JOIN Invoice inv ON res.Reservation_ID = inv.Reservation_ID";

                // If the user is a Guest, filter by Guest_ID
                if (Role == "Guest")
                {
                    query += " WHERE res.Guest_ID = @GuestID";
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    if (Role == "Guest")
                    {
                        cmd.Parameters.AddWithValue("@GuestID", GuestID);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable; // 🔹 Now the columns exist

                        // Add "Cancel" button column if not already added
                        if (!dataGridView1.Columns.Contains("Cancel"))
                        {
                            DataGridViewButtonColumn cancelButton = new DataGridViewButtonColumn
                            {
                                HeaderText = "Action",
                                Text = "Cancel",
                                UseColumnTextForButtonValue = true,
                                Name = "Cancel"
                            };
                            dataGridView1.Columns.Add(cancelButton);
                        }
                    }
                }
            }


            CustomizeDataGridView(); // 🔹 Now call it AFTER data is loaded
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Cancel"].Index && e.RowIndex >= 0)
            {
                // 🔹 Make sure "Reservation_ID" exists in DataGridView
                if (dataGridView1.Rows[e.RowIndex].Cells["Reservation_ID"] != null)
                {
                    int reservationId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Reservation_ID"].Value);
                    CancelBooking(reservationId);
                }
                else
                {
                    MessageBox.Show("Error: Reservation ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void CancelBooking(int reservationId)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel this booking?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CancelBooking", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReservationID", reservationId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Booking cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookingHistory(); // Refresh the data grid
            }
        }
        private void GuestBookingList_Load(object sender, EventArgs e)
        {

        }
        private void CustomizeDataGridView()
        {
            if (dataGridView1.Columns.Count == 0) return;

            // Set column headers
            dataGridView1.Columns[0].HeaderText = "Room Type";
            dataGridView1.Columns[1].HeaderText = "Total Amount";
            dataGridView1.Columns[2].HeaderText = "Check-In Date";
            dataGridView1.Columns[3].HeaderText = "Check-Out Date";

            if (dataGridView1.Columns.Contains("Cancel"))
            {
                dataGridView1.Columns["Cancel"].Width = 100;
            }

            // Header styling
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Row styling
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SteelBlue; // Highlight selected row
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Alternating row color (Zebra Striping)
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;

            // Remove row headers
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;

            // Remove default grid lines
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.GridColor = Color.LightGray;

            // Adjust column width
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Beautify 'Cancel' button column
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewButtonCell buttonCell = row.Cells["Cancel"] as DataGridViewButtonCell;
                if (buttonCell != null)
                {
                    buttonCell.Style.BackColor = Color.Red;
                    buttonCell.Style.ForeColor = Color.White;
                    buttonCell.Style.Font = new Font("Arial", 9, FontStyle.Bold);
                }
            }

            // Border styling for a modern look
            dataGridView1.BorderStyle = BorderStyle.None;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadBookingHistory();
        }
    }
}
