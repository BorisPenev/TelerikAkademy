using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Midget
{
    class Program
    {
        static void Main()
        {
            //Console.SetIn(File.OpenText("TextFile1.txt"));
            int index = 0;
            int patternsN = 0;
            List<int> valeys = Console.ReadLine().Split(new string[] { "," }, StringSplitOptions.None).Select(n => int.Parse(n)).ToList();
            bool[] VisitedValeys = new bool[valeys.Count];
            patternsN = int.Parse(Console.ReadLine());
            List<Patterns> Patterns = new List<Patterns>();
            
            for (int i = 0; i < patternsN; i++)
			{
                var r = Console.ReadLine().Split(new string[] { ", " }, StringSplitOptions.None).Select(n => int.Parse(n)).ToList();
                Patterns.Add(new Patterns() { Pattern = r });
			}

            for (int i = 0; i < patternsN; i++)
            {
                for (int j = 0; j < Patterns[i].Pattern.Count; j++)
                {
                    if (index < VisitedValeys.Length && index >= 0)
                    {
                        if (!VisitedValeys[index])
                        {
                            VisitedValeys[index] = true;
                        }
                        else
                            break;
                        if (index < valeys.Count && index >= 0)
                            Patterns[i].Sum += valeys[index];
                        else
                            break;
                        index = index + Patterns[i].Pattern[j];
                        if (j + 1 == Patterns[i].Pattern.Count)
                            j = -1;
                    }
                    else
                        break;
                }
                VisitedValeys = new bool[valeys.Count];
                index = 0;
            }
            Console.WriteLine(Patterns.Max(p => p.Sum));
        }
    }

    public class Patterns
    {
        private List<int> pattern = new List<int>();
        public List<int> Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        private int sum = 0;
        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }
    }
}
