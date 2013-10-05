using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem4OddNumber
{
    class Program
    {
        static void Main()
        {
            int N = int.Parse(Console.ReadLine());
            if (N == 1)
            {
                Console.WriteLine(Console.ReadLine());
            }
            else
            {
                Dictionary<long, int> numberCount = new Dictionary<long, int>();
                for (int i = 0; i < N; i++)
                {
                    var v = long.Parse(Console.ReadLine());
                    if (numberCount.ContainsKey(v))
                        numberCount[v]++;
                    else
                        numberCount.Add(v, 1);
                }

                foreach (var key in numberCount.Keys)
                {
                    if (numberCount[key] % 2 != 0)
                    {
                        Console.WriteLine(key);
                        break;
                    }
                }
            }
        }
    }
}
