using System;

namespace UsingVariables
{
    internal class Program
    {
        private static void Main()
        {
        }
        
        public static Size GetRotatedSize(Size size, double angle)
        {
            var newWidth = Math.Abs(Math.Cos(angle))*size.Width + Math.Abs(Math.Sin(angle))*size.Height;
            var newHeight = Math.Abs(Math.Sin(angle))*size.Width + Math.Abs(Math.Cos(angle))*size.Height;
            return new Size(newWidth, newHeight);
        }

        public void PrintStatistics(double[] source, int count)
        {
            var max = PrintMax(source, count);
            Console.WriteLine(max);

            var min = PrintMin(source, count);
            Console.WriteLine(min);

            var average = PrintAverage(source, count);
            Console.WriteLine(average);
        }

        private static double PrintAverage(double[] source, int count)
        {
            double tmp = 0;
            for (int i = 0; i < count; i++)
            {
                tmp += source[i];
            }

            return tmp/count;
        }

        private static double PrintMin(double[] source, int count)
        {
            double min = 0;
            for (int i = 0; i < count; i++)
            {
                if (source[i] < min)
                {
                    min = source[i];
                }
            }

            return min;
        }

        private static double PrintMax(double[] source, int count)
        {
            double max = 0;
            for (int i = 0; i < count; i++)
            {
                if (source[i] > max)
                {
                    max = source[i];
                }
            }

            return max;
        }
    }
}
