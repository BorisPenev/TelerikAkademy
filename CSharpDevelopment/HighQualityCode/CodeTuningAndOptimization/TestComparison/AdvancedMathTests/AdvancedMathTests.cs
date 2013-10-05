namespace AdvancedMathTests
{
    class AdvancedMathTests
    {
        static void Main()
        {
            LogTests.LogDouble(256d, 10000000);
            LogTests.LogFloat(256f, 10000000);
            LogTests.LogInt(256, 10000000);
            LogTests.LogLong(256L, 10000000);

            SinTests.SinDouble(256d, 10000000);
            SinTests.SinFloat(256f, 10000000);
            SinTests.SinInt(256, 10000000);
            SinTests.SinLong(256L, 10000000);

            SqrtTests.SqrtDouble(256d, 10000000);
            SqrtTests.SqrtFloat(256f, 10000000);
            SqrtTests.SqrtInt(256, 10000000);
            SqrtTests.SqrtLong(256L, 10000000);
        }
    }
}
