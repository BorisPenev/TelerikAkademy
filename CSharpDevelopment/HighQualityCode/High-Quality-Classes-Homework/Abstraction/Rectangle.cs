namespace Abstraction
{
    class Rectangle : Figure
    {
        private readonly double width = 0;
        private readonly double height = 0;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public override double CalcPerimeter()
        {
            double perimeter = 2 * (this.width + this.height);
            return perimeter;
        }

        public override double CalcSurface()
        {
            double surface = this.width * this.height;
            return surface;
        }
    }
}
