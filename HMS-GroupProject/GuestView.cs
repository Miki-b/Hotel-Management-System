using HMS_GroupProject.user_control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

namespace HMS_GroupProject
{

    public partial class GuestView : UserControl
    {
        private string connectionString = "Data Source=DESKTOP-A3UB2QO\\MSSQLSERVER2022;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False;";
        public GuestUserControl ucguest = new GuestUserControl();
        public BookingListUserControl ucbooking;
        public HomeUserControl uchome;
        public RoomUserControl ucroom = new RoomUserControl();
        public GuestHome ucGuestHome;
        public GuestBookingList ucGuestBookList;
        public GuestServiceRequest ucGuestServiceRequest;
        string Role;
        int GuestId;
        //public BookingListView view;
        public GuestView(string role,int guestId )
        {
            InitializeComponent();

            // Set TabControl properties for custom drawing
            tabControl3.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl3.DrawItem += TabControl3_DrawItem;

            if (tabControl3.TabPages["tabPageGuestHome"] == null)
            {
                TabPage newTab = new TabPage("Guest Home");
                newTab.Name = "tabPageGuestHome";
                tabControl3.TabPages.Add(newTab);
            }

            if (tabControl3.TabPages["tabPageGuestBookingList"] == null)
            {
                TabPage newTab = new TabPage("Booking List");
                newTab.Name = "tabPageGuestBookingList";
                tabControl3.TabPages.Add(newTab);
            }
            if (tabControl3.TabPages["tabPageGuestServiceRequest"] == null)
            {
                TabPage newTab = new TabPage("Service Request");
                newTab.Name = "tabPageGuestServiceRequest";
                tabControl3.TabPages.Add(newTab);
            }
            this.GuestId = guestId;
            this.Role = role;
            ucGuestHome = new GuestHome(connectionString,this.Role,this.GuestId);
            ucGuestBookList = new GuestBookingList(connectionString, this.Role, this.GuestId);
            ucGuestServiceRequest = new GuestServiceRequest(connectionString,this.GuestId);

            AddGuestHomeToTabPage();
            AddGuestBookListToTabPage();
            AddGuestServiceRequestToTabPage();
            
        }

        // Custom draw for tab headers
        private void TabControl3_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage tabPage = tabControl3.TabPages[e.Index];
            Rectangle tabRect = tabControl3.GetTabRect(e.Index);
            Graphics g = e.Graphics;

            // Define colors
            Color backColor = Color.FromArgb(0x1F, 0x4E, 0x79); // Dark blue background
            Color textColor = Color.White; // White text

            using (SolidBrush brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, tabRect);
            }

            using (Font font = new Font("Segoe UI", 10, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Far,  // Align text to the right
                    LineAlignment = StringAlignment.Center, // Center vertically
                    Trimming = StringTrimming.EllipsisCharacter
                };

                // Add padding so the text doesn't touch the edge
                tabRect.Inflate(-10, 0);

                g.DrawString(tabPage.Text, font, textBrush, tabRect, sf);
            }
        }



        private void GuestView_Load(object sender, EventArgs e)
        {
            AddGuestHomeToTabPage();
            AddGuestBookListToTabPage();
        }

        private void AddGuestHomeToTabPage()
        {
            if (tabControl3 == null)
            {
                MessageBox.Show("TabControl3 is NULL!");
                return;
            }

            TabPage guestHomeTab = tabControl3.TabPages["tabPageGuestHome"];

            if (guestHomeTab == null)
            {
                MessageBox.Show("tabPageGuestHome NOT FOUND in tabControl3!");
                return;
            }

            ucGuestHome.Dock = DockStyle.Fill;
            guestHomeTab.Controls.Clear();
            guestHomeTab.Controls.Add(ucGuestHome);
        }
        private void AddGuestBookListToTabPage()
        {
            if (tabControl3 == null)
            {
                MessageBox.Show("TabControl3 is NULL!");
                return;
            }

            TabPage guestHomeTab = tabControl3.TabPages["tabPageGuestBookingList"];

            if (guestHomeTab == null)
            {
                MessageBox.Show("tabPageGuestBookingList NOT FOUND in tabControl3!");
                return;
            }

            ucGuestBookList.Dock = DockStyle.Fill;
            guestHomeTab.Controls.Clear();
            guestHomeTab.Controls.Add(ucGuestBookList);
        }
        private void AddGuestServiceRequestToTabPage()
        {
            if (tabControl3 == null)
            {
                MessageBox.Show("TabControl3 is NULL!");
                return;
            }

            TabPage guestHomeTab = tabControl3.TabPages["tabPageGuestServiceRequest"];

            if (guestHomeTab == null)
            {
                MessageBox.Show("tabPageGuestServiceRequest NOT FOUND in tabControl3!");
                return;
            }

            ucGuestServiceRequest.Dock = DockStyle.Fill;
            guestHomeTab.Controls.Clear();
            guestHomeTab.Controls.Add(ucGuestServiceRequest);
        }
        public void BOOKINGLIST()
        {

            //view.Visible = true;
            //view.Dock = DockStyle.Fill;
            //ucbooking.Visible = false;
        }

        private void StylePanel(Panel panel)
        {
            panel.BorderStyle = BorderStyle.FixedSingle; // Add a simple border
            panel.BackColor = Color.FromArgb(0x00, 0x1F, 0x54); // Set the background color to the second color (#001F54)
        }


        private void StyleSearchBox(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(0x28, 0x33, 0x4A); // A suitable gray from your palette
            textBox.ForeColor = Color.LightGray; // Light gray text for placeholder
            textBox.Font = new Font("Segoe UI", 12); // Slightly larger font
            textBox.BorderStyle = BorderStyle.FixedSingle; // Keep a simple border

            // Placeholder text (using Enter and Leave events)
            textBox.Enter += (sender, e) =>
            {
                if (textBox.Text == "Search...")
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.White; // Change text color when typing
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "Search...";
                    textBox.ForeColor = Color.LightGray; // Reset placeholder color
                }
            };

            textBox.Text = "Search..."; // Initial placeholder text
        }
        // ... (rest of your code)


        private void StyleButton(Button button)
        {
            button.BackColor = Color.FromArgb(0x03, 0x40, 0x78); // Lightest blue for background
            button.ForeColor = Color.White; // White text
            button.FlatStyle = FlatStyle.Flat; // Flat style
            button.FlatAppearance.BorderSize = 0; // Remove border
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0x00, 0x1F, 0x54); // Middle blue on hover
        }

        private void StyleLabel(System.Windows.Forms.Label label)
        {
            label.ForeColor = Color.White; // White text
            label.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Bigger font, bold (adjust size as needed)
        }


        private void adminView_Load(object sender, EventArgs e)
        {

        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            uchome = new HomeUserControl(connectionString,this.GuestId,this.Role);
            //panelMain.Controls.Clear();
            //panelMain.Controls.Add(uchome);
            uchome.Dock = DockStyle.Fill;
        }




        private void button3_Click_1(object sender, EventArgs e)
        {
            //panelMain.Controls.Clear();
            //panelMain.Controls.Add(ucbooking);
            //panelMain.Controls.Add(view);
            //ucbooking.Visible = true;
            //view.Visible = false;
            ucbooking.Dock = DockStyle.Fill;

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserName_Click(object sender, EventArgs e)
        {

        }

        private void HMS_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            AddGuestHomeToTabPage();
            tabControl3.SelectedTab = tabControl3.TabPages["tabPageGuestHome"]; // Switch to the tab
        }

        private void tabPageGuestHome_Click(object sender, EventArgs e)
        {

        }
    }
}
