namespace Abstraction
{
    abstract class Figure : IFigureCalculations
    {
        public abstract double CalcPerimeter();
        public abstract double CalcSurface();
    }
}
