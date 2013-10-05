using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentSystem
{
    public static class Extensions
    {
        public static bool AddSafe(this List<Document> l, Document value)
        {
            if(string.IsNullOrEmpty(value.Name))
                return false;
            l.Add(value);
            return true;
        }
    }
}
