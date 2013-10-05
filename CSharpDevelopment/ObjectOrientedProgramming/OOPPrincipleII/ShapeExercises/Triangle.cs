using System;
using System.Linq;

namespace ShapeExercises
{
    public class Triangle : Shape
    {
        public Triangle(double width, double height)
            : base(width, height)
        {
        }

        internal override double CalculateSurface()
        {
            return this.height * this.width / 2;
        }
    }
}
