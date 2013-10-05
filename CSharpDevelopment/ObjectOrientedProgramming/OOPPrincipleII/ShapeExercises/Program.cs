using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeExercises
{
    class Program
    {
        static void Main()
        {
            List<Shape> shapes = new List<Shape>()
            {
                new Triangle(10,25),
                new Rectangle(22, 33),
                new Circle(15),
            };
            shapes.ForEach(s =>
                {
                    Console.WriteLine(s.CalculateSurface());
                });
        }
    }
}
