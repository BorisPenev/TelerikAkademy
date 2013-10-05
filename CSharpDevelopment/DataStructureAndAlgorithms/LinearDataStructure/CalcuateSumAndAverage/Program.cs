using System;
using System.Collections.Generic;
using System.Linq;

namespace CalcuateSumAndAverage
{
    class Program
    {
        static void Main()
        {
            var numbers = new List<int>();
            Console.WriteLine("Enter a positive integers one per line.");

            while (true)
            {
                int n;
                if (int.TryParse(Console.ReadLine(), out n))
                {
                    numbers.Add(n);
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("The sum of numbers is: " + numbers.Sum());
            Console.WriteLine("The average of numbers is: " + numbers.Average());
        }
    }
}
