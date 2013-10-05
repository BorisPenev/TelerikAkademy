using System;
using System.Linq;

namespace Problem3ForestRoad
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0, j = n - 1; i < n - 1; i++, j--)
                Console.WriteLine(new string('.', i) + "*" + new string('.', j));
            for (int i = 0, j = n - 1; i < n; i++, j--)
                Console.WriteLine(new string('.', j) + "*" + new string('.', i));
        }
    }
}