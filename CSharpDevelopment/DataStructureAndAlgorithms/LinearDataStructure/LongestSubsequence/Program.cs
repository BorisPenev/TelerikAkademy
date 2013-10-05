using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestSubsequence
{
    public class Program
    {
        static void Main()
        {
            var numbers = 2444436522243355555.ToString().ToArray().Select(s => int.Parse(s.ToString())).ToList();
            GetLongestSeubsequence(numbers);
        }

        public static List<int> GetLongestSeubsequence(List<int> numbers)
        {
            var result = new List<int>();
            var temp = new List<int>();

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i - 1] == numbers[i])
                {
                    temp.Add(numbers[i - 1]);
                    if (i == numbers.Count - 1)
                    {
                        temp.Add(numbers[i - 1]);
                        if (temp.Count > result.Count)
                        {
                            result = temp;
                        }
                    }
                }
                else
                {
                    if (temp.Count > 1)
                    {
                        temp.Add(numbers[i - 1]);
                    }

                    if (temp.Count > result.Count)
                    {
                        result = temp;
                    }
                    temp = new List<int>();
                }
            }

            return result;
        }
    }
}
