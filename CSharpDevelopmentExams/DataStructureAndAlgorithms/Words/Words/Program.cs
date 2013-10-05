using System;
using System.Collections.Generic;

namespace Words
{
    class Program
    {
        static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif

            Dictionary<string, HashSet<string>> words = new Dictionary<string, HashSet<string>>();
            for (char i = 'a'; i <= 'z'; i++)
            {
                words[i.ToString()] = new HashSet<string>();
            }

            int n = int.Parse(Console.ReadLine());
            
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine().ToLower();
                input += " ";
                string word = string.Empty;

                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j] >= 'a' && input[j] <= 'z')
                    {
                        word += input[j];
                    }
                    else if(word.Length > 0)
                    {
                        foreach (var w in word)
                        {
                            if (w == 'a')
                            {
                                
                            }
                            words[w.ToString()].Add(word);
                            
                            //words.AddSafeReturn(w, new HashSet<string>()).Add(word);
                        }
                        word = string.Empty;
                    }
                }
            }

            n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                string inputToLower = input.ToLower();
                HashSet<string> result = new HashSet<string>(words[inputToLower[0].ToString()]);

                for (int j = 1; j < inputToLower.Length; j++)
                {
                    result.IntersectWith(words[inputToLower[j].ToString()]);
                }

                Console.WriteLine("{0} -> {1}", input, result.Count);
            }
        }
    }
}
