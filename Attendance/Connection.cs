using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Attendance;
using System.Data.SqlClient;

namespace Attendance
{
    public class Connection
    {
        public SqlConnection conn { get; set; }
        public String connstring { get; set; }

        public Connection()
        {
            setConnString();
            this.conn = new SqlConnection(connstring);
        }
        public Connection(String dbServer, String username, String password, String initCatalog)
        {
            setConnString(dbServer, username, password, initCatalog);
            this.conn = new SqlConnection(connstring);
        }

        public void setConnString()
        {
            String dbServer = @"10.2.168.119";
            String username = "TALMRB";
            String password = "Talentamrb21";
            String initCatalog = "TALHRD";
            setConnString(dbServer, username, password, initCatalog);  
        }
        public void setConnString(String dbServer, String username, String password, String initCatalog)
        {
            connstring = "Data source=" + dbServer +";"+ "initial catalog=" + initCatalog + ";" + "user id=" + username +";" + "password=" + password + ";TrustServerCertificate=true";
        }

        public void open()
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {

            }
            
        }

        public String refreshConnection()
        {
            conn = new SqlConnection(connstring);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return conn.State.ToString();
        }
        public void close()
        {
            if (conn.State.ToString() == "Open")
            {
                conn.Close();
            }
        }

        public SqlDataReader Reader(String query)
        {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;            
        }

        public String executeQuery(String query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            return cmd.ExecuteNonQuery().ToString();
        }
        

    }
}
