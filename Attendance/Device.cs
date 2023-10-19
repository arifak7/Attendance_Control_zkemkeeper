using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance
{
    internal class Device
    {
        zkemkeeper.CZKEMClass device;
        public Device(zkemkeeper.CZKEMClass zk)
        {
            device = zk;
        }

        public Boolean connect (String ipaddr)
        {
            return device.Connect_Net(ipaddr, 4370);
        }
    }
    internal class OldRecord
    {
        public String Enroll;
        public DateTime time;
        public int VerifyMethod;
        public int AttState;
        public OldRecord(String Enroll, DateTime time, int VerifyMethod, int AttState)
        {
            this.Enroll = Enroll;
            this.time = time;
            this.VerifyMethod = VerifyMethod;
            this.AttState = AttState;
        }
    }
}
