using System;

namespace AdvancedMathTests
{
    class LogTests
    {
        public static void LogInt(int startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Log(startNumber);
            }
        }

        public static void LogLong(long startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Log(startNumber);
            }
        }

        public static void LogFloat(float startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Log(startNumber);
            }
        }

        public static void LogDouble(double startNumber, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                Math.Log(startNumber);
            }
        }
    }
}
