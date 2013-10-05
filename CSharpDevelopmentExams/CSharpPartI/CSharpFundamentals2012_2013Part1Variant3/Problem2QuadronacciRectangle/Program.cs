using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem2QuadronacciRectangle
{
    class Program
    {
        static void Main()
        {
            List<long> numbers = new List<long>();
            for (int i = 0; i < 4; i++)
            {
                numbers.Add(long.Parse(Console.ReadLine()));
            }
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());

            for (int i = 4; i < row * col; i++)
            {
                numbers.Add(numbers[i - 1] + numbers[i - 2] + numbers[i - 3] + numbers[i - 4]);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= col; j++)
                {
                    sb.Append(numbers[0] + " ");
                    numbers.RemoveAt(0);
                }
                sb.Append(Environment.NewLine);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
