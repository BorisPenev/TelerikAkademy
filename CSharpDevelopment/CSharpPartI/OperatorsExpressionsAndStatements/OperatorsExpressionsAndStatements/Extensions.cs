using System;
using System.Drawing;
using System.Linq;

namespace OperatorsExpressionsAndStatements
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

        public static bool IsInCircle(this Point source, int centerX, int centerY, int radius)
        {
            double result = Math.Sqrt(source.X - centerX) + Math.Sqrt(source.Y - centerY);
            if (result <= radius)
                return true;
            return false;
        }

        public static bool IsInRectangle(this Point source, Rectangle rect)
        {
            if ((rect.X <= source.X && source.X <= rect.Width) && (rect.Y <= source.Y  && source.Y <= rect.Height))
                return true;
            return false;
        }

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
    }
}
