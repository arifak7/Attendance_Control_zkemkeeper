using Axzkemkeeper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using zkemkeeper;
using Attendance;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.SqlClient;

namespace Attendance
{
    public partial class Home : Form
    {
        AddDevice addDev;
        Connection connection;
        Dictionary<int, Thread> Threads;
        static Dictionary<int, zkemkeeper.CZKEMClass> Devices;
        bool loadingPaused;
        bool loaded;
        static bool namaInitialized;
        static bool rosterInitialized;
        Dictionary<String, String> namaRef;
        Dictionary<String, Roster> rosterRef;
        Thread databaseThread;
        static Dictionary<String, Record> Records;
        Thread displayThread;
        Queue<Dictionary<String, Record>> databaseQueue;
        public Home()
        {
            loaded = false;
            rosterInitialized = false;
            databaseQueue = new Queue<Dictionary<String, Record>>();
            namaRef = new Dictionary<String, String>();
            rosterRef = new Dictionary<String, Roster>();
            Records = new Dictionary<String, Record>();
            Threads = new Dictionary<int, Thread>();
            Devices = new Dictionary<int, zkemkeeper.CZKEMClass>();

            loadingPaused = false;
            namaInitialized = false;
            InitializeComponent();
            refreshIpTable();

            dbIPaddr.Text = Properties.Settings.Default.IpDB;
            dbIPaddr2.Text = Properties.Settings.Default.newIP;
        }

        public void startDisplayConstantly()
        {
            displayThread = new Thread(() => refreshTable(this));
            //Threads.Add(9999, thread);
            displayThread.Start();
        }

        private void startDatabaseSync()
        {
            String ipDB = Properties.Settings.Default.IpDB;
            String username = Properties.Settings.Default.usernameDB;
            String password = Properties.Settings.Default.passwordDB;
            String catalog = Properties.Settings.Default.catalogDB;
            String newIP = Properties.Settings.Default.newIP;
            String newPassword = Properties.Settings.Default.newPassword;
            String newCatalog = Properties.Settings.Default.newCatalog;
            String newUsername = Properties.Settings.Default.newUsername;
            connection = new Connection(ipDB, username, password, catalog);
            databaseThread = new Thread(()=> databaseCheck(this, ipDB, username, password, catalog, newIP,newUsername, newPassword, newCatalog ));
            databaseThread.Start(); 
        }

        private void databaseCheck(Home home, String ipDB, String username, String password, String catalog, String newIP, String newUsername
            , String newPassword, String newCatalog)
        {
            Connection DBConn = new Connection(ipDB, username, password, catalog);
            Connection newDB = new Connection(newIP, newUsername, newPassword, newCatalog);
            DBConn.open();
            newDB.open();
            Model model = new Model(DBConn, newDB);
            while (true)
            {
                if (DBConn.conn.State.ToString() == "Open")
                {
                    if (!namaInitialized)
                    {
                        namaRef = model.getKaryawanNama();
                        foreach(KeyValuePair<String,Record> entry in Records)
                        {
                            if (namaRef.ContainsKey(entry.Key))
                            {
                                entry.Value.nama = namaRef[entry.Key];
                            }
                        }
                        namaInitialized = true;
                    }
                    if (!rosterInitialized)
                    {
                        rosterRef = model.getRosters();
                        foreach (KeyValuePair<String, Record> entry in Records)
                        {
                            if (rosterRef.ContainsKey(entry.Key))
                            {
                                entry.Value.roster = rosterRef[entry.Key];
                            }
                        }
                        rosterInitialized = true;
                    }
                    home.dbConnStatus.Invoke(new Action(delegate ()
                    {
                        home.dbConnStatus.Text = DBConn.conn.State.ToString();
                        if (DBConn.conn.State.ToString() == "Open")
                        {
                            dbConnStatus.ForeColor = Color.Green;
                        }
                        else
                        {
                            dbConnStatus.ForeColor = Color.Red;
                        }
                    }));
                }
                else
                {
                    home.dbConnStatus.Invoke(new Action(delegate ()
                    {
                        home.dbConnStatus.Text = "Retrying...";
                    }));
                    DBConn.open();

                    home.dbConnStatus.Invoke(new Action(delegate ()
                    {
                        home.dbConnStatus.Text = DBConn.conn.State.ToString();
                    }));
                }

                if (newDB.conn.State.ToString() == "Open")
                {
                    home.dbConnStatus2.Invoke(new Action(delegate ()
                    {
                        home.dbConnStatus2.Text = newDB.conn.State.ToString();
                        home.dbConnStatus2.ForeColor = Color.Green; 
                    }));
                }
                else
                {
                    home.dbConnStatus.Invoke(new Action(delegate ()
                    {
                        home.dbConnStatus2.Text = "Retrying...";
                        home.dbConnStatus2.ForeColor = Color.Red;
                    }));
                    newDB.open();
                    home.dbConnStatus.Invoke(new Action(delegate ()
                    {
                        home.dbConnStatus2.Text = DBConn.conn.State.ToString();
                        home.dbConnStatus2.ForeColor = Color.Red;
                    }));
                }

                Thread.Sleep(1000);
            }
        }

