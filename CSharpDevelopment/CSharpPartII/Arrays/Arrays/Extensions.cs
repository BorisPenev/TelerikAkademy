using System;
using System.Collections.Generic;
using System.Linq;

namespace Arrays
{
    public static class Extensions
    {
        public static IEnumerable<T> MergePreserveOrder<T, TOrder>(this IEnumerable<IEnumerable<T>> source, Func<T, TOrder> orderFunc) where TOrder : IComparable<TOrder>
        {
            var items = source.Select(xx => xx.GetEnumerator())
                          .Where(s => s.MoveNext())
                          .Select(s => Tuple.Create(orderFunc(s.Current), s))
                          .OrderBy(s => s.Item1).ToList();

            while (items.Count > 0)
            {
                yield return items[0].Item2.Current;

                var next = items[0];
                items.RemoveAt(0);
                if (next.Item2.MoveNext())
                {
                    var value = orderFunc(next.Item2.Current);
                    var ii = 0;
                    for (; ii < items.Count; ++ii)
                    {
                        if (value.CompareTo(items[ii].Item1) <= 0)
                        {
                            items.Insert(ii, Tuple.Create(value, next.Item2));
                            break;
                        }
                    }

                    if (ii == items.Count) items.Add(Tuple.Create(value, next.Item2));
                }
                else next.Item2.Dispose();
            }
        }

        public static IEnumerable<T> Quicksort<T>(this IEnumerable<T> source) where T : IComparable<T>
        {
            if (!source.Any())
                return source;
            var pivot = source.First();
            var remaining = source.Skip(1);
            return remaining
                .Where(a => a.CompareTo(pivot) <= 0).Quicksort()
                .Concat(new[] { pivot })
                .Concat(remaining.Where(a => a.CompareTo(pivot) > 0).Quicksort());
        }
    }
}
