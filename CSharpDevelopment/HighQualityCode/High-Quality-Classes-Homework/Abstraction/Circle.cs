using System;

namespace Abstraction
{
    class Circle : Figure
    {
        private readonly double radius = 0;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public override double CalcPerimeter()
        {
            double perimeter = 2 * Math.PI * this.radius;
            return perimeter;
        }

        public override double CalcSurface()
        {
            double surface = Math.PI * this.radius * this.radius;
            return surface;
        }
    }
}