        private void refreshTable(Home home)
        {
            int loop = 0;
            List<String> statusCode = new List<string>() { "Data Lokal", "Belum Di Database", "Sedang Diproses", "Gagal Diproses", "Sudah di Database" };
            while (true)
            {
                if (loop == 10)
                {
                    home.date_time.Invoke(new Action(delegate ()
                    {
                        home.date_time.Value = DateTime.Now;
                    }));
                    loop = 0;
                }
                if (!loadingPaused)
                {
                    String loading = "loading...";
                    home.statusLabel.Invoke(new Action(delegate ()
                    {
                        home.statusLabel.Text = loading;
                    }));
                    home.karyawan_table.Invoke(new Action(delegate ()
                    {
                        home.karyawan_table.Rows.Clear();
                    }));
                    Dictionary<String, Record> toCheck = Records;
                    foreach (KeyValuePair<String, Record> entry in toCheck)
                    {
                        home.karyawan_table.Invoke(new Action(delegate ()
                        {
                            String inTime = (entry.Value.inTime.TimeOfDay == TimeSpan.Zero) ? "" : entry.Value.inTime.TimeOfDay.ToString();
                            String outTime = (entry.Value.outTime.TimeOfDay == TimeSpan.Zero) ? "" : entry.Value.outTime.TimeOfDay.ToString();
                            String roster = "-";
                            if(entry.Value.roster!=null)
                            {
                                roster = entry.Value.roster.today;
                            }
                            else
                            {
                                if (rosterRef.ContainsKey(entry.Key))
                                {
                                    entry.Value.roster = rosterRef[entry.Key];
                                    roster = entry.Value.roster.today;
                                }
                            }
                            home.karyawan_table.Rows.Add(entry.Key, entry.Value.nama, inTime, outTime, entry.Value.machine, statusCode[entry.Value.validation],roster);
                        }));
                    }
                }
                else
                {
                    home.statusLabel.Invoke(new Action(delegate ()
                    {
                        home.statusLabel.Text = "Paused";
                    }));
                }
                loop++;
                Thread.Sleep(5000);
            }
        }

        public void loadDataFromMachine(zkemkeeper.CZKEMClass device, int row, Home home, String devName)
        {
            device.ReadGeneralLogData(1);
            String enroll = "";
            int verify = 0; int inOut = 0; int year = 0; int month = 0; int workCode = 0;
            int day = 0; int hour = 0; int minute = 0; int second = 0;
            while (device.SSR_GetGeneralLogData(0, out enroll, out verify, out inOut, out year, out month, out day, out hour, out minute, out second, ref workCode))
            {
                DateTime time = new DateTime(year, month, day, hour, minute, second);
                if (time.Date == date_time.Value.Date)
                {
                    /*
                    home.karyawan_table.Invoke(new Action(delegate ()
                    {
                        machine = home.karyawan_table.Rows[row].Cells["lokasi_karyawan"].Value.ToString();
                    }));
                    */
                    if (!Records.ContainsKey(enroll))
                    {
                        Record current = new Record(devName);
                        current.LogTime(time, inOut);
                        Records.Add(enroll, current);
                        if (namaRef.ContainsKey(enroll))
                        {
                            Records[enroll].nama = namaRef[enroll];
                        }
                    }
                    else
                    {
                        Records[enroll].LogTime(time, inOut);
                    }
                }
            }

            Thread.Sleep(5000);
        }
        

