using System;
using System.Linq;

namespace HowOldYouAreAfter10Years
{
    class HowOldYouAreAfter10Years
    {
        static void Main()
        {
            int age;
            Console.WriteLine("Please enter your age:");
            if (int.TryParse(Console.ReadLine(), out age))
                Console.WriteLine("After 10 years you will be on: " + (age + 10));
            else
            {
                Console.WriteLine("You not enter a correct age. Click any key to try again.");
                Console.ReadKey();
                Console.Clear();
                Main();
            }
            Console.ReadKey();
        }
    }
}
