using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem2AstrologicalDigits
{
    class Program
    {
        static void Main()
        {
            var n = Console.ReadLine().ToCharArray().ToList();
            n.Remove('-');
            n.Remove('.');
            var sum = n.Sum(s => long.Parse(s.ToString()));
            SumOfDigits(ref n, ref sum);
        }

        private static void SumOfDigits(ref List<char> n, ref long sum)
        {
            if (sum > 9)
            {
                n = sum.ToString().ToCharArray().ToList();
                sum = n.Sum(s => long.Parse(s.ToString()));
                SumOfDigits(ref n, ref sum);
            }
            else
                Console.WriteLine(sum);
        }
    }
}
