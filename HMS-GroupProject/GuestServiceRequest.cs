using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class GuestServiceRequest : UserControl
    {
        string connectionString;
        private Label lblTitle;
        private ComboBox cmbServiceType;
        private ListBox lstCategories;
        private TextBox txtDetails;
        private CheckBox chkUrgency;
        private Button btnSubmit;
        private Panel scrollablePanel;
        private Panel containerPanel;
        int GuestID;

        public GuestServiceRequest(string connectionString, int guestID)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            CreateUI();
            GuestID = guestID;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string selectedService = cmbServiceType.SelectedItem.ToString();
            string selectedCategory = lstCategories.SelectedItem?.ToString();
            string details = txtDetails.Text;
            bool isUrgent = chkUrgency.Checked;

            if (string.IsNullOrEmpty(selectedCategory))
            {
                MessageBox.Show("Please select a category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(details))
            {
                MessageBox.Show("Please provide details about your request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           // int guestId = 1; // Replace with actual guest ID in your application

            try
            {
                UploadServiceRequest(GuestID, selectedService, selectedCategory, details, isUrgent);
                MessageBox.Show("Your request has been submitted successfully.", "Request Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Request Submission Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UploadServiceRequest(int guestId, string serviceType, string category, string requestDetails, bool isUrgent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UploadGuestServiceRequest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Guest_ID", guestId);
                    command.Parameters.AddWithValue("@Service_Type", serviceType);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Request_Details", requestDetails);
                    command.Parameters.AddWithValue("@Urgency", isUrgent);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateUI()
        {
            // Set UserControl Background Color
            this.BackColor = Color.FromArgb(220, 220, 220); // Light Grey background

            // Scrollable Panel
            scrollablePanel = new Panel
            {
                AutoScroll = true, // Enable scrolling
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            this.Controls.Add(scrollablePanel);

            // Container Panel (White Box)
            containerPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(400, 700), // Set width and increased height for the container
                Padding = new Padding(20),
                Location = new Point((scrollablePanel.Width - 400) / 2, 0)
            };
            scrollablePanel.Controls.Add(containerPanel);

            // Title Label
            lblTitle = new Label
            {
                Text = "🛎️ Request Services",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 60
            };
            containerPanel.Controls.Add(lblTitle);

            // Service Type Label and ComboBox
            var lblServiceType = new Label
            {
                Text = "🔧 Service Type:",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                ForeColor = Color.SteelBlue,
                Location = new Point(25, 80),
                AutoSize = true
            };
            containerPanel.Controls.Add(lblServiceType);

            cmbServiceType = new ComboBox
            {
                Font = new Font("Segoe UI", 12F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 300,
                Location = new Point(25, 110)
            };
            cmbServiceType.Items.AddRange(new[] { "Room Service", "Maintenance" });
            cmbServiceType.SelectedIndex = 0; // Default selection
            cmbServiceType.SelectedIndexChanged += CmbServiceType_SelectedIndexChanged;
            containerPanel.Controls.Add(cmbServiceType);

            // Categories Label and ListBox
            var lblCategories = new Label
            {
                Text = "📋 Categories:",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                ForeColor = Color.SteelBlue,
                Location = new Point(25, 160),
                AutoSize = true
            };
            containerPanel.Controls.Add(lblCategories);

            lstCategories = new ListBox
            {
                Font = new Font("Segoe UI", 12F),
                Width = 300,
                Height = 100,
                Location = new Point(25, 190)
            };
            LoadCategories("Room Service");
            containerPanel.Controls.Add(lstCategories);

            // Details Label and TextBox
            var lblDetails = new Label
            {
                Text = "✍️ Request Details:",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                ForeColor = Color.SteelBlue,
                Location = new Point(25, 300),
                AutoSize = true
            };
            containerPanel.Controls.Add(lblDetails);

            txtDetails = new TextBox
            {
                Font = new Font("Segoe UI", 12F),
                Multiline = true,
                Width = 300,
                Height = 80,
                Location = new Point(25, 330),
                Text = "Add details about your request..."
            };
            containerPanel.Controls.Add(txtDetails);

            // Urgency CheckBox
            chkUrgency = new CheckBox
            {
                Text = "🚨 Mark as Urgent",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.SteelBlue,
                Location = new Point(25, 420)
            };
            containerPanel.Controls.Add(chkUrgency);

            // Submit Button
            btnSubmit = new Button
            {
                Text = "Submit Request",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Width = 300,
                Height = 40,
                Location = new Point(25, 460),
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;
            containerPanel.Controls.Add(btnSubmit);

            // Adjust container panel width and center dynamically
            this.SizeChanged += (s, e) =>
            {
                containerPanel.Location = new Point((scrollablePanel.Width - containerPanel.Width) / 2, 10);
            };
        }

        private void LoadCategories(string serviceType)
        {
            lstCategories.Items.Clear();
            if (serviceType == "Room Service")
            {
                lstCategories.Items.Add("Food");
                lstCategories.Items.Add("Drinks");
                lstCategories.Items.Add("Towels");
                lstCategories.Items.Add("Toiletries");
            }
            else if (serviceType == "Maintenance")
            {
                lstCategories.Items.Add("Plumbing");
                lstCategories.Items.Add("Electricity");
                lstCategories.Items.Add("Cleaning");
            }
        }

        private void CmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedService = cmbServiceType.SelectedItem.ToString();
            LoadCategories(selectedService);
        }
    }
}
