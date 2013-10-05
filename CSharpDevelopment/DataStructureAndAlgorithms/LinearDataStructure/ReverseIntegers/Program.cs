using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseIntegers
{
    class Program
    {
        static void Main()
        {
            var numbers = new Stack<int>();
            Console.WriteLine("Enter a positive integers one per line.");

            while (true)
            {
                int n;
                if (int.TryParse(Console.ReadLine(), out n))
                {
                    numbers.Push(n);
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Result:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
