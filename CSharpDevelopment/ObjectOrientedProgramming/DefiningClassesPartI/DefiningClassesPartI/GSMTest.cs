using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClassesPartI
{
    class GSMTest
    {
        static void Main()
        {
            List<GSM> GSMList = new List<GSM>();
            GSMList.Add(new GSM(Models.LG, ManufacturerType.Default));
            GSMList.Add(new GSM(Models.Nokia, ManufacturerType.Other, 12, null, null));
            GSMList.Add(new GSM(Models.Samsung, ManufacturerType.Default) { Owner = "Dqdo Mraz" });
            GSMList.ForEach(g =>
                {
                    Console.WriteLine(g.ToString());
                });
            Console.WriteLine(GSM.IPhone4S);

            GSMCallHistoryTest historyTest = new GSMCallHistoryTest();
            historyTest.Test();
        }
    }
}
