namespace BasicMathTests
{
    class DivideTests
    {
        public static void DivideInt(int startNumber, int step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber /= step;
            }
        }

        public static void DivideLong(long startNumber, long step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber /= step;
            }
        }

        public static void DivideFloat(float startNumber, float step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber /= step;
            }
        }

        public static void DivideDouble(double startNumber, double step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber /= step;
            }
        }

        public static void DivideDecimal(decimal startNumber, decimal step, int duration)
        {
            for (var i = 0; i < duration; i++)
            {
                startNumber /= step;
            }
        }
    }
}
