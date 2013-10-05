using System;
using System.Linq;

namespace ShapeExercises
{
    public abstract class Shape
    {
        protected double width;
        protected double height;

        public Shape(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        internal abstract double CalculateSurface();
    }
}
