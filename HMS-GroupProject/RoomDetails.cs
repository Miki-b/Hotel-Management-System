using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class RoomDetails : UserControl
    {
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";
        //RoomDetails uc=new RoomDetails();
        public RoomDetails()
        {
            InitializeComponent();
            LoadRoomTable();
            //LoadRoomDetailsForEdit(int roomId);
            pnlEditRoom.Visible = false; // Hide edit panel initially
        }
        private void StyleUI()
        {
           panel1.BackColor = Color.FromArgb(240, 240, 240);

            // Style labels
            foreach (Control control in panel1.Controls)
            {
                if (control is Label label)
                {
                    label.Font = new Font("Segoe UI", 10);
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
        private void RoomDetails_Load(object sender, EventArgs e)
        {
        }

        private void LoadRoomTable()
        {
            string query = "SELECT Room_ID,  Room_Type, Room_Capacity, Price_Per_Night, Status FROM Room";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable roomTable = new DataTable();
                adapter.Fill(roomTable);

                dataGridView1.DataSource = roomTable;
                FormatRoomTable();
            }
        }

        private void FormatRoomTable()
        {
            dataGridView1.Columns["Room_ID"].Visible = false;

            // Adding an Actions column
            DataGridViewButtonColumn actionColumn = new DataGridViewButtonColumn();
            actionColumn.Name = "Actions";
            actionColumn.Text = "View / Edit";
            actionColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(actionColumn);

            // Style the grid
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Actions"].Index && e.RowIndex >= 0)
            {
                int roomId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Room_ID"].Value);
                LoadRoomDetailsForEdit(roomId);
            }
        }

        private void LoadRoomDetailsForEdit(int roomId)
        {
            string roomQuery = "SELECT * FROM Room WHERE Room_ID = @Room_ID";
            string imageQuery = "SELECT Image_Path FROM Room_Image WHERE Room_ID = @Room_ID";
            string amenityQuery = "SELECT Amenity_ID FROM RoomAmenities WHERE Room_ID = @Room_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Fetch Room Details
                using (SqlCommand cmd = new SqlCommand(roomQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Room_ID", roomId);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtRoomId.Text = reader["Room_ID"].ToString();
                        label13.Text= reader["Room_ID"].ToString();
                        //txtRoomNumber.Text = reader["Room_Number"].ToString();
                        txtRoomType.Text = reader["Room_Type"].ToString();
                        label12.Text = reader["Room_Type"].ToString();
                        txtCapacity.Text = reader["Room_Capacity"].ToString();
                        label10.Text = reader["Room_Capacity"].ToString();
                        txtPrice.Text = reader["Price_Per_Night"].ToString();
                        label17.Text = reader["Price_Per_Night"].ToString();
                        cmbStatus.SelectedItem = reader["Status"].ToString();
                        label11.Text = reader["Status"].ToString();
                        label9.Text = reader["Total_Rooms"].ToString();
                        label15.Text = reader["Available_Rooms"].ToString();
                    }
                    connection.Close();
                }

                // Fetch Room Image
                using (SqlCommand cmd = new SqlCommand(imageQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Room_ID", roomId);
                    connection.Open();
                    string imagePath = cmd.ExecuteScalar()?.ToString();
                    Console.WriteLine("Yes");
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                    connection.Close();
                }

                // Fetch Amenities
                using (SqlCommand cmd = new SqlCommand(amenityQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Room_ID", roomId);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int amenityId = reader.GetInt32(0);
                        foreach (CheckBox checkBox in chkListAmenities.Controls.OfType<CheckBox>())
                        {
                            if ((int)checkBox.Tag == amenityId)
                            {
                                checkBox.Checked = true;
                            }
                        }
                    }
                    connection.Close();
                }

                pnlEditRoom.Visible = true;
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string roomQuery = "UPDATE Room SET  Room_Type = @Room_Type, Room_Capacity = @Room_Capacity, Price_Per_Night = @Price_Per_Night, Status = @Status WHERE Room_ID = @Room_ID";
                string imageQuery = "UPDATE Room_Image SET Image_Path = @Image_Path WHERE Room_ID = @Room_ID";
                string deleteAmenitiesQuery = "DELETE FROM RoomAmenities WHERE Room_ID = @Room_ID";
                string insertAmenityQuery = "INSERT INTO RoomAmenities (Room_ID, Amenity_ID) VALUES (@Room_ID, @Amenity_ID)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Update Room Details
                    using (SqlCommand cmd = new SqlCommand(roomQuery, connection))
                    {
                        //cmd.Parameters.AddWithValue("@Room_Number", txtRoomNumber.Text);
                        cmd.Parameters.AddWithValue("@Room_Type", txtRoomType.Text);
                        cmd.Parameters.AddWithValue("@Room_Capacity", txtCapacity.Text);
                        cmd.Parameters.AddWithValue("@Price_Per_Night", txtPrice.Text);
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Room_ID", txtRoomId.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // Update Room Image
                    if (pictureBox1.Image != null)
                    {
                        string imagePath = SaveRoomImage(txtRoomId.Text);
                        using (SqlCommand cmd = new SqlCommand(imageQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@Image_Path", imagePath);
                            cmd.Parameters.AddWithValue("@Room_ID", txtRoomId.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update Amenities
                    using (SqlCommand cmd = new SqlCommand(deleteAmenitiesQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@Room_ID", txtRoomId.Text);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (CheckBox checkBox in chkListAmenities.Controls.OfType<CheckBox>())
                    {
                        if (checkBox.Checked)
                        {
                            using (SqlCommand cmd = new SqlCommand(insertAmenityQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@Room_ID", txtRoomId.Text);
                                cmd.Parameters.AddWithValue("@Amenity_ID", (int)checkBox.Tag);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    connection.Close();
                }

                MessageBox.Show("Room details updated successfully.");
                pnlEditRoom.Visible = false;
                LoadRoomTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private string SaveRoomImage(string roomId)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No image selected.");
                return string.Empty;
            }

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


        private void InitializeComponent()
        {
            this.pnlEditRoom = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRoomId = new System.Windows.Forms.TextBox();
            this.txtRoomType = new System.Windows.Forms.TextBox();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.chkListAmenities = new System.Windows.Forms.CheckedListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlEditRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEditRoom
            // 
            this.pnlEditRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEditRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEditRoom.Controls.Add(this.button1);
            this.pnlEditRoom.Controls.Add(this.label21);
            this.pnlEditRoom.Controls.Add(this.label20);
            this.pnlEditRoom.Controls.Add(this.label19);
            this.pnlEditRoom.Controls.Add(this.label18);
            this.pnlEditRoom.Controls.Add(this.label14);
            this.pnlEditRoom.Controls.Add(this.txtRoomId);
            this.pnlEditRoom.Controls.Add(this.txtRoomType);
            this.pnlEditRoom.Controls.Add(this.txtCapacity);
            this.pnlEditRoom.Controls.Add(this.txtPrice);
            this.pnlEditRoom.Controls.Add(this.cmbStatus);
            this.pnlEditRoom.Controls.Add(this.btnSaveChanges);
            this.pnlEditRoom.Controls.Add(this.chkListAmenities);
            this.pnlEditRoom.Location = new System.Drawing.Point(467, 20);
            this.pnlEditRoom.Name = "pnlEditRoom";
            this.pnlEditRoom.Size = new System.Drawing.Size(252, 550);
            this.pnlEditRoom.TabIndex = 1;
            this.pnlEditRoom.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlEditRoom_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Change Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(14, 173);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(78, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Price Per Night";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 118);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 22;
            this.label20.Text = "Room Capacity";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(14, 214);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 13);
            this.label19.TabIndex = 20;
            this.label19.Text = "Room Status";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "Room Type";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "Room ID";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // txtRoomId
            // 
            this.txtRoomId.Location = new System.Drawing.Point(16, 34);
            this.txtRoomId.Name = "txtRoomId";
            this.txtRoomId.ReadOnly = true;
            this.txtRoomId.Size = new System.Drawing.Size(212, 20);
            this.txtRoomId.TabIndex = 0;
            this.txtRoomId.TextChanged += new System.EventHandler(this.txtRoomId_TextChanged);
            // 
            // txtRoomType
            // 
            this.txtRoomType.Location = new System.Drawing.Point(16, 88);
            this.txtRoomType.Name = "txtRoomType";
            this.txtRoomType.Size = new System.Drawing.Size(212, 20);
            this.txtRoomType.TabIndex = 2;
            this.txtRoomType.TextChanged += new System.EventHandler(this.txtRoomType_TextChanged);
            // 
            // txtCapacity
            // 
            this.txtCapacity.Location = new System.Drawing.Point(16, 135);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(212, 20);
            this.txtCapacity.TabIndex = 3;
            this.txtCapacity.TextChanged += new System.EventHandler(this.txtCapacity_TextChanged);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(16, 189);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(212, 20);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            // 
            // cmbStatus
            // 
            this.cmbStatus.Items.AddRange(new object[] {
            "Available",
            "Occupied",
            "Under Maintenance"});
            this.cmbStatus.Location = new System.Drawing.Point(16, 239);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(212, 21);
            this.cmbStatus.TabIndex = 5;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(16, 311);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(214, 30);
            this.btnSaveChanges.TabIndex = 6;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // chkListAmenities
            // 
            this.chkListAmenities.CheckOnClick = true;
            this.chkListAmenities.FormattingEnabled = true;
            this.chkListAmenities.Items.AddRange(new object[] {
            "Wi-Fi",
            "Air Conditioning",
            "TV",
            "Mini Bar"});
            this.chkListAmenities.Location = new System.Drawing.Point(33, 390);
            this.chkListAmenities.Name = "chkListAmenities";
            this.chkListAmenities.Size = new System.Drawing.Size(214, 94);
            this.chkListAmenities.TabIndex = 8;
            this.chkListAmenities.SelectedIndexChanged += new System.EventHandler(this.chkListAmenities_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Location = new System.Drawing.Point(3, 258);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(458, 230);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Room Image";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(20, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(193, 184);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Room Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Room ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Room Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Room Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Total Rooms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(265, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Room Capacity";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(265, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Price Per Night";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(371, 178);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Room Details";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(371, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Room Details";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(371, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Room Details";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(371, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Room Details";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(371, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Room Details";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(371, 226);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "Price Per Night";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(265, 226);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Available Rooms";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(371, 202);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Price Per Night";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(721, 389);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(13, 101);
            this.panel1.TabIndex = 20;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Snow;
            this.button2.Location = new System.Drawing.Point(3, 510);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 30);
            this.button2.TabIndex = 21;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RoomDetails
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pnlEditRoom);
            this.Name = "RoomDetails";
            this.Size = new System.Drawing.Size(735, 600);
            this.Load += new System.EventHandler(this.RoomDetails_Load_1);
            this.pnlEditRoom.ResumeLayout(false);
            this.pnlEditRoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Panel pnlAmenities;

        private void txtRoomNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void chkListAmenities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pnlEditRoom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtRoomId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtRoomType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCapacity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RoomDetails_Load_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadRoomTable();
        }
    }
}
