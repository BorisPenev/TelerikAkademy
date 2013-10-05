using System;
using System.Linq;

namespace ShapeExercises
{
    public class Rectangle : Shape
    {
        public Rectangle(double width, double height)
            : base(width, height)
        {
        }

        internal override double CalculateSurface()
        {
            return this.height * this.width;
        }
    }
}
