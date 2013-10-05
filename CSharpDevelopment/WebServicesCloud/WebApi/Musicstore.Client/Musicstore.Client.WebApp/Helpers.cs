using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Musicstore.Client.WebApp
{
    public static class Helpers
    {
        public static DateTime CheckDate(DateTime value)
        {
            var result = DateTime.Now;

            if (value < SqlDateTime.MinValue.Value)
            {
                result = SqlDateTime.MinValue.Value;
            }
            else if (value > SqlDateTime.MaxValue.Value)
            {
                result = SqlDateTime.MaxValue.Value;
            }

            return result;
        }
    }
}