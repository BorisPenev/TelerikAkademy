using System.Collections.Generic;

namespace Salaries
{
    public static class Extensions
    {
        public static void AddSafe<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, TValue value)
        {
            if (!d.ContainsKey(key))
            {
                d.Add(key, value);
            }
        }
    }
}
