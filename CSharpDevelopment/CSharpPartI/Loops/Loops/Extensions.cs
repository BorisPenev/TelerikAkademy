using System;
using System.Linq;
using System.Numerics;

namespace Loops
{
    public static class Extensions
    {
        public static bool IsFloatPrecision(this float source, float compare, float precision)
        {
            if (((source - precision) < compare) && ((source + precision) > compare))
                return true;
            return false;
        }

        public static bool IsEven(this int source)
        {
            if (source % 2 == 0)
                return true;
            else
                return false;
        }

        #region Bitwise Extensions

        public static int GetBitByPosition(this int source, int position)
        {
            int mask = 1 << position;
            int nAndMask = source & mask;
            return nAndMask >> position;
        }

        public static int SetBitByPosition(this int source, int position, bool isNotZero)
        {
            int mask;
            if (isNotZero)
            {
                mask = 1 << position;
                return source | mask;
            }
            else
            {
                mask = ~(1 << position);
                return source & mask;
            }
        }

        public static string ToBitString(this int source, int bit)
        {
            return Convert.ToString(source, 2).PadLeft(bit, '0');
        }

        #region UINT Bitwise Extensions

        public static uint GetBitByPosition(this uint source, int position)
        {
            uint mask = (uint)1 << position;
            uint nAndMask = source & mask;
            return nAndMask >> position;
        }

        public static uint SetBitByPosition(this uint source, int position, bool isNotZero)
        {
            uint mask;
            if (isNotZero)
            {
                mask = (uint)1 << position;
                return source | mask;
            }
            else
            {
                mask = ~((uint)1 << position);
                return source & mask;
            }
        }

        public static string ToBitString(this uint source, int bit)
        {
            return Convert.ToString(source, 2).PadLeft(bit, '0');
        }

        #endregion

        #endregion

        public static bool IsPrime(this int source)
        {
            if (source <= 1)
                return false;
            for (int i = 2; i < Math.Sqrt(source); i++)
            {
                if (source % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ToBool(this string str)
        {
            int i = 0;
            bool b = false;
            if (int.TryParse(str, out i))
                return i == 1;
            else if (bool.TryParse(str, out b))
                return b;
            return b;
        }

        public static BigInteger GetFactoriel(this int source)
        {
            BigInteger result = 1;
            if (source != 0)
            {
                for (int i = 1; i <= source; i++)
                {
                    result *= i;
                }
            }
            return result;
        }
    }
}
