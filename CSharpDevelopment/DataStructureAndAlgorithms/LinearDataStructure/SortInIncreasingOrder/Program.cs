using System;
using System.Collections.Generic;
using System.Linq;

namespace SortInIncreasingOrder
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

            Console.WriteLine(string.Join(Environment.NewLine, numbers.OrderBy(n => n)));
        }
    }
}
