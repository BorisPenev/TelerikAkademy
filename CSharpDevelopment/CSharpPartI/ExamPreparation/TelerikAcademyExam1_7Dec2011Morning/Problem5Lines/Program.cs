using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem5Lines
{
    class Program
    {
        static void Main()
        {
            int count = 0;
            List<int> numbers = new List<int>();
            Dictionary<int, int> resultDict = new Dictionary<int, int>();
            for (int i = 0; i < 8; i++)
            {
                numbers.Add(int.Parse(Console.ReadLine()));
                for (int j = 0; j < 8; j++)
                {
                    if (numbers[i].GetBitByPosition(j) == 1)
                    {
                        count++;
                        if (numbers[i].GetBitByPosition(j + 1) == 0)
                        {
                            if (resultDict.ContainsKey(count))
                                resultDict[count]++;
                            else
                                resultDict.Add(count, 1);
                            count = 0;
                        }
                    }
                }
            }

            for (int col = 0; col < 8; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if (numbers[row].GetBitByPosition(col) == 1)
                    {
                        count++;
                        if (row + 1 >= numbers.Count || numbers[row + 1].GetBitByPosition(col) == 0)
                        {
                            if (resultDict.ContainsKey(count))
                                resultDict[count]++;
                            else
                                resultDict.Add(count, 1);
                            count = 0;
                        }
                    }
                }
            }

            if (resultDict.Count > 1)
            {
                var result = resultDict.Where(r => r.Key != 1).OrderByDescending(r => r.Key).ThenBy(r => r.Value).FirstOrDefault();
                Console.WriteLine(result.Key);
                Console.WriteLine(result.Value);
            }
            else
            {
                var result = resultDict.FirstOrDefault();
                Console.WriteLine(result.Key);
                if(result.Key == 1)
                    Console.WriteLine(result.Value / 2);
                else
                    Console.WriteLine(result.Value);
            }
        }
    }

    public static class Extensions
    {
        public static int GetBitByPosition(this int source, int position)
        {
            int mask = (int)1 << position;
            int nAndMask = source & mask;
            return nAndMask >> position;
        }

        public static int SetBitByPosition(this int source, int position, bool isZero)
        {
            int mask;
            if (isZero)
            {
                mask = ~((int)1 << position);
                return source & mask;
            }
            else
            {
                mask = (int)1 << position;
                return source | mask;
            }
        }
    }
}
