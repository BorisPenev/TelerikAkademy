using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagesInBottle
{
    class Program
    {
        static Dictionary<string, char> chiphers = new Dictionary<string, char>();
        static List<string> endResult = new List<string>();
        static string secretCode = "1122";
        private static string chipher = "A1B12C11D2";

        static void Main()
        {
            secretCode = Console.ReadLine();
            chipher = Console.ReadLine();

            StringBuilder sbNumber = new StringBuilder();
            char key = new char();

            for (int i = 0; i < chipher.Length; i++)
            {
                if (chipher[i] >= 'A' && chipher[i] <= 'Z')
                {
                    if (key > 0)
                    {
                        chiphers.Add(sbNumber.ToString(), key);
                        sbNumber.Clear();
                    }
                    key = chipher[i];
                }
                else
                {
                    sbNumber.Append(chipher[i]);
                }
            }
            if (sbNumber.Length > 0)
            {
                chiphers.Add(sbNumber.ToString(), key);
                sbNumber.Clear();
            }

            Solve(0, new List<char>());

            Console.WriteLine(endResult.Count);
            Console.WriteLine(string.Join(Environment.NewLine, endResult.OrderBy(s => s)));
        }

        static void Solve(int index, List<char> result)
        {
            if (index >= secretCode.Length)
            {
                endResult.Add(string.Join("", result));
                return;
            }

            foreach (var chip in chiphers)
            {
                if (secretCode.Substring(index).StartsWith(chip.Key))
                {
                    result.Add(chip.Value);
                    Solve(index + chip.Key.Length, result);
                    result.RemoveAt(result.Count - 1);
                }
            }
        }

        //static int ConvertArrayToInt(int[] digits)
        //{
        //    int result = 0;
        //    for (int i = 0; i < digits.Count(); i++)
        //    {
        //        result *= 10;
        //        result += digits[i];
        //    }
        //    return result;
        //}

        //static int[] ConvertNumberToArray(int number)
        //{
        //    List<int> digits = new List<int>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        digits.Add(number % 10);
        //        number /= 10;
        //    }
        //    digits.Reverse();
        //    return digits.ToArray();
        //}
    }
}
