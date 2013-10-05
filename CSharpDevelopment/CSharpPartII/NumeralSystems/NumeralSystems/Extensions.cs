using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumeralSystems
{
    public static class Extensions
    {
        public static string ToBinary(this int value)
        {
            List<int> result = new List<int>();
            while (value > 0)
            {
                if (value % 2 == 0)
                    result.Add(0);
                else
                    result.Add(1);
                value /= 2;
            }
            result.Reverse();
            return string.Join("", result);
        }

        public static int ToXNumericalSystem(this string source, int value)
        {
            var dec = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[source.Length - i - 1] == '0') continue;
                dec += (int)Math.Pow(value, i);
            }

            return dec;
        }

        public static string HexToBinary(this string value)
        {
            return String.Join(String.Empty, value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))).TrimStart(new Char[] { '0' });
        }

        public static string ToBase(this string number, int startBase, int targetBase)
        {
            int base10 = ToBase10(number, startBase);
            string rtn = FromBase10(base10, targetBase);
            return rtn;
        }

        public static int ToBase10(string number, int startBase)
        {

            if (startBase < 2 || startBase > 36) return 0;
            if (startBase == 10) return Convert.ToInt32(number);

            char[] chrs = number.ToCharArray();
            int m = chrs.Length - 1;
            int n = startBase;
            int x;
            int rtn = 0;

            foreach (char c in chrs)
            {

                if (char.IsNumber(c))
                    x = int.Parse(c.ToString());
                else
                    x = Convert.ToInt32(c) - 55;

                rtn += x * (Convert.ToInt32(Math.Pow(n, m)));

                m--;

            }
            return rtn;
        }

        public static string FromBase10(int number, int targetBase)
        {

            if (targetBase < 2 || targetBase > 36) return "";
            if (targetBase == 10) return number.ToString();

            int n = targetBase;
            int q = number;
            int r;
            List<string> rtn = new List<string>();

            while (q >= n)
            {

                r = q % n;
                q = q / n;

                if (r < 10)
                    rtn.Insert(0, r.ToString());
                else
                    rtn.Insert(0, Convert.ToChar(r + 55).ToString());
            }

            if (q < 10)
                rtn.Insert(0, q.ToString());
            else
                rtn.Insert(0, Convert.ToChar(q + 55).ToString());

            return string.Join("", rtn);

        }
    }
}
