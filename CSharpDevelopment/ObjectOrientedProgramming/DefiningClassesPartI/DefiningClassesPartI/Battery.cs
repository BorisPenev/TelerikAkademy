using System;
using System.Linq;

namespace DefiningClassesPartI
{
    public class Battery
    {
        private BatteryType batteryType = BatteryType.LiIon;
        public BatteryType BatteryType
        {
            get { return batteryType; }
            set { batteryType = value; }
        }

        private int hoursIdle = 0;
        public int HoursIdle
        {
            get { return hoursIdle; }
            set { hoursIdle = value; }
        }

        private int hoursTalk = 0;
        public int HoursTalk
        {
            get { return hoursTalk; }
            set { hoursTalk = value; }
        }
    }
}