        public void refreshIpTable()
        {
            loaded = false;
            allDeviceCombo.Items.Clear();
            device_table.Rows.Clear();
            allDeviceCombo.Items.Add("All");
            foreach (String device in Properties.Settings.Default.IpAddress)
            {
                String[] toSplit = device.Split('/');
                device_table.Rows.Add(toSplit[0], toSplit[1]);
                allDeviceCombo.Items.Add(toSplit[0]);
            }
            if (allDeviceCombo.Items.Count > 0) allDeviceCombo.SelectedIndex = 0;
            loaded = true;

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (addDev == null)
            {
                addDev = new AddDevice(this);
            }
            addDev.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (device_table.RowCount > 0)
            {
                Properties.Settings.Default.IpAddress.RemoveAt(device_table.SelectedRows[0].Index);
                refreshIpTable();
                Properties.Settings.Default.Save();
            }
        }
        private void flushThreads()
        {
            foreach(KeyValuePair<int,Thread> entry in Threads)
            {
                entry.Value.Abort();
            }
            if (displayThread != null)
            {
                displayThread.Abort();
                displayThread = null;
            }
            Threads.Clear();
        }
        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Enabled = false;
            addToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = false;
            pauseLoadToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = true;

            int i = 0;
            flushThreads();
            foreach (String machine in Properties.Settings.Default.IpAddress)
            {
                String[] tmp = machine.Split('/');
                Thread thread = new Thread(()=> checkConnection(this, tmp[1], i, tmp[0]));
                thread.Start();
                Threads.Add(i, thread);
                Thread.Sleep(100);
                i++;
            }
            startDisplayConstantly();
        }

        private void checkConnection(Home home, String ipAddr, int row, String devName)
        {
            zkemkeeper.CZKEMClass dev = new zkemkeeper.CZKEMClass();
            bool connected=false;
            home.device_table.Invoke(new Action(delegate ()
            {
                home.device_table.Rows[row].Cells["device_status"].Value = "Trying...";
            }));
            while (true)
            {
                if (true)
                {
                    home.device_table.Invoke(new Action(delegate ()
                    {
                        home.device_table.Rows[row].Cells["device_status"].Value = "Trying...";
                        home.device_table.Rows[row].Cells["device_status"].Style.ForeColor = Color.Black;
                        home.device_table.Rows[row].Cells["device_status"].Style.SelectionForeColor = Color.Black;
                    }));
                }
                connected = dev.Connect_Net(ipAddr, 4370);
                
                if (connected)
                {
                    home.device_table.Invoke(new Action(delegate ()
                    {
                        home.device_table.Rows[row].Cells["device_status"].Value = "Connected";
                        home.device_table.Rows[row].Cells["device_status"].Style.ForeColor = Color.Green;
                        home.device_table.Rows[row].Cells["device_status"].Style.SelectionForeColor = Color.Green;
                    }));
                        
                    if (Devices != null && dev!=null && row !=null)
                    {
                        if (!Devices.ContainsKey(row))
                        {
                            try
                            {
                                Devices.Add(row, dev);
                            }
                            catch (Exception e)
                            {
                                home.device_table.Invoke(new Action(delegate ()
                                {
                                    home.device_table.Rows[row].Cells["device_status"].Value = "Error Connecting...";
                                    home.device_table.Rows[row].Cells["device_status"].Style.ForeColor = Color.Red;
                                    home.device_table.Rows[row].Cells["device_status"].Style.SelectionForeColor = Color.Red;
                                }));
                                continue;
                            }
                        }
                    }
                    /*
                    dev.RegEvent(1, 1);
                    dev.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(onTransact);
                    dev.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(onTransact);
                    */
                    if (!loadingPaused)
                    {
                        loadDataFromMachine(dev, row, home, devName);
                    }
                }
                else
                {
                    home.device_table.Invoke(new Action(delegate ()
                    {
                        home.device_table.Rows[row].Cells["device_status"].Value = "Disconnected";
                        home.device_table.Rows[row].Cells["device_status"].Style.ForeColor = Color.Red;
                        home.device_table.Rows[row].Cells["device_status"].Style.SelectionForeColor = Color.Red;
                    }));
                    if (Devices.ContainsKey(row))
                    {
                        Devices.Remove(row);
                    }
                }
                Thread.Sleep(5000);
            }
        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(databaseThread!=null) databaseThread.Abort();
            flushThreads();
        }
        private void onTransact(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            DateTime time = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
            addData(sEnrollNumber, time, "Test", iAttState);
            //home.recordTable.Rows.Add(sEnrollNumber, time, code[iAttState],machine);
            //home.recordTable.FirstDisplayedScrollingRowIndex = home.recordTable.Rows.Count-1;
        }
        private void addData(String NIK, DateTime time, String Device, int AttState)
        {
            List<String> attending = new List<String>() {"IN","OUT","B-IN","B-OUT","OT-IN","OT-OUT"};
            karyawan_table.Invoke(new Action(delegate ()
            {
                karyawan_table.Rows.Add(NIK, time.ToString(), Device, attending[AttState]);
            }));
        }
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Enabled = true;
            addToolStripMenuItem.Enabled = true;
            removeToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;
            pauseLoadToolStripMenuItem.Enabled = false;

