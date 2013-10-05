using System;
using System.Linq;
using System.Numerics;

namespace Problem4DancingBits
{
    class Program
    {
        static void Main()
        {
            int result = 0;
            BigInteger source = 0;
            int count = 0;
            int countK = 0;
            BigInteger temp = 0;
            BigInteger workingNumber = 0;
            int k = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                source = BigInteger.Parse(Console.ReadLine());
                switch (i)
                {
                    case 0:
                        workingNumber = source;
                        break;
                    default:
                        temp = source;
                        count = 0;
                        while (source > 0)
                        {
                            source = source.SetBitByPosition(count, true);
                            count++;
                        }
                        workingNumber = workingNumber << count;
                        count = 0;
                        while (temp > 0)
                        {
                            workingNumber = workingNumber.SetBitByPosition(count, temp.GetBitByPosition(count) == 0 ? true : false);
                            temp = temp.SetBitByPosition(count, true);
                            count++;
                        }
                        break;
                }
            }

            temp = workingNumber;
            count = 0;
            while (temp > 0)
            {
                temp = temp.SetBitByPosition(count, true);
                count++;
            }

            for (int i = 0; i < count; i++)
            {
                countK++;
                if (workingNumber.GetBitByPosition(i) != workingNumber.GetBitByPosition(i + 1) || i + 1 >= count)
                {
                    if (countK == k)
                        result++;
                    countK = 0;
                }
            }
            Console.WriteLine(result);
        }
    }

    public static class Extensions
    {
        public static BigInteger GetBitByPosition(this BigInteger source, int position)
        {
            BigInteger mask = (BigInteger)1 << position;
            BigInteger nAndMask = source & mask;
            return nAndMask >> position;
        }

        public static BigInteger SetBitByPosition(this BigInteger source, int position, bool isZero)
        {
            BigInteger mask;
            if (isZero)
            {
                mask = ~((BigInteger)1 << position);
                return source & mask;
            }
            else
            {
                mask = (BigInteger)1 << position;
                return source | mask;
            }
        }
    }
}
