using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheHiddenTruth.Library.Utils
{
    public static class Extensions
    {
        public static void AddSafe<TKey, TValue>(this IDictionary<TKey, TValue> d, TKey key, TValue value)
        {
            if (!d.ContainsKey(key))
            {
                d.Add(key, value);
            }
        }

        public static int ToInt(this string str)
        {
            int num;
            if (str != null && int.TryParse(str, out num))
                return num;
            return int.MinValue;
        }

        public static int ToInt(this string str, int defaultValue)
        {
            int num;
            if (str != null && int.TryParse(str, out num))
                return num;
            return defaultValue;
        }

        public static int ToInt(this object str)
        {
            int num;
            if (str != null && int.TryParse(str.ToString(), out num))
                return num;
            return int.MinValue;
        }
    }
}
