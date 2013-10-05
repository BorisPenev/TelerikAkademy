using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlignBoth
{
    class Program
    {
        static int w = 0;
        static void Main()
        {
            //Console.SetIn(File.OpenText("TextFile3.txt"));
            int n = int.Parse(Console.ReadLine());
            w = int.Parse(Console.ReadLine());
            int count = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                if (sb.Length == 0)
                    sb.Append(Console.ReadLine());
                else
                    sb.Append(" " + Console.ReadLine());
            }
            List<string> words = Regex.Matches(sb.ToString(), @"\w+").Cast<Match>().Select(s => s.Value).ToList();
            List<string> lines = new List<string>();
            sb.Clear();
            for (int i = 0; i < words.Count; i++)
            {
                if (sb.Length > 0)
                {
                    count = sb.Length + words[i].Length + 1;
                }
                else
                {
                    count = sb.Length + words[i].Length;
                }
                if (count <= w)
                {
                    AddWord(sb, words, i);
                }
                else
                {
                    FormatAndPrintLine(sb, 1);
                    AddWord(sb, words, i);
                }
            }
            FormatAndPrintLine(sb, 1);
        }

        private static void FormatAndPrintLine(StringBuilder sb, int index)
        {
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                var m = w - sb.Length;
                if (m > 0 && m < w)
                {
                    var word = Regex.Matches(sb.ToString(), @"\w+").Cast<Match>().Select(s => s.Value).ToList();
                    sb.Clear();
                    if (word.Count == 1)
                    {
                        Console.Write(word[0]);
                    }
                    else
                    {
                        for (int i = 0; i < word.Count; i++, m--)
                        {
                            if (i + 1 == word.Count)
                                sb.Append(word[i]);
                            else if (m > 0)
                                sb.Append(word[i].PadRight(word[i].Length + index + 1, ' '));
                            else
                                sb.Append(word[i].PadRight(word[i].Length + index, ' '));
                        }
                        if(sb.Length < w)
                            FormatAndPrintLine(sb, index + 1);
                    }
                    if (index == 1)
                    {
                        Console.WriteLine(sb.ToString());
                        sb.Clear();
                    }
                }
                else
                {
                    Console.WriteLine(sb.ToString());
                    sb.Clear();
                }
            }
        }

        private static void AddWord(StringBuilder sb, List<string> words, int i)
        {
            if (sb.Length == 0)
                sb.Append(words[i]);
            else
                sb.Append(" " + words[i]);
        }
    }
}
