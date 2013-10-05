using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFiles
{
    class Program
    {
        static int n = 0;
        static string[,] matrix;
        static void Main()
        {
            //1. Write a program that reads a text file and prints on the console its odd lines.
            //PrintOddLines();

            //2. Write a program that concatenates two text files into another text file.
            //ConcatenatesFiles();

            //3. Write a program that reads a text file and inserts line numbers in front of each of its lines. The result should be written to another text file.
            //InsertLineNumber();

            //4. Write a program that compares two text files line by line and prints the number of lines that are the same and the number of lines that are different. Assume the files have equal number of lines.
            //EqualFilesLineByLine();

            //5. Write a program that reads a text file containing a square matrix of numbers and finds in the matrix an area of size 2 x 2 with a maximal sum of its elements. The first line in the input file contains the size of matrix N. Each of the next N lines contain N numbers separated by space. The output should be a single number in a separate text file. Example:
                //4
                //2 3 3 4
                //0 2 3 4			17
                //3 7 1 2
                //4 3 3 2
            //MaxSum();

            //6. Write a program that reads a text file containing a list of strings, sorts them and saves them to another text file. Example:
                //Ivan			George
                //Peter			Ivan
                //Maria			Maria
                //George	    Peter
            //List<string> sb1 = ReadFromFile("Test6.txt");
            //using (StreamWriter sw = new StreamWriter("NewTest6.txt"))
            //{
            //    foreach (var line in sb1.OrderBy(o => o))
            //    {
            //        sw.WriteLine(line);
            //    }
            //}

            //7. Write a program that replaces all occurrences of the substring "start" with the substring "finish" in a text file. Ensure it will work with large files (e.g. 100 MB).
            //nqmam 100MB tekstov fail. Taka che ne sym go testval za 100MB
            //ReplaceString(false);

            //8. Modify the solution of the previous problem to replace only whole words (not substrings).
            //ReplaceString(true);

            //9. Write a program that deletes from given text file all odd lines. The result should be in the same file.
            //DeleteOddLines();

            //10. Write a program that extracts from given XML file all the text without the tags.
            //<?xml version="1.0"><student><name>Pesho</name><age>21</age><interests count="3"><interest> Games</instrest><interest>C#</instrest><interest> Java</instrest></interests></student>
            //ParseString();

            //11.Write a program that deletes from a text file all words that start with the prefix "test". Words contain only the symbols 0...9, a...z, A…Z, _.
            //RemoveWordStartWith();

            //12. Write a program that removes from a text file all words listed in given another text file. Handle all possible exceptions in your methods.
            //RemoveMatchedWords();

            //13. Write a program that reads a list of words from a file words.txt and finds how many times each of the words is contained in another file test.txt. The result should be written in the file result.txt and the words should be sorted by the number of their occurrences in descending order. Handle all possible exceptions in your methods.
            //CountMatchesWords();
        }

        private static void CountMatchesWords()
        {
            try
            {
                List<string> sb1 = ReadFromFile("Test1.txt");
                List<string> wordList = ReadFromFile("WordList.txt");
                var content = string.Join(Environment.NewLine, sb1);
                var match = @"\b(" + string.Join("|", wordList) + ")\\b";
                var result = Regex.Matches(content, match, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Dictionary<string, int> matches = new Dictionary<string, int>();
                foreach (var item in result)
                {
                    if (matches.ContainsKey(item.ToString()))
                        matches[item.ToString()] += 1;
                    else
                        matches.Add(item.ToString(), 1);
                }
                matches = matches.OrderByDescending(o => o.Value).ToDictionary(k => k.Key, v => v.Value);
                using (StreamWriter sw = new StreamWriter("result.txt"))
                {
                    foreach (var key in matches.Keys)
                        sw.WriteLine(key);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void RemoveMatchedWords()
        {
            try
            {
                List<string> sb1 = ReadFromFile("Test1.txt");
                List<string> wordList = ReadFromFile("WordList.txt");
                var content = string.Join(Environment.NewLine, sb1);
                var match = @"\b(" + string.Join("|", wordList) + ")\\b";
                var result = Regex.Replace(content, match, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            }
            catch (Exception ex)
            {
            }
        }

        private static void RemoveWordStartWith()
        {
            List<string> sb = ReadFromFile("Test1.txt");
            var result = (from line in sb
                          let split = line.Split(' ')
                          select split);//;
            using (StreamWriter sw = new StreamWriter("Test10.txt"))
            {
                foreach (var line in result)
                {
                    var resultLine = line.ToList().Where(a => !a.ToLower().StartsWith("test")).ToList();
                    if (resultLine.Count() > 0)
                        sw.Write(string.Join(" ", resultLine) + Environment.NewLine);
                }
            }
        }

        private static void ParseString()
        {
            string str = "<?xml version=\"1.0\"><student><name>Pesho</name><age>21</age><interests count=\"3\"><interest>Games</instrest><interest>C#</instrest><interest>Java</instrest></interests></student>";
            Regex reg = new Regex(">(.*?)<");
            List<string> result = new List<string>();
            var mc = reg.Matches(str);
            foreach (var m in mc)
            {
                if (m.ToString().Length > 2)
                {
                    result.Add(m.ToString().Substring(1, m.ToString().Length - 2));
                }
            }
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void DeleteOddLines()
        {
            List<string> sb = ReadFromFile("Test9.txt");
            List<string> result = new List<string>();
            for (int i = 0; i < sb.Count; i++)
            {
                if (i % 2 == 0)
                {
                    result.Add(sb[i] + Environment.NewLine);
                }
            }
            using (StreamWriter sw = new StreamWriter("Test9.txt"))
            {
                sw.Write(string.Join("", result));
            }
        }

        private static void ReplaceString(bool isWord)
        {
            StreamReader sr = new StreamReader("Test1.txt");
            StreamWriter sw = new StreamWriter("result1.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var content = string.Join(Environment.NewLine, line);
                var match = @"\b(start)\b";
                sw.WriteLine(Regex.Replace(content, match, "finish", RegexOptions.IgnoreCase | RegexOptions.Multiline));
            }
            sr.Close();
            sw.Close();
        }

        private static void MaxSum()
        {
            ReadFromConsole();
            int tempSum = 0;
            int sum = 0;
            int iRow = 0;
            int iCol = 0;

            for (int row = 0; row < n - 1; row++)
            {
                for (int col = 0; col < n - 1; col++)
                {
                    tempSum = 0;
                    tempSum += int.Parse(matrix[row, col]);
                    tempSum += int.Parse(matrix[row, col + 1]);
                    tempSum += int.Parse(matrix[row + 1, col]);
                    tempSum += int.Parse(matrix[row + 1, col + 1]);
                    if (tempSum > sum)
                    {
                        sum = tempSum;
                        iRow = row;
                        iCol = col;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter("NewTest3.txt"))
            {
                sw.WriteLine(sum);
            }
        }

        private static void EqualFilesLineByLine()
        {
            List<string> sb1 = ReadFromFile("Test1.txt");
            List<string> sb2 = ReadFromFile("Test2.txt");
            int countEqualLines = 0;
            int countNotEqualLines = 0;
            for (int i = 0; i < sb1.Count; i++)
            {
                if (sb1[i].Equals(sb2[i]))
                    countEqualLines++;
                else
                    countNotEqualLines++;
            }
            Console.WriteLine("Same lines: " + countEqualLines + ", different lines: " + countNotEqualLines);
        }

        private static void InsertLineNumber()
        {
            List<string> sb1 = ReadFromFile("Test1.txt");
            using (StreamWriter sw = new StreamWriter("NewTest2.txt"))
            {
                for (int i = 0; i < sb1.Count; i++)
                {
                    sw.WriteLine(i + 1 + ". " + sb1[i]);
                }
            }
        }

        private static void ConcatenatesFiles()
        {
            List<string> sb1 = ReadFromFile("Test1.txt");
            List<string> sb2 = ReadFromFile("Test2.txt");
            using (StreamWriter sw = new StreamWriter("NewTest1.txt"))
            {
                foreach (var f in sb1)
                    sw.WriteLine(f);
                foreach (var f in sb2)
                    sw.WriteLine(f);
            }
        }

        private static void PrintOddLines()
        {
            List<string> sb = ReadFromFile("Test1.txt");
            for (int i = 0; i < sb.Count; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine(sb[i]);
                }
            }
        }

        private static List<string> ReadFromFile(string filename)
        {
            List<string> sb = new List<string>();
            using (StreamReader sr = new StreamReader(filename))
            {
                try
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Add(line);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return sb;
        }

        private static void ReadFromConsole()
        {
            var result = ReadFromFile("Test3.txt");
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    n = Convert.ToInt32(result[i]);
                else
                {
                    matrix = new string[n, n];
                    for (int j = i; j < result.Count; j++)
                    {
                        var lines = result[j].Split(' ').ToList();
                        if (lines.Count > 1)
                            for (int col = 0; col < n; col++)
                                matrix[j - 1, col] = lines[col];
                    }
                }
            }
        }
    }
}
