using System;
using System.Linq;

namespace PrimitiveDataTypesAndVariables
{
    public static class Extensions
    {
        public static bool IsFloatPrecision(this float source, float compare, float precision)
        {
            if (((source - precision) < compare) && ((source + precision) > compare))
                return true;
            return false;
        }
    }
}
