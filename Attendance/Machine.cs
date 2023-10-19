using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
    internal class Machine
    {
        public String ipAddr; 
        public String machineName; 
        public Connection connection;
        public zkemkeeper.CZKEMClass zk;
        public List<OldRecord> record;
        public bool activated;
        private Home home;
        public Machine(Home home,String ipAddr, String machine, Connection connection) 
        {
            zk = new zkemkeeper.CZKEMClass();
            this.home = home;
            this.activated = false;
            this.ipAddr = ipAddr;
            this.connection = connection;
            this.machineName = machine;
            record = new List<OldRecord>();
            connect();
            initevents();
            getInitData();
        }

        public void activate(){ this.activated = true;}
        public void deactivate() { this.activated = false;}
        public void getInitData()
        {
           // zk.RefreshData(0);
            String enroll = "";
            int verify = 0;
            int inOut = 0;
            int year = 0; int month = 0; int day = 0; int hour = 0; int minute = 0; int second = 0;
            int workCode = 0;
            if (zk.ReadGeneralLogData(0)){
                while (zk.SSR_GetGeneralLogData(1, out enroll, out verify, out inOut, out year, out month, out day, out hour, out minute, out second, ref workCode))
                {
                    record.Add( new OldRecord (enroll, new DateTime(year,month, day, hour, minute, second), inOut, verify));
                }
            }
        }
        public bool connect()
        {
            return isConnected();
        }

        public bool isConnected()
        {
            return zk.Connect_Net(ipAddr, 4370);
        }
        public void deleteEvents()
        {
            this.zk.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(onTransact);
            zk.Disconnect();
        }
        private void initevents()
        {
            connect();
            zk.RegEvent(1, 1);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            this.zk.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(onTransact);
        }
        
        private void onTransact(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            DateTime time = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
            record.Add(new OldRecord(sEnrollNumber, time , iVerifyMethod, iAttState));
            //if(activated)
        }


        /*private void getAllUser()
        {
            zk.ReadAllUserID(0);
            String enroll = "";
            String name = " ";
            String password = " ";
            int priv = 0;
            bool enable = true;
            while (zk.SSR_GetAllUserInfo(0, out enroll, out name, out password, out priv, out enable))
            {
            }
        }

        QUERY//
        IF EXISTS (SELECT * FROM Absensi WHERE CONVERT(date, Enter)= CONVERT(date,'07/18/2023 02:59:11') AND NIK='TB000431')
BEGIN 
UPDATE Absensi SET Leave = '07/18/2023 03:00:00' WHERE NIK='TB000431'
	END
ELSE
BEGIN
INSERT INTO Absensi (NIK, Nama,Enter, KodeSite) VALUES ('TB000431','Arif Abdul Kadir', '07/18/2023 02:59:11','MRB')
END
//
        private void getAllLog()
        {
            zk.RefreshData(0);
            zk.ReadGeneralLogData(0);
            String enroll = "";
            int verify = 0;
            int inOut = 0;
            int year = 0; int month = 0; int day = 0; int hour = 0; int minute = 0; int second = 0;
            int workCode = 0;
            while (zk.SSR_GetGeneralLogData(0, out enroll, out verify, out inOut, out year, out month, out day, out hour, out minute, out second, ref workCode))
            {
            }
        }*/
    }
}
