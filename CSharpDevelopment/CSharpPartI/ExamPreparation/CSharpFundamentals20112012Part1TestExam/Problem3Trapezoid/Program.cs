using System;
using System.Linq;

namespace Problem3Trapezoid
{
    class Program
    {
        static void Main()
        {
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine(new string('.', N) + new string('*', N));
            for (int i = N - 1, j = i; i > 0; i--, j++)
                Console.WriteLine(new string('.', i) + "*" + new string('.', j) + "*");
            Console.WriteLine(new string('*', N * 2));
        }
    }
}