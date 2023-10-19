using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
    public partial class AddDevice : Form
    {
        Home home;
        public AddDevice(Home home)
        {
            InitializeComponent();
            this.home = home;
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
        }

        private void submit_button_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.IpAddress.Add(nama_box.Text + "/" + ip_box1.Text + "." + ip_box2.Text + "." + ip_box3.Text + "." + ip_box4.Text);
            Properties.Settings.Default.Save();
            nama_box.Clear();
            home.refreshIpTable();
            this.Hide();
        }
    }
}
