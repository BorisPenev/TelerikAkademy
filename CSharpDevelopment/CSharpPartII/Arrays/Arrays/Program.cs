using System;
using System.Collections.Generic;
using System.Linq;

namespace Arrays
{
    class Program
    {
        static void Main()
        {
            //1. Write a program that allocates array of 20 integers and initializes each element by its index multiplied by 5. Print the obtained array on the console.
            //InitializeArray();

            //2. Write a program that reads two arrays from the console and compares them element by element.
            //IsArrayEqual();

            //3. Write a program that compares two char arrays lexicographically (letter by letter).
            //IsArrayEquallExicographically();

            //4. Write a program that finds the maximal sequence of equal elements in an array.
            //Example: {2, 1, 1, 2, 3, 3, 2, 2, 2, 1}  {2, 2, 2}.
            //MaximalSequence();

            //5. Write a program that finds the maximal increasing sequence in an array. Example: {3, 2, 3, 4, 2, 2, 4}  {2, 3, 4}.
            //MaximalIncreasingSequence();

            //6. Write a program that reads two integer numbers N and K and an array of N elements from the console. Find in the array those K elements that have maximal sum.
            //FindMaxSum();

            //7. Sorting an array means to arrange its elements in increasing order. Write a program to sort an array. Use the "selection sort" algorithm: Find the smallest element, move it at the first position, find the smallest from the rest, move it at the second position, etc.
            //SelectionSort();

            //8. Write a program that finds the sequence of maximal sum in given array. Example:
            //{2, 3, -6, -1, 2, -1, 6, 4, -8, 8}  {2, -1, 6, 4}
            //Can you do it with only one loop (with single scan through the elements of the array)?
            //FindsSequenceOfMaximalSum();

            //9. Write a program that finds the most frequent number in an array. Example:
            //{4, 1, 1, 4, 2, 3, 4, 4, 1, 2, 4, 9, 3}  4 (5 times)
            //FindsMostFrequentNumber();      

            //10. Write a program that finds in given array of integers a sequence of given sum S (if present). Example:	 {4, 3, 1, 4, 2, 5, 8}, S=11  {4, 2, 5}
            //FindsSequenceOfSumEqualTo();

            //11. Write a program that finds the index of given element in a sorted array of integers by using the binary search algorithm (find it in Wikipedia).
            //int[] array = { 4, 3, 1, 4, 2, 5, 8 };
            //Console.WriteLine(BinarySearch(array, 4));

            //12. Write a program that creates an array containing all letters from the alphabet (A-Z). Read a word from the console and print the index of each of its letters in the array.
            //PrintIndexOfLetters();

            //13. * Write a program that sorts an array of integers using the merge sort algorithm (find it in Wikipedia).
            //MergeSort();

            //14. Write a program that sorts an array of strings using the quick sort algorithm (find it in Wikipedia).
            //int[] a = { 4, 1, 1, 4, 2, 3, 4, 4, 1, 2, 4, 9, 3 };
            //Console.WriteLine(string.Join(" ", a.Quicksort()));

            //15. Write a program that finds all prime numbers in the range [1...10 000 000]. Use the sieve of Eratosthenes algorithm (find it in Wikipedia).
            //SieveOfEratosthenes();

            //16. * We are given an array of integers and a number S. Write a program to find if there exists a subset of the elements of the array that has a sum S. Example:
            //arr={2, 1, 2, 4, 3, 5, 2, 6}, S=14  yes (1+2+5+6)

            //17 *

            //18* Write a program that reads an array of integers and removes from it a minimal number of elements in such way that the remaining array is sorted in increasing order. Print the remaining sorted array. Example:
            //{6, 1, 4, 3, 0, 3, 6, 4, 5}  {1, 3, 3, 4, 5}
            //ExtractMinimalLine();

            //19* * Write a program that reads a number N and generates and prints all the permutations of the numbers [1 … N]. Example:
            //n = 3  {1, 2, 3}, {1, 3, 2}, {2, 1, 3}, {2, 3, 1}, {3, 1, 2}, {3, 2, 1}
            int n = 3;
            for (int i = 0; i < n; i++)
            {

            }
        }

