namespace BasicMathTests
{
    class AddTests
    {
        public static void AddInt(int startNumber, int step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber += step;
            }
        }

        public static void AddLong(long startNumber, long step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber += step;
            }
        }

        public static void AddFloat(float startNumber, float step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber += step;
            }
        }

        public static void AddDouble(double startNumber, double step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber += step;
            }
        }

        public static void AddDecimal(decimal startNumber, decimal step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber += step;
            }
        }
    }
}
