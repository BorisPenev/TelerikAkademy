using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace ImplementBiDictionary
{
    class BiDictionary<TKey1, TKey2, TValue>
    {
        private MultiDictionary<TKey1, TValue> key1 = new MultiDictionary<TKey1, TValue>(true);
        private MultiDictionary<TKey2, TValue> key2 = new MultiDictionary<TKey2, TValue>(true);

    }
}
