using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecoverMessage
{
    class Program
    {
        private static string[] words;
        static List<string> sb = new List<string>();

        static void Main(string[] args)
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            int n = int.Parse(Console.ReadLine());
            words = new string[n];

            for (int i = 0; i < n; i++)
            {
                words[i] = Console.ReadLine();
            }

            int indexStartMached = int.MaxValue;
            int indexEndMatched = int.MaxValue;

            var chars = words[0].ToCharArray();
            var vv = chars.Select(s => s.ToString()).ToList();
            sb.AddRange(vv);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    if (i + 1 < words.Length)
                    {
                        for (int k = 0; k < words[i + 1].Length; k++)
                        {
                            if (words[i][j] == words[i + 1][k])
                            {
                                indexStartMached = k;
                            }
                            else
                            {
                                if (indexStartMached != int.MaxValue)
                                {
                                    sb.Insert(0, words[i + 1].Substring(0, indexStartMached));
                                    sb.Add(words[i + 1].Substring(indexStartMached + 1, 
                                              words[i + 1].Length - 1 - indexStartMached));
                                    indexStartMached = int.MaxValue;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(string.Join("", sb.OrderBy(s => s)));
        }
    }
}
