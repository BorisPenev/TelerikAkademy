using System;
using System.Linq;
using System.Text;

namespace Problem3SandGlass
{
    class Program
    {
        static void Main()
        {
            StringBuilder sb = new StringBuilder();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0, j = n; i < n; i++, j--)
            {
                if (j > 0)
                {
                    sb.AppendFormat(new string('.', i) + new string('*', j) + new string('.', i) + Environment.NewLine);
                    //Console.WriteLine(new string('.', i) + new string('*', j) + new string('.', i));
                    j--;
                }
            }
            var sbMirror = sb.ToString().Split('\r' , '\n').ToList();
            sbMirror = sbMirror.Where(s => !string.IsNullOrEmpty(s)).ToList();
            for (int i = sbMirror.Count - 2; i >= 0; i--)
            {
                sb.Append(sbMirror[i] + Environment.NewLine);
            }
            Console.Write(sb.ToString());
        }
    }
}
