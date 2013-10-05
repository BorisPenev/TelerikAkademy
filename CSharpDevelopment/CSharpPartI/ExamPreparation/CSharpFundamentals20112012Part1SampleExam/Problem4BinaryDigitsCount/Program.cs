using System;
using System.Linq;

namespace Problem4BinaryDigitsCount
{
    class Program
    {
        static void Main()
        {
            byte b = byte.Parse(Console.ReadLine());
            ushort n = ushort.Parse(Console.ReadLine());
            uint source = 0;
            byte position = 0;
            byte count = 0;
            for (int i = 0; i < n; i++)
            {
                source = uint.Parse(Console.ReadLine());
                position = 0;
                count = 0;
                while (source > 0)
                {
                    if (source.GetBitByPosition(position) == b)
                        count++;
                    source = source.SetBitByPosition(position, true);
                    position++;
                }
                Console.WriteLine(count);
            }
        }
    }

    public static class Extensions
    {
        public static uint GetBitByPosition(this uint source, int position)
        {
            uint mask = (uint)1 << position;
            uint nAndMask = source & mask;
            return nAndMask >> position;
        }

        public static uint SetBitByPosition(this uint source, int position, bool isZero)
        {
            uint mask;
            if (isZero)
            {
                mask = ~((uint)1 << position);
                return source & mask;
            }
            else
            {
                mask = (uint)1 << position;
                return source | mask;
            }
        }
    }
}