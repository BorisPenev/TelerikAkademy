using System;
using System.Linq;

namespace Problem1MathExpression
{
    class Program
    {
        static void Main()
        {
            decimal N = decimal.Parse(Console.ReadLine());
            decimal M = decimal.Parse(Console.ReadLine());
            decimal P = decimal.Parse(Console.ReadLine());
            Console.WriteLine(Math.Round((((N * N) + (1 / (M * P)) + 1337) / (N - 128.523123123M * P)) + (decimal)Math.Sin((int)M % 180), 6));
        }
    }
}
