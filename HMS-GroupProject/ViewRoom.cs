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

namespace HMS_GroupProject
{
    partial class ViewRoom : System.Windows.Forms.UserControl


    {
        private string connectionString;

        public ViewRoom(string connString)
        {
            InitializeComponent();
            this.connectionString = connString;
            LoadRoomData(); // Load data when the form is initialized
        }

        public void LoadRoomData()
        {
            try
            {
                string query = "SELECT Room_ID, Room_Type, Status, FO_Status, Room_Number, Price_Per_Night, Room_Capacity FROM [Hotel_Managment].[dbo].[Room]";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table; // Bind the data to the DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading room data: " + ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of RoomUserControl
            RoomUserControl roomUserControl = new RoomUserControl();

            // Get the parent container (e.g., Panel) where ViewRoom is hosted
            var parentContainer = this.Parent;
            if (parentContainer != null)
            {
                parentContainer.Controls.Clear(); // Remove ViewRoom from the container
                roomUserControl.Dock = DockStyle.Fill; // Make RoomUserControl fill the parent container
                parentContainer.Controls.Add(roomUserControl); // Add RoomUserControl to the container
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Room_ID"].Value); // Replace "ID" with your primary key column name

                // Confirmation dialog
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Delete query example
                    string query = "DELETE FROM Room WHERE Room_ID = @Room_ID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("Room_ID", id);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    // Refresh the DataGridView
                    LoadRoomData();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

