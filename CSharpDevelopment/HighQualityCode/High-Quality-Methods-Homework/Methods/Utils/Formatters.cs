using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methods.Utils
{
    public static class Formatters
    {
        public static string Number(object number, string format)
        {
            var result = String.Empty;

            switch (format)
            {
                case "f":
                    result = String.Format("{0:f2}", number);
                    break;
                case "%":
                    result = String.Format("{0:p0}", number);
                    break;
                case "r":
                    result = String.Format("{0,8}", number);
                    break;
            }

            return result;
        }
    }
}