        private static void ExtractMinimalLine()
        {
            List<int> numbers = new List<int>() { 6, 1, 4, 3, 0, 3, 6, 4, 5 };
            List<int> result = new List<int>();
            int max = numbers.OrderByDescending(o => o).FirstOrDefault();
            numbers.Remove(0);
            result.Add(numbers.OrderBy(o => o).FirstOrDefault());
            numbers.RemoveRange(0, numbers.IndexOf(result[0]) + 1);
            for (int i = result.Last(); i < max; i++)
            {
                AddResult(numbers, result, i);
            }
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static void AddResult(List<int> numbers, List<int> result, int i)
        {
            if (numbers.IndexOf(i) > -1)
            {
                result.Add(i);
                numbers.RemoveRange(0, numbers.IndexOf(i) + 1);
                AddResult(numbers, result, i);
            }
        }

        private static void SieveOfEratosthenes()
        {
            int max = 100;
            var maxSquareRoot = Math.Sqrt(max);
            var primes = Enumerable.Range(2, (int)maxSquareRoot + 2)
                        .Aggregate(Enumerable.Range(2, max - 1).ToArray(), (sieve, i) =>
                        {
                            if (sieve[i - 2] == 0) return sieve;
                            for (int m = 2; m <= max / i; m++)
                                sieve[i * m - 2] = 0;
                            return sieve;
                        })
                        .Where(n => n != 0);
        }

        private static void MergeSort()
        {
            List<int> ws = new List<int>() { 1, 8, 9, 16, 17, 21 };
            List<int> xs = new List<int>() { 2, 7, 10, 15, 18 };
            List<int> ys = new List<int>() { 3, 6, 11, 14 };
            List<int> zs = new List<int>() { 4, 5, 12, 13, 19, 20 };
            List<IEnumerable<int>> lss = new List<IEnumerable<int>> { ws, xs, ys, zs };
            Console.WriteLine(string.Join(" ", lss.MergePreserveOrder(p => p)));
        }

        

        private static void PrintIndexOfLetters()
        {
            char[] alphabet = new char[26]
            {
                'A', 'B', 'C', 'D', 'E', 'F',
                'G', 'H', 'I', 'J', 'K', 'L',
                'M', 'N', 'O', 'P', 'Q', 'R',
                'S', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z'
            };
            Console.Write("Write a word: ");
            var word = Console.ReadLine().ToCharArray();
            word.ToList().ForEach(w =>
            {
                Console.WriteLine(alphabet.ToList().IndexOf(char.Parse(w.ToString().ToUpper())));
            });
        }

        static int BinarySearch(int[] array, int value)
        {
            Array.Sort(array);
            int low = 0, high = array.Length - 1, midpoint = 0;

            while (low <= high)
            {
                midpoint = low + (high - low) / 2;

                // check to see if value is equal to item in array
                if (value == array[midpoint])
                {
                    return midpoint;
                }
                else if (value < array[midpoint])
                    high = midpoint - 1;
                else
                    low = midpoint + 1;
            }

            // item was not found
            return -1;
        }

        private static void FindsSequenceOfSumEqualTo()
        {
            int[] a = { 4, 3, 1, 4, 2, 5, 8 };
            int index = int.MinValue;
            int length = 0;
            int sum = 11;
            for (int n = 0; n < a.Length; n++)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (i + n < a.Length && a.ToList().GetRange(i, n).Sum() == sum)
                    {
                        index = i;
                        length = n;
                    }
                }
            }
            if (index == int.MinValue)
                Console.WriteLine("No Answer");
            else
                Console.WriteLine(string.Join(" ", a.ToList().GetRange(index, length)));
        }

        private static void FindsMostFrequentNumber()
        {
            int[] a = { 4, 1, 1, 4, 2, 3, 4, 4, 1, 2, 4, 9, 3 };
            var result = a.GroupBy(g => g).OrderByDescending(o => o.Count()).FirstOrDefault();
            Console.WriteLine(result.Key + ", Count: " + result.Count() + " times");
        }

        private static void FindsSequenceOfMaximalSum()
        {
            int[] a = { 2, 3, -6, -1, 2, -1, 6, 4, -8, 8 };
            int n = 4;
            int index = 0;
            int sum = int.MinValue;
            for (int i = 0; i < a.Length; i++)
            {
                if (i + n < a.Length && a.ToList().GetRange(i, n).Sum() > sum)
                {
                    sum = a.ToList().GetRange(i, n).Sum();
                    index = i;
                }
            }
            Console.WriteLine(string.Join(" ", a.ToList().GetRange(index, n)));
            Console.WriteLine(sum);
        }

