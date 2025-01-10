using HMS_GroupProject.user_control;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class Form1 : Form
    {
        public GuestUserControl ucguest = new GuestUserControl();
        public BookingListUserControl ucbooking = new BookingListUserControl();
        public HomeUserControl uchome = new HomeUserControl();
        public RoomUserControl ucroom = new RoomUserControl();
        public GuestList gl = new GuestList();
        public Services srvc = new Services();
        public GuestRegUserCo gReg = new GuestRegUserCo();
        public RoomViewUserCo rview = new RoomViewUserCo();
        private SqlConnection connection;
        private string connectionString = "Data Source=DESKTOP-B392QTM\\SQLEXPRESS;Initial Catalog=Hotel_Managment;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            // Disable buttons initially
            //ToggleButtons(false);

            // Load the login page
            LoginPage loginPage = new LoginPage(this);
            //adminView view1 = new adminView();
            this.Controls.Add(loginPage);
            loginPage.Visible = true;
            loginPage.Dock = DockStyle.Fill;



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

        public void adminView()
        {
            this.Controls.Clear();
            var admin = new adminView();
            this.Controls.Add(admin);
            admin.Dock = DockStyle.Fill;
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
