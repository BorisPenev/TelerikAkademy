using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem5SubsetSums
{
    class Program
    {
        static int count = 0;
        static void Main()
        {
            List<long> numbers = new List<long>();
            long s = long.Parse(Console.ReadLine());
            byte n = byte.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
                numbers.Add(long.Parse(Console.ReadLine()));

            for (int i = 0; i < numbers.Count; i++)
            {
                long temp = numbers[i];
                if (temp == s)
                    count++;

                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == s)
                        count++;
                    temp += numbers[j];
                    if (j >= 1 && temp == s)
                        count++;
                }
            }
            Console.WriteLine(count);
        }
    }
}