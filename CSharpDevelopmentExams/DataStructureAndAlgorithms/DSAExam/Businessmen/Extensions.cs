using System.Numerics;

namespace Businessmen
{
    public static class Extensions
    {
        public static BigInteger GetFactoriel(this int source)
        {
            BigInteger result = 1;
            if (source != 0)
            {
                for (int i = 1; i <= source; i++)
                {
                    result *= i;
                }
            }
            return result;
        }
    }
}
