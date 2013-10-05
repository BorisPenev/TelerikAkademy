using System;
using System.Linq;
using System.Text;

namespace PrintFirst10Numbers
{
    class PrintFirst10Numbers
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            for (int i = 2; i < 12; i++)
            {
                if (count == 0)
                {
                    count++;
                    sb.Append(i + ", ");
                }
                else
                {
                    count--;
                    sb.Append(-i + ", ");
                }
            }
            Console.WriteLine(sb.ToString().Remove(sb.Length - 2));
            Console.ReadKey();
        }
    }
}