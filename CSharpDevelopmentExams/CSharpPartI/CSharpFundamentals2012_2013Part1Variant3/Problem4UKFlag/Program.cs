using System;
using System.Linq;
using System.Text;

namespace Problem4UKFlag
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0, j = ((n - 3) / 2); i < n / 2; i++, j--)
            //{
            //    if(j >= 0)
            //    sb.AppendFormat(new string('.', i) + new string('\\', 1) + new string('.', j) + new string('|', 1) + new string('.', j) + new string('/', 1) + new string('.', i) + Environment.NewLine);
            //}
            //sb.AppendFormat(new string('-', n / 2) + new string('*', 1) + new string('-', n / 2) + Environment.NewLine);
            //var sbMirror = sb.ToString().Split('\r', '\n').ToList();
            //sbMirror = sbMirror.Where(s => !string.IsNullOrEmpty(s)).ToList();
            //for (int i = sbMirror.Count - 2; i >= 0; i--)
            //{
            //    sb.Append(sbMirror[i].Replace("\\", "$").Replace("/", "\\").Replace("$", "/") + Environment.NewLine);
            //}
            //Console.Write(sb.ToString());

            string[] result = new string[n / 2];

            for (int i = 0, j = ((n - 3) / 2); i < n / 2; i++, j--)
            {
                if (j >= 0)
                    result[i] = new string('.', i) + new string('\\', 1) + new string('.', j) + new string('|', 1) + new string('.', j) + new string('/', 1) + new string('.', i);
            }

            Console.WriteLine(string.Join(Environment.NewLine, result));
            Console.WriteLine(new string('-', n / 2) + new string('*', 1) + new string('-', n / 2));

            for (int i = result.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(result[i].Replace("\\", "$").Replace("/", "\\").Replace("$", "/"));
            }
        }
    }
}
