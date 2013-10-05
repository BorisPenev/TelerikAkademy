using System;

namespace PointInTriangle
{
    class Program
    {
        static void Main()
        {
            /* Let us check whether the point P(10, 15) lies inside the triangle 
                formed by A(0, 0), B(20, 0) and C(10, 30) */
            Console.WriteLine(IsInside(0, 0, 20, 0, 10, 30, 10, 15));
        }

        /* A function to check whether point P(x, y) lies inside the triangle formed 
            by A(x1, y1), B(x2, y2) and C(x3, y3) */
        static bool IsInside(int x1, int y1, int x2, int y2, int x3, int y3, int x, int y)
        {
            /* Calculate area of triangle ABC */
            var A = CalculateArea(x1, y1, x2, y2, x3, y3);

            /* Calculate area of triangle PBC */
            var A1 = CalculateArea(x, y, x2, y2, x3, y3);

            /* Calculate area of triangle PAC */
            var A2 = CalculateArea(x1, y1, x, y, x3, y3);

            /* Calculate area of triangle PAB */
            var A3 = CalculateArea(x1, y1, x2, y2, x, y);

            /* Check if sum of A1, A2 and A3 is same as A */
            return (A == A1 + A2 + A3);
        }

        /* A utility function to calculate area of triangle formed by (x1, y1), 
            (x2, y2) and (x3, y3) */
        static double CalculateArea(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            return Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2.0);
        }
    }
}
