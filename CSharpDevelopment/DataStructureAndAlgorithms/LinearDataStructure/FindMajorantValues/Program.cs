using System;
using System.Linq;

namespace FindMajorantValues
{
    class Program
    {
        static void Main()
        {
            var numbers = 223323433.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            var result = from num in numbers
                         group num by num
                             into g
                             where g.Count() >= numbers.Count() / 2 + 1
                             select new
                             {
                                 Key = g.Key,
                                 Count = g.Count()
                             };

            if (result.Count() == 0)
            {
                Console.WriteLine("No majorant value found");
            }
            else
            {
                foreach (var rem in result.OrderBy(n => n.Key))
                {
                    Console.WriteLine("Majorant Value: {0} -> {1} times", rem.Key, rem.Count);
                }
            }
        }
    }
}
