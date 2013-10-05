using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    public static class Program
    {
        static string[] numbers = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        static void Main()
        {
            //1. Write a method that asks the user for his name and prints “Hello, <name>” (for example, “Hello, Peter!”). Write a program to test this method.
            //HelloName();

            //2. Write a method GetMax() with two parameters that returns the bigger of two integers. Write a program that reads 3 integers from the console and prints the biggest of them using the method GetMax().
            //Console.Write("Write a 3 numbers separated by space: ");
            //string[] strArr = Console.ReadLine().Split(' ');
            //Console.WriteLine(GetMax(GetMax(int.Parse(strArr[0]), int.Parse(strArr[1])), int.Parse(strArr[2])));

            //3. Write a method that returns the last digit of given integer as an English word. Examples: 512  "two", 1024  "four", 12309  "nine".
            //Console.Write("Write a number: ");
            //Console.WriteLine(ReturnLastNumber(int.Parse(Console.ReadLine()) % 10));

            //4. Write a method that counts how many times given number appears in given array. Write a test program to check if the method is working correctly.
            //Console.WriteLine(CountNumberInArray(new int[] { 3, 2, 3, 4, 2, 2, 4 }, 2));

            //5. Write a method that checks if the element at given position in given array of integers is bigger than its two neighbors (when such exist).
            //Console.WriteLine(IsBiggerThanNighboars(new int[] { 3, 2, 3, 4, 2, 2, 4 }, 2));

            //6. Write a method that returns the index of the first element in array that is bigger than its neighbors, or -1, if there’s no such element.
            //Use the method from the previous exercise.
            //GetIndexOfFirstBiggerNumber();

            //7. Write a method that reverses the digits of given decimal number. Example: 256  652
            //Console.WriteLine(ReverseNumber(256));

            //8. Write a method that adds two positive integer numbers represented as arrays of digits (each array element arr[i] contains a digit; the last digit is kept in arr[0]). Each of the numbers that will be added could have up to 10 000 digits.
            //List<int> firstList = new List<int>();
            //for (int i = 1; i <= 10000; i++)
            //{
            //    firstList.Add(9);
            //}
            //Console.WriteLine(string.Join("", SumNumbers(firstList, firstList)) + Environment.NewLine + string.Join("", AddNumbers(99, 1)));

            //9. Write a method that return the maximal element in a portion of array of integers starting at given index. Using it write another method that sorts an array in ascending / descending order.
            //int[] arr = new int[] { 3, 2, 3, 4, 2, 2, 4 };
            //Console.WriteLine(string.Join(" ", SelectionSort(arr, true)));
            //arr = new int[] { 3, 2, 3, 4, 2, 2, 4 };
            //Console.WriteLine(string.Join(" ", SelectionSort(arr, false)));

            //10. Write a program to calculate n! for each n in the range [1..100]. Hint: Implement first a method that multiplies a number represented as array of digits by given integer number. 
            //Console.WriteLine(string.Join(Environment.NewLine, CalculateFactoriel(100)));

            //11.Write a method that adds two polynomials. Represent them as arrays of their coefficients as in the example below: x2 + 5 = 1x2 + 0x + 5  501

            //12. Extend the program to support also subtraction and multiplication of polynomials.

            //13. Write a program that can solve these tasks:
            //Reverses the digits of a number
            //Calculates the average of a sequence of integers
            //Solves a linear equation a * x + b = 0
            //        Create appropriate methods.
            //        Provide a simple text-based menu for the user to choose which task to solve.
            //        Validate the input data:
            //The decimal number should be non-negative
            //The sequence should not be empty
            //a should not be equal to 0
            //RunProgram();

            //14. Write methods to calculate minimum, maximum, average, sum and product of given set of integer numbers. Use variable number of arguments.
            //CalculationsMethods();

        }

        private static void RunProgram()
        {
            Console.WriteLine("Please select 1 for Reverses the digits of a number");
            Console.WriteLine("Please select 2 for Calculates the average of a sequence of integers");
            Console.WriteLine("Please select 3 for Solves a linear equation a * x + b = 0");
            int selection = int.Parse(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    ReverseNumber();
                    break;
                case 2:
                    CalculateAverage();
                    break;
                case 3:
                    SolvesLinearEquation();
                    break;
                default:
                    Console.WriteLine("Wrong choise");
                    RunProgram();
                    break;
            }
        }

        private static void SolvesLinearEquation()
        {
            Console.Write("a = ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("b = ");
            double b = double.Parse(Console.ReadLine());
            if (a == 0)
            {
                if (b == 0)
                    Console.WriteLine("Every x is a solution.");
                else
                    Console.WriteLine("Can't be solved");
            }
            else
                Console.WriteLine("The root x = " + (-b / a));
        }

        private static void CalculateAverage()
        {
            Console.WriteLine("Enter a list of integers for calculation separeted by space");
            var list = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            Console.WriteLine(list.Average());
        }

        private static void ReverseNumber()
        {
            Console.Write("Enter a integer number for reversed:");
            string reverse = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            for (int i = reverse.Length - 1; i >= 0; i--)
            {
                sb.Append(reverse[i]);
            }
            Console.WriteLine(sb.ToString());
        }

        private static void CalculationsMethods()
        {
            int[] arr = new int[] { 3, 2, 3, 4, 2, 2, 4 };
            int min = arr.Min();
            int max = arr.Max();
            double average = arr.Average();
            int sum = arr.Sum();
            int product = CalculateProduct(arr);
        }

        private static int CalculateProduct(int[] arr)
        {
            int product = 0;
            foreach (var a in arr)
            {
                product *= a;
            }
            return product;
        }

        private static List<BigInteger> CalculateFactoriel(int number)
        {
            List<BigInteger> facts = new List<BigInteger>();
            facts.Add(1);
            for (int i = 2; i <= number; i++)
            {
                facts.Add(facts[i - 2] * i);
            }
            return facts;
        }

        private static int[] SelectionSort(int[] source, bool isDescending)
        {
            for (int i = 0; i < source.Length; i++)
                ExchangePosition(source, i, GetIndexByPosition(source, i, isDescending));
            return source;
        }

        private static void ExchangePosition(int[] source, int firstNumber, int secondNumber)
        {
            int temp = source[firstNumber];
            source[firstNumber] = source[secondNumber];
            source[secondNumber] = temp;
        }

        private static int GetIndexByPosition(int[] source, int position, bool isDescending)
        {
            int result = position;
            for (int i = position + 1; i < source.Length; i++)
            {
                if (isDescending ? source[i] < source[result] : source[i] > source[result])//if ((isDescending && source[i] < source[position]) || (!isDescending && source[i] > source[position]))
                    result = i;
            }
            return result;
        }

        private static List<int> AddNumbers(int first, int second)
        {
            List<int> firstList = new List<int>();
            foreach (var n in first.ToString())
            {
                firstList.Add(int.Parse(n.ToString()));
            }
            List<int> secondList = new List<int>();
            foreach (var n in second.ToString())
            {
                secondList.Add(int.Parse(n.ToString()));
            }
            if (firstList.Count > secondList.Count)
                return SumNumbers(firstList, secondList);
            else
                return SumNumbers(secondList, firstList);
        }

        private static List<int> SumNumbers(List<int> first, List<int> second)
        {
            int diff = first.Count - second.Count;
            int sum = 0;
            int tempSum = 0;
            List<int> result = new List<int>();
            for (int i = 0; i < diff; i++)
            {
                second.Insert(0, 0);
            }
            for (int i = first.Count - 1; i >= 0; i--)
            {
                sum = first[i] + second[i] + tempSum;
                if (sum.ToString().Length > 1)
                {
                    result.Insert(0, int.Parse(sum.ToString().ToCharArray()[1].ToString()));
                    tempSum = int.Parse(sum.ToString().ToCharArray()[0].ToString());
                }
                else
                {
                    result.Insert(0, int.Parse(sum.ToString().ToCharArray()[0].ToString()) + tempSum);
                    tempSum = 0;
                }
            }
            if (tempSum > 0)
                result.Insert(0, tempSum);
            result.Reverse();
            return result;
        }

        private static int ReverseNumber(int source)
        {
            var s = source.ToString().ToList();
            s.Reverse();
            return int.Parse(string.Join("", s));
        }

        private static void GetIndexOfFirstBiggerNumber()
        {
            int[] arr = new int[] { 3, 2, 3, 4, 2, 2, 4 };
            bool isFind = false;

            for (int i = 0; i < arr.Length; i++)
            {
                isFind = IsBiggerThanNighboars(arr, i);
                if (isFind)
                {
                    Console.WriteLine(i);
                    break;
                }
            }
            if (!isFind)
                Console.WriteLine("-1");
        }

        private static bool IsBiggerThanNighboars(int[] source, int position)
        {
            if (position - 1 >= 0 && position + 1 < source.Length)
            {
                if (source[position - 1] < source[position] && source[position] > source[position + 1])
                    return true;
            }
            return false;
        }

        public static int CountNumberInArray(int[] source, int number)
        {
            return source.Count(s => s == number);
        }

        private static string ReturnLastNumber(int source)
        {
            return numbers[source];
        }

        private static int GetMax(int first, int second)
        {
            if (first > second)
                return first;
            else
                return second;
        }

        private static void HelloName()
        {
            Console.Write("What is your name?: ");
            Console.WriteLine("Hello, " + Console.ReadLine());
        }
    }
}