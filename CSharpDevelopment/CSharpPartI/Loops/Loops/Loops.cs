using System;
using System.Linq;
using System.Numerics;

namespace Loops
{
    class Loops
    {
        static void Main()
        {
            //Console.WriteLine(cat);
            //PrintNumbersFromOneToN();
            //PrintNumberThatIsNotDevided();
            //FindMinMaxNumbers();
            //CalculateFactoriel1();
            //CalculateFactoriel2();
            //Calculate6();
            //Fibonacci(int.Parse(Console.ReadLine()));
            //GCD();
            //CalculateCatalanNumber();
            //PrintCardName();
            //PrintMatrix();
            //TrailingZeros();
            //Spiral();
        }

        private static void Spiral()
        {
            Console.Write("Enter number for Size: ");
            int size = int.Parse(Console.ReadLine());
            int count = 1;
            int[,] matrix = new int[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    switch (row)
                    {
                        case 0:
                            matrix[row, col] = count++;
                            break;
                        case 1:
                            if (col + 1 < size)
                                matrix[col + 1, size - 1] = count++;
                            else
                                for (int bottom = col; bottom > 0; bottom--)
                                    matrix[col, bottom - 1] = count++;
                            break;
                        case 2:
                            if (col == 0)
                                for (int left = row; left <= size - row + 1; left++)
                                    matrix[size - left, col] = count++;
                            else if (col <= size - row)
                                matrix[row - 1, col] = count++;
                            break;
                        case 3:
                            if (row - col - 1 > 0)
                                matrix[row - 1, row - col - 1] = count++;
                            break;
                        default:
                            break;
                    }

                }
            }

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Console.Write(matrix[row, col] + "|");
                }
                Console.WriteLine();
            }
        }

        private static void TrailingZeros()
        {
            Console.Write("Enter number for N: ");
            int n = int.Parse(Console.ReadLine());
            string num = n.GetFactoriel().ToString();
            int count = 0;
            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (num[i] == (char)48)
                    count++;
                else
                    break;
            }
            Console.WriteLine("There are " + count + " trailing zeros.");
        }

        private static void PrintMatrix()
        {
            Console.Write("Please enter a N:");
            int n = int.Parse(Console.ReadLine());
            for (int row = 1; row <= n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    Console.Write((row + col) + "|");
                }
                Console.WriteLine();
            }
        }

        private static void PrintCardName()
        {
            string[] cards = { "Ace", "Deuce", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };
            string[] colors = { "clubs", "diamonds", "hearts", "spades" };
            for (int i = 0; i < colors.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    Console.WriteLine(cards[j] + " " + colors[i]);
                }
            }
        }

        private static void CalculateCatalanNumber()
        {
            Console.Write("Please enter a N:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine((2 * n).GetFactoriel() / ((n + 1).GetFactoriel() * n.GetFactoriel()));
        }

        public void GCD()
        {
            Console.Write("Please enter a first number:");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Please enter a second number:");
            int b = int.Parse(Console.ReadLine());

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            if (a == 0)
                Console.WriteLine(b);
            else
                Console.WriteLine(a);
        }

        public static void Fibonacci(int n)
        {
            BigInteger a = 0;
            BigInteger b = 1;
            for (int i = 0; i < n; i++)
            {
                BigInteger temp = a;
                a = b;
                b = temp + b;
                Console.WriteLine(a);
            }
        }

        private static void Calculate6()
        {
            Console.Write("Please enter a N:");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Please enter a X:");
            int x = int.Parse(Console.ReadLine());
            BigInteger sum = 1;
            for (int i = 1; i <= n; i++)
            {
                sum += (i.GetFactoriel() / BigInteger.Parse((Math.Pow(x, n).ToString())));
            }
            Console.WriteLine(sum);
        }

        private static void CalculateFactoriel2()
        {
            Console.WriteLine("1 < K < N");
            Console.Write("Please enter a K:");
            int k = int.Parse(Console.ReadLine());
            Console.Write("Please enter a N:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine((n.GetFactoriel() * k.GetFactoriel()) / (n - k ).GetFactoriel());
        }

        private static void CalculateFactoriel1()
        {
            Console.WriteLine("1 < K < N");
            Console.Write("Please enter a K:");
            int k = int.Parse(Console.ReadLine());
            Console.Write("Please enter a N:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(n.GetFactoriel() / k.GetFactoriel());
        }

        private static void FindMinMaxNumbers()
        {
            int min = 0;
            int max = 0;
            Console.Write("Please enter a numbers separated by space:");
            var numbers = Console.ReadLine().Split(' ').ToList();
            foreach (var n in numbers)
            {
                if (min == 0)
                    min = int.Parse(n);
                if (max == 0)
                    max = int.Parse(n);

                if (max < int.Parse(n))
                    max = int.Parse(n);
                else if (min > int.Parse(n))
                    min = int.Parse(n);
            }
            Console.WriteLine("max number is: " + max);
            Console.WriteLine("min number is: " + min);
        }

        private static void PrintNumberThatIsNotDevided()
        {
            Console.Write("Please enter a number:");
            int n = int.Parse(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                if (!(i % 3 == 0 && i % 7 == 0))
                    Console.WriteLine(i);
            }
        }

        private static void PrintNumbersFromOneToN()
        {
            Console.Write("Please enter a number:");
            int n = int.Parse(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
