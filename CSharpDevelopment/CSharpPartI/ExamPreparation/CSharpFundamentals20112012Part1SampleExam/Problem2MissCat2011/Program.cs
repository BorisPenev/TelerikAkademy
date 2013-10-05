using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem2MissCat2011
{
    class Program
    {
        static readonly Dictionary<int, int> numDict = new Dictionary<int, int>();
        static int number = 0;

        static void Main()
        {
            number = int.Parse(Console.ReadLine());
            for (int i = 0; i < number; i++)
            {
                var n = int.Parse(Console.ReadLine());
                if (numDict.ContainsKey(n))
                    numDict[n]++;
                else
                    numDict.Add(n, 1);
            }
            Console.WriteLine(numDict.OrderByDescending(n => n.Value).ThenBy(n => n.Key).First().Key);
        }
    }
}
