namespace BasicMathTests
{
    class BasicMathTests
    {
        static void Main()
        {
            AddTests.AddDecimal(26m, 9m, 10000000);
            AddTests.AddDouble(26d, 9d, 10000000);
            AddTests.AddFloat(26f, 9f, 10000000);
            AddTests.AddInt(26, 9, 10000000);
            AddTests.AddLong(26L, 9L, 10000000);

            DivideTests.DivideDecimal(26m, 9m, 10000000);
            DivideTests.DivideDouble(26d, 9d, 10000000);
            DivideTests.DivideFloat(26f, 9f, 10000000);
            DivideTests.DivideInt(26, 9, 10000000);
            DivideTests.DivideLong(26L, 9L, 10000000);

            MultiplyTests.MultiplyDecimal(26m, 9m, 10000000);
            MultiplyTests.MultiplyDouble(26d, 9d, 10000000);
            MultiplyTests.MultiplyFloat(26f, 9f, 10000000);
            MultiplyTests.MultiplyInt(26, 9, 10000000);
            MultiplyTests.MultiplyLong(26L, 9L, 10000000);

            SubstractTests.SubstractDecimal(26m, 9m, 10000000);
            SubstractTests.SubstractDouble(26d, 9d, 10000000);
            SubstractTests.SubstractFloat(26f, 9f, 10000000);
            SubstractTests.SubstractInt(26, 9, 10000000);
            SubstractTests.SubstractLong(26L, 9L, 10000000);
        }
    }
}
