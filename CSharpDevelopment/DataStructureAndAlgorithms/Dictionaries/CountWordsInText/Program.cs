using System;
using System.Collections.Generic;
using System.Linq;

namespace CountWordsInText
{
    class Program
    {
        static void Main()
        {
            string[] str =
                "This is the TEXT. Text, text, text - THIS TEXT! Is this the text?".ToLower().Split(
                    new Char[] {' ', ',', '.', '-', '?', '!'}, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> countValues = new Dictionary<string, int>();
            foreach (var s in str)
            {
                if (countValues.ContainsKey(s))
                {
                    countValues[s]++;
                }
                else
                {
                    countValues.Add(s, 1);
                }
            }

            foreach (var countValue in countValues.OrderBy(d => d.Value))
            {
                Console.WriteLine("{0} -> {1}", countValue.Key, countValue.Value);
            }
        }
    }
}
