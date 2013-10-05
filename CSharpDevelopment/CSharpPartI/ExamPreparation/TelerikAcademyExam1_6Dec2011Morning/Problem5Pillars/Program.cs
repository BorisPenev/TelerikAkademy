using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem5Pillars
{
    class Program
    {
        static void Main()
        {
            bool isResult = false;
            List<int> numberList = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                numberList.Add(int.Parse(Console.ReadLine()));
            }

            int[] results = new int[8];
            for (int i = 0; i < numberList.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (numberList[i].GetBitByPosition(j) == 1)
                        results[j]++;
                }
            }

            for (int i = results.Length - 1; i >= 0 ; i--)
            {
                int sumLeft = 0;
                for (int j = results.Length - 1; j > i; j--) 
                    sumLeft += results[j];

                int sumRight = 0;
                for (int j = 0; j < i; j++)
                    sumRight += results[j];

                if (sumLeft == sumRight)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(sumRight);
                    isResult = true;
                    break;
                }
            }
            if(!isResult)
                Console.WriteLine("No");
        }
    }

    public static class Extensions
    {
        public static int GetBitByPosition(this int source, int position)
        {
            int mask = 1 << position;
            int nAndMask = source & mask;
            return nAndMask >> position;
        }
    }
}
