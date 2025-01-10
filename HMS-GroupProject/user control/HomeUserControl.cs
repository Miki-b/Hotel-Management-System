using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_GroupProject.user_control
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
            StyleHomeControl();
        }

        private void StyleHomeControl()
        {
            this.BackColor = Color.FromArgb(0x1A, 0x21, 0x37); // Darker gray background (like panelMain)

            // Style Labels
            StyleLabel(label2);
            StyleLabel(label14);
            StyleLabel(label11);
            StyleLabel(label13);
            StyleLabel(label3);
            StyleLabel(label12);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label4);

            // Style DataGridView
            StyleDataGridView(dataGridView1);

            // Style MonthCalendar
            StyleMonthCalendar(monthCalendar1);
        }

        private void StyleMonthCalendar(MonthCalendar monthCalendar)
        {
            monthCalendar.BackColor = Color.FromArgb(0x28, 0x33, 0x4A);
            monthCalendar.ForeColor = Color.White;
            monthCalendar.TitleBackColor = Color.FromArgb(0x00, 0x1F, 0x54);
            monthCalendar.TitleForeColor = Color.White;
            monthCalendar.TrailingForeColor = Color.Gray;
        }

        private void StyleDataGridView(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = Color.FromArgb(0x28, 0x33, 0x4A);
            dataGridView.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0x00, 0x1F, 0x54);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(0x28, 0x33, 0x4A);
            dataGridView.DefaultCellStyle.ForeColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.GridColor = Color.FromArgb(0x00, 0x1F, 0x54);
        }



        private void StyleLabel(Label label)
        {
            label.ForeColor = Color.White;
            label.Font = new Font("Segoe UI", 12);
        }

        private void HomeUserControl_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
