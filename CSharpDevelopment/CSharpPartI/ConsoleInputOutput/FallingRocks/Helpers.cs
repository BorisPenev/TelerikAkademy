using System;
using System.Collections.Generic;
using System.Linq;

namespace FallingRocks
{
    static class Helpers
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public static sbyte RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return Convert.ToSByte(random.Next(min, max));
            }
        }

        public static bool ForEach<T>(this IEnumerable<T> sequence, Func<T, bool> action)
        {
            bool result = false;
            foreach (var item in sequence)
            {
                result = action(item);
                if (result)
                    break;
            }
            return result;
        }
    }
}
