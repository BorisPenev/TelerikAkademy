using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Problem1ShipDamage
{
    class Program
    {
        static void Main()
        {
            int scores = 0;
            int Sx1 = int.Parse(Console.ReadLine());
            int Sy1 = int.Parse(Console.ReadLine());
            int Sx2 = int.Parse(Console.ReadLine());
            int Sy2 = int.Parse(Console.ReadLine());
            int H = int.Parse(Console.ReadLine());
            int Cx1 = int.Parse(Console.ReadLine());
            int Cy1 = int.Parse(Console.ReadLine());
            int Cx2 = int.Parse(Console.ReadLine());
            int Cy2 = int.Parse(Console.ReadLine());
            int Cx3 = int.Parse(Console.ReadLine());
            int Cy3 = int.Parse(Console.ReadLine());

            Rectangle rect = new Rectangle(Math.Min(Sx1, Sx2), Math.Max(Sy1, Sy2), Math.Max(Sx1, Sx2), Math.Min(Sy1, Sy2));
            List<Point> ships = new List<Point>();
            ships.Add(new Point() { X = Cx1, Y = Cy1 });
            ships.Add(new Point() { X = Cx2, Y = Cy2 });
            ships.Add(new Point() { X = Cx3, Y = Cy3 });

            //fire
            for (int i = 0; i < ships.Count; i++)
            {
                Point p = new Point(ships[i].X, H - ships[i].Y + H);
                scores += p.IsInRectangle(rect);
            }

            Console.WriteLine(scores + "%");
        }
    }

    public static class Extensions
    {
        public static int IsInRectangle(this Point source, Rectangle rect)
        {
            //out = 0%;
            //in = 100%;
            //corner = 25%;
            //border = 50%;
            if ((rect.X < source.X && source.X < rect.Width) && (rect.Y > source.Y && source.Y > rect.Height))
                return 100;
            else if (((rect.X == source.X || source.X == rect.Width) && (rect.Y == source.Y || source.Y == rect.Height)))
                return 25;
            else if (((rect.X <= source.X && source.X <= rect.Width) && (rect.Y >= source.Y && source.Y >= rect.Height)))
                return 50;
            else
                return 0;
        }
    }
}