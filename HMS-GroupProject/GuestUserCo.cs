using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject
{
    public partial class GuestUserCo : UserControl
    {
        public GuestUserCo()
        {
            InitializeComponent();
            InitializeUI();
        }
        private void InitializeUI()
        {
            // Create and arrange controls (example)
            // 1. Create labels and textboxes
            Label lblGuestName = new Label();
            lblGuestName.Text = "Guest:";
            lblGuestName.Location = new Point(10, 10);

        TextBox txtGuestName = new TextBox();
        txtGuestName.Location = new Point(100, 10);
        txtGuestName.Text = "Glenn Zimmerman"; // Sample data

            // 2. Create a panel for the booking summary
            Panel bookingSummaryPanel = new Panel();
        bookingSummaryPanel.Location = new Point(300, 10);
        bookingSummaryPanel.Size = new Size(300, 200);
        bookingSummaryPanel.BorderStyle = BorderStyle.FixedSingle;

            // 3. Add controls to the panel
            bookingSummaryPanel.Controls.Add(CreateLabel("Room Total (1 night):", 10, 10));
            bookingSummaryPanel.Controls.Add(CreateLabel("$200.00", 150, 10)); 
            // ... add other summary labels (Extras, Subtotal, etc.)

            // 4. Create a panel for booking history (if needed)
            // ...

            // 5. Add controls to the UserControl
            Controls.Add(lblGuestName);
            Controls.Add(txtGuestName);
            Controls.Add(bookingSummaryPanel);
            // ... add other panels or controls

            // 6. Set UserControl size and layout (optional)
            this.Size = new Size(650, 400); // Adjust as needed
        }

        private Label CreateLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            return label;
        }
        private void GuestUserCo_Load(object sender, EventArgs e)
        {

        }
    }
}
