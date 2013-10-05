namespace UsingVariables
{
    public class Size
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        public Size(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
