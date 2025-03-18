using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class GuestList : UserControl
    {
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";

        public GuestList()
        {
            InitializeComponent();
            LoadGuestList();
            panel1.Visible = false; // Hide the details panel initially
            panel1.BackColor = Color.WhiteSmoke;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Padding = new Padding(15);
        }

        private void LoadGuestList()
        {
            string query = "SELECT g.Guest_ID, g.Name, g.Contact, g.Email, g.Address, r.Check_in_Date, r.Check_out_Date, r.Total_Amount, r.Room_ID, r.Reservation_ID FROM Guest g JOIN Reservations r ON g.Guest_ID = r.Guest_ID;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable guestTable = new DataTable();
                adapter.Fill(guestTable);
                dataGridView1.DataSource = guestTable;
                CustomizeDataGridView();
            }
        }

        private void CustomizeDataGridView()
        {
            if (dataGridView1.Columns.Count == 0) return;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            // Hide specific columns
            dataGridView1.Columns["Guest_ID"].Visible = false;
            dataGridView1.Columns["Contact"].Visible = false;
            dataGridView1.Columns["Email"].Visible = false;
            dataGridView1.Columns["Address"].Visible = false;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int guestId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value); // 0 is the index of Guest_ID

                LoadGuestDetails(guestId);
            }
        }

        private void LoadGuestDetails(int guestId)
        {
            string reservationQuery = "SELECT g.Name, g.Contact, g.Email, g.Address, r.Check_in_Date, r.Check_out_Date, r.Total_Amount, r.Room_ID FROM Guest g JOIN Reservations r ON g.Guest_ID = r.Guest_ID WHERE g.Guest_ID = @Guest_ID;";
            string serviceQuery = "SELECT Service_Type, Category, Request_Details, Urgency, Request_Date FROM GuestServiceRequests WHERE Guest_ID = @Guest_ID";

            panel1.Controls.Clear();
            panel1.Visible = true;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                GroupBox guestInfoGroup = new GroupBox
                {
                    Text = "Guest Information",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Location = new Point(10, 10),
                    Size = new Size(380, 180),
                    BackColor = Color.White
                };

                int yOffset = 25;
                using (SqlCommand cmd = new SqlCommand(reservationQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Guest_ID", guestId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string[] labels = { "Name", "Contact", "Email", "Address", "Check-in", "Check-out", "Total Amount", "Room ID" };
                        string[] values = {
                            reader["Name"].ToString(),
                            reader["Contact"].ToString(),
                            reader["Email"].ToString(),
                            reader["Address"].ToString(),
                            Convert.ToDateTime(reader["Check_in_Date"]).ToShortDateString(),
                            Convert.ToDateTime(reader["Check_out_Date"]).ToShortDateString(),
                            "$" + reader["Total_Amount"].ToString(),
                            reader["Room_ID"].ToString()
                        };

                        for (int i = 0; i < labels.Length; i++)
                        {
                            Label lbl = new Label
                            {
                                Text = $"{labels[i]}: {values[i]}",
                                Font = new Font("Segoe UI", 10),
                                Location = new Point(10, yOffset),
                                AutoSize = true
                            };
                            guestInfoGroup.Controls.Add(lbl);
                            yOffset += 22;
                        }
                    }
                    reader.Close();
                }

                panel1.Controls.Add(guestInfoGroup);

                GroupBox serviceGroup = new GroupBox
                {
                    Text = "Service Requests",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Location = new Point(10, guestInfoGroup.Bottom + 20),
                    Size = new Size(380, 220),
                    BackColor = Color.White
                };

                yOffset = 25;
                using (SqlCommand cmd = new SqlCommand(serviceQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Guest_ID", guestId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Label noServiceLbl = new Label
                        {
                            Text = "No service requests found.",
                            Font = new Font("Segoe UI", 10, FontStyle.Italic),
                            ForeColor = Color.Gray,
                            Location = new Point(10, yOffset),
                            AutoSize = true
                        };
                        serviceGroup.Controls.Add(noServiceLbl);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Label lblService = new Label
                            {
                                Text = $"🛠 {reader["Service_Type"]} - {reader["Category"]}\n🔍 {reader["Request_Details"]}\n⚡ Urgency: {reader["Urgency"]}\n📅 {Convert.ToDateTime(reader["Request_Date"]).ToShortDateString()}",
                                Location = new Point(10, yOffset),
                                AutoSize = true,
                                Font = new Font("Segoe UI", 9),
                                ForeColor = Color.DarkBlue
                            };
                            serviceGroup.Controls.Add(lblService);
                            yOffset += lblService.Height + 10;
                        }
                    }
                    reader.Close();
                }

                panel1.Controls.Add(serviceGroup);
                connection.Close();
            }
        }
    }
}
