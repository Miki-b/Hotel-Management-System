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
    public partial class RoomView : UserControl
    {
        public RoomDetails ucRoomDetails=new RoomDetails();
        public RoomView()
        {
            InitializeComponent();
            if (tabControl1.TabPages["tabPage2"] == null)
            {
                TabPage newTab = new TabPage("Room Details");
                newTab.Name = "tabPage2";
                tabControl1.TabPages.Add(newTab);
            }
            AddGuestHomeToTabPage();
        }
        private void AddGuestHomeToTabPage()
        {
            if (tabControl1 == null)
            {
                MessageBox.Show("TabControl1 is NULL!");
                return;
            }

            TabPage tabPage1Tab = tabControl1.TabPages["tabPage2"];

            if (tabPage1Tab == null)
            {
                MessageBox.Show("tabPage2 NOT FOUND in tabControl1!");
                return;
            }

            ucRoomDetails.Dock = DockStyle.Fill;
            tabPage1Tab.Controls.Clear();
            tabPage1Tab.Controls.Add(ucRoomDetails);
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
