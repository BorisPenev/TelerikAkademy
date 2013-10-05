using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommonTypes
{
    class BitArray64 : IEnumerable<int>
    {
        ulong[] values;
        public int Length
        {
            get { return values.Length; }
        }

        public BitArray64(ulong length)
        {
            values = new ulong[length];
        }

        public override bool Equals(object obj)
        {
            BitArray64 source = obj as BitArray64;
            if (source == null)
                return false;
            for (int i = 0; i < source.Length; i++)
            {
                if (this[i] != source[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            foreach (var v in values)
            {
                hash = (hash * 7) + v.GetHashCode();
            }
            hash = (hash * 7) + values.Length.GetHashCode();
            return hash;
        }

        public ulong this[int i]
        {
            get { return values[i]; }
            set { values[i] = value; }
        }

        public static bool operator ==(BitArray64 source, BitArray64 obj)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != obj[i])
                    return false;
            }
            return true;
        }

        public static bool operator !=(BitArray64 source, BitArray64 obj)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == obj[i])
                    return false;
            }
            return true;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this[i];
            }
        }

        //neshto ne razbiram tuk. ako gi mahna <int> mi kazva che nqmalo metod koyto se iska ot intefeisa.
        //v sashtoto vreme ne moje da sloja: return this.GetEnumerator();
        //i ko praim... nikakva ideq.
        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
