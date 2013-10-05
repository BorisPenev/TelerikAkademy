using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem5FallDown
{
    class Program
    {
        static void Main()
        {
            List<int> numberList = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                numberList.Add(int.Parse(Console.ReadLine()));
            }

            for (int i = numberList.Count - 2; i >=0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = numberList.Count - 1; k >= 0; k--)
                    {
                        if (numberList[i].GetBitByPosition(j) == 1 && numberList[k].GetBitByPosition(j) == 0)
                        {
                            numberList[i] = numberList[i].SetBitByPosition(j, true);
                            numberList[k] = numberList[k].SetBitByPosition(j, false);
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(numberList[i]);
            }
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

        public static int SetBitByPosition(this int source, int position, bool isZero)
        {
            int mask;
            if (isZero)
            {
                mask = ~(1 << position);
                return source & mask;
            }
            else
            {
                mask = 1 << position;
                return source | mask;
            }
        }
    }
}
