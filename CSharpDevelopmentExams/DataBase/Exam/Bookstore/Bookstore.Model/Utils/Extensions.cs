using System;
using System.Collections.Generic;

namespace Bookstore.Model.Utils
{
    public static class Extensions
    {
        public static void ForEachStoppable<T>(this IEnumerable<T> input, Func<T, bool> action)
        {
            foreach (T t in input)
            {
                if (!action(t))
                {
                    break;
                }
            }
        }
    }
}
