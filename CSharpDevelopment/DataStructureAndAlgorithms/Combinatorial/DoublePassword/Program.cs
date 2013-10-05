using System;
using System.Linq;

namespace DoublePassword
{
    class Program
    {
        static void Main()
        {
            string str = Console.ReadLine();
            var asteriks = str.Count(c => c == '*');
            if (asteriks == 0)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(Math.Pow(2, asteriks));
            }
        }
    }
}
