using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImplementTrie
{
    class Program
    {
        private static void Main()
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                string inputText;
                StreamReader sr = new StreamReader(@"../../text.txt");
                using (sr)
                {
                    inputText = sr.ReadToEnd().ToLower();
                }
                var matches = Regex.Matches(inputText, @"[a-zA-Z]+");

                stopwatch.Stop();
                Console.WriteLine("Read from txt file and get words with regex: {0}", stopwatch.Elapsed);

                stopwatch.Reset();
                stopwatch.Restart();

                Tries t = new Tries();
                t.Insert(string.Join(" ", matches.Cast<Match>().Select(s => s.Value)));

                stopwatch.Stop();
                Console.WriteLine("insert words in trie: {0}", stopwatch.Elapsed);

                stopwatch.Reset();
                stopwatch.Restart();

                t.Contains("he");

                stopwatch.Stop();
                Console.WriteLine("search for word 'he': {0}", stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
