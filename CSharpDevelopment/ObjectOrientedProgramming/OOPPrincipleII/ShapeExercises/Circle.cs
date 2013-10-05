using System;
using System.Linq;

namespace ShapeExercises
{
    public class Circle : Shape
    {
        public Circle(double width): base(width, width)
        {
        }

        internal override double CalculateSurface()
        {
            return Math.PI * this.width * 2;
        }
    }
}