        private static void SelectionSort()
        {
            int[] a = { 3, 2, 3, 4, 2, 2, 4 };
            int i, j;
            int min, temp;

            for (i = 0; i < a.Length - 1; i++)
            {
                min = i;

                for (j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < a[min])
                    {
                        min = j;
                    }
                }

                temp = a[i];
                a[i] = a[min];
                a[min] = temp;
            }
            Console.WriteLine(string.Join(" ", a));
        }

        private static void FindMaxSum()
        {
            Console.Write("N: ");
            var n = int.Parse(Console.ReadLine());
            Console.Write("K: ");
            var k = int.Parse(Console.ReadLine());
            Console.Write("Write integers separated by space: ");
            string[] result = Console.ReadLine().Split(' ');
            var numList = result.Select(x => Int32.Parse(x)).OrderBy(o => o).ToList().GetRange(n - k, k);
            Console.WriteLine(string.Join(" ", numList));
            Console.WriteLine(numList.Sum());
        }

        private static void MaximalIncreasingSequence()
        {
            int count = 0;
            int maxCount = 0;
            int lastIndex = 0;
            int[] arr = { 3, 2, 3, 4, 2, 2, 4 };
            for (int i = 0; i < arr.Length; i++)
            {
                if (i + 1 < arr.Length)
                {
                    if (arr[i] + 1 == arr[i + 1])
                        count++;
                    else
                    {
                        if (count > maxCount)
                        {
                            maxCount = count;
                            lastIndex = i;
                        }
                        count = 0;
                    }
                }
            }

            for (int i = 0; i <= maxCount; i++)
            {
                Console.WriteLine(arr[i + lastIndex - maxCount]);
            }
        }

        private static void MaximalSequence()
        {
            int count = 0;
            int maxCount = 0;
            int lastIndex = 0;
            int[] arr = { 2, 1, 1, 2, 3, 3, 2, 2, 2, 1 };
            for (int i = 0; i < arr.Length; i++)
            {
                if (i + 1 < arr.Length)
                {
                    if (arr[i] == arr[i + 1])
                        count++;
                    else
                    {
                        if (count > maxCount)
                        {
                            maxCount = count;
                            lastIndex = i;
                        }
                        count = 0;
                    }
                }
            }

            for (int i = 0; i <= maxCount; i++)
            {
                Console.WriteLine(arr[i + lastIndex - maxCount]);
            }
        }

        private static void IsArrayEquallExicographically()
        {
            Console.Write("Write string for first array: ");
            char[] firstArr = Console.ReadLine().ToCharArray();

            Console.Write("Write integers for second array: ");
            char[] secondArr = Console.ReadLine().ToCharArray();

            bool isEqual = true;
            if (firstArr.Length == secondArr.Length)
            {
                for (int i = 0; i < firstArr.Length; i++)
                {
                    if (firstArr[i] != secondArr[i])
                    {
                        isEqual = false;
                        break;
                    }
                }
            }
            else
                isEqual = false;
            Console.WriteLine(isEqual);
        }

        private static void IsArrayEqual()
        {
            Console.Write("Initialize array lenght: ");
            int length = int.Parse(Console.ReadLine());
            int[] firstArr = new int[length];


            Console.Write("Write integers for first array separated by space: ");
            string[] result = Console.ReadLine().Split(' ');
            for (int i = 0; i < result.Length; i++)
            {
                firstArr[i] = int.Parse(result[i]);
            }

            int[] secondArr = new int[length];
            Console.Write("Write integers for second array separated by space: ");
            result = Console.ReadLine().Split(' ');
            for (int i = 0; i < result.Length; i++)
            {
                secondArr[i] = int.Parse(result[i]);
            }

            bool isEqual = true;
            for (int i = 0; i < length; i++)
            {
                if (firstArr[i] != secondArr[i])
                {
                    isEqual = false;
                    break;
                }
            }
            Console.WriteLine(isEqual);
        }

        private static void InitializeArray()
        {
            int[] arr = new int[20];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i * 5;
                Console.WriteLine(arr[i]);
            }
        }
    }
}
