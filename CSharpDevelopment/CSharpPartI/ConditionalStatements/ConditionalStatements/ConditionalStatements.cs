using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionalStatements
{
    class ConditionalStatements
    {
        static void Main()
        {
            ExchangeValue();
            //ShowNumberSign();
            //FindBiggerNumber();
            //OrderByDescending();
            //NameOfDigit();
            //SolveQuadratic(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
            //FindGreatestNumber();
            //UsersChoise();
            //SumOfSubset();
            //CalculateScores();
            //ConvertNumberstToText();
        }

        private static void ConvertNumberstToText()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirdtheen", "fourthen", "fiftheen", "sixtheen", "seventheen", "eightheen", "ninetheen" };
            string[] dec = { "and", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.Clear();
                var iArray = i.ToString().ToCharArray().ToList();

                switch (iArray.Count)
                {
                    case 1:
                        sb.Append(digits[int.Parse(iArray[0].ToString())]);
                        break;
                    case 2:
                        if (i < 20)
                            sb.Append(digits[i]);
                        else
                        {
                            switch (int.Parse(iArray[1].ToString()))
                            {
                                case 0:
                                    sb.Append(dec[int.Parse(iArray[0].ToString())]);
                                    break;
                                default:
                                    sb.Append(dec[int.Parse(iArray[0].ToString())] + "-" + digits[int.Parse(iArray[1].ToString())]);
                                    break;
                            }
                        }
                        break;
                    case 3:
                        if (int.Parse(iArray[1].ToString()) == 0 && int.Parse(iArray[2].ToString()) == 0)
                            sb.Append(digits[int.Parse(iArray[0].ToString())] + " hundred");
                        else
                        {
                            for (int j = 0; j < iArray.Count; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        sb.Append(digits[int.Parse(iArray[j].ToString())] + " hundred");
                                        break;
                                    default:
                                        if (j == 1)
                                        {
                                            if (int.Parse(iArray[j].ToString()) == 0)
                                            {
                                                sb.Append(" and " + digits[int.Parse(iArray[j + 1].ToString())]);
                                            }
                                            else if (int.Parse(iArray[j].ToString()) == 1)
                                            {
                                                sb.Append(" and " + digits[10 + int.Parse(iArray[j + 1].ToString())]);
                                            }
                                            else if (int.Parse(iArray[j + 1].ToString()) == 0)
                                            {
                                                sb.Append(" " + dec[int.Parse(iArray[j].ToString())]);
                                            }
                                            else
                                            {
                                                sb.Append(" " + dec[int.Parse(iArray[j].ToString())] + "-" + digits[int.Parse(iArray[j + 1].ToString())]);
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
                Console.WriteLine(sb.ToString().UppercaseFirst());
            }
        }

        private static void CalculateScores()
        {
            Console.WriteLine("Please, enter a score from 1 to 9.");
            byte input = byte.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                case 2:
                case 3:
                    Console.WriteLine(input * 10);
                    break;
                case 4:
                case 5:
                case 6:
                    Console.WriteLine(input * 100);
                    break;
                case 7:
                case 8:
                case 9:
                    Console.WriteLine(input * 1000);
                    break;
                default:
                    Console.WriteLine("Error! Score is not from 1 to 9");
                    break;
            }
        }

        private static void SumOfSubset()
        {
            int[] numbers = { 3, -2, 1, 1, 8 };
            for (int n1 = 0; n1 < numbers.Length - 2; n1++)
            {
                for (int n2 = n1 + 1; n2 < numbers.Length; n2++)
                {
                    if (numbers[n1] + numbers[n2] == 0)
                    {
                        Console.WriteLine("{0},{1}", numbers[n1], numbers[n2]);
                    }
                    for (int n3 = n2 + 1; n3 < numbers.Length; n3++)
                    {
                        if (numbers[n1] + numbers[n2] + numbers[n3] == 0)
                        {
                            Console.WriteLine("{0},{1},{2}", numbers[n1], numbers[n2], numbers[n3]);
                        }
                        for (int n4 = n3 + 1; n4 < numbers.Length; n4++)
                        {
                            if (numbers[n1] + numbers[n2] + numbers[n3] + numbers[n4] == 0)
                            {
                                Console.WriteLine("{0},{1},{2},{3}", numbers[n1], numbers[n2], numbers[n3], numbers[n4]);
                            }
                        }
                    }
                }
            }
        }

        private static int SumNumbers(int n1, int n2)
        {
            return n1 + n2;
        }

        private static void UsersChoise()
        {
            Console.WriteLine("Please, enter 1 for int, 2 for double, and 3 for string.");
            byte input = byte.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    int nInt = int.Parse(Console.ReadLine());
                    Console.WriteLine(++nInt);
                    break;
                case 2:
                    double nDouble = double.Parse(Console.ReadLine());
                    Console.WriteLine(++nDouble);
                    break;
                default:
                    string s = Console.ReadLine();
                    Console.WriteLine(s + "*");
                    break;
            }
        }

        private static void FindGreatestNumber()
        {
            Console.WriteLine("Enter number separated by space:");
            var input = Console.ReadLine().Split(' ').ToList();
            Console.WriteLine(input.Max(a => int.Parse(a)));
        }

        public static void SolveQuadratic(double a, double b, double c)
        {
            //Quadratic Formula: x = (-b +- sqrt(b^2 - 4ac)) / 2a
            double insideSquareRoot = (b * b) - 4 * a * c;
            if (insideSquareRoot < 0)
            {
                Console.WriteLine("No Solution");
            }
            else
            {
                double sqrt = Math.Sqrt(insideSquareRoot);
                double x1 = (-b + sqrt) / (2 * a);
                double x2 = (-b - sqrt) / (2 * a);
                if (x1 == x2)
                    Console.WriteLine("Only one solution: " + x1);
                else
                    Console.WriteLine("2 Solutions: " + x1 + " and " + x2);
            }
        }

        private static void NameOfDigit()
        {
            Console.Write("Enter digit: ");
            byte digit = byte.Parse(Console.ReadLine());
            switch (digit)
            {
                case 0:
                    Console.WriteLine("Zero");
                    break;
                case 1:
                    Console.WriteLine("One");
                    break;
                case 2:
                    Console.WriteLine("Two");
                    break;
                case 3:
                    Console.WriteLine("Three");
                    break;
                case 4:
                    Console.WriteLine("Four");
                    break;
                case 5:
                    Console.WriteLine("Five");
                    break;
                case 6:
                    Console.WriteLine("Six");
                    break;
                case 7:
                    Console.WriteLine("Seven");
                    break;
                case 8:
                    Console.WriteLine("Eight");
                    break;
                case 9:
                    Console.WriteLine("Nine");
                    break;
                default:
                    Console.WriteLine("Unknown digit!");
                    break;
            }
        }

        private static void OrderByDescending()
        {
            int a = 20;
            int b = 30;
            int c = 55;
            if (a > b)
            {
                if (a > c)
                {
                     Console.WriteLine(a);
                    if (b > c)
                    {
                         Console.WriteLine(b);
                        Console.WriteLine(c);
                    }
                    else
                    {
                        Console.WriteLine(c);
                        Console.WriteLine(b);
                    }
                }
                else
                {
                    Console.WriteLine(c);
                    Console.WriteLine(a);
                    Console.WriteLine(b);
                }
            }
            else
            {
                if (b > c)
                {
                    Console.WriteLine(b);
                    if (a > c)
                    {
                        Console.WriteLine(a);
                        Console.WriteLine(c);
                    }
                    else
                    {
                        Console.WriteLine(c);
                        Console.WriteLine(a);
                    }
                }
                else
                {
                    Console.WriteLine(c);
                    Console.WriteLine(b);
                    Console.WriteLine(a);
                }
            }
        }

        private static void FindBiggerNumber()
        {
            int a = 55;
            int b = -132;
            int c = -78;
            if (a > b)
            {
                if (a > c)
                    Console.WriteLine(a);
                else
                    Console.WriteLine(c);
            }
            else
            {
                if (b > c)
                    Console.WriteLine(b);
                else
                    Console.WriteLine(c);
            }
        }

        private static void ShowNumberSign()
        {
            int a = 55;
            double b = -132.6;
            int c = -78;
            if (a < 0 || b < 0 || c < 0)
                Console.WriteLine("-");
            else
                Console.WriteLine("+");
        }

        private static void ExchangeValue()
        {
            int first = 15;
            int second = 10;
            if (first > second)
            {
                first += second;
                second = first - second;
                first -= second;
            }
        }
    }
}
