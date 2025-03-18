using HMS_GroupProject.user_control;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Security;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class adminView : UserControl
    {
        string Role;
        int EmpID;
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";
        public GuestUserControl ucguest = new GuestUserControl();
        public BookingListUserControl ucbooking;
        public HomeUserControl uchome;
        public RoomUserControl ucroom = new RoomUserControl();
        public RoomView ucroomView=new RoomView();
        public GuestList ucguestList=new GuestList();
        public BookingListUserControl ucBookRooms;
        public BookingListView ucbookingListView;
        public GuestBookingList guestBookingList;
        public EmployeeReg employeeReg=new EmployeeReg();
        public EmployeeList employeeList=new EmployeeList();
        public adminView(Form1 main, string role, int empID)
        {
            this.EmpID = empID;
            this.Role = role;
            ucBookRooms = new BookingListUserControl(connectionString, "", this.EmpID, this.Role);
            guestBookingList = new GuestBookingList(connectionString, Role, EmpID);
            ucbookingListView =new BookingListView(connectionString,0,this.Role);
            InitializeComponent();
            uchome = new HomeUserControl(connectionString,this.EmpID,this.Role);
            panelMain.Controls.Clear();
            panelMain.Controls.Add(uchome);
            uchome.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(0xD3, 0xD3, 0xD3); // Light Gray Background
            if (this.Role != "Admin")
            {
                UpdateButtonVisibility(this.EmpID);
            }
            else if (this.Role == "Admin")
            {
                
                button5.Visible = false;
                
            }
            // Apply styles
            StyleButton(homeButton);
            StyleButton(button1);
            StyleButton(button2);
            StyleButton(button3);
            StyleButton(button5);
            StyleButton(button6);
            StyleButton(button7);

            StyleLabel(HMS);
            StyleLabel(UserName);
            //StyleSearchBox(textBox1); // Style the search box

            StylePanel(panel1);
            StylePanel(panel2);
            StylePanel(panelMain);
            panelMain.BackColor = Color.White; 
            panel1.BackColor = Color.White;// White background for the main container
        }
        private void UpdateButtonVisibility(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT Role_ID FROM Employee WHERE Employee_ID = @EmployeeID";
                

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    object roleResult = cmd.ExecuteScalar();
                   
                    if (roleResult != null)
                    {
                        int roleId = Convert.ToInt32(roleResult);

                        // Role_ID 1 = Admin, Role_ID 2 = Manager
                        bool isManagerOrAdmin = (roleId == 1 );

                        homeButton.Visible = isManagerOrAdmin;
                        button6.Visible = isManagerOrAdmin;
                        button7.Visible = isManagerOrAdmin;
                        if (roleId == 1 || Role=="Admin")
                        {
                            button5.Visible = false;
                        }


                    }

                    
                    
                }
            }
        }

        private void StylePanel(Panel panel)
        {
            panel.BorderStyle = BorderStyle.None;
            panel.BackColor = Color.LightGray; // White Panels
            panel.Padding = new Padding(20); // Adds spacing inside the panels
        }

        private void StyleSearchBox(TextBox textBox)
        {
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.Black;
            textBox.Font = new Font("Segoe UI", 12);
            textBox.BorderStyle = BorderStyle.FixedSingle;

            // Placeholder text effect
            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == "Search...")
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "Search...";
                    textBox.ForeColor = Color.Gray;
                }
            };

            textBox.Text = "Search...";
        }

        private void StyleButton(Button button)
        {
            button.BackColor = Color.SteelBlue;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Arial", 12, FontStyle.Bold);
            button.Height = 40;
            button.Width = 150;
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.MouseOverBackColor = Color.DarkBlue;
            button.FlatAppearance.MouseDownBackColor = Color.MidnightBlue;
            //button.Region = new Region(new System.Drawing.Drawing2D.GraphicsPath());

            //button.Paint += (s, e) =>
            //{
            //    System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            //    gp.AddEllipse(2, 2, button.Width - 4, button.Height - 4);
            //    button.Region = new Region(gp);
            //};
            //button.BackColor = Color.FromArgb(0x03, 0x40, 0x78); // Lightest blue for background
            //button.ForeColor = Color.White; // White text
            //button.FlatStyle = FlatStyle.Flat; // Flat style
            //button.FlatAppearance.BorderSize = 0; // Remove border
            //button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0x00, 0x1F, 0x54); // Middle blue on hover
        }

        private void StyleLabel(Label label)
        {
            label.ForeColor = Color.Black; // Black text for contrast
            label.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            uchome = new HomeUserControl(connectionString, this.EmpID, this.Role);
            panelMain.Controls.Clear();
            panelMain.Controls.Add(uchome);
            uchome.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
          panelMain.Controls.Add(ucroomView);
            ucroomView.Dock = DockStyle.Fill;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(ucguestList);
            ucguestList.Dock = DockStyle.Fill;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(guestBookingList);
            guestBookingList.Dock = DockStyle.Fill;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roomUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(ucBookRooms);
            ucBookRooms.Dock = DockStyle.Fill;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(employeeReg);
            employeeReg.Dock = DockStyle.Fill;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(employeeList);
            employeeList.Dock = DockStyle.Fill;
        }
    }
}
