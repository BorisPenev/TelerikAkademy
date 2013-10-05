using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultidimensionalArrays
{
    class Program
    {
        static int n = 0;
        static int m = 0;
        static string[,] matrix;
        public static readonly List<string>[] MyList = new List<string>[4];

        static void Main()
        {
            //Console.WriteLine(lastSB.ToString());
            //1. Write a program that fills and prints a matrix of size (n, n) as shown below: (examples for n = 4)
            //PrintMatrixA();
            //PrintMatrixB();
            //PrintMatrixC();
            //*

            //2. Write a program that reads a rectangular matrix of size N x M and finds in it the square 3 x 3 that has maximal sum of its elements.
            //Open 2Test1 file from the project.
            //Copy and paste the content in console.
            //Click Enter.
            //Click Ctrl+Z and click enter to start reading from the console.
            //MaxSum();

            //3. We are given a matrix of strings of size N x M. Sequences in the matrix we define as sets of several neighbor elements located on the same line, column or diagonal. Write a program that finds the longest sequence of equal strings in the matrix. 
            //Open 3Test1 or 3Test2 file from the project.
            //Copy and paste the content in console.
            //Click Enter.
            //Click Ctrl+Z and click enter to start reading from the console.
            //LongestSequenceOfEqualStrings();

            //4. Write a program, that reads from the console an array of N integers and an integer K, sorts the array and using the method Array.BinSearch() finds the largest number in the array which is ≤ K. 
            //FindLargeNumberBellowK();

            //5. You are given an array of strings. Write a method that sorts the array by the length of its elements (the number of characters composing them).
            //SortArrayByElementLength();

            //6*

            //7*
        }

        private static void SortArrayByElementLength()
        {
            string[] sArr = { "zxcvbnmasdfgh", "qwerty", "1234567", "12" };
            Console.WriteLine(string.Join(" ", sArr.ToList().OrderBy(s => s.Length).ToArray()));
        }

        private static void FindLargeNumberBellowK()
        {
            Console.Write("Write integers separated by space: ");
            string[] strArray = Console.ReadLine().Split(' ');
            int[] arr = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                arr[i] = int.Parse(strArray[i]);
            }
            Console.Write("K: ");
            var k = int.Parse(Console.ReadLine());

            Array.Sort(arr);
            Console.WriteLine(string.Join(",", arr));
            if (Array.BinarySearch(arr, k) < 0)
                Console.WriteLine("No result");
            else
                Console.WriteLine(arr[Array.BinarySearch(arr, k) - 1]);
        }

        private static void MaxSum()
        {
            ReadFromConsole();
            int tempSum = 0;
            int sum = 0;
            int iRow = 0;
            int iCol = 0;

            for (int row = 1; row < m - 1; row++)
            {
                for (int col = 1; col < n - 1; col++)
                {
                    tempSum = 0;
                    tempSum += int.Parse(matrix[row - 1, col - 1]);
                    tempSum += int.Parse(matrix[row - 1, col]);
                    tempSum += int.Parse(matrix[row - 1, col + 1]);
                    tempSum += int.Parse(matrix[row, col - 1]);
                    tempSum += int.Parse(matrix[row, col]);
                    tempSum += int.Parse(matrix[row, col + 1]);
                    tempSum += int.Parse(matrix[row + 1, col - 1]);
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

            Console.WriteLine(sum);
        }

        private static void LongestSequenceOfEqualStrings()
        {
            ReadFromConsole();
            int maxCount = 0;
            StringBuilder lastSB = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < n - 1; col++)
                {
                    if (matrix[row, col] == matrix[row, col + 1])
                        sb.Append(matrix[row, col] + ",");
                    else
                    {
                        if (sb.Length > maxCount)
                        {
                            maxCount = sb.Length;
                            lastSB = sb;
                            sb.Clear();
                        }
                    }
                }

                for (int col = 0; col < n - 1; col++, row++)
                {
                    if (matrix[row, col] == matrix[row + 1, col + 1])
                        sb.Append(matrix[row, col] + ",");
                    else
                    {
                        if (sb.Length > maxCount)
                        {
                            maxCount = sb.Length;
                            lastSB = new StringBuilder(sb.ToString());
                            sb.Clear();
                        }
                    }
                }
            }

            for (int col = 0; col < n; col++)
            {
                for (int row = 0; row < m - 1; row++)
                {
                    if (matrix[row, col] == matrix[row + 1, col])
                        sb.Append(matrix[row, col] + ",");
                    else
                    {
                        if (sb.Length > maxCount)
                        {
                            maxCount = sb.Length;
                            lastSB = sb;
                            sb.Clear();
                        }
                    }
                }
            }

            Console.WriteLine(lastSB.ToString() + lastSB.ToString().Split(',')[0]);
        }

        private static void ReadFromConsole()
        {
            var result = Regex.Split(Console.In.ReadToEnd(), @"\r?\n|\r");
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    n = Convert.ToInt32(result[i]);
                else
                {
                    m = Convert.ToInt32(result[i]);
                    matrix = new string[n,m];
                    for (int j = i + 1; j < result.Length; j++)
                    {
                        var lines = result[j].Split(' ').ToList();
                        if (lines.Count > 1)
                            for (int col = 0; col < n; col++)
                                matrix[j - 2, col] = lines[col];
                    }
                }
            }
        }

        private static void PrintMatrixC()
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];
            int counter = 1;
            int rowCount = 1;
            for (int row = n - 1; row >= 0; row--, rowCount++)
            {
                for (int i = 0; i < rowCount; i++, counter++)
                {
                    matrix[row + i, i] = counter;
                }
            }

            rowCount -= 2;

            for (int row = 0; row < n - 1; row++, rowCount--)
            {
                for (int i = 0; i < rowCount; i++, counter++)
                {
                    matrix[i, row + i + 1] = counter;
                }
            }

            PrintMatrix(n, matrix);
        }

        private static void PrintMatrixB()
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];
            int counter = 1;

            for (int col = 0; col < n; col++)
            {
                if (col % 2 == 0)
                {
                    for (int row = 0; row < n; row++, counter++)
                    {
                        matrix[row, col] = counter;
                    }
                }
                else
                {
                    for (int row = n - 1; row >= 0; row--, counter++)
                    {
                        matrix[row, col] = counter;
                    }
                }
            }

            PrintMatrix(n, matrix);
        }


        private static void PrintMatrixA()
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];
            int counter = 1;

            for (int col = 0; col < n; col++)
            {
                for (int row = 0; row < n; row++, counter++)
                {
                    matrix[row, col] = counter;
                }
            }

            PrintMatrix(n, matrix);
        }

        private static void PrintMatrix(int n, int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    sb.Append(matrix[row, col] + " ");
                }
                sb.Append(Environment.NewLine);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
