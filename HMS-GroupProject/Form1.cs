using HMS_GroupProject.user_control;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class Form1 : Form
    {

        //private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";
        public GuestUserControl ucguest = new GuestUserControl();
        //public BookingListUserControl ucbooking = new BookingListUserControl();
        public HomeUserControl uchome;
        public AdminReg ucadminReg;
        public RoomUserControl ucroom = new RoomUserControl();
       // public BookingListView gl = new BookingListView();
        //public Services srvc = new Services();
        public GuestRegUserCo gReg;
        //public RoomViewUserCo rview = new RoomViewUserCo();
        private SqlConnection connection;
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";
        private Stack<UserControl> userControlStack = new Stack<UserControl>();

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            InitializeComponent();



            ShowLoginPage();
            //ShowUserControl(new GuestHome(connectionString));

            //ShowUserControl(new GuestView());
            //ShowUserControl(new GuestList());
            //ShowUserControl(new GuestBookingList(connectionString));
            //ShowUserControl(new GuestServiceRequest(connectionString));

            //ShowUserControl(new RoomDetails());

            //// Disable buttons initially
            ////ToggleButtons(false);

            //// Load the login page
            //LoginPage loginPage = new LoginPage(this);
            ////adminView view1 = new adminView();
            //this.Controls.Add(loginPage);
            //loginPage.Visible = true;
            //loginPage.Dock = DockStyle.Fill;
           // adminView();


        }

        private void ShowUserControl(UserControl newControl)
        {
            if (Controls.Count > 0)
            {
                UserControl currentControl = (UserControl)Controls[0];
                userControlStack.Push(currentControl);  // Push current control to stack
                Controls.Remove(currentControl);
            }

            newControl.Dock = DockStyle.Fill;
            Controls.Add(newControl);
        }

        public void ShowLoginPage()
        {
            ShowUserControl(new LoginPage(this));
        }

        public void ShowGuestRegPage()
        {
            ShowUserControl(new GuestRegUserCo(connectionString));
        }
        public void ShowAdminRegPage()
        {
            ShowUserControl(new AdminReg(connectionString));
        }

        public void GoBack()
        {
            if (userControlStack.Count > 0)
            {
                UserControl previousControl = userControlStack.Pop();
                Controls.Clear();
                Controls.Add(previousControl);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initially, show the login page - already handled in the constructor
        }
        private void InitializeDatabase()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                //MessageBox.Show("Connected to Database!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Failed: " + ex.Message);
            }
        }

        public void adminView(string Role, int EmpID)
        {
            this.Controls.Clear();
            var admin = new adminView(this, Role, EmpID);
            this.Controls.Add(admin);
            admin.Dock = DockStyle.Fill;
        }
        public void GuestView(string Role,int GuestID)
        {
            
            this.Controls.Clear();
            var guest = new GuestView(Role,GuestID);
            this.Controls.Add(guest);
            guest.Dock = DockStyle.Fill;
        }
        public void DontHaveAccount()
        {
            this.Controls.Clear();
            gReg = new GuestRegUserCo(connectionString);
            //var guestt = new GuestUserControl();
            this.Controls.Add(gReg);
            gReg.Dock = DockStyle.Fill;
        }
        //public void ToggleButtons(bool enable)
        //{
        //    homeButton.Enabled = enable;
        //    button1.Enabled = enable;
        //    button2.Enabled = enable;
        //    button3.Enabled = enable;
        //}

        //private void homeButton_Click(object sender, EventArgs e)
        //{
        //    panelMain.Controls.Clear();
        //    panelMain.Controls.Add(uchome);
        //    uchome.Dock = DockStyle.Fill;
        //}

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    panelMain.Controls.Clear();
        //    panelMain.Controls.Add(ucroom);
        //    ucroom.Dock = DockStyle.Fill;
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    panelMain.Controls.Clear();
        //    panelMain.Controls.Add(ucguest);
        //    ucguest.Dock = DockStyle.Fill;
        //}
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    panelMain.Controls.Clear();
        //    panelMain.Controls.Add(ucbooking);
        //    ucbooking.Dock = DockStyle.Fill;
        //}
    }
}
