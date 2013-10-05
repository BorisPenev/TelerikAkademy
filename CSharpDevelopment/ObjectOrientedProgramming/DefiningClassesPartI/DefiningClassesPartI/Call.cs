using System;
using System.Linq;

namespace DefiningClassesPartI
{
    public class Call
    {
        private Guid callID = Guid.NewGuid();
        public Guid CallID
        {
            get { return callID; }
            //set { callID = value; }
        }

        private DateTime dTime = DateTime.Now;
        public DateTime DTime
        {
            get { return dTime; }
            set { dTime = value; }
        }

        private string dialedPhoneNumber = string.Empty;
        public string DialedPhoneNumber
        {
            get { return dialedPhoneNumber; }
            set { dialedPhoneNumber = value; }
        }

        private int durationInSeconds = 0;
        public int DurationInSeconds
        {
            get { return durationInSeconds; }
            set { durationInSeconds = value; }
        }
    }
}
