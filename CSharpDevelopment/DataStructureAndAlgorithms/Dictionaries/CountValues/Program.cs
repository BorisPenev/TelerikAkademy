using System;
using System.Collections.Generic;

namespace CountValues
{
    class Program
    {
        static void Main()
        {
            Dictionary<double, int> countValues = new Dictionary<double, int>();
            var array = new List<double>() {3, 4, 4, -2.5, 3, 3, 4, 3, -2.5};
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
                Console.WriteLine("{0} -> {1} times", countValue.Key, countValue.Value);
            }
        }
    }
}