            flushThreads();
            foreach(DataGridViewRow row in  device_table.Rows)
            {
                row.Cells["device_status"].Value = "Disconnected";
                row.Cells["device_status"].Style.ForeColor = Color.Red;
            }
            Records.Clear();
            karyawan_table.Rows.Clear();
        }

        private void pauseLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadingPaused = !loadingPaused;
            pauseLoadToolStripMenuItem.Text = (loadingPaused) ? "Continue Loading" : "Pause Loading";
            findNIK.Enabled = loadingPaused;
            findButton.Enabled = loadingPaused;
            allDeviceCombo.Enabled = loadingPaused;

        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void findButton_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in karyawan_table.Rows)
            {
                if (row.Cells[0].Value.ToString().Contains(findNIK.Text))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void keyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                findButton_Click(sender, e);
            }
        }

        private void valuechanged(object sender, EventArgs e)
        {
            Records.Clear();
            rosterRef.Clear();
            rosterInitialized = false;

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseSettings settings = new DatabaseSettings();
            settings.Show();
        }

        private void connectDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startDatabaseSync();
            connectDBToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem1.Enabled = true;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void changeMachine(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (allDeviceCombo.Text == "All")
                {
                    karyawan_table.Invalidate();
                }
                else
                {
                    foreach (DataGridViewRow row in karyawan_table.Rows)
                    {
                        row.Visible = row.Cells["lokasi_karyawan"].Value.ToString() == allDeviceCombo.Text;
                    }
                }
            }
        }

        private void disconnectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            databaseThread.Abort();
            rosterRef.Clear();
            rosterInitialized = false;
            connectDBToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem1.Enabled = false;
        }
    }
    internal class Record {
        public DateTime inTime;
        public DateTime outTime;
        public String machine;
        public int validation;
        public String nama;
        public Roster roster;
        public Record(String machine)
        {
            this.machine = machine;
            validation = 0;
            nama = "-";
        }
        public void LogTime(DateTime time, int code)
        {
            if (code==0|| code == 3|| code == 4)
            {
                if(inTime == DateTime.MinValue)
                {
                    inTime = time;
                }
                else
                {
                    inTime = (time < inTime) ? time : inTime;
                }
            }
            else
            {
                if (outTime == DateTime.MinValue)
                {
                    outTime = time;
                }
                else
                {
                    outTime = (time > outTime) ? time : outTime;
                }

            }
        }
    
    }

}
