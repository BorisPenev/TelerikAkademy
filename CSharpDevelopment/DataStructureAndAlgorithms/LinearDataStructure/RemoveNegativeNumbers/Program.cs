using System;
using System.Collections.Generic;

namespace RemoveNegativeNumbers
{
    class Program
    {
        static void Main()
        {
            var numbers = new List<int> {1, 4, -2, 8, 5, -1, 0, 3};
            numbers.RemoveAll(n => n < 0);
            Console.WriteLine(string.Join(",", numbers));
        }
    }
}
