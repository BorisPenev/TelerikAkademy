using System;

namespace AdvancedMathTests
{
    class SqrtTests
    {
        public static void SqrtInt(int startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sqrt(startNumber);
            }
        }

        public static void SqrtLong(long startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sqrt(startNumber);
            }
        }

        public static void SqrtFloat(float startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sqrt(startNumber);
            }
        }

        public static void SqrtDouble(double startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sqrt(startNumber);
            }
        }
    }
}
