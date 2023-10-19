namespace Attendance
{
    partial class AddDevice
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.submit_button = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.nama_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ip_box4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ip_box3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ip_box2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ip_box1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(11, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 170);
            this.panel1.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.submit_button);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 111);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panel10.Size = new System.Drawing.Size(312, 46);
            this.panel10.TabIndex = 6;
            // 
            // submit_button
            // 
            this.submit_button.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.submit_button.BackgroundImage = global::Attendance.Properties.Resources.check_mark_blue;
            this.submit_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.submit_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.submit_button.FlatAppearance.BorderSize = 0;
            this.submit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit_button.ForeColor = System.Drawing.Color.White;
            this.submit_button.Location = new System.Drawing.Point(259, 0);
            this.submit_button.Margin = new System.Windows.Forms.Padding(4);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(43, 46);
            this.submit_button.TabIndex = 4;
            this.submit_button.UseVisualStyleBackColor = false;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click_1);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 64);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(312, 47);
            this.panel5.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.nama_box);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(102, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(20, 10, 10, 0);
            this.panel6.Size = new System.Drawing.Size(210, 47);
            this.panel6.TabIndex = 2;
            // 
            // nama_box
            // 
            this.nama_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nama_box.Location = new System.Drawing.Point(20, 10);
            this.nama_box.Name = "nama_box";
            this.nama_box.Size = new System.Drawing.Size(180, 25);
            this.nama_box.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 47);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nama Mesin";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 17);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(312, 47);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ip_box4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.ip_box3);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.ip_box2);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.ip_box1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(102, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(20, 10, 10, 0);
            this.panel4.Size = new System.Drawing.Size(210, 47);
            this.panel4.TabIndex = 2;
            // 
            // ip_box4
            // 
            this.ip_box4.Dock = System.Windows.Forms.DockStyle.Left;
            this.ip_box4.Location = new System.Drawing.Point(164, 10);
            this.ip_box4.Name = "ip_box4";
            this.ip_box4.Size = new System.Drawing.Size(34, 25);
            this.ip_box4.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(152, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 37);
            this.label5.TabIndex = 6;
            this.label5.Text = ".";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ip_box3
            // 
            this.ip_box3.Dock = System.Windows.Forms.DockStyle.Left;
            this.ip_box3.Location = new System.Drawing.Point(116, 10);
            this.ip_box3.Name = "ip_box3";
            this.ip_box3.Size = new System.Drawing.Size(36, 25);
            this.ip_box3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(104, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 37);
            this.label4.TabIndex = 4;
            this.label4.Text = ".";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ip_box2
            // 
            this.ip_box2.Dock = System.Windows.Forms.DockStyle.Left;
            this.ip_box2.Location = new System.Drawing.Point(68, 10);
            this.ip_box2.Name = "ip_box2";
            this.ip_box2.Size = new System.Drawing.Size(36, 25);
            this.ip_box2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(56, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 37);
            this.label3.TabIndex = 2;
            this.label3.Text = ".";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ip_box1
            // 
            this.ip_box1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ip_box1.Location = new System.Drawing.Point(20, 10);
            this.ip_box1.Name = "ip_box1";
            this.ip_box1.Size = new System.Drawing.Size(36, 25);
            this.ip_box1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 17);
            this.panel2.TabIndex = 0;
            // 
            // AddDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(334, 194);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cascadia Mono", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AddDevice";
            this.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddDevice";
            this.panel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox ip_box1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox nama_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox ip_box4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ip_box3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ip_box2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button submit_button;
    }
}