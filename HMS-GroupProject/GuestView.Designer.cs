namespace HMS_GroupProject
{
    partial class GuestView
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
            this.tabPageGuestBookingList = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPageGuestServiceRequest = new System.Windows.Forms.TabPage();
            this.tabPageGuestHome = new System.Windows.Forms.TabPage();
            this.tabControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageGuestBookingList
            // 
            this.tabPageGuestBookingList.Location = new System.Drawing.Point(4, 49);
            this.tabPageGuestBookingList.Name = "tabPageGuestBookingList";
            this.tabPageGuestBookingList.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageGuestBookingList.Size = new System.Drawing.Size(888, 320);
            this.tabPageGuestBookingList.TabIndex = 2;
            this.tabPageGuestBookingList.Text = "Booking List";
            this.tabPageGuestBookingList.UseVisualStyleBackColor = true;
            this.tabPageGuestBookingList.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPageGuestHome);
            this.tabControl3.Controls.Add(this.tabPageGuestBookingList);
            this.tabControl3.Controls.Add(this.tabPageGuestServiceRequest);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl3.ItemSize = new System.Drawing.Size(68, 45);
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(10, 13, 13, 13);
            this.tabControl3.Multiline = true;
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.Padding = new System.Drawing.Point(15, 5);
            this.tabControl3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(896, 373);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPageGuestServiceRequest
            // 
            this.tabPageGuestServiceRequest.AutoScroll = true;
            this.tabPageGuestServiceRequest.Location = new System.Drawing.Point(4, 49);
            this.tabPageGuestServiceRequest.Name = "tabPageGuestServiceRequest";
            this.tabPageGuestServiceRequest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGuestServiceRequest.Size = new System.Drawing.Size(888, 320);
            this.tabPageGuestServiceRequest.TabIndex = 0;
            this.tabPageGuestServiceRequest.Text = "Service and Maintaince";
            this.tabPageGuestServiceRequest.UseVisualStyleBackColor = true;
            // 
            // tabPageGuestHome
            // 
            this.tabPageGuestHome.AutoScroll = true;
            this.tabPageGuestHome.Location = new System.Drawing.Point(4, 49);
            this.tabPageGuestHome.Name = "tabPageGuestHome";
            this.tabPageGuestHome.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPageGuestHome.Size = new System.Drawing.Size(888, 320);
            this.tabPageGuestHome.TabIndex = 3;
            this.tabPageGuestHome.Text = "Home";
            this.tabPageGuestHome.UseVisualStyleBackColor = true;
            this.tabPageGuestHome.Click += new System.EventHandler(this.tabPageGuestHome_Click);
            // 
            // GuestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl3);
            this.Name = "GuestView";
            this.Size = new System.Drawing.Size(896, 373);
            this.tabControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageGuestBookingList;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPageGuestServiceRequest;
        private System.Windows.Forms.TabPage tabPageGuestHome;
    }
}
