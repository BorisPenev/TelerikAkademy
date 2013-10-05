using System;
using System.Linq;

namespace Problem4WeAllLoveBits
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                int p = int.Parse(Console.ReadLine());
                int pNew = (p ^ (~p)) & p.ReverseBits();
                Console.WriteLine(pNew);
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

        public static int ReverseBits(this int source)
        {
            int result = 0;
            int position = 0;
            while (source > 0)
            {
                result <<= 1;
                result = result.SetBitByPosition(0, source.GetBitByPosition(position) == 0 ? true : false);
                source = source.SetBitByPosition(position, true);
                position++;
            }
            return result;
        }
    }
}