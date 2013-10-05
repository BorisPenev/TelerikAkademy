using System;
using System.Linq;

namespace Problem3FirTree
{
    class Program
    {
        static void Main()
        {
            int N = int.Parse(Console.ReadLine());
            for (int i = 0, j = N - 2; i < N - 1; i++, j--)
                Console.WriteLine(new string('.', j) + new string('*', i * 2 + 1) + new string('.', j));
            Console.WriteLine(new string('.', N - 2) + "*" + new string('.', N - 2));
        }
    }
}
