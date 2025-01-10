using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_GroupProject.user_control
{
    public partial class BookingListView : UserControl
    {
        private string connectionString;

        public BookingListView(string Connect)
        {
            connectionString = Connect;
            InitializeComponent();
            LoadFilteredBookings(dataGridView2);
            dataGridView2.CellClick += DataGridView2_CellClick; // Attach the event handler
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

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the click is not on the header
            {
                try
                {
                    // Clear the ListBox before populating new details
                    listBoxDetails.Items.Clear();

                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                    // Extract the basic details
                    string roomType = selectedRow.Cells["Room_Type"].Value.ToString();
                    string checkInDate = Convert.ToDateTime(selectedRow.Cells["Check_in_Date"].Value).ToString("yyyy-MM-dd");
                    string checkOutDate = Convert.ToDateTime(selectedRow.Cells["Check_out_Date"].Value).ToString("yyyy-MM-dd");
                    string totalAmount = selectedRow.Cells["Total_amount"].Value.ToString();
                    string duration = selectedRow.Cells["Duration"].Value.ToString();

                    // Fetch additional details (Guest Profile, Tax, Discount)
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("GetAllBookingsWithInvoices", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Open connection
                        conn.Open();

                        // Execute the command
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable fullTable = new DataTable();
                        adapter.Fill(fullTable);

                        // Find the corresponding row in the full table
                        DataRow[] rows = fullTable.Select($"Room_Type = '{roomType}' AND Check_in_Date = '{checkInDate}'");

                        if (rows.Length > 0)
                        {
                            DataRow detailsRow = rows[0];

                            // Extract detailed data
                            string guestName = detailsRow["Guest_Name"].ToString();
                            string guestContact = detailsRow["Guest_Contact"].ToString();
                            string guestEmail = detailsRow["Guest_Email"].ToString();
                            string tax = detailsRow["Tax"].ToString();
                            string discount = detailsRow["Discount"].ToString();

                            // Add the details to the ListBox
                            listBoxDetails.Items.Add($"Room Type: {roomType}");
                            listBoxDetails.Items.Add($"Check-in Date: {checkInDate}");
                            listBoxDetails.Items.Add($"Check-out Date: {checkOutDate}");
                            listBoxDetails.Items.Add($"Total Amount: {totalAmount}");
                            listBoxDetails.Items.Add($"Duration: {duration} days");
                            listBoxDetails.Items.Add("");
                            listBoxDetails.Items.Add($"Guest Name: {guestName}");
                            listBoxDetails.Items.Add($"Contact: {guestContact}");
                            listBoxDetails.Items.Add($"Email: {guestEmail}");
                            listBoxDetails.Items.Add($"Tax: {tax}");
                            listBoxDetails.Items.Add($"Discount: {discount}");
                        }
                        else
                        {
                            listBoxDetails.Items.Add("Details not found for the selected booking.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching details: " + ex.Message);
                }
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void GuestList_Load(object sender, EventArgs e)
        {

        }

        private void Details_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
