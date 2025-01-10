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
    public partial class BossControl : UserControl
    {
        public BossControl()
        {
            InitializeComponent();
            /*Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;*/
            Controls.Add(panel1);

            LoadHomePage();
        }
        private void LoadHomePage()
        {
            Form1 homePageForm = new Form1();
            homePageForm.TopLevel = false;
            homePageForm.FormBorderStyle = FormBorderStyle.None;
            homePageForm.Dock = DockStyle.Fill;
            panel1.Controls.Add(homePageForm);
            homePageForm.Show();
        }

        private void BossControl_Load(object sender, EventArgs e)
        {

        }
    }
}
