using System;
using System.Collections.Generic;

namespace OddNumberTimes
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new List<string>() {"C#", "SQL", "PHP", "PHP", "SQL", "SQL"};
            Dictionary<string, int> countValues = new Dictionary<string, int>();
            foreach (var val in array)
            {
                if (countValues.ContainsKey(val))
                {
                    countValues[val]++;
                }
                else
                {
                    countValues.Add(val, 1);
                }
            }

            foreach (var countValue in countValues)
            {
                if (countValue.Value%2 != 0)
                {
                    Console.WriteLine(countValue.Key);    
                }
            }
        }
    }
}
