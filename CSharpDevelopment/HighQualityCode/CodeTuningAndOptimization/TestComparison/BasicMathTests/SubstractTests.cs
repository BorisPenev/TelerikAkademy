namespace BasicMathTests
{
    class SubstractTests
    {
        public static void SubstractInt(int startNumber, int step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber -= step;
            }
        }

        public static void SubstractLong(long startNumber, long step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber -= step;
            }
        }

        public static void SubstractFloat(float startNumber, float step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber -= step;
            }
        }

        public static void SubstractDouble(double startNumber, double step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber -= step;
            }
        }

        public static void SubstractDecimal(decimal startNumber, decimal step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber -= step;
            }
        }
    }
}
