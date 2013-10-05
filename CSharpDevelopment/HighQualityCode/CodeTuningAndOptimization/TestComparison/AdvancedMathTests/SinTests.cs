using System;

namespace AdvancedMathTests
{
    class SinTests
    {
        public static void SinInt(int startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sin(startNumber);
            }
        }

        public static void SinLong(long startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sin(startNumber);
            }
        }

        public static void SinFloat(float startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sin(startNumber);
            }
        }

        public static void SinDouble(double startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Sin(startNumber);
            }
        }
    }
}
