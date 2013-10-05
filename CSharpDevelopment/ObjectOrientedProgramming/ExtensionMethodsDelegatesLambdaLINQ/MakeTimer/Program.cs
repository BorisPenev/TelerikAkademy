using System;
using System.Linq;
using System.Threading;

namespace MakeTimer
{
    class Program
    {
        // Ima malak bug tuka. Sled kato se spre parviq timer asinhronnata zaqvka prodaljava oshte malko.
        // Ako ima nqkoy jelanie da si poigrae da nameri kak se spira vednaha.
        // Tarsih v neta... no ne mi se zanimava poveche po tova da otdelqm vreme.
        // Taka ili inache e poveche ot iskanoto ot zadachata a i nikoy nqma da polzva tozi taimer sled kato ima gotov :)
        static void Main(string[] args)
        {
            // Create a timer with a ten second interval.
            Timer t = new Timer(10000);
            // Set the Interval to 2 seconds (2000 milliseconds).
            t.Interval = 2000;
            t.ExecuteCommand = TestTimer;
            t.Start();

            //add a work in main thread to be sure the timer work in a second thread and not block this.
            for (int i = 0; i < 55; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i);
                if(i == 44) // if we hit number 44 time will stop to print the existing date. But main thread will continue to work
                    t.Stop();
            }

            //add event
            t.Elapsed += t_Elapsed;
            t.StartEvent(); //run the event
            for (int i = 0; i < 155; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i);
            }
        }

        static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("run from event: " + DateTime.Now);
        }

        public static void TestTimer()
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}