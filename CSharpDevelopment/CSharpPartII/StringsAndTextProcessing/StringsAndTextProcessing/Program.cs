using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringsAndTextProcessing
{
    class Program
    {
        static void Main()
        {
            //1. Describe the strings in C#. What is typical for the string data type? Describe the most important methods of the String class.

            //2. Write a program that reads a string, reverses it and prints the result at the console.
            //Example: "sample"  "elpmas".
            //ReverseString();

            //3. Write a program to check if in a given expression the brackets are put correctly.
            //Example of correct expression: ((a+b)/5-d).
            //Example of incorrect expression: )(a+b)).
            CheckCorrectBrackets("((a+b)/5-d)");
            //CheckCorrectBrackets(")(a+b))");

            //4. Write a program that finds how many times a substring is contained in a given text (perform case insensitive search).
		    //Example: The target substring is "in". The text is as follows:
            //SearchSubstringInString();

            //5. You are given a text. Write a program that changes the text in all regions surrounded by the tags <upcase> and </upcase> to uppercase. The tags cannot be nested.
            //ToUpperRegionOfString();

            //6. Write a program that reads from the console a string of maximum 20 characters. If the length of the string is less than 20, the rest of the characters should be filled with '*'. Print the result string into the console
            //FilledMissingStrings();

            //7. Write a program that encodes and decodes a string using given encryption key (cipher). The key consists of a sequence of characters. The encoding/decoding is done by performing XOR (exclusive or) operation over the first letter of the string with the first of the key, the second – with the second, etc. When the last key character is reached, the next is the first
            //string msg = "Write a program that encodes and decodes a string using given encryption key (cipher). The key consists of a sequence of characters.";
            //string key = "kukumqvka";
            //Console.WriteLine(Encrypt(msg, key));
            //Console.WriteLine(Decrypt(Encrypt(msg, key), key));

            //8. Write a program that extracts from a given text all sentences containing given word.
            //ExtractIfContains();

            //9. We are given a string containing a list of forbidden words and a text containing some of these words. Write a program that replaces the forbidden words with asterisks.
            //ReplaceWords();

            //10. Write a program that converts a string to a sequence of C# Unicode character literals. Use format strings.
            //ConvertToUnicode("Hi!");

            //11. Write a program that reads a number and prints it as a decimal number, hexadecimal number, percentage and in scientific notation. Format the output aligned right in 15 symbols
            //FormatNumber(int.Parse(Console.ReadLine()));

            //12. Write a program that parses an URL address given in the format:
            //and extracts from it the [protocol], [server] and [resource] elements. For example from the URL http://www.devbg.org/forum/index.php the following information should be extracted:
            //[protocol] = "http"
            //[server] = "www.devbg.org"
            //[resource] = "/forum/index.php"
            //ParsesURL(http://www.devbg.org/forum/index.php);

            //13. Write a program that reverses the words in given sentence.
	        //Example: "C# is not C++, not PHP and not Delphi!"  "Delphi not and PHP, not C++ not is C#!".

            //14. A dictionary is stored as a sequence of text lines containing words and their explanations. Write a program that enters a word and translates it by using the dictionary. Sample dictionary   
            //DictionarySearch();

            //15. Write a program that replaces in a HTML document given as string all the tags <a href="…">…</a> with corresponding tags [URL=…]…/URL].
            //ReplaceHtmlTags();

            //16. Write a program that reads two dates in the format: day.month.year and calculates the number of days between them.
            //CalculateDates();

            //17. Write a program that reads a date and time given in the format: day.month.year hour:minute:second and prints the date and time after 6 hours and 30 minutes (in the same format) along with the day of week in Bulgarian
            //PrintDateTime();

            //18. Write a program for extracting all email addresses from given text. All substrings that match the format <identifier>@<host>…<domain> should be recognized as emails.
            //ExtractEmailFromText();

            //19. Write a program that extracts from a given text all dates that match the format DD.MM.YYYY. Display them in the standard date format for Canada.
            //ExtractDateTime();

            //20. Write a program that extracts from a given text all palindromes, e.g. "ABBA", "lamal", "exe".
            //FindPilindromWords();

            //21. Write a program that reads a string from the console and prints all different letters in the string along with information how many times each letter is found. 
            //CountLettersInString();

            //22. Write a program that reads a string from the console and lists all different words in the string along with information how many times each word is found.
            //CountWordsInString();

            //23. Write a program that reads a string from the console and replaces all series of consecutive identical letters with a single one.
            //PrintMultipleCharToSingle();

            //24. Write a program that reads a list of words, separated by spaces and prints the list in an alphabetical order
            //OrderWords(); 

            //25. Write a program that extracts from given HTML file its title (if available), and its body text without the HTML tags
            //ParseHtml();
        }

        private static void ParseHtml()
        {
            string str = @"<html><head><title>News</title></head><body><p><a href=""http://academy.telerik.com"">TelerikAcademy</a>aims to provide free real-world practicaltraining for young people who want to turn into skillful .NET software engineers.</p></body></html>";
            foreach (Match text in Regex.Matches(str, ">(.*?)<"))
                if (!String.IsNullOrWhiteSpace(text.Groups[1].Value))
                    Console.WriteLine(text.Groups[1]);
        }

        private static void OrderWords()
        {
            string str = "dada erere fgfdfg rew 3tgdgd yuiree";
            Console.WriteLine(string.Join(" ", str.Split(' ').ToList().OrderBy(o => o)));
        }

        private static void PrintMultipleCharToSingle()
        {
            string str = "aaaaabbbbbcdddeeeedssaa";
            var strArr = str.ToCharArray();
            for (int i = 0; i < strArr.Length; i++)
            {
                if (i + 1 < strArr.Length && !strArr[i].Equals(strArr[i + 1]))
                    Console.Write(strArr[i]);
                if (i == strArr.Length - 1 && strArr[i].Equals(strArr[i - 1]))
                {
                    Console.Write(strArr[i]);
                }

            }
            Console.WriteLine();
        }

        private static void CountWordsInString()
        {
            string str = "Write a program that reads a string from the console and lists all different words in the string along with information how many times each word is found";
            Regex.Matches(str, @"\w+").Cast<Match>().GroupBy(i => i).Select(i => new { Letter = i.Key, Count = i.Count() }).ToList().ForEach(g =>
            {
                Console.WriteLine(g.Letter + " : " + g.Count);
            });
        }

        private static void CountLettersInString()
        {
            string str = "Write a program that reads a string from the console and prints all different letters in the string along with information how many times each letter is found";
            str.Replace(" ", "").ToCharArray().GroupBy(i => i).Select(i => new { Letter = i.Key, Count = i.Count() }).ToList().ForEach(g =>
            {
                Console.WriteLine(g.Letter + " : " + g.Count);
            });
        }

        private static void FindPilindromWords()
        {
            string str = "Write a program that extracts from a given text all palindromes, e.g. \"ABBA\", \"lamal\", \"exe\"";
            foreach (Match item in Regex.Matches(str, @"\w+"))
            {
                if (item.Value.Length > 1)
                {
                    if (IsPalindrom(item.Value))
                        Console.WriteLine(item);
                }
            }
        }

        private static bool IsPalindrom(string str)
        {
            for (int i = 0; i < str.Length / 2; i++)
                if (str[i] != str[str.Length - 1 - i])
                    return false;
            return true;
        }

        private static void ExtractDateTime()
        {
            DateTime dt;
            const string MatchEmailPattern = @"\b[0-9]{2}.[0-9]{2}.[0-9]{4}\b";
            Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches("dsadasd 22.12.1978 fdfsd fdsfsd 2.12.1978");
            foreach (Match match in matches)
            {
                if (DateTime.TryParse(match.Value.ToString(), out dt))
                    Console.WriteLine(dt.ToString(CultureInfo.GetCultureInfo("en-CA").DateTimeFormat.ShortDatePattern));
            }
        }

        private static void ExtractEmailFromText()
        {
            const string MatchEmailPattern = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches("dsadasd none@none.no fdfsd fdsfsd none@none.no");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value.ToString());
            }
        }

        private static void PrintDateTime()
        {
            string str = "24.01.2013 23:00:00";
            DateTime date = DateTime.ParseExact(str, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture).AddHours(6.5);
            Console.WriteLine("{0} {1}", date.ToString("dddd", new CultureInfo("bg-BG")), date);
        }

        private static void CalculateDates()
        {
            string start = "27.02.2006";
            string end = "3.03.2006";
            DateTime startDate = DateTime.ParseExact(start, "d.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(end, "d.MM.yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine((endDate - startDate).TotalDays);
        }

        private static void ReplaceHtmlTags()
        {
            string str = "<p>Please visit <a href=\"http://academy.telerik.com\">our site</a> to choose a training course. Also visit <a href=\"www.devbg.org\">our forum</a> to discuss the courses.</p>";
            Console.WriteLine(str.Replace("<a href=\"", "[URL=").Replace("\">", "]").Replace("</a>", "[/URL]"));
        }

        private static void DictionarySearch()
        {
            string[] dictionary = {
                                    ".NET - platform for applications from Microsoft",
                                    "CLR - managed execution environment for .NET",
                                    "namespace - hierarchical - organization of classes"
                                    };
            Dictionary<string, string> dict = dictionary.ToDictionary(k => k.Split(new string[] { " - " }, StringSplitOptions.None)[0], v => v.Split(new string[] { " - " }, StringSplitOptions.None)[1]);
            Console.WriteLine(dict[Console.ReadLine()]);
        }

        private static void ParsesURL(string str)
        {
            string[] strArr = str.Split(':');
            Console.WriteLine("[protocol] = \"{0}\"", strArr[0]);
            string result = strArr[1].Remove(0, 2);
            Console.WriteLine("[server] = \"{0}\"", result.Substring(0, result.IndexOf('/')));
            Console.WriteLine("[server] = \"{0}\"", result.Substring(result.IndexOf('/'), result.Length - result.IndexOf('/')));
        }

        private static void FormatNumber(int n)
        {
            Console.WriteLine("{0,15}", n);
            Console.WriteLine("{0,15:X}", n);
            Console.WriteLine("{0,15:P}", n);
            Console.WriteLine("{0,15:E}", n);
        }

        private static void ConvertToUnicode(string str)
        {
            foreach (var c in str)
            {
                Console.Write("\\u{0:x4}", (int)c);
            }
            Console.WriteLine();
        }

        private static void ReplaceWords()
        {
            string str = "Microsoft announced its next generation PHP compiler today. It is based on .NET Framework 4.0 and is implemented as a dynamic language in CLR.";
            string forbiddenWords = "PHP, CLR, Microsoft";
            Console.WriteLine(Regex.Replace(str, forbiddenWords.Replace(", ", "|"), m => new String('*', m.Length)));
        }

        private static void ExtractIfContains()
        {
            string str = "We are living in a yellow submarine. We don't have anything else. Inside the submarine is very tight. So we are drinking all the day. We will move out of it in 5 days.";
            var separator = @"\b(in)\b";
            foreach (var item in str.Split('.'))
            {
                if (Regex.IsMatch(item, separator, RegexOptions.IgnoreCase))
                    Console.WriteLine(item);
            }
        }

        private static string Decrypt(string message, string key)
        {
            return Encrypt(message, key);
        }

        private static string Encrypt(string message, string key)
        {
            var result = new StringBuilder(message.Length);

            for (int i = 0, j = key.Length - 1; i < message.Length; i++, j--)
            {
                result.Append((char)(message[i] ^ key[j]));
                if (j == 0)
                    j = key.Length - 1;
            }
            return result.ToString();
        }

        private static void FilledMissingStrings()
        {
            string str = Console.ReadLine();
            int maxLength = 20;

            if (str.Length < maxLength)
                Console.WriteLine(str.PadRight(maxLength, '*'));
            else
                Console.WriteLine(str);
        }

        private static void ToUpperRegionOfString()
        {
            string str = "We are living in a <upcase>yellow submarine</upcase>. We don't have <upcase>anything</upcase> else.";
            Regex reg = new Regex("<upcase>(.*?)</upcase>");
            var mc = reg.Matches(str);
            foreach (var m in mc)
            {
                var r = m.ToString().Replace("<upcase>", "").Replace("</upcase>", "");
                str = str.Replace(r, r.ToUpper());
            }
            Console.WriteLine(str.Replace("<upcase>", "").Replace("</upcase>", ""));
        }

        private static void SearchSubstringInString()
        {
            string str = "We are living in an yellow submarine. We don't have anything else. Inside the submarine is very tight. So we are drinking all the day. We will move out of it in 5 days.";
            Console.WriteLine(Regex.Matches(str.ToLower(), "in").Cast<Match>().Count());
        }

        private static void CheckCorrectBrackets(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '(':
                        count++;
                        break;
                    case ')':
                        count--;
                        break;
                    default:
                        break;
                }
                if (count < 0)
                    break;
            }
            Console.WriteLine(count == 0);
        }

        private static void ReverseString()
        {
            string str = "sample";
            var result = str.ToList();
            result.Reverse();
            Console.WriteLine(string.Join("", result));
        }
    }
}
