using System;
using System.Linq;

namespace RemoveOccuredOddNumber
{
    class Program
    {
        static void Main()
        {
            var numbers = 42252323152.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            var result = from num in numbers
                         group num by num
                         into g
                         where g.Count() % 2 != 0
                         select new
                             {
                                 Key = g.Key,
                                 Count = g.Count()
                             };
            foreach (var rem in result)
            {
                numbers.RemoveAll(n => n == rem.Key);
            }
            Console.WriteLine(string.Join(",", numbers));
        }
    }
}
