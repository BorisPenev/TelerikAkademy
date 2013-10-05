using System;
using System.Linq;

namespace DefiningClassesPartI
{
    public class GSMCallHistoryTest
    {
        public void Test()
        {
            GSM g = new GSM(Models.LG, ManufacturerType.Default);
            g.Add(new Call() { DialedPhoneNumber = "934732592", DTime = new DateTime(2013, 01, 01), DurationInSeconds = 600 });
            g.Add(new Call() { DialedPhoneNumber = "93473259234", DTime = new DateTime(2013, 01, 02), DurationInSeconds = 50 });
            g.Add(new Call() { DialedPhoneNumber = "4543645592", DTime = new DateTime(2013, 01, 03), DurationInSeconds = 7543 });
            Call gs = g.CallHistory[0];
            gs.DialedPhoneNumber = "111111111111";
            g.Add(gs);

            Console.WriteLine(g.TotalPrice(0.37));
            Call call = g.CallHistory.OrderBy(c => c.DurationInSeconds).FirstOrDefault();
            if (call != null)
            {
                g.Remove(call);
                Console.WriteLine(g.TotalPrice(0.37));
            }
            g.Clear();
            Console.WriteLine("Finally clear the call history and print it." + Environment.NewLine + "Kakvo trqbva da printna? Nali sam q izchistil.");
        }
    }
}
