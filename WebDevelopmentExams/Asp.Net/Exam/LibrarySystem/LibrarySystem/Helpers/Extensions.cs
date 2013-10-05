using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Helpers
{
    public static class Extensions
    {
        public static int ToInt(this string str)
        {
            int num;
            if (str != null && int.TryParse(str, out num))
                return num;
            return int.MinValue;
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