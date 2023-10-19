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
    public partial class DatabaseSettings : Form
    {
        public DatabaseSettings()
        {
            
            InitializeComponent();
            init();
        }

        private void init()
        {
            if (!Properties.Settings.Default.IpDB.ToString().Equals(String.Empty))
            {
                String[] ip = Properties.Settings.Default.IpDB.Split('.');
                ip_box1.Text = ip[0];
                ip_box2.Text = ip[1];
                ip_box3.Text = ip[2];
                ip_box4.Text = ip[3];
            }
            if(!Properties.Settings.Default.newIP.ToString().Equals(String.Empty))
            {
                String[] ip = Properties.Settings.Default.newIP.Split('.');
                newIP1.Text = ip[0];
                newIP2.Text = ip[1];
                newIP3.Text = ip[2];
                newIP4.Text = ip[3];
            }
            username_box.Text = Properties.Settings.Default.usernameDB;
            password_box.Text = Properties.Settings.Default.passwordDB;
            database_box.Text = Properties.Settings.Default.catalogDB;

            newUsername.Text = Properties.Settings.Default.newUsername;
            newPassword.Text = Properties.Settings.Default.newPassword;
            newCatalog.Text = Properties.Settings.Default.newCatalog;
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IpDB = ip_box1.Text + "." + ip_box2.Text + "." + ip_box3.Text + "." + ip_box4.Text;
            Properties.Settings.Default.usernameDB = username_box.Text;
            Properties.Settings.Default.passwordDB = password_box.Text;
            Properties.Settings.Default.catalogDB = database_box.Text;
            Properties.Settings.Default.Save();
            statuslabel.Text = "Saved";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ipDB= ip_box1.Text + "." + ip_box2.Text + "." + ip_box3.Text + "." + ip_box4.Text;
            Connection connection = new Connection(ipDB, username_box.Text, password_box.Text, database_box.Text);
            connection.open();
            statuslabel.Text = connection.conn.State.ToString() ;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.newIP = newIP1.Text + "." + newIP2.Text + "." + newIP3.Text + "." + newIP4.Text;
            Properties.Settings.Default.newUsername = newUsername.Text;
            Properties.Settings.Default.newPassword = newPassword.Text;
            Properties.Settings.Default.newCatalog = newCatalog.Text;
            Properties.Settings.Default.Save();
            newStatus.Text = "Saved";
        }

        private void newConnect_Click(object sender, EventArgs e)
        {

            String ipDB = newIP1.Text + "." + newIP2.Text + "." + newIP3.Text + "." + newIP4.Text;
            Connection connection = new Connection(ipDB, newUsername.Text, newPassword.Text, newCatalog.Text);
            connection.open();
            newStatus.Text = connection.conn.State.ToString();
        }
    }
}
