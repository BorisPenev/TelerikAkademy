using System;
using System.Collections.Generic;
using System.Text;

namespace CrossWord
{
    class Program
    {
        static HashSet<string> allWords = new HashSet<string>(); 
        private static string[] words;
        private static string[] crossword;

        static void Main(string[] args)
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif

            int n = int.Parse(Console.ReadLine());
            words = new string[2 * n];
            crossword = new string[n];

            for (int i = 0; i < 2*n; i++)
            {
                words[i] = Console.ReadLine();
                allWords.Add(words[i]);
            }

            Array.Sort(words);

            Solver(0);

            Console.WriteLine("NO SOLUTION!");
        }

        static void Solver(int indexLine)
        {
            if (indexLine >= crossword.Length)
            {
                if (CheckVertical())
                {
                    Print();
                    Environment.Exit(0);
                }
                return;
            }

            for (int i = 0; i < words.Length; i++)
            {
                crossword[indexLine] = words[i];
                Solver(indexLine + 1);
                crossword[indexLine] = null;
            }
        }

        static bool CheckVertical()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < crossword.Length; row++)
            {
                sb.Clear();
                for (int col = 0; col < crossword.Length; col++)
                {
                    sb.Append(crossword[col][row]);
                }
                if (!allWords.Contains(sb.ToString()))
                {
                    return false;
                }
                
            }
            return true;
        }

        static void Print()
        {
            for (int i = 0; i < crossword.Length; i++)
            {
                Console.WriteLine(crossword[i]);
            }
        }
    }
}
