using System;
using System.Linq;

namespace FindDuplicateNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = 344233432.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            var result = from num in numbers
                         group num by num
                            into g
                            select new
                            {
                                Key = g.Key,
                                Count = g.Count()
                            };
            foreach (var rem in result.OrderBy(n => n.Key))
            {
                Console.WriteLine("{0} -> {1} times", rem.Key, rem.Count);
            }
        }
    }
}
