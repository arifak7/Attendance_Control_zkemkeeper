using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
    internal class Model
    {
        Connection conn;
        Connection newDB;
        public Model(Connection conn, Connection newDB)
        {
            this.conn = conn;
            this.newDB = newDB;
        }

        public Dictionary<String,String> getKaryawanNama()
        {
            Dictionary<String,String> karyawanNama = new Dictionary<String,String>();
            String Query = "SELECT TKaryawan.NIK, TKaryawan.Nama FROM TKaryawan";
            SqlDataReader reader = conn.Reader(Query);
            while (reader.Read())
            {
                karyawanNama.Add(reader.GetString(0), reader.GetString(1));
            }
            reader.Close();
            return karyawanNama;
        }

        public Dictionary<String, Roster> getRosters()
        {
            DateTime yesterday = DateTime.Now - TimeSpan.FromDays(1);
            DateTime tomorrow = DateTime.Now + TimeSpan.FromDays(1);
            Dictionary<String, Roster>rosters = new Dictionary<String, Roster>();
            String Query = "SELECT * FROM Roster WHERE Tanggal >= '"+yesterday.ToString("MM/dd/yyyy")+"' AND Tanggal <= '"+ tomorrow.ToString("MM/dd/yyyy") + "'";
            SqlDataReader reader = newDB.Reader(Query);
            while (reader.Read())
            {
                if (!rosters.ContainsKey(reader.GetString(1))){
                    Roster roster = new Roster();
                    rosters.Add(reader.GetString(1), roster);
                }
                rosters[reader.GetString(1)].addDay(reader[2].ToString(), reader.GetString(3)) ;
            }
            reader.Close();
            return rosters;

        }

    }

    internal class Roster
    {
        public String dayBefore;
        public String today;
        public String dayAfter;


        public Roster()
        {
            dayBefore = "-";
            today = "-";
            dayAfter = "-";
        }

        public void addDay(String date, String roster)
        {
            DateTime day = Convert.ToDateTime(date);
            if(day.Date == (DateTime.Now - TimeSpan.FromDays(1)).Date)
            {
                dayBefore = roster;
            }
            else if(day.Date == DateTime.Now.Date)
            {
                today = roster;
            }
            else if(day.Date == (DateTime.Now + TimeSpan.FromDays(1)).Date)
            {
                dayAfter = roster;
            }
        }


    }
}
