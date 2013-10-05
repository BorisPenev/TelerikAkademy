using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostCommon
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif

            Dictionary<string, int> firstName = new Dictionary<string, int>();
            Dictionary<string, int> lastName = new Dictionary<string, int>();
            Dictionary<string, int> year = new Dictionary<string, int>();
            Dictionary<string, int> eyeColor = new Dictionary<string, int>();
            Dictionary<string, int> hairColor = new Dictionary<string, int>();
            Dictionary<string, int> height = new Dictionary<string, int>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(new Char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (!firstName.ContainsKey(input[0]))
                {
                    firstName.Add(input[0], 0);
                }
                firstName[input[0]]++;

                if (!lastName.ContainsKey(input[1]))
                {
                    lastName.Add(input[1], 0);
                }
                lastName[input[1]]++;

                if (!year.ContainsKey(input[2]))
                {
                    year.Add(input[2], 0);
                }
                year[input[2]]++;

                if (!eyeColor.ContainsKey(input[3]))
                {
                    eyeColor.Add(input[3], 0);
                }
                eyeColor[input[3]]++;

                if (!hairColor.ContainsKey(input[4]))
                {
                    hairColor.Add(input[4], 0);
                }
                hairColor[input[4]]++;

                if (!height.ContainsKey(input[5]))
                {
                    height.Add(input[5], 0);
                }
                height[input[5]]++;
            }

            Console.WriteLine(Search(firstName));
            Console.WriteLine(Search(lastName));
            Console.WriteLine(Search(year));
            Console.WriteLine(Search(eyeColor));
            Console.WriteLine(Search(hairColor));
            Console.WriteLine(Search(height));
        }

        static string Search(Dictionary<string, int> dictionary)
        {
            string result = string.Empty;
            int max = Int16.MinValue;

            foreach (var item in dictionary)
            {
                if (item.Value > max)
                {
                    result = item.Key;
                    max = item.Value;
                }
                if (item.Value == max && result.CompareTo(item.Key) > 0)
                {
                    result = item.Key;
                }
            }

            return result;

        }
    }
}
