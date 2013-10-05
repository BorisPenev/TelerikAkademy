using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Problem1FighterAttack
{
    class Program
    {
        static void Main()
        {
            int scores = 0;
            int px1 = int.Parse(Console.ReadLine());
            int py1 = int.Parse(Console.ReadLine());
            int px2 = int.Parse(Console.ReadLine());
            int py2 = int.Parse(Console.ReadLine());
            int fx1 = int.Parse(Console.ReadLine());
            int fy1 = int.Parse(Console.ReadLine());
            int d = int.Parse(Console.ReadLine());

            Rectangle rect = new Rectangle(Math.Min(px1, px2), Math.Max(py1, py2), Math.Max(px1, px2), Math.Min(py1, py2));
            List<Point> missaleRanges = new List<Point>();
            missaleRanges.Add(new Point() { X = fx1 + d, Y = fy1 });
            missaleRanges.Add(new Point() { X = fx1 + 1 + d, Y = fy1 });
            missaleRanges.Add(new Point() { X = fx1 + d, Y = fy1 + 1 });
            missaleRanges.Add(new Point() { X = fx1 + d, Y = fy1 - 1 });

            //fire
            for (int i = 0; i < missaleRanges.Count; i++)
            {
                if (i == 0 && missaleRanges[i].IsInRectangle(rect))
                    scores += 100;
                else if (i == 1 && missaleRanges[i].IsInRectangle(rect))
                    scores += 75;
                else if (missaleRanges[i].IsInRectangle(rect))
                    scores += 50;

            }

            Console.WriteLine(scores + "%");
        }
    }

    public static class Extensions
    {
        public static bool IsInRectangle(this Point source, Rectangle rect)
        {
            if ((rect.X <= source.X && source.X <= rect.Width) && (rect.Y >= source.Y && source.Y >= rect.Height))
                return true;
            return false;
        }
    }
}
