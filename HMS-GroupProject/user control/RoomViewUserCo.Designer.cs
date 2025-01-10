namespace HMS_GroupProject.user_control
{
    partial class RoomViewUserCo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RoomNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoomStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FO_status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Res_Status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 60);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "All Rooms";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(321, 60);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 28);
            this.button2.TabIndex = 1;
            this.button2.Text = "Available";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(535, 60);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 28);
            this.button3.TabIndex = 2;
            this.button3.Text = "Sold out";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1041, 28);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 36);
            this.button4.TabIndex = 3;
            this.button4.Text = "Create New Booking";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.RoomNumber,
            this.RoomStatus,
            this.FO_status,
            this.Res_Status});
            this.dataGridView1.Location = new System.Drawing.Point(60, 116);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1131, 313);
            this.dataGridView1.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "RoomType";
            this.Column1.Items.AddRange(new object[] {
            "ALL",
            "Delux",
            "Supreme",
            "King"});
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // RoomNumber
            // 
            this.RoomNumber.HeaderText = "Room#";
            this.RoomNumber.MinimumWidth = 6;
            this.RoomNumber.Name = "RoomNumber";
            this.RoomNumber.Width = 125;
            // 
            // RoomStatus
            // 
            this.RoomStatus.HeaderText = "Room_Status";
            this.RoomStatus.Items.AddRange(new object[] {
            "All",
            "Clean",
            "Dirty",
            "Out_of_order"});
            this.RoomStatus.MinimumWidth = 6;
            this.RoomStatus.Name = "RoomStatus";
            this.RoomStatus.Width = 125;
            // 
            // FO_status
            // 
            this.FO_status.HeaderText = "FO_status";
            this.FO_status.Items.AddRange(new object[] {
            "All",
            "Vacant",
            "Occupied"});
            this.FO_status.MinimumWidth = 6;
            this.FO_status.Name = "FO_status";
            this.FO_status.Width = 125;
            // 
            // Res_Status
            // 
            this.Res_Status.HeaderText = "Res_status";
            this.Res_Status.Items.AddRange(new object[] {
            "All",
            "Reserved ",
            "Not Reserved"});
            this.Res_Status.MinimumWidth = 6;
            this.Res_Status.Name = "Res_Status";
            this.Res_Status.Width = 125;
            // 
            // RoomViewUserCo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RoomViewUserCo";
            this.Size = new System.Drawing.Size(1228, 443);
            this.Load += new System.EventHandler(this.RoomViewUserCo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoomNumber;
        private System.Windows.Forms.DataGridViewComboBoxColumn RoomStatus;
        private System.Windows.Forms.DataGridViewComboBoxColumn FO_status;
        private System.Windows.Forms.DataGridViewComboBoxColumn Res_Status;
    }
}
