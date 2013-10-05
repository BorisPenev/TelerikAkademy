using System;
using System.Collections.Generic;
using System.Linq;

namespace IEnumerableExtension
{
    public static class Extensions
    {
        public static T Sum<T>(this IEnumerable<T> source) where T : IComparable
        {
            dynamic result = 0;
            source.CheckType(() =>
                {
                    foreach (var s in source)
                    {
                        result += s;
                    }
                });
            return result;
        }

        public static T Min<T>(this IEnumerable<T> source) where T : IComparable
        {
            dynamic result = int.MaxValue;
            source.CheckType(() =>
                {
                    foreach (var s in source)
                    {
                        if (s < result)
                            result = s;
                    }
                });
            return result;
        }

        public static T Max<T>(this IEnumerable<T> source) where T : IComparable
        {
            dynamic result = int.MinValue;
            source.CheckType(() =>
                {
                    foreach (var s in source)
                    {
                        if (s > result)
                            result = s;
                    }
                });
            return result;
        }

        public static decimal Average<T>(this IEnumerable<T> source) where T : IComparable
        {
            dynamic result = 0;
            int counter = 0;
            source.CheckType(() =>
                {
                    foreach (var s in source)
                    {
                        result += s;
                        counter++;
                    }
                });
            return (decimal)result / counter;
        }

        //proverqvame tipa na T dali moje da se polzvat na[ite metodi. Ako e nqkakav custom type po-dobre tuk da hva]ame greshkata i da q obrabotvame.
        private static void CheckType<T>(this IEnumerable<T> source, Action act)
        {
            if(source.Count() > 0)
            {
                foreach (var src in source)
                {
                    if (src is int ||
                        src is double ||
                        src is decimal ||
                        src is float ||
                        src is byte ||
                        src is long)
                    {
                        if (act != null)
                            act();
                    }
                    else
                        throw new InvalidCastException("Ivalid type. We accept only: int, double, decimal, float, byte, long");
                    break;
                }
            }
        }
    }
}
