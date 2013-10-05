using System;
using System.Collections.Generic;

namespace IEnumerableExtension
{
    class Program
    {
        static void Main()
        {
            //Implement a set of extension methods for IEnumerable<T> that implement the following group functions: sum, product, min, max, average.

            List<int> stringList = new List<int>();
            stringList.Add(5);
            stringList.Add(6);
            Console.WriteLine(stringList.Sum());
            Console.WriteLine(stringList.Min());
            Console.WriteLine(stringList.Max());
            Console.WriteLine(stringList.Average());
        }
    }
}
