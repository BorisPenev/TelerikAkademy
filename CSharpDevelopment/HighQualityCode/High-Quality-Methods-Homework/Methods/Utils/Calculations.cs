using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methods.Utils
{
    public static class Calculations
    {
        public static double TriangleArea(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentException("CalcTriangleArea: arguments need to be a positive");
            }
            var s = (a + b + c) / 2;
            var area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
            return area;
        }

        public static double Distance(double x1, double y1, double x2, double y2)
        {
            var distance = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return distance;
        }
    }
}
