using Axzkemkeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
    public class DeviceOld
    {
        public zkemkeeper.CZKEMClass zk;
        public String machine;
        public String ipAddr;
        public Home home;
        List<OldRecord> record;
        List<String> code;
        public Connection newDB;

        public DeviceOld(Home home, String ipAddr, String machine)
        {
            init(home,ipAddr,machine);
            Connect();
            initEvents();
        }

        public void init(Home home, String ipAddr, String machine)
        {
            this.newDB = new Connection();
            //newDB.open();
            this.zk = new zkemkeeper.CZKEMClass();
            record = new List<OldRecord>();
            code = new List<String>() { "IN", "OUT", "BREAK-OUT", "BREAK-IN", "OT-IN", "OT-OUT" };
            this.ipAddr = ipAddr;
            this.machine = machine;
            this.home = home;
        }
        public void initEvents()
        {
            zk.RegEvent(1, 1);
            this.zk.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(onTransact);
        }

        private void onTransact(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            DateTime time = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
            record.Add(new OldRecord(sEnrollNumber, time, iVerifyMethod, iAttState));
            //home.recordTable.Rows.Add(sEnrollNumber, time, code[iAttState],machine);
            //home.recordTable.FirstDisplayedScrollingRowIndex = home.recordTable.Rows.Count-1;
        }

        public void loadData()
        {
            zk.ReadGeneralLogData(1);
            record.Clear();
            String enroll = "";
            int verify = 0; int inOut = 0; int year = 0; int month = 0; int workCode = 0;
            int day = 0; int hour = 0; int minute = 0; int second = 0; 
            while (zk.SSR_GetGeneralLogData(0, out enroll, out verify, out inOut, out year, out month, out day, out hour, out minute, out second, ref workCode))
            {
                DateTime time = new DateTime(year, month, day, hour, minute, second);
                record.Add(new OldRecord(enroll, time, verify, inOut));
            }
            setToTable();
        }

        public void setToTable()
        {
            foreach(OldRecord timesheet in record)
            {
                //home.recordTable.Rows.Add(timesheet.Enroll, timesheet.time, code[timesheet.AttState], machine);
            }
            //home.recordTable.FirstDisplayedScrollingRowIndex = home.recordTable.Rows.Count - 1;
        }

        public bool Connect()
        {
            return zk.Connect_Net(ipAddr, 4370);
        }
        public void closeConnection()
        {
            zk.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(onTransact);
            zk.Disconnect();
        }

        public void restartDevice()
        {
            zk.RestartDevice(1);
        }

    }
}
