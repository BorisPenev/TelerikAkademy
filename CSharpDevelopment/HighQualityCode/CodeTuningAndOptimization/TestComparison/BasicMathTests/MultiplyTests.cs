using System;

namespace BasicMathTests
{
    class MultiplyTests
    {
        public static void MultiplyInt(int startNumber, int step, int duration)
        {
            int result = 0;
            for (var i = 0; i < duration; i++)
            {
                result = startNumber * step;
            }
        }

        public static void MultiplyLong(long startNumber, long step, int duration)
        {
            long result = 0;
            for (var i = 0; i < duration; i++)
            {
                result = startNumber * step;
            }
        }

        public static void MultiplyFloat(float startNumber, float step, int duration)
        {
            float result = 0;
            for (var i = 0; i < duration; i++)
            {
                result = startNumber * step;
            }
        }

        public static void MultiplyDouble(double startNumber, double step, int duration)
        {
            double result = 0;
            for (var i = 0; i < duration; i++)
            {
                result = startNumber * step;
            }
        }

        public static void MultiplyDecimal(decimal startNumber, decimal step, int duration)
        {
            decimal result = 0;
            for (var i = 0; i < duration; i++)
            {
                result = startNumber * step;
            }
        }
    }
}
